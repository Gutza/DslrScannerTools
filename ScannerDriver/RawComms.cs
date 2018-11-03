using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;

namespace ScannerDriver
{
    public enum PortStatus
    {
        Ok,
        Failed,
        InUse,
        OtherError,
    }

    public static class RawComms
    {
        public static event EventHandler<string> OnLogScannerOutput;
        public static event EventHandler<string> OnLogScannerCommand;
        public static event EventHandler<string> OnLogMessage;

        private static string SerialPort = null;
        private static SerialPort ScannerPort;

        private static string PartialDatagram = string.Empty;
        private static readonly char LINE_END_CHAR = (char)10;
        private static readonly string LINE_END_STRING = LINE_END_CHAR.ToString();

        private static readonly object __datagramProcessingLock = new object();

        public static PortStatus OpenPort(string portName, int baudRate)
        {
            ClosePort(true);
            SerialPort = portName;
            ScannerPort = new SerialPort(portName, baudRate);
            ScannerPort.DataReceived += ScannerDataReceived;
            try
            {
                ScannerPort.Open();
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

            LogMessage("Successfully opened raw serial port " + SerialPort);

            return PortStatus.Ok;
        }

        public static void ClosePort(bool failSilently)
        {
            if (ScannerPort == null || !ScannerPort.IsOpen)
            {
                if (!failSilently)
                {
                    LogMessage("Can't close the serial port, it's already closed.");
                }
                return;
            }

            ScannerPort.Close();
            LogMessage("Closed serial port " + SerialPort + ".");
            SerialPort = null;
        }

        private static void ScannerDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var input = ScannerPort.ReadExisting();
            lock (__datagramProcessingLock)
            {
                ProcessInput(input);
            }
        }

        private static void ProcessInput(string incoming)
        {
            PartialDatagram += incoming;
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

            foreach (var datagram in datagrams)
            {
                LogScannerOuput(datagram);
            }
        }

        public static void SendRawDatagram(string datagram)
        {
            if (ScannerPort == null || !ScannerPort.IsOpen)
            {
                LogMessage("Failed sending datagram «" + datagram + "» because the scanner is not connected.");
                return;
            }

            try
            {
                ScannerPort.Write(datagram + LINE_END_STRING);
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
}
