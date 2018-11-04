using ScannerDriver;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        List<SweepSettings> SweepSettingsList = new List<SweepSettings>();

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
            tbMessageLog.Invoke(new MethodInvoker(delegate { tbMessageLog.Text += message + Environment.NewLine; }));
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
            string name = Microsoft.VisualBasic.Interaction.InputBox("Name your sweep", "Save sweep settings");
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            var sweep = new SweepSettings()
            {
                Name = name,
                DslrSize = DslrSize,
                SweepSize = SweepSize,
            };
            SweepSettingsList.Add(sweep);
            SaveSweeps();
        }

        private void SaveSweeps()
        {
            System.Xml.Serialization.XmlSerializer writer =
            new System.Xml.Serialization.XmlSerializer(typeof(List<SweepSettings>));

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml";
            System.IO.FileStream file = System.IO.File.Create(path);

            writer.Serialize(file, SweepSettingsList);
            file.Close();
        }
    }
}
