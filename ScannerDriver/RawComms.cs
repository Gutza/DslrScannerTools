using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;

namespace ScannerDriver
{
    public static class RawComms
    {
        public static event EventHandler<string> OnLogScannerOutput;
        public static event EventHandler<string> OnLogScannerCommand;
        public static event EventHandler<string> OnLogMessage;

        private static SerialPort port;

        private static ScannerState State = ScannerState.Disconnected;

        private static List<string> IncomingDatagrams = new List<string>();
        private static string PartialDatagram = string.Empty;
        private static readonly char LINE_END_CHAR = (char)10;
        private static readonly string LINE_END_STRING = LINE_END_CHAR.ToString();

        private static readonly object __datagramProcessingLock = new object();

        enum ScannerState
        {
            Disconnected,
            Idle,
            WaitingHello,
        }

        public static PortStatus SetPort(string portName, int baudRate)
        {
            port = new SerialPort(portName, baudRate);
            port.DataReceived += ScannerDataReceived;
            try
            {
                port.Open();
            }
            catch (IOException ex)
            {
                LogMessage("Failed opening port " + portName + " because of an IO exception: " + ex.Message);
                return PortStatus.Failed;
            }
            catch (UnauthorizedAccessException ex)
            {
                LogMessage("Failed opening port " + portName + " because it's busy: " + ex.Message);
                return PortStatus.InUse;
            }
            catch (Exception ex)
            {
                LogMessage("Failed opening port " + portName + " because of a generic exception: " + ex.Message);
                return PortStatus.OtherError;
            }

            State = ScannerState.WaitingHello;
            SendRawDatagram("Chello");

            return PortStatus.Ok;
        }

        private static void ScannerDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var incoming = port.ReadExisting();
            Debug.WriteLine("«" + incoming + "»");
            lock (__datagramProcessingLock)
            {
                PartialDatagram += incoming;
                ProcessInput();
                ProcessDatagrams();
            }
        }

        private static void ProcessInput()
        {
            if (!PartialDatagram.Contains(LINE_END_STRING))
            {
                return;
            }

            var datagrams = new List<string>(PartialDatagram.Split(LINE_END_CHAR));
            if (PartialDatagram.EndsWith(LINE_END_STRING))
            {
                PartialDatagram = string.Empty;
            }
            else
            {
                PartialDatagram = datagrams[datagrams.Count - 1];
                datagrams.RemoveAt(datagrams.Count - 1);
            }

            for (int i = 0; i < datagrams.Count; i++)
            {
                datagrams[i] = datagrams[i].Trim();
                if (string.IsNullOrWhiteSpace(datagrams[i]))
                {
                    datagrams.RemoveAt(i);
                    i--;
                }
            }
            IncomingDatagrams.AddRange(datagrams);
        }

        private static void ProcessDatagrams()
        {
            foreach (var datagram in IncomingDatagrams)
            {
                LogScannerOuput(datagram);
            }
            IncomingDatagrams.Clear();
        }

        public static void SendRawDatagram(string datagram)
        {
            if (port == null || !port.IsOpen)
            {
                LogMessage("Failed sending datagram «" + datagram + "» because the scanner is not connected.");
                State = ScannerState.Disconnected;
                return;
            }

            try
            {
                port.Write(datagram + LINE_END_STRING);
            }
            catch (Exception ex)
            {
                LogMessage("Failed sending datagram «" + datagram + "»: " + ex.Message);
                return;
            }
            LogScannerCommand(datagram);
        }

        private static void LogMessage(string message)
        {
            if (OnLogMessage == null)
            {
                return;
            }

            OnLogMessage(null, message);
        }

        private static void LogScannerOuput(string datagram)
        {
            if (OnLogScannerOutput == null)
            {
                return;
            }

            OnLogScannerOutput(null, datagram);
        }

        private static void LogScannerCommand(string datagram)
        {
            if (OnLogScannerCommand == null)
            {
                return;
            }

            OnLogScannerCommand(null, datagram);
        }
    }

    public enum PortStatus
    {
        Ok,
        Failed,
        InUse,
        OtherError,
    }
}
