using LGPLC.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LGPLC
{
    public partial class Adresleri : FormHelper
    {
        Device Cihaz;
        public Datapoint Point { get; set; }
        public Adresleri(Device cihaz)
        {
            Cihaz = cihaz;
            InitializeComponent();
        }

        private void Adresleri_Load(object sender, EventArgs e)
        {
            dgAyarlar.DataSource = Cihaz.DataPoints.ToList();

            dgAyarlar.Columns["id"].Visible = false;
            //dgAyarlar.Columns["Value"].Visible = false;
            dgAyarlar.Columns["Device"].Visible = false;
            dgAyarlar.Columns["Label"].HeaderText = "Açıklama";

            cmbDatatype.DataSource = Enum.GetValues(typeof(DataType));


        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if ((DataType)cmbDatatype.SelectedItem == DataType.RaporBiti & Cihaz.DataPoints.Any(x => x.DeviceID == Cihaz.id & x.DataType == DataType.RaporBiti))
            {
                MessageBox.Show("Raporlama Adresi Zaten Kayıtlı!");
                return;
            }
            if (Cihaz.DataPoints.Any(x => x.DeviceID == Cihaz.id & x.DataType == (DataType)cmbDatatype.SelectedItem & x.Address == (int)nmAdress.Value))
            {
                MessageBox.Show("Bu Adres zaten var!");
                return;
            }

            DB.DataPointler.Add(new Datapoint()
            {
                id = DB.DataPointler.Count + 1,
                Address = (int)nmAdress.Value,
                DataType = (DataType)cmbDatatype.SelectedItem,
                DeviceID = Cihaz.id,
                Label = txtLabel.Text

            });
            DB.Save();
            dgAyarlar.DataSource = Cihaz.DataPoints.ToList();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (Point == null) return;
            if (MessageBox.Show(string.Format("{0}\nSilmek istediğinize eminmisiniz?", Point.Label), "Sil", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                DB.DataPointler.Remove(Point);
            }
            DB.Save();
            dgAyarlar.DataSource = Cihaz.DataPoints.ToList();
        }

        private void dgAyarlar_SelectionChanged(object sender, EventArgs e)
        {
            if (dgAyarlar.RowCount > 0 & dgAyarlar.CurrentRow != null)
            {

                Point = (Datapoint)dgAyarlar.CurrentRow.DataBoundItem;
            }
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            if (Point == null) return;
            if (MessageBox.Show(string.Format("{0}\nGüncellemek istediğinize eminmisiniz?", Point.Label), "Güncelle", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Point.Address = (int)nmAdress.Value;
                Point.DataType = (DataType)cmbDatatype.SelectedItem;
                Point.Label = txtLabel.Text;
                DB.Save();
                dgAyarlar.DataSource = Cihaz.DataPoints.ToList();
            }
        }
    }
}
