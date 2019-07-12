using LGPLC.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LGPLC
{
    public partial class Ayarlar : FormHelper
    {

        public Device Cihaz { get; set; }
        public Ayarlar()
        {
            InitializeComponent();
        }
        private void Ayarlar_Load(object sender, EventArgs e)
        {
            dgCihazlar.DataSource = DB.Cihazlar;
            dgCihazlar.Columns.Add(new DataGridViewButtonColumn()
            {
                UseColumnTextForButtonValue = true,
                HeaderText = "Ayarları",
                Name = "mybtn",
                Text = "Adresleri"
            });
            dgCihazlar.Columns["id"].Visible = false;
            dgCihazlar.Columns["DataPoints"].Visible = false;
            dgCihazlar.Columns["Timer"].Visible = false;
            dgCihazlar.Columns["Modbus"].Visible = false;
            dgCihazlar.Columns["Reporting"].Visible = false;
            //dgCihazlar.Columns["Device"].Visible = false;

            dgCihazlar.Columns["DeviceName"].MinimumWidth = 100;
            dgCihazlar.Columns["DeviceAddr"].HeaderText = "Device Address";
            dgCihazlar.Columns["SerialPort"].HeaderText = "Serial Port";



            cmbConType.DataSource = Enum.GetValues(typeof(ConType));
            for (int i = 1; i < 50; i++)
            {
                cmbAdress.Items.Add(i.ToString());
            }
            cmbBaudRate.Items.Add(9600);
            cmbBaudRate.Items.Add(19200);
            cmbBaudRate.Items.Add(38400);
            cmbBaudRate.Items.Add(115200);
            cmbPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            cmbParity.DataSource = Enum.GetValues(typeof(System.IO.Ports.Parity));
            cmbStopBits.DataSource = Enum.GetValues(typeof(System.IO.Ports.StopBits));
            if (cmbPort.Items.Count > 0) cmbPort.SelectedIndex = 0;
            cmbBaudRate.SelectedIndex = 0;
            cmbAdress.SelectedIndex = 0;
            cmbConType.SelectedIndex = 1;
            cmbStopBits.SelectedIndex = (int)System.IO.Ports.StopBits.One;
            cmbParity.SelectedIndex = (int)System.IO.Ports.Parity.None;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if ((ConType)cmbConType.SelectedItem == ConType.Serial & DB.Cihazlar.Any(x => x.ConType == ConType.Serial & x.SerialPort == cmbPort.Text))
            {
                MessageBox.Show("Aynı Kayıttan zaten var! ");
                return;
            }
            if ((ConType)cmbConType.SelectedItem == ConType.Serial & cmbPort.Items.Count == 0)
            {
                MessageBox.Show("Seri Port Yok!");
                return;
            }
            if ((ConType)cmbConType.SelectedItem == ConType.TCP & DB.Cihazlar.Any(x => x.ConType == ConType.TCP & x.IP == txtIP.Text.Trim()))
            {
                MessageBox.Show("Aynı Kayıttan zaten var! ");
                return;
            }
            ConType Tipi = (ConType)cmbConType.SelectedItem;
            DB.Cihazlar.Add(new Database.Device()
            {
                id = DB.Cihazlar.Count + 1,
                ConType = Tipi,
                Baudrate = Tipi == ConType.Serial ? (int)cmbBaudRate.SelectedItem : 0,
                DeviceName = txtDeviceName.Text,
                Parity = Tipi == ConType.Serial ? (System.IO.Ports.Parity)cmbParity.SelectedItem : 0,
                SerialPort = Tipi == ConType.Serial ? cmbPort.Text : "none",
                Stopbits = Tipi == ConType.Serial ? (System.IO.Ports.StopBits)cmbStopBits.SelectedItem : 0,
                DeviceAddr = Tipi == ConType.Serial ? Convert.ToInt32(cmbAdress.SelectedItem) : 0,
                Timeout = (int)nmTimeout.Value,
                IP = Tipi == ConType.TCP ? txtIP.Text.Trim() : "none",
                Port = Tipi == ConType.TCP ? (int)nmPort.Value : 0,
                Interval = (int)nmbInterval.Value
            });
            DB.Save();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Cihaz == null) return;
            if (MessageBox.Show(string.Format("{0}\nGüncellemek istediğinize eminmisiniz?", Cihaz.DeviceName), "Güncelle", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                ConType Tipi = (ConType)cmbConType.SelectedItem;

                Cihaz.ConType = Tipi;
                Cihaz.Baudrate = Tipi == ConType.Serial ? (int)cmbBaudRate.SelectedItem : 0;
                Cihaz.DeviceName = txtDeviceName.Text;
                Cihaz.Parity = Tipi == ConType.Serial ? (System.IO.Ports.Parity)cmbParity.SelectedItem : 0;
                Cihaz.SerialPort = Tipi == ConType.Serial ? cmbPort.Text : "none";
                Cihaz.Stopbits = Tipi == ConType.Serial ? (System.IO.Ports.StopBits)cmbStopBits.SelectedItem : 0;
                Cihaz.DeviceAddr = Tipi == ConType.Serial ? Convert.ToInt32(cmbAdress.SelectedItem) : 0;
                Cihaz.Timeout = (int)nmTimeout.Value;
                Cihaz.IP = Tipi == ConType.TCP ? txtIP.Text.Trim() : "none";
                Cihaz.Port = Tipi == ConType.TCP ? (int)nmPort.Value : 0;
                DB.Save();
            }

        }
        private void btnrefreshport_Click(object sender, EventArgs e)
        {
            cmbPort.Items.Clear();
            cmbPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            if (cmbPort.Items.Count > 0) cmbPort.SelectedIndex = 0;
        }
        private void cmbConType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((ConType)cmbConType.SelectedItem)
            {
                case ConType.TCP:
                    gBoxSerial.Enabled = false;
                    gBoxTCP.Enabled = true;
                    break;
                case ConType.Serial:
                    gBoxTCP.Enabled = false;
                    gBoxSerial.Enabled = true;

                    break;
                default:
                    break;
            }
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            if (Cihaz == null) return;
            if (MessageBox.Show(string.Format("{0}\nSilmek istediğinize eminmisiniz?", Cihaz.DeviceName), "Sil", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                DB.Cihazlar.Remove(Cihaz);
            }
            DB.Save();

        }
        private void dgCihazlar_SelectionChanged(object sender, EventArgs e)
        {
            if (dgCihazlar.RowCount > 0 & dgCihazlar.CurrentRow != null)
            {

                Cihaz = (Device)dgCihazlar.CurrentRow.DataBoundItem;
            }
        }

        private void dgCihazlar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine(e.ColumnIndex);
            if (e.ColumnIndex == dgCihazlar.ColumnCount - 1 & e.RowIndex >= 0)
            {
                Adresleri frm = new LGPLC.Adresleri(Cihaz);
                frm.ShowDialog();
            }
        }

        private void dgCihazlar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
