
namespace GUI_Control_and_Display
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.btnSetTime = new System.Windows.Forms.Button();
            this.btnSetDate = new System.Windows.Forms.Button();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtSetDate = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSetTime = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDHT11DataFrame = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDHT11Status = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtHu = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTemp = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.butOnLED = new System.Windows.Forms.Button();
            this.txtLEDStatus = new System.Windows.Forms.TextBox();
            this.butOffLED = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLEDDataFrame = new System.Windows.Forms.TextBox();
            this.Communication = new System.Windows.Forms.GroupBox();
            this.cboBaudrate = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboComPort = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.butConnect = new System.Windows.Forms.Button();
            this.butExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.serCOM = new System.IO.Ports.SerialPort(this.components);
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.Communication.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.txtTime);
            this.groupBox3.Controls.Add(this.btnSetTime);
            this.groupBox3.Controls.Add(this.btnSetDate);
            this.groupBox3.Controls.Add(this.txtDate);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.txtSetDate);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtSetTime);
            this.groupBox3.Location = new System.Drawing.Point(572, 440);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(509, 293);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "RTC DS3231";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(25, 184);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(55, 20);
            this.label13.TabIndex = 13;
            this.label13.Text = "Date:";
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(247, 239);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(152, 22);
            this.txtTime.TabIndex = 12;
            // 
            // btnSetTime
            // 
            this.btnSetTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSetTime.Location = new System.Drawing.Point(416, 118);
            this.btnSetTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSetTime.Name = "btnSetTime";
            this.btnSetTime.Size = new System.Drawing.Size(78, 29);
            this.btnSetTime.TabIndex = 11;
            this.btnSetTime.Text = "Send";
            this.btnSetTime.UseVisualStyleBackColor = true;
            this.btnSetTime.Click += new System.EventHandler(this.btnSetTime_Click);
            // 
            // btnSetDate
            // 
            this.btnSetDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSetDate.Location = new System.Drawing.Point(416, 52);
            this.btnSetDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSetDate.Name = "btnSetDate";
            this.btnSetDate.Size = new System.Drawing.Size(78, 31);
            this.btnSetDate.TabIndex = 10;
            this.btnSetDate.Text = "Send";
            this.btnSetDate.UseVisualStyleBackColor = true;
            this.btnSetDate.Click += new System.EventHandler(this.btnSetDate_Click);
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(247, 182);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(152, 22);
            this.txtDate.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(25, 241);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 20);
            this.label12.TabIndex = 8;
            this.label12.Text = "Time:";
            // 
            // txtSetDate
            // 
            this.txtSetDate.Location = new System.Drawing.Point(247, 58);
            this.txtSetDate.Name = "txtSetDate";
            this.txtSetDate.Size = new System.Drawing.Size(152, 22);
            this.txtSetDate.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(25, 63);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(180, 20);
            this.label10.TabIndex = 7;
            this.label10.Text = "Set Day/DD/MM/YY:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(25, 127);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(143, 20);
            this.label11.TabIndex = 4;
            this.label11.Text = "Set HH/MM/SS:";
            // 
            // txtSetTime
            // 
            this.txtSetTime.Location = new System.Drawing.Point(247, 122);
            this.txtSetTime.Name = "txtSetTime";
            this.txtSetTime.Size = new System.Drawing.Size(152, 22);
            this.txtSetTime.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox2.Controls.Add(this.txtDHT11DataFrame);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtDHT11Status);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtHu);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtTemp);
            this.groupBox2.Location = new System.Drawing.Point(36, 440);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(498, 293);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DHT11 Control";
            // 
            // txtDHT11DataFrame
            // 
            this.txtDHT11DataFrame.Location = new System.Drawing.Point(229, 239);
            this.txtDHT11DataFrame.Name = "txtDHT11DataFrame";
            this.txtDHT11DataFrame.Size = new System.Drawing.Size(218, 22);
            this.txtDHT11DataFrame.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(24, 241);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(179, 20);
            this.label9.TabIndex = 10;
            this.label9.Text = "DHT11 Data Frame:";
            // 
            // txtDHT11Status
            // 
            this.txtDHT11Status.Location = new System.Drawing.Point(229, 180);
            this.txtDHT11Status.Name = "txtDHT11Status";
            this.txtDHT11Status.Size = new System.Drawing.Size(218, 22);
            this.txtDHT11Status.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(24, 182);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(134, 20);
            this.label8.TabIndex = 8;
            this.label8.Text = "DHT11 Status:";
            // 
            // txtHu
            // 
            this.txtHu.Location = new System.Drawing.Point(229, 122);
            this.txtHu.Name = "txtHu";
            this.txtHu.Size = new System.Drawing.Size(92, 22);
            this.txtHu.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(24, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "Humidity (%):";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(24, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(168, 20);
            this.label7.TabIndex = 4;
            this.label7.Text = "Temperature (DC):";
            // 
            // txtTemp
            // 
            this.txtTemp.Location = new System.Drawing.Point(229, 58);
            this.txtTemp.Name = "txtTemp";
            this.txtTemp.Size = new System.Drawing.Size(92, 22);
            this.txtTemp.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.butOnLED);
            this.groupBox1.Controls.Add(this.txtLEDStatus);
            this.groupBox1.Controls.Add(this.butOffLED);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtLEDDataFrame);
            this.groupBox1.Location = new System.Drawing.Point(572, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(509, 293);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LED Control";
            // 
            // butOnLED
            // 
            this.butOnLED.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butOnLED.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.butOnLED.Location = new System.Drawing.Point(81, 63);
            this.butOnLED.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.butOnLED.Name = "butOnLED";
            this.butOnLED.Size = new System.Drawing.Size(138, 44);
            this.butOnLED.TabIndex = 3;
            this.butOnLED.Text = "On LED";
            this.butOnLED.UseVisualStyleBackColor = true;
            this.butOnLED.Click += new System.EventHandler(this.butOnLED_Click);
            // 
            // txtLEDStatus
            // 
            this.txtLEDStatus.Location = new System.Drawing.Point(258, 159);
            this.txtLEDStatus.Name = "txtLEDStatus";
            this.txtLEDStatus.Size = new System.Drawing.Size(152, 22);
            this.txtLEDStatus.TabIndex = 6;
            // 
            // butOffLED
            // 
            this.butOffLED.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butOffLED.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.butOffLED.Location = new System.Drawing.Point(273, 63);
            this.butOffLED.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.butOffLED.Name = "butOffLED";
            this.butOffLED.Size = new System.Drawing.Size(137, 44);
            this.butOffLED.TabIndex = 4;
            this.butOffLED.Text = "Off LED";
            this.butOffLED.UseVisualStyleBackColor = true;
            this.butOffLED.Click += new System.EventHandler(this.butOffLED_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(77, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "LED Status:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(77, 229);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(157, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "LED Data Frame:";
            // 
            // txtLEDDataFrame
            // 
            this.txtLEDDataFrame.Location = new System.Drawing.Point(258, 227);
            this.txtLEDDataFrame.Name = "txtLEDDataFrame";
            this.txtLEDDataFrame.Size = new System.Drawing.Size(152, 22);
            this.txtLEDDataFrame.TabIndex = 5;
            // 
            // Communication
            // 
            this.Communication.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Communication.Controls.Add(this.cboBaudrate);
            this.Communication.Controls.Add(this.label3);
            this.Communication.Controls.Add(this.cboComPort);
            this.Communication.Controls.Add(this.label2);
            this.Communication.Controls.Add(this.butConnect);
            this.Communication.Controls.Add(this.butExit);
            this.Communication.Location = new System.Drawing.Point(36, 93);
            this.Communication.Name = "Communication";
            this.Communication.Size = new System.Drawing.Size(498, 293);
            this.Communication.TabIndex = 15;
            this.Communication.TabStop = false;
            this.Communication.Text = "Communication";
            // 
            // cboBaudrate
            // 
            this.cboBaudrate.FormattingEnabled = true;
            this.cboBaudrate.Location = new System.Drawing.Point(229, 83);
            this.cboBaudrate.Name = "cboBaudrate";
            this.cboBaudrate.Size = new System.Drawing.Size(152, 24);
            this.cboBaudrate.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Select Baudrate:";
            // 
            // cboComPort
            // 
            this.cboComPort.FormattingEnabled = true;
            this.cboComPort.Location = new System.Drawing.Point(229, 31);
            this.cboComPort.Name = "cboComPort";
            this.cboComPort.Size = new System.Drawing.Size(152, 24);
            this.cboComPort.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Select COM:";
            // 
            // butConnect
            // 
            this.butConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butConnect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.butConnect.Location = new System.Drawing.Point(89, 140);
            this.butConnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.butConnect.Name = "butConnect";
            this.butConnect.Size = new System.Drawing.Size(308, 55);
            this.butConnect.TabIndex = 1;
            this.butConnect.Text = "Connect COM Port";
            this.butConnect.UseVisualStyleBackColor = true;
            this.butConnect.Click += new System.EventHandler(this.butConnect_Click);
            // 
            // butExit
            // 
            this.butExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.butExit.Location = new System.Drawing.Point(176, 220);
            this.butExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.butExit.Name = "butExit";
            this.butExit.Size = new System.Drawing.Size(135, 55);
            this.butExit.TabIndex = 2;
            this.butExit.Text = "Exit";
            this.butExit.UseVisualStyleBackColor = true;
            this.butExit.Click += new System.EventHandler(this.butExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(258, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(580, 38);
            this.label1.TabIndex = 14;
            this.label1.Text = "PC-based Measurement and Control";
            // 
            // serCOM
            // 
            this.serCOM.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serCOM_DataReceived);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1120, 763);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Communication);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Communication.ResumeLayout(false);
            this.Communication.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtSetDate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSetTime;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtDHT11DataFrame;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDHT11Status;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtHu;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTemp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button butOnLED;
        private System.Windows.Forms.TextBox txtLEDStatus;
        private System.Windows.Forms.Button butOffLED;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLEDDataFrame;
        private System.Windows.Forms.GroupBox Communication;
        private System.Windows.Forms.ComboBox cboBaudrate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboComPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button butConnect;
        private System.Windows.Forms.Button butExit;
        private System.Windows.Forms.Label label1;
        private System.IO.Ports.SerialPort serCOM;
        private System.Windows.Forms.Button btnSetTime;
        private System.Windows.Forms.Button btnSetDate;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtTime;
    }
}

