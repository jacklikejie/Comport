using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comport
{
    public class SerialPortClass
    {
        SerialPort _sp;
        SerialPort serialPort = new SerialPort();

        public SerialPortClass(string portName, int baudRate = 9600, int dataBits = 8, int stopBits = 1)
        {
            serialPort = new SerialPort
            {
                PortName = portName,
                BaudRate = baudRate,
                DataBits = dataBits,
                StopBits = (StopBits)stopBits
            };
        }

        public void  SetSerialPort() 
        {
            string[] serialPortArray= SerialPort.GetPortNames();
            serialPort.PortName = "COM1";
            serialPort.BaudRate = 9600;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.DataReceived += ReceiveDataMethod;
        }

        private void ReceiveDataMethod(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] result = new byte[serialPort.BytesToRead];
            serialPort.Read(result, 0, serialPort.BytesToRead);
        }

        public void Open()
        {
            serialPort.Open();
        }
        public void Close() 
        {
            serialPort.Close();
        }
        public void SendDataMethod(byte[] data)
        {
            bool isOpen = serialPort.IsOpen;
            if (isOpen) 
            {
                Open();
            }
            serialPort.Write(data, 0, data.Length);
        }
        public void SendDataMethod(string data)
        {
            bool isOpen = serialPort.IsOpen;
            if (isOpen)
            {
                Open();
            }
            serialPort.Write(data);
        }
    }
}
