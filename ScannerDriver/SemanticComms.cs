using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannerDriver
{
    public class MoveState
    {
        public enum MoveStates {
            Stopped,
            MovingPositive,
            MovingNegative,
        }

        public MoveStates MoveDirectionX;
        public MoveStates MoveDirectionY;
    }

    public static class SemanticComms
    {
        public static event EventHandler<MoveState> OnScannerMoveChange;
        public static event EventHandler<string> OnRawScannerOutput;
        public static event EventHandler<string> OnRawScannerCommand;
        public static event EventHandler<string> OnLogMessage;

        public static void Initialize()
        {
            RawComms.OnLogScannerOutput += ProcessScannerOutput;
            RawComms.OnLogMessage += ProcessRawLogMessage;
            RawComms.OnLogScannerCommand += ProcessRawScannerCommand;
        }

        private static void ProcessRawScannerCommand(object sender, string command)
        {
            if (OnRawScannerCommand == null)
            {
                return;
            }

            OnRawScannerCommand(null, command);
        }

        private static void ProcessRawLogMessage(object sender, string message)
        {
            LogMessage(message);
        }

        private static void ProcessScannerOutput(object sender, string datagram)
        {
            OnRawScannerOutput?.Invoke(null, datagram);

            switch (datagram[0])
            {
                case 'M':
                    ChangeMoveState(datagram);
                    break;
                case 'C':
                    LogMessage("Received comment «" + datagram.Substring(1) + "»");
                    break;
                case 'K':
                    // All is well
                    break;
                case 'E':
                    LogMessage("The scanner couldn't understand the last command!");
                    break;
                default:
                    LogMessage("Failed interpreting the scanner's output: «" + datagram + "»");
                    break;
            }
        }

        private static void ChangeMoveState(string moveStateDatagram)
        {
            if (moveStateDatagram.Length!=3)
            {
                LogMessage("The scanner move state datagram is poorly formatted: " + moveStateDatagram);
                return;
            }

            // Duplicated code on purpose -- we'll probably want to tinker with these
            var newMoveState = new MoveState();
            switch(moveStateDatagram[1])
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
        }

        public static PortStatus Connect(string portName)
        {
            var result = RawComms.OpenPort(portName, 115200);
            RawComms.SendRawDatagram("Chello");
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
    }
}
