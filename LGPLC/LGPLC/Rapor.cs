using LGPLC.Database;
using System;
using System.Data;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace LGPLC
{
    public partial class Rapor : FormHelper
    {
        Device Cihaz;
        public Rapor(Device cihaz)
        {
            Cihaz = cihaz;
            InitializeComponent();
        }

        private void Rapor_Load(object sender, EventArgs e)
        {
            dtStart.Value = DateTime.Now.AddMonths(-1);
            DB.DataValues.Where(x => x == null).ToList().ForEach(x =>
            {
                DB.DataValues.Remove(x);
            });
            var liste = DB.DataValues
                .Where(x => x.Datapoint.DeviceID == Cihaz.id)
                .OrderByDescending(x => x.id)
                .Select(x => new { x.Datapoint.Label, x.Value, x.Status, x.Date })
                .Take(Cihaz.DataPoints.Count() - 1)
                .ToList();
            dgRapor.DataSource = DB.DataValues
                .Where(x => x.Datapoint.DeviceID == Cihaz.id)
                .OrderByDescending(x => x.id)
                .Select(x => new { x.Datapoint.Label, x.Value, x.Status, x.Date })
                .Take(Cihaz.DataPoints.Count() - 1)
                .ToList();
            dgRapor.Columns["Label"].HeaderText = "Açıklama";
            dgRapor.Columns["Value"].HeaderText = "Değeri";
            dgRapor.Columns["Date"].HeaderText = "Tarih";
            //dgRapor.Columns["Date"].DefaultCellStyle.Format = "dd.MM.yyyy hh:mm:ss";
            dgRapor.Columns["Status"].HeaderText = "Durum";
            lblToplam.Text = $"Toplam :{DB.DataValues.Where(x => x.Datapoint.DeviceID == Cihaz.id).Count()}";

        }

        private void dgRapor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tüm Kayıtlar Silinice Eminmisiniz?", "Sil", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                DB.DataValues.Clear();
                DB.Save();
                dgRapor.DataSource = DB.DataValues.OrderByDescending(x => x.id).ToList();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //dgRapor.DataSource = null;
            dgRapor.DataSource = DB.DataValues
    .Where(x => x.Datapoint.DeviceID == Cihaz.id)
    .OrderByDescending(x => x.id)
    .Select(x => new { x.Datapoint.Label, x.Value, x.Status, x.Date })
    .ToList();
        }

        private void btnRaporla_Click(object sender, EventArgs e)
        {
            //BackgroundWorker bc = new BackgroundWorker();
            //dgRapor.DataSource = MakeDataTable();
            //try
            //{
            Task.Factory.StartNew(() =>
            {
                this.InvokeIfRequired(() =>
                {
                    btnRaporla.Enabled = false;
                    string btntex = btnRaporla.Text;
                    btnRaporla.Text = "Alınıyor...";


                    SaveFileDialog fs = new SaveFileDialog()
                    {
                        OverwritePrompt = true,
                        Filter = "Rapor|*.pdf",
                        FileName = "Rapor.pdf"
                    };
                    if (fs.ShowDialog() == DialogResult.OK)
                    {
                        var table = MakeDataTable(dtStart.Value, dtFinish.Value);
                        string path = GetUniqueFilePath(fs.FileName);
                        //string path = fs.FileName;

                        ExportDataTableToPdf(table, path, "Rapor");
                        System.Diagnostics.Process.Start(path);
                        //this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                    };
                    btnRaporla.Enabled = true;
                    btnRaporla.Text = btntex;
                });

            });




            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.ToString());
            //}

        }
        const string TarihColumnName = "Tarih ve Saat";
        public static string GetUniqueFilePath(string filepath)
        {
            if (File.Exists(filepath))
            {
                string folder = Path.GetDirectoryName(filepath);
                string filename = Path.GetFileNameWithoutExtension(filepath);
                string extension = Path.GetExtension(filepath);
                int number = 1;

                Match regex = Regex.Match(filepath, @"(.+) \((\d+)\)\.\w+");

                if (regex.Success)
                {
                    filename = regex.Groups[1].Value;
                    number = int.Parse(regex.Groups[2].Value);
                }

                do
                {
                    number++;
                    filepath = Path.Combine(folder, string.Format("{0} ({1}){2}", filename, number, extension));
                }
                while (File.Exists(filepath));
            }

            return filepath;
        }
        DataTable MakeDataTable( DateTime StartDate, DateTime StopDate)
        {
            //Create friend table object
            DataTable Table = new DataTable();
            Table.Columns.Add("No");

            Cihaz.DataPoints.Where(x =>  x.DataType != DataType.RaporBiti).ToList()
                .ForEach(point =>
                {
                    Table.Columns.Add(point.Label);

                });
            Table.Columns.Add(TarihColumnName);

            var asdgf = DB.DataValues.Where(x => x.Datapoint.DeviceID == Cihaz.id & x.Date >= StartDate & x.Date <= StopDate).ToList();
            DB.DataValues
                .Where(x => x.Datapoint.DeviceID == Cihaz.id & x.Date >= StartDate & x.Date <= StopDate)
                .ToList().ForEach(Dvalue =>
                {

                    DataRow isDublicate = null;
                    foreach (DataRow row in Table.Rows)
                    {
                        string format = "dd/MM/yyyy HH:mm:ss";
                        if (Convert.ToDateTime(row.Field<string>(Table.Columns.Count - 1)).ToString(format) == Dvalue.Date.ToString(format))
                            isDublicate = row;
                    }
                    if (isDublicate != null)
                    {
                        //Console.WriteLine("Aynı Tarihli");
                        isDublicate.SetField(Dvalue.Datapoint.Label, Dvalue.Value);
                    }
                    else
                    {
                        object[] values = new object[Table.Columns.Count];
                        foreach (DataColumn column in Table.Columns)
                        {
                            int index = Table.Columns.IndexOf(column);
                            switch (column.ColumnName)
                            {
                                case "No":
                                    values[index] = Table.Rows.Count + 1;
                                    break;
                                case TarihColumnName:
                                    values[index] = Dvalue.Date;
                                    break;
                                default:
                                    values[index] = column.ColumnName == Dvalue.Datapoint.Label ? Dvalue.Value : 0;
                                    break;
                            }
                        }
                        Table.Rows.Add(values);
                    }
                });
            return Table;
        }
        void ExportDataTableToPdf(DataTable dtblTable, String strPdfPath, string strHeader)
        {
            System.IO.FileStream fs = new FileStream(strPdfPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            //fs.SetLength(0);
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();
                        
            //Report Header
            BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fntHead = new Font(bfntHead, 16, 1, BaseColor.GRAY);
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_CENTER;
            prgHeading.Add(new Chunk(strHeader.ToUpper(), fntHead));
            document.Add(prgHeading);

            //Author
            Paragraph prgAuthor = new Paragraph();
            BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntAuthor = new Font(btnAuthor, 8, 2, BaseColor.GRAY);
            prgAuthor.Alignment = Element.ALIGN_RIGHT;
            prgAuthor.Add(new Chunk("Yayımcı : Sur Otomasyon", fntAuthor));
            prgAuthor.Add(new Chunk("\nTarih : " + DateTime.Now.ToShortDateString(), fntAuthor));
            document.Add(prgAuthor);

            //Add a line seperation
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            document.Add(p);

            //Add line break
            document.Add(new Chunk("\n", fntHead));
            int[] tablewidth = new int[dtblTable.Columns.Count];
            //Write the table
            PdfPTable table = new PdfPTable(dtblTable.Columns.Count) { HeaderRows = 2 };
            for (int i = 0; i < dtblTable.Columns.Count; i++)
            {
                if (i == dtblTable.Columns.Count - 1)
                {
                    tablewidth[i] = 2;

                }
                else
                {
                    tablewidth[i] = 1;
                }
            }

            table.SetWidths(tablewidth);
            BaseFont btnColumnHeader = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntColumnHeader = new Font(btnColumnHeader, 10, 1, BaseColor.WHITE);
            for (int i = 0; i < dtblTable.Columns.Count; i++)
            {
                PdfPCell cell = new PdfPCell() { HorizontalAlignment = Element.ALIGN_CENTER };
                cell.BackgroundColor = BaseColor.GRAY;
                cell.AddElement(new Chunk(dtblTable.Columns[i].ColumnName.ToUpper(), fntColumnHeader));
                table.AddCell(cell);
            }


            for (int i = 0; i < dtblTable.Rows.Count; i++)
            {
                for (int j = 0; j < dtblTable.Columns.Count; j++)
                {

                    table.AddCell(dtblTable.Rows[i][j].ToString());

                }
            }

            document.Add(table);
            document.Close();
            writer.Close();
            fs.Close();
        }
    }
}
