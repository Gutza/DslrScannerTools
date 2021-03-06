﻿using Newtonsoft.Json;
using ScannerDriver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using WindowsInput;
using WindowTitleWatcher.Util;

namespace DSLR_Digitizer
{
    public partial class MainScannerForm : Form
    {
        List<ScannerIcon> NavigationIcons;
        string ComPort = null;

        WindowInfo EOSWindow = null;

        SweepSettingsCollection SweepSettingsList = new SweepSettingsCollection();
        SweepSettings CurrentSweepSettings = new SweepSettings();
        GlobalSettings Settings;
        const string LOCAL_APP_FOLDER_NAME = "DSLR Scanner";
        const string SWEEP_SETTINGS_FILENAME = "SweepSettings.json";
        const string GLOBAL_SETTINGS_FILENAME = "GlobalSettings.json";
        const string HUGIN_PANORAMA_FOLDER = "Panorama templates";

        bool ScannerIsMoving = false;

        const int BACKLASH = 1500;
        const int ONE_STEP_IN_STEPS = 1000;
        const int INFINITE_STEPS = int.MaxValue;

        private bool WasAltDown = false;

        private string BaseFolder { get { return GetBaseFolder(); } }
        private string SweepSettingsFilename { get { return Path.Combine(BaseFolder, SWEEP_SETTINGS_FILENAME); } }
        private string GlobalSettingsFilename { get { return Path.Combine(BaseFolder, GLOBAL_SETTINGS_FILENAME); } }
        private string HuginPanoramaFolder { get { return GetPanoramaFolder(); } }
        private List<Point> MoveQueue = new List<Point>();
        private bool MoveToOriginRequested = false;
        private readonly TimeSpan PulseSpan = new TimeSpan(0, 0, 0, 0, 500);
        private DateTime LastPulse;

        int SweepStep;
        int ShotsInSweep;
        List<string> PrevImageFileList;

        bool _isSweepRunning = false;
        bool _isShotRequested = false;
        bool _isShotInProgress = false;

        [Flags]
        enum KeyMoveOrders
        {
            Stop = 0,
            Left = 1,
            Right = 2,
            Up = 4,
            Down = 8,
        }
        private KeyMoveOrders CurrentKeyOrders = KeyMoveOrders.Stop;
        private KeyMoveOrders PreviousKeyOrders = KeyMoveOrders.Stop;

        public MainScannerForm()
        {
            InitializeComponent();
            NavigationIcons = new List<ScannerIcon>()
            {
                iconLeft,
                iconRight,
                iconUp,
                iconDown,
                iconStop,
            };
            foreach (var icon in NavigationIcons)
            {
                icon.Initialize();
            }

            ResetCommPortList();
            LoadSweepSettings();

            SemanticComms.Initialize();
            SemanticComms.OnLogMessage += RawCommsLogMessage;
            SemanticComms.OnRawScannerCommand += RawCommsLogScannerCommand;
            SemanticComms.OnRawScannerOutput += RawCommsLogScannerOutput;
            SemanticComms.OnScannerMoveChange += ProcessScannerMoveChange;
            SemanticComms.OnPositionChange += ScannerPositionChanged;
        }

        private void ScannerPositionChanged(object sender, Point e)
        {
            tsPosition.Text = e.ToString();
        }

        // TODO: This is horrible, need to address it more elegantly
        private void ExecuteMoveToOrigin()
        {
            MoveToOriginRequested = false;
            var currentPos = SemanticComms.GetCurrentPos();
            if (currentPos.Equals(new Point(0, 0)))
            {
                return; // Duh!
            }

            MoveWithBacklash(new Point(-currentPos.X, -currentPos.Y));
        }

        private bool HandleMovementQueue()
        {
            if (MoveQueue.Count == 0)
            {
                return false;
            }

            SemanticComms.Move(new Point(MoveQueue[0].X, MoveQueue[0].Y));
            MoveQueue.RemoveAt(0);
            return true;
        }

        private void ProcessScannerMoveChange(object sender, MoveState e)
        {
            navigationGroup.Invoke(new MethodInvoker(delegate
            {
                ResetNavigation();
                if (e.IsStopped())
                {
                    SetInterfaceMoving(false);
                    iconStop.IconState = ScannerIcon.IconStates.Active;
                    if (MoveToOriginRequested)
                    {
                        ExecuteMoveToOrigin();
                    }
                    else if (!HandleMovementQueue() && _isShotRequested)
                    {
                        ShootDSLR();
                    }
                    return;
                }

                SetInterfaceMoving(true);
                if (e.MoveDirectionX == MoveState.MoveStates.MovingPositive)
                {
                    iconRight.IconState = ScannerIcon.IconStates.Active;
                }
                else if (e.MoveDirectionX == MoveState.MoveStates.MovingNegative)
                {
                    iconLeft.IconState = ScannerIcon.IconStates.Active;
                }

                if (e.MoveDirectionY == MoveState.MoveStates.MovingPositive)
                {
                    iconUp.IconState = ScannerIcon.IconStates.Active;
                }
                else if (e.MoveDirectionY == MoveState.MoveStates.MovingNegative)
                {
                    iconDown.IconState = ScannerIcon.IconStates.Active;
                }
            }));
        }

        private void RawCommsLogScannerOutput(object sender, string datagram)
        {
            LogScannerIn(datagram);
        }

        private void RawCommsLogScannerCommand(object sender, string datagram)
        {
            LogScannerOut(datagram);
        }

        private void RawCommsLogMessage(object sender, string message)
        {
            LogMessage("Driver: " + message);
        }

        private void ResetNavigation(ScannerIcon.IconStates iconState = ScannerIcon.IconStates.Default)
        {
            foreach (var icon in NavigationIcons)
            {
                icon.IconState = iconState;
            }
        }

        private void ResetCommPortList()
        {
            string selected = null;
            if (commPortCombo.SelectedItem != null)
            {
                selected = commPortCombo.SelectedItem.ToString();
            }

            commPortCombo.Items.Clear();
            var ports = System.IO.Ports.SerialPort.GetPortNames();
            foreach (var port in ports)
            {
                commPortCombo.Items.Add(port);
                if (port.Equals(selected))
                {
                    commPortCombo.SelectedIndex = commPortCombo.Items.Count - 1;
                }
            }
        }

        private void btnRefreshCommPortList_Click(object sender, EventArgs e)
        {
            ResetCommPortList();
        }

        private void commPortCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (commPortCombo.SelectedIndex < 0)
            {
                return;
            }

            var selectedPort = commPortCombo.SelectedItem.ToString();
            if (ComPort != null && ComPort.Equals(selectedPort))
            {
                return;
            }

            var result = SemanticComms.Connect(selectedPort);
            if (result == PortStatus.Ok)
            {
                SetInterfaceEnabled(true);
                ResetNavigation(ScannerIcon.IconStates.Default);
                Settings.SerialPortName = selectedPort;
            }
            else
            {
                SetInterfaceEnabled(false);
                ResetNavigation(ScannerIcon.IconStates.Disabled);
            }

            ComPort = selectedPort;
        }

        public void LogMessage(string message)
        {
            tbMessageLog.Invoke(new MethodInvoker(delegate { tbMessageLog.AppendText(message + Environment.NewLine); tbMessageLog.Refresh(); }));
        }

        public void LogScannerIn(string datagram)
        {
            tbScannerLog.Invoke(new MethodInvoker(delegate { tbScannerLog.AppendText("< " + datagram + Environment.NewLine); tbScannerLog.Refresh(); }));
        }

        public void LogScannerOut(string datagram)
        {
            tbScannerLog.Invoke(new MethodInvoker(delegate { tbScannerLog.AppendText("> " + datagram + Environment.NewLine); tbScannerLog.Refresh(); }));
        }

        private void iconStop_Click(object sender, EventArgs e)
        {
            StopMoving();
        }

        private void iconRight_Click(object sender, EventArgs e)
        {
            SemanticComms.Move(new Point(INFINITE_STEPS, 0));
        }

        private void iconUp_Click(object sender, EventArgs e)
        {
            SemanticComms.Move(new Point(0, INFINITE_STEPS));
        }

        private void iconDown_Click(object sender, EventArgs e)
        {
            SemanticComms.Move(new Point(0, -INFINITE_STEPS));
        }

        private void iconLeft_Click(object sender, EventArgs e)
        {
            SemanticComms.Move(new Point(-INFINITE_STEPS, 0));
        }

        private void btnResetOrigin_Click(object sender, EventArgs e)
        {
            SemanticComms.ResetOrigin();
            btnSetDslrSize.Enabled = true;
            btnSetNegativeSize.Enabled = true;
        }

        private void SetInterfaceEnabled(bool enabled)
        {
            navigationGroup.Enabled = enabled;
            sweepSettingsGroup.Enabled = enabled;
        }

        private void SetInterfaceMoving(bool moving)
        {
            ScannerIsMoving = moving;
            btnSetDslrSize.Enabled = !moving;
            btnSetNegativeSize.Enabled = !moving;
            btnGoToOrigin.Enabled = !moving;
        }

        private void btnSetDslrSize_Click(object sender, EventArgs e)
        {
            CurrentSweepSettings.DslrSize = new Size(
                Math.Abs(SemanticComms.GetCurrentPos().X),
                Math.Abs(SemanticComms.GetCurrentPos().Y)
            );
            LogMessage("DSLR size set to " + CurrentSweepSettings.DslrSize.Width + " by " + CurrentSweepSettings.DslrSize.Height + " steps.");
        }

        private void btnGoToOrigin_Click(object sender, EventArgs e)
        {
            if (!ScannerIsMoving)
            {
                ExecuteMoveToOrigin();
                return;
            }

            StopMoving();
            MoveToOriginRequested = true;
            return;
        }

        private void btnSetNegativeSize_Click(object sender, EventArgs e)
        {
            CurrentSweepSettings.FilmSize = new Size(
                Math.Abs(SemanticComms.GetCurrentPos().X),
                Math.Abs(SemanticComms.GetCurrentPos().Y)
            );
            LogMessage("Negative size set to " + CurrentSweepSettings.FilmSize.Width + " by " + CurrentSweepSettings.FilmSize.Height + " steps.");
        }

        private void btnSaveSweepSettings_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbHuginTemplate.Text) || !File.Exists(tbHuginTemplate.Text))
            {
                MessageBox.Show("You must specify the Hugin panorama file before saving the sweep.");
                return;
            }

            string name;
            while (true)
            {
                name = Microsoft.VisualBasic.Interaction.InputBox("Name your sweep", "Save sweep settings");
                if (string.IsNullOrWhiteSpace(name))
                {
                    return;
                }

                if (name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                {
                    MessageBox.Show("Invalid sweep name; it has to be a valid filename");
                    continue;
                }

                if (!SweepSettingsList.ContainsKey(name))
                {
                    break;
                }

                if (DialogResult.OK == MessageBox.Show("Sweep «" + name + "» already exists. Overwrite?", "Overwrite sweep?", MessageBoxButtons.OKCancel))
                {
                    break;
                }
            }

            var finalFilename = Path.Combine(HuginPanoramaFolder, name + ".pto");
            File.Copy(tbHuginTemplate.Text, finalFilename, true);
            tbHuginTemplate.Text = finalFilename;

            var sweep = new SweepSettings()
            {
                DslrSize = CurrentSweepSettings.DslrSize,
                FilmSize = CurrentSweepSettings.FilmSize,
                HuginTemplate = finalFilename,
            };
            SweepSettingsList[name] = sweep;
            SaveSweepSettings();
        }

        private void SaveSweepSettings()
        {
            File.WriteAllText(SweepSettingsFilename, JsonConvert.SerializeObject(SweepSettingsList));
        }

        private string GetBaseFolder()
        {
            var baseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), LOCAL_APP_FOLDER_NAME);
            if (!Directory.Exists(baseFolder))
            {
                Directory.CreateDirectory(baseFolder);
            }

            return baseFolder;
        }

        private string GetPanoramaFolder()
        {
            var folderName = Path.Combine(BaseFolder, HUGIN_PANORAMA_FOLDER);
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }
            return folderName;
        }

        private void LoadSweepSettings()
        {
            var sweepSettingsFilename = SweepSettingsFilename;
            if (!File.Exists(sweepSettingsFilename))
            {
                return;
            }
            SweepSettingsList = JsonConvert.DeserializeObject<SweepSettingsCollection>(File.ReadAllText(sweepSettingsFilename));

            cbSweepSettings.Items.Clear();
            foreach (var sweepSettings in SweepSettingsList)
            {
                cbSweepSettings.Items.Add(sweepSettings.Key);
            }
        }

        private void cbSweepSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSweepSettings.SelectedItem == null)
            {
                return;
            }

            var sweepName = cbSweepSettings.SelectedItem as string;
            if (string.IsNullOrEmpty(sweepName))
            {
                LogMessage("Failed loading sweep name!");
                return;
            }


            if (!SweepSettingsList.ContainsKey(sweepName))
            {
                LogMessage("Failed finding sweep " + sweepName);
                return;
            }

            CurrentSweepSettings = SweepSettingsList[sweepName];
            tbHuginTemplate.Text = CurrentSweepSettings.HuginTemplate;
            btnNextSweepStep.Enabled = btnStartSweep.Enabled = true;
            btnResetSweep.Enabled = true;
            btnGoToMidFrame.Enabled = true;
        }

        private void btnShootLocation_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbShootLocation.Text))
            {
                folderBrowserDialog.SelectedPath = tbShootLocation.Text;
            }

            if (DialogResult.OK != folderBrowserDialog.ShowDialog())
            {
                return;
            }

            tbShootLocation.Text = folderBrowserDialog.SelectedPath;
        }

        private void MainScannerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveGlobalSettings();
        }

        private void SaveGlobalSettings()
        {
            Settings.ImageSaveFolder = tbShootLocation.Text;
            File.WriteAllText(GlobalSettingsFilename, JsonConvert.SerializeObject(Settings));
        }

        private void LoadGlobalSettings()
        {
            var globalSettingsFilename = GlobalSettingsFilename;
            if (!File.Exists(globalSettingsFilename))
            {
                Settings = new GlobalSettings();
                return;
            }

            Settings = JsonConvert.DeserializeObject<GlobalSettings>(File.ReadAllText(GlobalSettingsFilename));

            if (!string.IsNullOrEmpty(Settings.SerialPortName) && commPortCombo.Items.Contains(Settings.SerialPortName))
            {

                commPortCombo.SelectedIndex = commPortCombo.Items.IndexOf(Settings.SerialPortName);
            }

            if (!string.IsNullOrEmpty(Settings.ImageSaveFolder))
            {
                tbShootLocation.Text = Settings.ImageSaveFolder;
            }
        }

        private void MainScannerForm_Shown(object sender, EventArgs e)
        {
            LogMessage("Loading settings, and opening the last scanner port.");
            LoadGlobalSettings();
        }

        private void btnHuginTemplate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbHuginTemplate.Text))
            {
                openPtoFileDialog.FileName = tbHuginTemplate.Text;
            }

            if (DialogResult.OK == openPtoFileDialog.ShowDialog())
            {
                tbHuginTemplate.Text = openPtoFileDialog.FileName;
            }
        }

        private bool HandleKeyOrders()
        {
            CurrentKeyOrders = KeyMoveOrders.Stop;
            if (!Keyboard.IsKeyDown(Key.LeftAlt))
            {
                if (WasAltDown)
                {
                    StopMoving();
                }
                WasAltDown = false;
                return false;
            }

            WasAltDown = true;

            if (Keyboard.IsKeyDown(Key.Escape))
            {
                StopMoving();
                return true;
            }

            int x = 0;
            int y = 0;
            if (Keyboard.IsKeyDown(Key.Left))
            {
                CurrentKeyOrders |= KeyMoveOrders.Left;
                x = -INFINITE_STEPS;
            }
            else if (Keyboard.IsKeyDown(Key.Right))
            {
                CurrentKeyOrders |= KeyMoveOrders.Right;
                x = INFINITE_STEPS;
            }
            if (Keyboard.IsKeyDown(Key.Up))
            {
                CurrentKeyOrders |= KeyMoveOrders.Up;
                y = INFINITE_STEPS;
            }
            else if (Keyboard.IsKeyDown(Key.Down))
            {
                CurrentKeyOrders |= KeyMoveOrders.Down;
                y = -INFINITE_STEPS;
            }

            if (CurrentKeyOrders == PreviousKeyOrders)
            {
                return true;
            }
            PreviousKeyOrders = CurrentKeyOrders;

            if (CurrentKeyOrders == KeyMoveOrders.Stop)
            {
                StopMoving();
                return true;
            }

            SemanticComms.Move(new Point(x, y));
            return true;
        }

        private void MainScannerForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            e.Handled = HandleKeyOrders();
        }

        private void MainScannerForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            e.Handled = HandleKeyOrders();
        }

        private void StopMoving()
        {
            MoveQueue.Clear();
            SemanticComms.Stop();
        }

        private void ResetSweep()
        {
            SweepStep = ShotsInSweep = 0;
            btnNextSweepStep.Enabled = btnStartSweep.Enabled = true;
        }

        private void btnResetSweep_Click(object sender, EventArgs e)
        {
            ResetSweep();
        }

        private void btnNextSweepStep_Click(object sender, EventArgs e)
        {
            AdvanceSweepStep();
        }

        private bool AdvanceSweepStep()
        {
            _isShotRequested = true;
            bool lastSweep = false;
            SweepStep++;
            if (SweepStep == CurrentSweepSettings.SweepCount.Width * CurrentSweepSettings.SweepCount.Height - 1) // zero-indexed
            {
                btnNextSweepStep.Enabled = btnStartSweep.Enabled = false;
                lastSweep = true;
            }

            if (SweepStep % CurrentSweepSettings.SweepCount.Height != 0)
            {
                SemanticComms.Move(new Point(0, CurrentSweepSettings.SweepDelta.Height));
                return !lastSweep;
            }

            SemanticComms.Move(new Point(
                -CurrentSweepSettings.SweepDelta.Width,
                -CurrentSweepSettings.SweepDelta.Height * (CurrentSweepSettings.SweepCount.Height - 1) - BACKLASH
            ));
            MoveQueue.Add(new Point(0, BACKLASH));

            return !lastSweep;
        }

        private void cbHidePositionDatagrams_CheckedChanged(object sender, EventArgs e)
        {
            SemanticComms.IgnorePositionInLogs = ((CheckBox)sender).Checked;
        }

        private void btnStartSweep_Click(object sender, EventArgs e)
        {
            if (_isSweepRunning)
            {
                StopSweep();
                return;
            }

            if (!Directory.Exists(tbShootLocation.Text))
            {
                LogMessage("Please provide a valid shoot location before starting the sweep.");
                return;
            }

            Windows.ForEach(winInfo =>
            {
                if (winInfo.ProcessName.Equals("EOS Utility 3") && winInfo.Title.TrimStart().StartsWith("EOS"))
                {
                    EOSWindow = winInfo;
                    return false;
                }
                return true;
            });

            if (EOSWindow == null)
            {
                LogMessage("Please start the EOS Utility live shooting window before attempting the sweep");
                return;
            }

            EnsureDslrInForeground();

            _isSweepRunning = true;
            _isShotRequested = true;
            PrevImageFileList = GetImageFileList();

            if (!ScannerIsMoving)
            {
                ShootDSLR();
            }

            LastPulse = DateTime.Now;
            btnStartSweep.BackColor = Color.Red;
            btnStartSweep.ForeColor = Color.White;
            btnStartSweep.Text = "Stop";
        }

        private void ShootDSLR()
        {
            LogMessage("Shooting DSLR frame " + ShotsInSweep);

            _isShotRequested = false;
            EnsureDslrInForeground();

            var insim = new InputSimulator();
            MoveMouse(insim, 200, 70);

            for (int i = 0; i < 40; i++)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }

            insim.Mouse.LeftButtonClick();

            //EOSWindow.ClickOnPoint(new Point(200, 70));
            Thread.Sleep(50);

            _isShotInProgress = true;
        }

        private void MoveMouse(InputSimulator sim, int x, int y)
        {
            sim.Mouse.MoveMouseToPositionOnVirtualDesktop(
                (EOSWindow.GetRectangle().Left + x) / (double)SystemInformation.VirtualScreen.Width * 65535.0,
                (EOSWindow.GetRectangle().Top + y) / (double)SystemInformation.VirtualScreen.Height * 65535.0
            );
        }

        private bool IsDslrInForeground()
        {
            if (EOSWindow == null)
            {
                return false;
            }

            return Windows.GetForegroundWindowInfo().Handle.Equals(EOSWindow.Handle) && EOSWindow.IsVisible;
        }

        private void EnsureDslrInForeground()
        {
            if (IsDslrInForeground())
            {
                return;
            }

            EOSWindow.SetForeground();
            while (true)
            {
                if (IsDslrInForeground())
                {
                    break;
                }
                Application.DoEvents();
                Thread.Sleep(50);
            }
            Thread.Sleep(100);
        }

        private List<string> GetImageFileList()
        {
            return new List<string>(Directory.GetFiles(tbShootLocation.Text, "*.cr2"));
        }

        private void PulseSweepButton()
        {
            if (DateTime.Now - LastPulse <= PulseSpan)
            {
                return;
            }

            LastPulse = DateTime.Now;
            if (btnStartSweep.BackColor == Color.Red)
            {
                btnStartSweep.BackColor = Color.Black;
            }
            else
            {
                btnStartSweep.BackColor = Color.Red;
            }
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            if (_isSweepRunning)
            {
                PulseSweepButton();
            }

            var shotDetected = false;

            if (_isShotInProgress)
            {
                var currentImageFileList = GetImageFileList();
                if (currentImageFileList.Count == PrevImageFileList.Count)
                {
                    return;
                }

                _isShotInProgress = false;
                shotDetected = true;

                var diffImageFileList = currentImageFileList.Select(file => (string)file.Clone()).ToList();
                foreach (var prevImage in PrevImageFileList)
                {
                    diffImageFileList.Remove(prevImage);
                }

                if (diffImageFileList.Count != 1)
                {
                    LogMessage("Number of new CR2 images is different from 1: " + diffImageFileList.Count);
                    StopSweep();
                    return;
                }

                ShotsInSweep++;

                ProcessRaw(diffImageFileList[0]);
            }

            if (_isSweepRunning && shotDetected && !AdvanceSweepStep())
            {
                StopSweep();
            }
        }

        private void ProcessRaw(string originalFullFilename)
        {
            var originalRawFilename = Path.GetFileName(originalFullFilename);
            var finalPath = Path.Combine(tbShootLocation.Text, GetFilmFolderName(), GetFrameFolderName());
            Directory.CreateDirectory(finalPath);
            var fullPathFinalFilename = Path.Combine(finalPath, originalRawFilename);

            // Shitty way of ensuring the file handle is closed by the Canon utility thing.
            // Source: https://stackoverflow.com/questions/876473/is-there-a-way-to-check-if-a-file-is-in-use
            // N.B. We don't catch anything other than IOException
            while (true)
            {
                try
                {
                    using (var fp = File.OpenWrite(originalFullFilename))
                    {
                        break;
                    }
                }
                catch (IOException)
                {
                    if (!File.Exists(originalFullFilename))
                    {
                        throw; // Uh-oh, this is worse than we thought!
                    }
                    continue;
                }
            }
            File.Move(originalFullFilename, fullPathFinalFilename);

            if (!cbPostProcess.Checked)
            {
                return;
            }

            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(@"Resources\Auto-develop.exe", fullPathFinalFilename);
            p.Start();
        }

        private string GetFrameFolderName()
        {
            return GetFilmFolderName() + "P" + ((int)numFrameNumber.Value).ToString("D2");
        }

        private string GetFilmFolderName()
        {
            return "F" + ((int)numFilmNumber.Value).ToString("D3");
        }

        private void StopSweep()
        {
            _isSweepRunning = false;
            btnStartSweep.BackColor = SystemColors.Control;
            btnStartSweep.ForeColor = SystemColors.ControlText;
            btnStartSweep.Text = "Start";
        }

        private void btnResetFilm_Click(object sender, EventArgs e)
        {
            MoveWithBacklash(new Point(81500, -20959));
            numFrameNumber.Value++;
            ResetSweep();
        }

        private void btnNextFrame_Click(object sender, EventArgs e)
        {
            MoveWithBacklash(new Point(-16427 + 2113, -20839 + 526));
            numFrameNumber.Value++;
            ResetSweep();
        }

        private void MoveWithBacklash(Point relativeMove)
        {
            SemanticComms.Move(new Point(relativeMove.X + BACKLASH, relativeMove.Y - BACKLASH));
            MoveQueue.Add(new Point(-BACKLASH, BACKLASH));
        }

        private void btnGoToMidFrame_Click(object sender, EventArgs e)
        {
            var x = CurrentSweepSettings.FilmSize.Width / 2;
            var y = CurrentSweepSettings.FilmSize.Height / 2;
            MoveWithBacklash(new Point(-x, y));
        }
    }
}
