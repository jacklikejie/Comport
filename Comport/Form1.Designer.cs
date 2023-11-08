namespace Comport
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.RTBMessage = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.RTBSend = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.cbxSendStatus = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbxState = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cbxStop = new System.Windows.Forms.ComboBox();
            this.StopLabel = new System.Windows.Forms.Label();
            this.cbxData = new System.Windows.Forms.ComboBox();
            this.DataLabel = new System.Windows.Forms.Label();
            this.cbxParity = new System.Windows.Forms.ComboBox();
            this.ParityLabel = new System.Windows.Forms.Label();
            this.cbxBaudRate = new System.Windows.Forms.ComboBox();
            this.BaudRateLabel = new System.Windows.Forms.Label();
            this.StateLabel = new System.Windows.Forms.Label();
            this.cbxSerialPortList = new System.Windows.Forms.ComboBox();
            this.COMLabel = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 450);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(262, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(538, 450);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "测试面板";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.RTBMessage);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 191);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(532, 256);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "接收消息";
            // 
            // RTBMessage
            // 
            this.RTBMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RTBMessage.Location = new System.Drawing.Point(3, 21);
            this.RTBMessage.Name = "RTBMessage";
            this.RTBMessage.Size = new System.Drawing.Size(526, 232);
            this.RTBMessage.TabIndex = 0;
            this.RTBMessage.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.RTBSend);
            this.groupBox3.Controls.Add(this.btnSend);
            this.groupBox3.Controls.Add(this.btnClear);
            this.groupBox3.Controls.Add(this.cbxSendStatus);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 21);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(532, 170);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "发送消息";
            // 
            // RTBSend
            // 
            this.RTBSend.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.RTBSend.Location = new System.Drawing.Point(3, 71);
            this.RTBSend.Name = "RTBSend";
            this.RTBSend.Size = new System.Drawing.Size(526, 96);
            this.RTBSend.TabIndex = 4;
            this.RTBSend.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(395, 22);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(99, 30);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "发送消息";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(282, 21);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(88, 31);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "清空消息";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cbxSendStatus
            // 
            this.cbxSendStatus.FormattingEnabled = true;
            this.cbxSendStatus.Items.AddRange(new object[] {
            "文本",
            "字节"});
            this.cbxSendStatus.Location = new System.Drawing.Point(99, 22);
            this.cbxSendStatus.Name = "cbxSendStatus";
            this.cbxSendStatus.Size = new System.Drawing.Size(121, 23);
            this.cbxSendStatus.TabIndex = 1;
            this.cbxSendStatus.SelectedIndexChanged += new System.EventHandler(this.cbxSendStatus_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "消息格式:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbxState);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.cbxStop);
            this.groupBox1.Controls.Add(this.StopLabel);
            this.groupBox1.Controls.Add(this.cbxData);
            this.groupBox1.Controls.Add(this.DataLabel);
            this.groupBox1.Controls.Add(this.cbxParity);
            this.groupBox1.Controls.Add(this.ParityLabel);
            this.groupBox1.Controls.Add(this.cbxBaudRate);
            this.groupBox1.Controls.Add(this.BaudRateLabel);
            this.groupBox1.Controls.Add(this.StateLabel);
            this.groupBox1.Controls.Add(this.cbxSerialPortList);
            this.groupBox1.Controls.Add(this.COMLabel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 450);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "控制面板";
            // 
            // tbxState
            // 
            this.tbxState.Location = new System.Drawing.Point(108, 99);
            this.tbxState.Name = "tbxState";
            this.tbxState.ReadOnly = true;
            this.tbxState.Size = new System.Drawing.Size(120, 25);
            this.tbxState.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(143, 415);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 29);
            this.button1.TabIndex = 0;
            this.button1.Text = "打开串口";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbxStop
            // 
            this.cbxStop.FormattingEnabled = true;
            this.cbxStop.Location = new System.Drawing.Point(107, 366);
            this.cbxStop.Name = "cbxStop";
            this.cbxStop.Size = new System.Drawing.Size(121, 23);
            this.cbxStop.TabIndex = 11;
            // 
            // StopLabel
            // 
            this.StopLabel.AutoSize = true;
            this.StopLabel.Location = new System.Drawing.Point(41, 369);
            this.StopLabel.Name = "StopLabel";
            this.StopLabel.Size = new System.Drawing.Size(60, 15);
            this.StopLabel.TabIndex = 10;
            this.StopLabel.Text = "停止位:";
            // 
            // cbxData
            // 
            this.cbxData.FormattingEnabled = true;
            this.cbxData.Location = new System.Drawing.Point(107, 297);
            this.cbxData.Name = "cbxData";
            this.cbxData.Size = new System.Drawing.Size(121, 23);
            this.cbxData.TabIndex = 9;
            // 
            // DataLabel
            // 
            this.DataLabel.AutoSize = true;
            this.DataLabel.Location = new System.Drawing.Point(41, 300);
            this.DataLabel.Name = "DataLabel";
            this.DataLabel.Size = new System.Drawing.Size(60, 15);
            this.DataLabel.TabIndex = 8;
            this.DataLabel.Text = "数据位:";
            // 
            // cbxParity
            // 
            this.cbxParity.FormattingEnabled = true;
            this.cbxParity.Location = new System.Drawing.Point(107, 228);
            this.cbxParity.Name = "cbxParity";
            this.cbxParity.Size = new System.Drawing.Size(121, 23);
            this.cbxParity.TabIndex = 7;
            // 
            // ParityLabel
            // 
            this.ParityLabel.AutoSize = true;
            this.ParityLabel.Location = new System.Drawing.Point(26, 231);
            this.ParityLabel.Name = "ParityLabel";
            this.ParityLabel.Size = new System.Drawing.Size(75, 15);
            this.ParityLabel.TabIndex = 6;
            this.ParityLabel.Text = "奇偶校验:";
            // 
            // cbxBaudRate
            // 
            this.cbxBaudRate.FormattingEnabled = true;
            this.cbxBaudRate.Location = new System.Drawing.Point(107, 168);
            this.cbxBaudRate.Name = "cbxBaudRate";
            this.cbxBaudRate.Size = new System.Drawing.Size(121, 23);
            this.cbxBaudRate.TabIndex = 5;
            // 
            // BaudRateLabel
            // 
            this.BaudRateLabel.AutoSize = true;
            this.BaudRateLabel.Location = new System.Drawing.Point(41, 171);
            this.BaudRateLabel.Name = "BaudRateLabel";
            this.BaudRateLabel.Size = new System.Drawing.Size(60, 15);
            this.BaudRateLabel.TabIndex = 4;
            this.BaudRateLabel.Text = "波特率:";
            // 
            // StateLabel
            // 
            this.StateLabel.AutoSize = true;
            this.StateLabel.Location = new System.Drawing.Point(26, 102);
            this.StateLabel.Name = "StateLabel";
            this.StateLabel.Size = new System.Drawing.Size(75, 15);
            this.StateLabel.TabIndex = 2;
            this.StateLabel.Text = "连接状态:";
            // 
            // cbxSerialPortList
            // 
            this.cbxSerialPortList.FormattingEnabled = true;
            this.cbxSerialPortList.Location = new System.Drawing.Point(107, 30);
            this.cbxSerialPortList.Name = "cbxSerialPortList";
            this.cbxSerialPortList.Size = new System.Drawing.Size(121, 23);
            this.cbxSerialPortList.TabIndex = 1;
            // 
            // COMLabel
            // 
            this.COMLabel.AutoSize = true;
            this.COMLabel.Location = new System.Drawing.Point(26, 33);
            this.COMLabel.Name = "COMLabel";
            this.COMLabel.Size = new System.Drawing.Size(75, 15);
            this.COMLabel.TabIndex = 0;
            this.COMLabel.Text = "可用串口:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(226, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "串口工具";
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label COMLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbxStop;
        private System.Windows.Forms.Label StopLabel;
        private System.Windows.Forms.ComboBox cbxData;
        private System.Windows.Forms.Label DataLabel;
        private System.Windows.Forms.ComboBox cbxParity;
        private System.Windows.Forms.Label ParityLabel;
        private System.Windows.Forms.ComboBox cbxBaudRate;
        private System.Windows.Forms.Label BaudRateLabel;
        private System.Windows.Forms.Label StateLabel;
        private System.Windows.Forms.ComboBox cbxSerialPortList;
        private System.Windows.Forms.TextBox tbxState;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox RTBMessage;
        private System.Windows.Forms.RichTextBox RTBSend;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ComboBox cbxSendStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
    }
}

