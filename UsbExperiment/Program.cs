using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Ports;

namespace UsbExperiment
{
    class Program
    {
        // Create the serial port with basic settings
        static SerialPort port = new SerialPort("COM8", 115200, Parity.None, 8, StopBits.One);

        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Incoming Data:");

            // Attach a method to be called when there
            // is data waiting in the port's buffer
            port.DataReceived += port_DataReceived;

            port.PinChanged += port_PinChanged;

            // Begin communications
            port.Open(); // TODO: This throws exceptions if the port is busy

            //Thread.Sleep(100);

            port.WriteLine("R1000,0");

            for (int i = 0; i<100; i++)
            {
                Thread.Sleep(100);
            }
        }

        private static void port_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            Console.WriteLine("PinChanged");
        }

        private static void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Show all the incoming data in the port's buffer
            Console.WriteLine(port.ReadExisting());
        }
    }
}
