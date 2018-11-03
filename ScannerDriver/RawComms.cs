using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;

namespace ScannerDriver
{
    public static class RawComms
    {
        public static event EventHandler<string> OnLogScanner;
        public static event EventHandler<string> OnLogMessage;

        private static SerialPort port;

        private static ScannerState State = ScannerState.Disconnected;

        private static List<string> IncomingDatagrams = new List<string>();
        private static string PartialDatagram = string.Empty;
        private static readonly char LINE_END_CHAR = (char)10;
        private static readonly string LINE_END_STRING = LINE_END_CHAR.ToString();

        private static readonly object processingLock = new object();

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
            port.WriteLine("Chello");

            return PortStatus.Ok;
        }

        private static void ScannerDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var incoming = port.ReadExisting();
            Debug.WriteLine("«" + incoming + "»");
            lock (processingLock)
            {
                PartialDatagram += incoming;
                ProcessDatagrams();
            }
        }

        private static void ProcessDatagrams()
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
            IncomingDatagrams.AddRange(datagrams);
        }

        private static void LogMessage(string message)
        {
            if (OnLogMessage == null)
            {
                return;
            }

            OnLogMessage(null, message);
        }

        private static void LogScanner(string datagram)
        {
            if (OnLogScanner == null)
            {
                return;
            }

            OnLogScanner(null, datagram);
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
