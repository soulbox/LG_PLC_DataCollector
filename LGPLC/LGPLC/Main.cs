using EasyModbus;
using LGPLC.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LGPLC
{

    public partial class Main : FormHelper
    {
        public Device Cihaz { get; set; }
        public bool isReporting { get; set; }
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            DgCihaz.SelectionChanged += DgCihaz_SelectionChanged;
            DgCihaz.CellClick += DgCihaz_CellClick;
            DgCihaz.DataSource = DB.Cihazlar;
            DgCihaz.Columns["id"].Visible = false;
            DgCihaz.Columns["Modbus"].Visible = false;
            DgCihaz.Columns["Timeout"].Visible = false;
            DgCihaz.Columns["Timer"].Visible = false;
            DgCihaz.Columns["IP"].Visible = false;
            DgCihaz.Columns["Port"].Visible = false;
            DgCihaz.Columns["Baudrate"].Visible = false;
            DgCihaz.Columns["Parity"].Visible = false;
            DgCihaz.Columns["Stopbits"].Visible = false;
            DgCihaz.Columns["id"].Visible = false;
            DgCihaz.Columns["Datapoints"].Visible = false;
            DgCihaz.Columns["DeviceAddr"].Visible = false;
            DgCihaz.Columns["SerialPort"].Visible = false;

            DgCihaz.Columns["Reporting"].HeaderText = "Rapor Biti/Adresi";
            DgCihaz.Columns["interval"].HeaderText = "Raporlama Aralığı(dk)";
            DgCihaz.Columns["DeviceName"].HeaderText = "Cihaz Adı";
            DgCihaz.Columns["ConType"].HeaderText = "Bağlantı Tipi";
            DgCihaz.Columns.Add(new DataGridViewButtonColumn()
            {
                HeaderText = "Rapor",
                Text = "Rapor Görüntüle",
                UseColumnTextForButtonValue = true,
            });





        }

        private void DgCihaz_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 & e.ColumnIndex == DgCihaz.ColumnCount - 1)
            {
                //Console.WriteLine(e.ColumnIndex);

                Rapor frm = new Rapor(Cihaz);
                frm.ShowDialog();

            }
        }

        private void DgCihaz_SelectionChanged(object sender, EventArgs e)
        {
            if (DgCihaz.RowCount > 0 & DgCihaz.CurrentRow != null)
            {
                Cihaz = (Device)DgCihaz.CurrentRow.DataBoundItem;
            }
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (isReporting)
            {
                isReporting = !isReporting;
                btnStart.Text = "Raporlamayı Başlat";
                btnStart.BackColor = Color.Empty;
                btnStart.ForeColor = Color.Black;
            }
            else
            {
                isReporting = !isReporting;
                btnStart.Text = "Raporlamayı Durdur";
                btnStart.BackColor = Color.LightSeaGreen;
                btnStart.ForeColor = Color.WhiteSmoke;


            }
            //Task.Factory.StartNew(()=>Procces());
            Procces2();

        }
        void Procces()
        {
            Device pcihaz;
            List<Device> ProccesCihazlar = new List<Device>(DB.Cihazlar.ToList());
            if (isReporting)
            {
                ProccesCihazlar.ForEach(x =>
                {

                    //x.Timer.Interval = x.Interval * 1000 * 60;
                    x.Timer.Interval = 1000;
                    x.Modbus.ConnectionTimeout = 1000;
                    pcihaz = x;
                    int con = 0;
                    x.Timer.Elapsed += (i, o) =>
                    {

                        //if (o.SignalTime.Second==)
                        //x.Timer.Stop();
                        //try
                        //{
                        var StartAdr = x.DataPoints.FirstOrDefault(a => a.DataType == DataType.RaporBiti);
                        if (StartAdr == null)
                        {
                            x.Timer.Stop();
                            MessageBox.Show("Rapor Başlama Biti/Adresi Ayarlanmamış!");
                            return;
                        }
                        if (!x.Modbus.Connected)
                        {
                            Console.WriteLine("Bağlanıyor...");
                            x.Modbus.Connect();
                            Console.WriteLine("Bağlandı:{0}", con);
                            con++;

                        }
                        //int Ccount = 0;
                        //while (x.Modbus.Connected)
                        //{
                        //    bool StartBits = x.Modbus.ReadCoils(StartAdr.Address, 1).FirstOrDefault();
                        //    Console.WriteLine("Bağlanıyor...:{0} StartBit:{1}", Ccount,StartBits);
                        //    Ccount++;
                        //}
                        bool StartBit = true;
                        //bool StartBit = x.Modbus.ReadCoils(StartAdr.Address, 1).FirstOrDefault();
                        if (StartAdr != null && StartBit)
                        {
                            int sorgu = 0;
                            x.DataPoints
                            .Where(a => a.DataType != DataType.RaporBiti).ToList()
                            .ForEach(a =>
                               {
                                   //this.InvokeIfRequired(() =>
                                   //{
                                   //x.Reporting = true;
                                   var gelendeğer = getData(a.DataType, x.Modbus, a.Address);
                                   a.Value = gelendeğer;
                                   DataValue ekle = new DataValue()
                                   {
                                       DatapointID = a.id,
                                       Date = o.SignalTime,
                                       id = DB.DataValues.Count + 1
                                   };
                                   if (gelendeğer == null)
                                   {
                                       ekle.Status = DataValue.StatusType.ConError;
                                       ekle.Value = 0;
                                   }
                                   else
                                   {
                                       ekle.Status = DataValue.StatusType.Ok;
                                       ekle.Value = (int)gelendeğer;
                                   }
                                   DB.DataValues.Add(ekle);
                                   //});
                                   sorgu++;
                               });
                            Console.WriteLine("Toplam Sorgu:{0}", sorgu);
                        }
                        else
                        {
                            //Kaldır
                            //x.DataPoints
                            //.Where(a => a.DataType != DataType.RaporBiti).ToList().ForEach(a =>
                            //   {
                            //       this.InvokeIfRequired(() =>
                            //       {
                            //           x.Reporting = false;
                            //           DB.DataValues.Add(new DataValue()
                            //           {
                            //               DatapointID = a.id,
                            //               Date = o.SignalTime,
                            //               id = DB.DataValues.Count + 1,
                            //               Status = DataValue.StatusType.Ok,
                            //               Value = getData(a.DataType, x.Modbus, a.Address)
                            //           });
                            //       });
                            //   });
                            //kaldır son
                        }
                        x.Modbus.Disconnect();
                        DB.SaveDatavalue();
                        Console.WriteLine("\nCount:{0}", DB.DataValues.Count);
                        //}
                        //catch (EasyModbus.Exceptions.ConnectionException ex)
                        //{
                        //    Console.WriteLine(ex.ToString());
                        //    throw new Exception(string.Format("Cihaz:{0}\nBağlantı Hatası!", pcihaz.DeviceName));
                        //}
                        //catch (EasyModbus.Exceptions.SerialPortNotOpenedException ex)
                        //{
                        //    Console.WriteLine(ex.ToString());
                        //    throw new Exception(string.Format("Cihaz:{0}\n{1}", pcihaz.DeviceName, ex.ToString()));
                        //}
                        //catch (System.IO.IOException ex)
                        //{
                        //    //timeout
                        //    Console.WriteLine(ex);
                        //}
                        //catch (Exception ex)
                        //{
                        //    Console.WriteLine(ex);
                        //    throw;
                        //}

                    };
                    x.Timer.Enabled = true;
                    //x.Timer.
                    x.Timer.Start();
                });
            }
            else
                DB.Cihazlar.ToList().ForEach(x =>
                {
                    x.Timer.Stop();
                    x.Modbus.Disconnect();
                });
        }
        bool Addedevent = false;
        void Procces2()
        {
            List<Device> ProccesCihazlar = new List<Device>(DB.Cihazlar.ToList());
            ProccesCihazlar.ForEach(Cihaz =>
            {
                Cihaz.VerileriOku(isReporting, this);

                if (!Addedevent)
                {
                    Cihaz.Cycle += (obj, cycletime) =>
                     {
                         //Console.WriteLine($"Devicename:[{obj.DeviceName}]");
                         this.InvokeIfRequired(() =>
                         {

                             switch (obj.DeviceName)
                             {
                                 case "LC":
                                     label1.Text = $"{obj.DeviceName} Cycle:{cycletime}ms";
                                     break;
                                 case "TCPLC":
                                     label2.Text = $"{obj.DeviceName} Cycle:{cycletime}ms";
                                     break;
                                 default:
                                     break;
                             }

                         });
                     };
                    Cihaz.StopsReads += (obj, exception) =>
                     {
                         textBox1.StatusShow($"Cihaz:{obj.DeviceName}\nRaporlama Durduruldu!");
                         //MessageBox.Show($"Cihaz:{obj.DeviceName}\nRaporlama Durduruldu!");

                     };
                    Cihaz.ConException += (obj, ex) =>
                   {
                       textBox1.StatusShow(ex);
                   };
                    Cihaz.ReportBitStatus += (obj, status) =>
                    {
                        textBox1.StatusShow(string.Format("Cihaz :{0} Raporlama Durumu:{1:Yapılıyor;1;Yapılmıyor!}", obj.DeviceName, status.GetHashCode()));
                    };
                    Cihaz.Timer.Elapsed += (obj, e) =>
                    {
                        DateTime zaman = e.SignalTime;
                        if (isReporting & Cihaz.Reporting)
                        {
                            Cihaz.DataPoints.Where(a => a.DataType != DataType.RaporBiti).ToList()
                            .ForEach(Point =>
                            {
                                this.InvokeIfRequired(() =>
                                {
                                    DB.DataValues.Add(new DataValue()
                                    {
                                        DatapointID = Point.id,
                                        id = DB.DataValues.Count + 1,
                                        Date = zaman,
                                        Status = DataValue.StatusType.Ok,
                                        Value = Convert.ToInt32(Point.Value)
                                    });
                                });
                            });

                            DB.SaveDatavalue();
                            string str = string.Format("{0} Raporlandı!", Cihaz.DeviceName);
                            textBox1.StatusShow(str);                     

                        }
                    };
                }
                if (isReporting)
                {
                    Cihaz.Timer.Interval = Cihaz.Interval * 1000 * 60;
                    Cihaz.Timer.Start();
                }
                else
                {
                    Cihaz.Timer.Stop();
                }

            });
            Addedevent = true;
        }



        private void RemoveElapsed(System.Timers.Timer b)
        {


            FieldInfo f1 = typeof(System.Timers.Timer).GetField("Elapsed",
                BindingFlags.Static | BindingFlags.NonPublic);
            object obj = f1.GetValue(b);
            PropertyInfo pi = b.GetType().GetProperty("Events",
                BindingFlags.NonPublic | BindingFlags.Instance);
            EventHandlerList list = (EventHandlerList)pi.GetValue(b, null);
            list.RemoveHandler(obj, list[obj]);
        }




        int? getData(DataType type, ModbusClient modbus, int adres)
        {
            int? dön = null;
            try
            {
                switch (type)
                {
                    case DataType.Bool:
                        dön = Convert.ToInt32(modbus.ReadCoils(adres, 1).FirstOrDefault());
                        break;
                    case DataType.Word:
                        dön = modbus.ReadHoldingRegisters(adres, 1).FirstOrDefault();
                        break;
                    case DataType.DoubleWord:
                        dön = ModbusClient.ConvertRegistersToInt(modbus.ReadHoldingRegisters(adres, 2));
                        break;
                    case DataType.RaporBiti:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }

            return dön;
        }

        private void btnAyarlar_Click(object sender, EventArgs e)
        {
            Ayarlar frm = new Ayarlar();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
    }


}
