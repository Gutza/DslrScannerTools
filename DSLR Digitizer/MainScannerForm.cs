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
        Point DslrAnchor = Point.Empty;
        Point DslrSize = Point.Empty;
        Point SweepAnchor = Point.Empty;
        Point SweepSize = Point.Empty;
        SweepSettingsCollection SweepSettingsList;
        SweepSettings CurrentSweepSettings = null;
        GlobalSettings Settings;
        const string LOCAL_APP_FOLDER_NAME = "DSLR Scanner";
        const string SWEEP_SETTINGS_FILENAME = "SweepSettings.xml";
        const string GLOBAL_SETTINGS_FILENAME = "GlobalSettings.xml";

        private string BaseFolder { get { return GetBaseFolder(); } }
        private string SweepSettingsFilename { get { return Path.Combine(BaseFolder, SWEEP_SETTINGS_FILENAME); } }
        private string GlobalSettingsFilename { get { return Path.Combine(BaseFolder, GLOBAL_SETTINGS_FILENAME); } }

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
            tbMessageLog.Invoke(new MethodInvoker(delegate { tbMessageLog.Text += message + Environment.NewLine; tbMessageLog.Refresh(); }));
        }

        public void LogScannerIn(string datagram)
        {
            tbScanLog.Invoke(new MethodInvoker(delegate { tbScanLog.Text += "< " + datagram + Environment.NewLine; }));
        }

        public void LogScannerOut(string datagram)
        {
            tbScanLog.Invoke(new MethodInvoker(delegate { tbScanLog.Text += "> " + datagram + Environment.NewLine; }));
        }

        private void iconRight_Click(object sender, EventArgs e)
        {
            SemanticComms.Move(1000, 0);
        }

        private void iconUp_Click(object sender, EventArgs e)
        {
            SemanticComms.Move(0, 1000);
        }

        private void iconStop_Click(object sender, EventArgs e)
        {
            SemanticComms.Stop();
        }

        private void iconDown_Click(object sender, EventArgs e)
        {
            SemanticComms.Move(0, -1000);
        }

        private void iconLeft_Click(object sender, EventArgs e)
        {
            SemanticComms.Move(-1000, 0);
        }

        private void btnLearnShotSize_Click(object sender, EventArgs e)
        {
            DslrAnchor = SemanticComms.GetCurrentPos();
            btnSetDslrHeight.Enabled = true;
            btnSetDslrWH.Enabled = true;
            btnSetDslrWidth.Enabled = true;
        }

        private void SetInterfaceEnabled(bool enabled)
        {
            navigationGroup.Enabled = enabled;
            sweepSettingsGroup.Enabled = enabled;
        }

        private void SetInterfaceMoving(bool moving)
        {
            if (!DslrAnchor.Equals(Point.Empty))
            {
                btnSetDslrHeight.Enabled = !moving;
                btnSetDslrWH.Enabled = !moving;
                btnSetDslrWidth.Enabled = !moving;
            }

            if (!SweepAnchor.Equals(Point.Empty))
            {
                btnSetDslrHeight.Enabled = !moving;
                btnSetDslrWidth.Enabled = !moving;
            }
        }

        private void btnSetDslrWidth_Click(object sender, EventArgs e)
        {
            DslrSize.X = Math.Abs(DslrAnchor.X - SemanticComms.GetCurrentPos().X);
            LogMessage("DSLR width set to " + DslrSize.X + " steps");
        }

        private void btnSetDslrHeight_Click(object sender, EventArgs e)
        {
            DslrSize.Y = Math.Abs(DslrAnchor.Y - SemanticComms.GetCurrentPos().Y);
            LogMessage("DSLR height set to " + DslrSize.Y + " steps");
        }

        private void btnSetDslrWH_Click(object sender, EventArgs e)
        {
            DslrSize.X = Math.Abs(DslrAnchor.X - SemanticComms.GetCurrentPos().X);
            DslrSize.Y = Math.Abs(DslrAnchor.Y - SemanticComms.GetCurrentPos().Y);
            LogMessage("DSLR width set to " + DslrSize.X + " steps, and height set to " + DslrSize.Y + "steps");
        }

        private void btnSetSweepStart_Click(object sender, EventArgs e)
        {
            SweepAnchor = SemanticComms.GetCurrentPos();
            btnSetDslrHeight.Enabled = true;
            btnSetDslrWidth.Enabled = true;
        }

        private void btnSetSweepWidth_Click(object sender, EventArgs e)
        {
            SweepSize.X = (int)Math.Ceiling(((double)Math.Abs(SemanticComms.GetCurrentPos().X - SweepAnchor.X)) / DslrSize.X);
            LogMessage("Horizontal sweep set to " + SweepSize.X + " frames");
        }

        private void btnSetSweepHeight_Click(object sender, EventArgs e)
        {
            SweepSize.Y = (int)Math.Ceiling(((double)Math.Abs(SemanticComms.GetCurrentPos().Y - SweepAnchor.Y)) / DslrSize.Y);
            LogMessage("Horizontal sweep set to " + SweepSize.X + " frames");
        }

        private void btnSaveSweepSettings_Click(object sender, EventArgs e)
        {
            string name;
            while (true)
            {
                name = Microsoft.VisualBasic.Interaction.InputBox("Name your sweep", "Save sweep settings");
                if (string.IsNullOrWhiteSpace(name))
                {
                    return;
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

            var sweep = new SweepSettings()
            {
                DslrSize = DslrSize,
                SweepSize = SweepSize,
            };
            SweepSettingsList[Name] = sweep;
            SaveSweepSettings();
        }

        private void SaveSweepSettings()
        {
            var writer = new System.Xml.Serialization.XmlSerializer(typeof(SweepSettingsCollection));

            using (var file = File.Create(SweepSettingsFilename))
            {
                writer.Serialize(file, SweepSettingsList);
            }
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

        private void LoadSweepSettings()
        {
            var sweepSettingsFilename = SweepSettingsFilename;
            if (!File.Exists(sweepSettingsFilename))
            {
                return;
            }
            var reader = new System.Xml.Serialization.XmlSerializer(typeof(SweepSettingsCollection));
            using (var file = new StreamReader(sweepSettingsFilename))
            {
                SweepSettingsList = reader.Deserialize(file) as SweepSettingsCollection;
            }

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
            var writer = new System.Xml.Serialization.XmlSerializer(typeof(GlobalSettings));

            using (var file = File.Create(GlobalSettingsFilename))
            {
                writer.Serialize(file, Settings);
            }
        }

        private void LoadGlobalSettings()
        {
            var globalSettingsFilename = GlobalSettingsFilename;
            if (!File.Exists(globalSettingsFilename))
            {
                Settings = new GlobalSettings();
                return;
            }

            var reader = new System.Xml.Serialization.XmlSerializer(typeof(GlobalSettings));
            using (var file = new StreamReader(globalSettingsFilename))
            {
                Settings = reader.Deserialize(file) as GlobalSettings;
            }

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
    }
}
