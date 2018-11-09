using Newtonsoft.Json;
using ScannerDriver;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DSLR_Digitizer
{
    public partial class MainScannerForm : Form
    {
        List<ScannerIcon> NavigationIcons;
        string ComPort = null;

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

        private string BaseFolder { get { return GetBaseFolder(); } }
        private string SweepSettingsFilename { get { return Path.Combine(BaseFolder, SWEEP_SETTINGS_FILENAME); } }
        private string GlobalSettingsFilename { get { return Path.Combine(BaseFolder, GLOBAL_SETTINGS_FILENAME); } }
        private string HuginPanoramaFolder { get { return GetPanoramaFolder(); } }
        private List<Point> MoveQueue = new List<Point>();
        private bool MoveToOriginRequested = false;

        int SweepStep;

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
        private readonly KeyMoveOrders AllKeyOrders = KeyMoveOrders.Left | KeyMoveOrders.Right | KeyMoveOrders.Up | KeyMoveOrders.Down;

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
            SemanticComms.Move(new Point(BACKLASH, -BACKLASH) - new Size(SemanticComms.GetCurrentPos()));
            MoveQueue.Add(new Point(-BACKLASH, BACKLASH));
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
                    else
                    {
                        HandleKeyOrders(false);
                    }
                    HandleMovementQueue();
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
            SemanticComms.Move(new Point(ONE_STEP_IN_STEPS, 0));
        }

        private void iconUp_Click(object sender, EventArgs e)
        {
            SemanticComms.Move(new Point(0, ONE_STEP_IN_STEPS));
        }

        private void iconDown_Click(object sender, EventArgs e)
        {
            SemanticComms.Move(new Point(0, -ONE_STEP_IN_STEPS));
        }

        private void iconLeft_Click(object sender, EventArgs e)
        {
            SemanticComms.Move(new Point(-ONE_STEP_IN_STEPS, 0));
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

        private void HandleKeyOrders(bool allowStop)
        {
            if (ScannerIsMoving && CurrentKeyOrders == PreviousKeyOrders)
            {
                return;
            }
            PreviousKeyOrders = CurrentKeyOrders;

            if (CurrentKeyOrders == KeyMoveOrders.Stop)
            {
                if (allowStop)
                {
                    StopMoving();
                }
                return;
            }

            var x = 0;
            var y = 0;
            if (KeyMoveOrders.Right == (CurrentKeyOrders & KeyMoveOrders.Right))
            {
                x = ONE_STEP_IN_STEPS;
            }
            else if (KeyMoveOrders.Left == (CurrentKeyOrders & KeyMoveOrders.Left))
            {
                x = -ONE_STEP_IN_STEPS;
            }

            if (KeyMoveOrders.Up == (CurrentKeyOrders & KeyMoveOrders.Up))
            {
                y = ONE_STEP_IN_STEPS;
            }
            else if (KeyMoveOrders.Down == (CurrentKeyOrders & KeyMoveOrders.Down))
            {
                y = -ONE_STEP_IN_STEPS;
            }

            SemanticComms.Move(new Point(x, y));
        }

        private void MainScannerForm_KeyDown(object sender, KeyEventArgs e)
        {
            CurrentKeyOrders = KeyMoveOrders.Stop;
            if (e.KeyCode == Keys.Escape)
            {
                StopMoving();
            }

            if (e.Modifiers != Keys.Alt)
            {
                return;
            }

            bool handled = true;
            switch (e.KeyCode)
            {
                case Keys.Left:
                    CurrentKeyOrders |= KeyMoveOrders.Left;
                    break;
                case Keys.Right:
                    CurrentKeyOrders |= KeyMoveOrders.Right;
                    break;
                case Keys.Up:
                    CurrentKeyOrders |= KeyMoveOrders.Up;
                    break;
                case Keys.Down:
                    CurrentKeyOrders |= KeyMoveOrders.Down;
                    break;
                default:
                    handled = false;
                    break;
            }

            if (e.Handled = handled)
            {
                HandleKeyOrders(true);
            }
        }

        private void MainScannerForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (CurrentKeyOrders == KeyMoveOrders.Stop)
            {
                return;
            }
            else if (e.Modifiers != Keys.Alt)
            {
                CurrentKeyOrders = KeyMoveOrders.Stop;
                HandleKeyOrders(true);
                return;
            }
            
            bool handled = true;
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    return;
                case Keys.Up:
                    CurrentKeyOrders = CurrentKeyOrders & (AllKeyOrders ^ KeyMoveOrders.Up);
                    break;
                case Keys.Down:
                    CurrentKeyOrders = CurrentKeyOrders & (AllKeyOrders ^ KeyMoveOrders.Down);
                    break;
                case Keys.Left:
                    CurrentKeyOrders = CurrentKeyOrders & (AllKeyOrders ^ KeyMoveOrders.Left);
                    break;
                case Keys.Right:
                    CurrentKeyOrders = CurrentKeyOrders & (AllKeyOrders ^ KeyMoveOrders.Right);
                    break;
                default:
                    handled = false;
                    break;
            }

            if (e.Handled = handled)
            {
                HandleKeyOrders(true);
            }
        }

        private void StopMoving()
        {
            MoveQueue.Clear();
            SemanticComms.Stop();
        }

        private void HandleMovementQueue()
        {
            if (MoveQueue.Count == 0)
            {
                return;
            }

            SemanticComms.Move(new Point(MoveQueue[0].X, MoveQueue[0].Y));
            MoveQueue.RemoveAt(0);
        }

        private void btnResetSweep_Click(object sender, EventArgs e)
        {
            SweepStep = 0;
            btnNextSweepStep.Enabled = true;
        }

        private void btnNextSweepStep_Click(object sender, EventArgs e)
        {
            SweepStep++;
            if (SweepStep == CurrentSweepSettings.SweepCount.Width * CurrentSweepSettings.SweepCount.Height - 1) // zero-indexed
            {
                btnNextSweepStep.Enabled = false;
            }

            if (SweepStep % CurrentSweepSettings.SweepCount.Height != 0)
            {
                SemanticComms.Move(new Point(0, CurrentSweepSettings.SweepDelta.Height));
                return;
            }

            SemanticComms.Move(new Point(
                -CurrentSweepSettings.SweepDelta.Width,
                -CurrentSweepSettings.SweepDelta.Height * (CurrentSweepSettings.SweepCount.Height - 1) - BACKLASH
            ));
            MoveQueue.Add(new Point(0, BACKLASH));
        }

        private void cbHidePositionDatagrams_CheckedChanged(object sender, EventArgs e)
        {
            SemanticComms.IgnorePositionInLogs = ((CheckBox)sender).Checked;
        }
    }
}
