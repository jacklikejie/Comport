using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Comport
{
    public partial class Form1 : Form
    {
        SerialPortClass portClass = null;
        SerialPort serialPort = new SerialPort();
        bool sendText = true;
        public Form1()
        {
            InitializeComponent();
            cbxSerialPortList.DataSource = SerialPort.GetPortNames().Length == 0 ? new object[] { "COM未知" } : SerialPort.GetPortNames();
            cbxBaudRate.DataSource = new object[] { 9600, 19200 };
            cbxParity.DataSource = new object[] { "None", "Odd", "Even", "Mark", "Space" };
            cbxData.DataSource = new object[] { 5, 6, 7, 8 };
            cbxStop.DataSource = new object[] { 1, 1.5, 2 };
            cbxSerialPortList.SelectedIndex = 0;
            cbxBaudRate.SelectedIndex = 0; 
            cbxParity.SelectedIndex = 0;
            cbxData.SelectedIndex = 3;
            cbxStop.SelectedIndex = 0;
            cbxSendStatus.SelectedIndex = 0;
            RTBMessage.Text = "";
            btnSend.Enabled = false;
            serialPort.DataReceived += GetReceiveMsg;
        }

        private void GetReceiveMsg(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] result = new byte[serialPort.BytesToRead];
            serialPort.Read(result, 0, serialPort.BytesToRead);
            string str = $"{DateTime.Now}:\n";
            if (sendText)
            {
                str += $"{Encoding.UTF8 .GetString(result)}";
            }
            else
            {
                for (int i = 0; i < result.Length; i++)
                {
                    str += $"{result[i].ToString("X2")}";
                }
            }
            SetRecMsgRbx(str.Trim());
        }

        private void SetRecMsgRbx(string str)
        {
            RTBMessage.Invoke(new Action(() => { RTBMessage.Text += str; }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)
            {
                serialPort.PortName = cbxSerialPortList.SelectedItem.ToString();
                serialPort.BaudRate = (int)cbxBaudRate.SelectedItem;
                serialPort.Parity = GetSelectedParity();
                serialPort.DataBits=(int)cbxData.SelectedItem;
                serialPort.StopBits = GetSelectedStopBits();
                serialPort.ReadBufferSize = 1024;
                serialPort.WriteBufferSize = 1024;  
                try
                {
                    if (!serialPort.IsOpen) serialPort.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message+"_无法打开此串口，请检查是否被占用");
                    return;
                }
                tbxState.Text = "已连接";
                button1.Text = "关闭串口";
                cbxSerialPortList.Enabled = false;
                cbxBaudRate.Enabled = false;
                cbxParity.Enabled = false;
                cbxData.Enabled = false;
                cbxStop.Enabled = false;
                btnSend.Enabled = true;
            }
            else
            {
                if (serialPort != null)
                {
                    serialPort.Close();
                }
                tbxState.Text = "未连接";
                button1.Text = "打开串口";
                cbxSerialPortList.Enabled = true;
                cbxBaudRate.Enabled = true;
                cbxParity.Enabled = true;
                cbxData.Enabled = true;
                cbxStop.Enabled = true;
                btnSend.Enabled = false;
            }
        }

        private Parity GetSelectedParity()
        {
            switch (cbxStop.SelectedItem.ToString())
            {
                case "Odd":
                    return Parity.Odd;
                case "Even":
                    return Parity.Even;
                case "Mark":
                    return Parity.Mark;
                case "Space":
                    return Parity.Space;
                default:
                    return Parity.None;
            }
        }

        private StopBits GetSelectedStopBits()
        {
            switch (Convert.ToDouble(cbxStop.SelectedItem))
            {
                case 1:
                    return StopBits.One;
                case 1.5:
                    return StopBits.OnePointFive;
                case 2:
                    return StopBits.Two;
                default:
                    return StopBits.One;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            RTBMessage.Text = string.Empty;    
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //string str = new TextRange(RTBSend.Text,);
            string str = RTBSend.Text;
            if (string.IsNullOrEmpty (str.Replace("\r\n","")))
            {
                MessageBox.Show("未输入消息");
                return;
            }
            if (sendText)
            {
                serialPort.Write(str);
                DateTime dt = DateTime.Now;
                while (serialPort.BytesToRead==0)
                {
                    Thread.Sleep(1);
                    if (DateTime.Now.Subtract(dt).TotalMilliseconds > 5000)
                    {
                        throw new Exception("串口无相应");
                    }
                }
                Thread.Sleep(50);
                byte[] recData = new byte[serialPort.BytesToRead];
                serialPort.Read(recData, 0, recData.Length);
                RTBMessage.Text = Convert.ToString(recData);
            }
            else
            {
                str = str.Replace(" ", "").Replace("\r\n", "");
                var strArr = Regex.Matches(str, ".{2}").Cast<Match>().Select(m => m.Value);
                byte[] data = new byte[strArr.Count()];
                int temp = 0;
                foreach (string  item in strArr)
                {
                    data[temp] = Convert.ToByte(item,16);
                    temp++;
                }
                serialPort.Write(data, 0, data.Length);
            }
        }

        private void cbxSendStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            sendText = cbxSendStatus.SelectedItem.ToString() == "文本" ? true : false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ScanCMD(RTBSend.Text);
        }
        private byte[] ScanCMD(string cmd)
        {
            string[] strings = cmd.Split(',');
            byte[] bytes = new byte[strings.Length];
            //bytes = Encoding.ASCII.GetBytes(cmd);
            for (int i = 0; i < strings.Length; i++)
            {
                bytes[i] = Convert.ToByte(strings[i], 16);
            }
            return bytes;
        }
    }
}
