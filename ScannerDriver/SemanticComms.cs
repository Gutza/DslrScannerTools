using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace ScannerDriver
{
    public class MoveState
    {
        public enum MoveStates
        {
            Stopped,
            MovingPositive,
            MovingNegative,
        }

        public MoveStates MoveDirectionX;
        public MoveStates MoveDirectionY;

        public bool IsStopped()
        {
            return MoveDirectionX == MoveStates.Stopped && MoveDirectionY == MoveStates.Stopped;
        }
    }

    public static class SemanticComms
    {
        public static event EventHandler<MoveState> OnScannerMoveChange;
        public static event EventHandler<string> OnRawScannerOutput;
        public static event EventHandler<string> OnRawScannerCommand;
        public static event EventHandler<string> OnLogMessage;
        public static event EventHandler<Point> OnPositionChange;

        private static Point RawPos = new Point()
        {
            X = 0,
            Y = 0,
        };

        private static Point Origin = new Point()
        {
            X = 0,
            Y = 0,
        };

        public static bool IgnorePositionInLogs = true;

        public static void Initialize()
        {
            RawComms.OnLogScannerOutput += ProcessScannerOutput;
            RawComms.OnLogMessage += ProcessRawLogMessage;
            RawComms.OnLogScannerCommand += ProcessRawScannerCommand;
        }

        public static Point GetCurrentPos()
        {
            return RawPos - new Size(Origin);
        }

        public static void ResetOrigin()
        {
            Origin = RawPos;
            OnPositionChange?.Invoke(null, new Point(0, 0));
        }

        private static void ProcessRawScannerCommand(object sender, string command)
        {
            OnRawScannerCommand?.Invoke(null, command);
        }

        private static void ProcessRawLogMessage(object sender, string message)
        {
            LogMessage(message);
        }

        private static void ProcessScannerOutput(object sender, string datagram)
        {
            var sendDatagram = true;

            switch (datagram[0])
            {
                case 'M':
                    ChangeMoveState(datagram);
                    break;
                case 'C':
                    LogMessage("Received comment «" + datagram.Substring(1) + "»");
                    break;
                case 'K':
                    // All is well, don't interpret it
                    break;
                case 'E':
                    LogMessage("The scanner couldn't understand the last command!");
                    break;
                case 'D':
                    // Debug message, don't interpret it
                    break;
                case 'P':
                    Regex stopFormat = new Regex(@"^.([\-0-9]+),([\-0-9]+)$");
                    var match = stopFormat.Match(datagram);
                    if (!match.Success)
                    {
                        LogMessage("Failed parsing the stop datagram: " + datagram);
                        break;
                    }
                    RawPos.X = int.Parse(match.Groups[1].Value);
                    RawPos.Y = int.Parse(match.Groups[2].Value);
                    OnPositionChange?.Invoke(null, RawPos - new Size(Origin));
                    if (IgnorePositionInLogs)
                    {
                        sendDatagram = false;
                    }
                    break;
                case 'I':
                    if (datagram.Equals(RawComms.ISTARTED_DATAGRAM))
                    {
                        LogMessage("The scanner started.");
                    }
                    else
                    {
                        LogMessage("Unknown information datagram: " + datagram);
                    }
                    break;
                default:
                    LogMessage("Failed interpreting the scanner's output: «" + datagram + "»");
                    break;
            }

            if (sendDatagram)
            {
                OnRawScannerOutput?.Invoke(null, datagram);
            }
        }

        public static bool Move(Point newPos)
        {
            return RawComms.SendRawDatagram("M" + newPos.X + "," + newPos.Y);
        }

        private static void ChangeMoveState(string moveStateDatagram)
        {
            if (moveStateDatagram.Length != 3)
            {
                LogMessage("The scanner move state datagram is poorly formatted: " + moveStateDatagram);
                return;
            }

            // Duplicated code on purpose -- we'll probably want to tinker with these
            var newMoveState = new MoveState();
            switch (moveStateDatagram[1])
            {
                case '+':
                    newMoveState.MoveDirectionX = MoveState.MoveStates.MovingPositive;
                    break;
                case '-':
                    newMoveState.MoveDirectionX = MoveState.MoveStates.MovingNegative;
                    break;
                case '.':
                    newMoveState.MoveDirectionX = MoveState.MoveStates.Stopped;
                    break;
                default:
                    LogMessage("The scanner move state X could not be interpreted: " + moveStateDatagram);
                    break;
            }

            switch (moveStateDatagram[2])
            {
                case '+':
                    newMoveState.MoveDirectionY = MoveState.MoveStates.MovingPositive;
                    break;
                case '-':
                    newMoveState.MoveDirectionY = MoveState.MoveStates.MovingNegative;
                    break;
                case '.':
                    newMoveState.MoveDirectionY = MoveState.MoveStates.Stopped;
                    break;
                default:
                    LogMessage("The scanner move state Y could not be interpreted: " + moveStateDatagram);
                    break;
            }

            LogMoveState(newMoveState);
        }

        public static PortStatus Connect(string portName)
        {
            var result = RawComms.OpenPort(portName, 115200);
            //RawComms.SendRawDatagram("Chello");
            return result;
        }

        public static void Disconnect()
        {
            RawComms.ClosePort(false);
        }

        private static void LogMessage(string message)
        {
            OnLogMessage?.Invoke(null, message);
        }

        private static void LogMoveState(MoveState moveState)
        {
            OnScannerMoveChange?.Invoke(null, moveState);
        }

        public static bool Stop()
        {
            return RawComms.SendRawDatagram("S");
        }
    }
}
