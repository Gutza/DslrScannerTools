using ScannerDriver;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DSLR_Digitizer
{
    public partial class MainScannerForm : Form
    {
        List<ScannerIcon> NavigationIcons;
        string ComPort = null;

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
            RawComms.OnLogMessage += RawCommsLogMessage;
            RawComms.OnLogScannerOutput += RawCommsLogScannerOutput;
            RawComms.OnLogScannerCommand += RawCommsLogScannerCommand;
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

        private void ResetNavigation(ScannerIcon.IconStates iconState = ScannerIcon.IconStates.Active)
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
            if (ComPort!=null && ComPort.Equals(selectedPort))
            {
                return;
            }

            var result = SemanticComms.Connect(selectedPort);
            if (result == PortStatus.Ok)
            {
                navigationGroup.Enabled = true;
                ResetNavigation(ScannerIcon.IconStates.Default);
            }
            else
            {
                navigationGroup.Enabled = false;
                ResetNavigation(ScannerIcon.IconStates.Disabled);
            }
            ComPort = selectedPort;
        }

        public void LogMessage(string message)
        {
            tbMessageLog.Text += message + Environment.NewLine;
        }

        public void LogScannerIn(string datagram)
        {
            tbScanLog.Invoke(new MethodInvoker(delegate{ tbScanLog.Text += "< " + datagram + Environment.NewLine; }));
        }

        public void LogScannerOut(string datagram)
        {
            tbScanLog.Text += "> " + datagram + Environment.NewLine;
        }
    }
}
