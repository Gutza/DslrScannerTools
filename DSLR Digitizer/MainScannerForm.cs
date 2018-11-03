using ScannerDriver;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DSLR_Digitizer
{
    public partial class MainScannerForm : Form
    {
        List<ScannerIcon> NavigationIcons;

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
            RawComms.OnLogScanner += RawCommsLogScanner;
        }

        private void RawCommsLogScanner(object sender, string datagram)
        {
            Log("Scanner: " + datagram);
        }

        private void RawCommsLogMessage(object sender, string message)
        {
            Log("Driver: " + message);
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
            var result = RawComms.SetPort(commPortCombo.SelectedItem.ToString(), 115200);
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
        }

        public void Log(string message)
        {
            tbLog.Text += message + Environment.NewLine;
        }
    }
}
