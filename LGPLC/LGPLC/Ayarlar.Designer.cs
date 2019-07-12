namespace LGPLC
{
    partial class Ayarlar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ayarlar));
            this.dgCihazlar = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSil = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.gBoxTCP = new System.Windows.Forms.GroupBox();
            this.nmPort = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gBoxSerial = new System.Windows.Forms.GroupBox();
            this.btnrefreshport = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbAdress = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.nmbInterval = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.nmTimeout = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbConType = new System.Windows.Forms.ComboBox();
            this.txtDeviceName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgCihazlar)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gBoxTCP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmPort)).BeginInit();
            this.gBoxSerial.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmbInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // dgCihazlar
            // 
            this.dgCihazlar.AllowUserToAddRows = false;
            this.dgCihazlar.AllowUserToDeleteRows = false;
            this.dgCihazlar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgCihazlar.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dgCihazlar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCihazlar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgCihazlar.Location = new System.Drawing.Point(224, 0);
            this.dgCihazlar.Name = "dgCihazlar";
            this.dgCihazlar.RowHeadersVisible = false;
            this.dgCihazlar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCihazlar.Size = new System.Drawing.Size(665, 486);
            this.dgCihazlar.TabIndex = 0;
            this.dgCihazlar.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCihazlar_CellClick);
            this.dgCihazlar.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCihazlar_CellContentClick);
            this.dgCihazlar.SelectionChanged += new System.EventHandler(this.dgCihazlar_SelectionChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.gBoxTCP);
            this.panel1.Controls.Add(this.gBoxSerial);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(224, 486);
            this.panel1.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnSil);
            this.groupBox3.Controls.Add(this.btnUpdate);
            this.groupBox3.Controls.Add(this.btnCreate);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 368);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(224, 118);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            // 
            // btnSil
            // 
            this.btnSil.Location = new System.Drawing.Point(74, 63);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(88, 49);
            this.btnSil.TabIndex = 2;
            this.btnSil.Text = "Sil";
            this.btnSil.UseVisualStyleBackColor = true;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(118, 9);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(88, 49);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Güncelle";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(24, 9);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(88, 49);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "Ekle";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.button1_Click);
            // 
            // gBoxTCP
            // 
            this.gBoxTCP.Controls.Add(this.nmPort);
            this.gBoxTCP.Controls.Add(this.label4);
            this.gBoxTCP.Controls.Add(this.txtIP);
            this.gBoxTCP.Controls.Add(this.label3);
            this.gBoxTCP.Dock = System.Windows.Forms.DockStyle.Top;
            this.gBoxTCP.Location = new System.Drawing.Point(0, 294);
            this.gBoxTCP.Name = "gBoxTCP";
            this.gBoxTCP.Size = new System.Drawing.Size(224, 74);
            this.gBoxTCP.TabIndex = 4;
            this.gBoxTCP.TabStop = false;
            this.gBoxTCP.Text = "TCP";
            // 
            // nmPort
            // 
            this.nmPort.Location = new System.Drawing.Point(78, 48);
            this.nmPort.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nmPort.Name = "nmPort";
            this.nmPort.Size = new System.Drawing.Size(78, 20);
            this.nmPort.TabIndex = 7;
            this.nmPort.Value = new decimal(new int[] {
            502,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Port :";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(78, 19);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(137, 20);
            this.txtIP.TabIndex = 5;
            this.txtIP.Text = "127.0.0.1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "IP :";
            // 
            // gBoxSerial
            // 
            this.gBoxSerial.Controls.Add(this.btnrefreshport);
            this.gBoxSerial.Controls.Add(this.label11);
            this.gBoxSerial.Controls.Add(this.cmbAdress);
            this.gBoxSerial.Controls.Add(this.label10);
            this.gBoxSerial.Controls.Add(this.cmbStopBits);
            this.gBoxSerial.Controls.Add(this.label9);
            this.gBoxSerial.Controls.Add(this.cmbParity);
            this.gBoxSerial.Controls.Add(this.label8);
            this.gBoxSerial.Controls.Add(this.cmbBaudRate);
            this.gBoxSerial.Controls.Add(this.label7);
            this.gBoxSerial.Controls.Add(this.cmbPort);
            this.gBoxSerial.Dock = System.Windows.Forms.DockStyle.Top;
            this.gBoxSerial.Location = new System.Drawing.Point(0, 143);
            this.gBoxSerial.Name = "gBoxSerial";
            this.gBoxSerial.Size = new System.Drawing.Size(224, 151);
            this.gBoxSerial.TabIndex = 5;
            this.gBoxSerial.TabStop = false;
            this.gBoxSerial.Text = "Serial";
            // 
            // btnrefreshport
            // 
            this.btnrefreshport.Location = new System.Drawing.Point(168, 19);
            this.btnrefreshport.Name = "btnrefreshport";
            this.btnrefreshport.Size = new System.Drawing.Size(47, 21);
            this.btnrefreshport.TabIndex = 18;
            this.btnrefreshport.Text = "Yenile";
            this.btnrefreshport.UseVisualStyleBackColor = true;
            this.btnrefreshport.Click += new System.EventHandler(this.btnrefreshport_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(21, 130);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Address :";
            // 
            // cmbAdress
            // 
            this.cmbAdress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAdress.FormattingEnabled = true;
            this.cmbAdress.Location = new System.Drawing.Point(78, 127);
            this.cmbAdress.Name = "cmbAdress";
            this.cmbAdress.Size = new System.Drawing.Size(137, 21);
            this.cmbAdress.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 76);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "StopBits :";
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStopBits.FormattingEnabled = true;
            this.cmbStopBits.Location = new System.Drawing.Point(78, 73);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(137, 21);
            this.cmbStopBits.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(33, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Parity :";
            // 
            // cmbParity
            // 
            this.cmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParity.FormattingEnabled = true;
            this.cmbParity.Location = new System.Drawing.Point(78, 100);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(137, 21);
            this.cmbParity.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Baudrate :";
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Location = new System.Drawing.Point(78, 46);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(137, 21);
            this.cmbBaudRate.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(40, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Port :";
            // 
            // cmbPort
            // 
            this.cmbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Location = new System.Drawing.Point(78, 19);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(84, 21);
            this.cmbPort.TabIndex = 8;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.nmbInterval);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.nmTimeout);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.cmbConType);
            this.groupBox4.Controls.Add(this.txtDeviceName);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(224, 143);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Cihaz";
            // 
            // nmbInterval
            // 
            this.nmbInterval.Location = new System.Drawing.Point(78, 103);
            this.nmbInterval.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nmbInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmbInterval.Name = "nmbInterval";
            this.nmbInterval.Size = new System.Drawing.Size(78, 20);
            this.nmbInterval.TabIndex = 28;
            this.nmbInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(167, 105);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(23, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "min";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(25, 105);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 13);
            this.label13.TabIndex = 26;
            this.label13.Text = "Interval :";
            // 
            // nmTimeout
            // 
            this.nmTimeout.Location = new System.Drawing.Point(79, 77);
            this.nmTimeout.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nmTimeout.Name = "nmTimeout";
            this.nmTimeout.Size = new System.Drawing.Size(78, 20);
            this.nmTimeout.TabIndex = 25;
            this.nmTimeout.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(168, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "ms";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "TimeOut :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Bağlantı Tipi :";
            // 
            // cmbConType
            // 
            this.cmbConType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConType.FormattingEnabled = true;
            this.cmbConType.Location = new System.Drawing.Point(79, 49);
            this.cmbConType.Name = "cmbConType";
            this.cmbConType.Size = new System.Drawing.Size(137, 21);
            this.cmbConType.TabIndex = 21;
            this.cmbConType.SelectedIndexChanged += new System.EventHandler(this.cmbConType_SelectedIndexChanged);
            // 
            // txtDeviceName
            // 
            this.txtDeviceName.Location = new System.Drawing.Point(79, 23);
            this.txtDeviceName.Name = "txtDeviceName";
            this.txtDeviceName.Size = new System.Drawing.Size(137, 20);
            this.txtDeviceName.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Cihaz Adı :";
            // 
            // Ayarlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 486);
            this.Controls.Add(this.dgCihazlar);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Ayarlar";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayarlar";
            this.Load += new System.EventHandler(this.Ayarlar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgCihazlar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.gBoxTCP.ResumeLayout(false);
            this.gBoxTCP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmPort)).EndInit();
            this.gBoxSerial.ResumeLayout(false);
            this.gBoxSerial.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmbInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmTimeout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgCihazlar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gBoxSerial;
        private System.Windows.Forms.GroupBox gBoxTCP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbAdress;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbStopBits;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.NumericUpDown nmPort;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown nmTimeout;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbConType;
        private System.Windows.Forms.TextBox txtDeviceName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Button btnrefreshport;
        private System.Windows.Forms.NumericUpDown nmbInterval;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
    }
}