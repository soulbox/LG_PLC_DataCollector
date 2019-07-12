using EasyModbus;
using EasyModbus.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LGPLC.Database
{

    public class Device : IPropertyChanged
    {
        public delegate void CycleDeviceEventHandler(Device obj, double CycleTime);
        public event CycleDeviceEventHandler Cycle;
        public delegate void DataReadException(Device obj, string exception);
        public delegate void ConExceptionEventHandler(Device obj, string exception);
        public delegate void ReportBitStatusEventHandler(Device obj,Boolean status);
        public event ReportBitStatusEventHandler ReportBitStatus;
        public event ConExceptionEventHandler ConException;
        public event DataReadException StopsReads;
        public static bool isCollectData = false;
        ModbusClient modbus;
        public Device()
        {
            //Timer.Elapsed += Timer_Elapsed;
        }

        public void TimerStart() { }
        public void TimerStop() { }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //DateTime zaman = e.SignalTime;
            //if (isReporting)
            //{
            //    ProccesCihazlar.ForEach(Cihaz =>
            //    {
            //        Cihaz.DataPoints.Where(a => a.DataType != DataType.RaporBiti).ToList()
            //        .ForEach(Point =>
            //        {
            //            this.InvokeIfRequired(() =>
            //            {
            //                DB.DataValues.Add(new DataValue()
            //                {
            //                    DatapointID = Point.id,
            //                    id = DB.DataValues.Count + 1,
            //                    Date = zaman,
            //                    Status = DataValue.StatusType.Ok,
            //                    Value = Convert.ToInt32(Point.Value)
            //                });
            //            });
            //        });
            //    });
            //    DB.SaveDatavalue();
            //    Console.WriteLine("{0} =>Added Values", zaman);

            //}
        }

        [JsonIgnore]
        public ModbusClient Modbus => modbus = contype == ConType.Serial ? modbus ?? new ModbusClient(SerialPort)
        {
            Baudrate = Baudrate,
            Parity = Parity,
            StopBits = Stopbits,
            UnitIdentifier = (byte)DeviceAddr,
            ConnectionTimeout = Timeout,
        } : modbus ?? new ModbusClient(IP, Port) { ConnectionTimeout = Timeout };
        Task görev;
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        CancellationToken ct;
        public void VerileriOku(bool isReporting, Control invokeControl = null)
        {

            ct = tokenSource.Token;
            if (ct.IsCancellationRequested) { tokenSource = new CancellationTokenSource(); ct = tokenSource.Token; }
            if (isReporting)
            {
                görev = Task.Factory.StartNew(() =>
                {
                    ct.ThrowIfCancellationRequested();
                    try
                    {
                        if (!Modbus.Connected) Modbus.Connect();

                    }
                    catch (ConnectionException ex)
                    {
                        string msg = string.Format("Cihaz:{0}\nBağlanılamadı Ayarlarınızı Kontrol edin! ", DeviceName);
                        if (ConException == null)
                            MessageBox.Show(msg);
                        else
                            ConException?.Invoke(this, msg);
                        return;
                    }
                    catch (Exception ex)
                    {
                        string msg = string.Format("Cihaz:{0}\n{1}", DeviceName, ex.ToString());
                        if (ConException == null)
                            MessageBox.Show(msg);
                        else
                            ConException?.Invoke(this, msg);
                        return;
                    }
                    var StartDatapoint = DataPoints.FirstOrDefault(x => x.DataType == DataType.RaporBiti);                   
                    if (StartDatapoint == null)
                    {
                        MessageBox.Show(string.Format("Cihaz:{0}\nRapor Başlama Biti/Adresi Ayarlanmamış!", DeviceName));
                        return;
                    }
                    Stopwatch sw = new Stopwatch();
                    List<Datapoint> Pointler = new List<Datapoint>(DataPoints.Where(x => x.DataType != DataType.RaporBiti).ToList());
                    while (Modbus.Connected)
                    {
                        sw.Start();
                        if (ct.IsCancellationRequested)
                        {
                            try
                            {
                                ct.ThrowIfCancellationRequested();

                            }
                            catch (OperationCanceledException ex)
                            {
                                StopsReads?.Invoke(this, ex.ToString());
                                invokeControl.InvokeIfRequired(() => Reporting = false);
                                ReportBitStatus?.Invoke(this, Reporting);
                                break;
                            }
                        }
                        try
                        {
                            var reportbit = Modbus.ReadCoils(StartDatapoint.Address, 1).FirstOrDefault();
                            invokeControl.InvokeIfRequired(() =>
                            {
                                if (reportbit != Reporting)
                                {
                                    Reporting = reportbit;
                                    ReportBitStatus?.Invoke(this, Reporting);

                                }
                            });

                        }
                        catch (Exception ex )
                        {

                            string msg = string.Format("Cihaz:{0}\nAdres:{1}\nOkuma Hatası!.\n{2}", DeviceName, StartDatapoint.Address, ex.ToString());
                            if (ConException == null)
                                MessageBox.Show(msg);
                            else
                                ConException?.Invoke(this, msg);
                            return;
                        }


                        Pointler.ForEach(Point =>
                            {

                                switch (Point.DataType)
                                {
                                    case DataType.Bool:
                                        Point.Value = modbus.ReadCoils(Point.Address, 1).FirstOrDefault();
                                        break;
                                    case DataType.Word:
                                        Point.Value = modbus.ReadHoldingRegisters(Point.Address, 1).FirstOrDefault();
                                        break;
                                    case DataType.DoubleWord:
                                        Point.Value = ModbusClient.ConvertRegistersToInt(modbus.ReadHoldingRegisters(Point.Address, 2));
                                        break;
                                    case DataType.RaporBiti:
                                        break;
                                    default:
                                        break;
                                }
                            });
                        sw.Stop();
                        Cycle?.Invoke(this, sw.Elapsed.TotalMilliseconds);
                        //Console.WriteLine("\nCycle Time:{0}ms", sw.Elapsed.TotalMilliseconds);
                        sw.Reset();
                    }


                }, tokenSource.Token);
                //if (görev.Status != TaskStatus.Running) 
                //    görev.Start();

            }
            else
            {
                if (görev.Status == TaskStatus.Running)
                    tokenSource.Cancel();
            }

        }



        public int id { get; set; }

        private string devicename;
        public string DeviceName
        {
            get { return devicename; }
            set { devicename = value; NotifyPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4)); }
        }
        private ConType contype;
        public ConType ConType
        {
            get { return contype; }
            set { contype = value; NotifyPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4)); }
        }

        private int timeout;

        public int Timeout
        {
            get { return timeout; }
            set
            {
                timeout = value; NotifyPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                if (modbus != null)
                    modbus.ConnectionTimeout = value;
            }
        }

        private int interval;

        public int Interval
        {
            get { return interval; }
            set { interval = value; NotifyPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4)); }
        }

        private string ip;

        public string IP
        {
            get { return ip; }
            set
            {
                ip = value;
                NotifyPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                if (modbus != null)
                    modbus.IPAddress = value;

            }
        }
        private int port;

        public int Port
        {
            get { return port; }
            set
            {
                port = value; NotifyPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                if (modbus != null)
                    modbus.Port = value;

            }
        }

        private string serialport;
        public string SerialPort
        {
            get { return serialport; }
            set
            {
                serialport = value; NotifyPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                if (modbus != null & ConType == ConType.Serial)
                    modbus.SerialPort = value;

            }
        }

        private int deviceadr;

        public int DeviceAddr
        {
            get { return deviceadr; }
            set
            {
                deviceadr = value; NotifyPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                if (modbus != null & ConType == ConType.Serial)
                    modbus.UnitIdentifier = (byte)value;
            }
        }

        private int baudrate;
        public int Baudrate
        {
            get { return baudrate; }
            set
            {
                baudrate = value; NotifyPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                if (modbus != null & ConType == ConType.Serial)
                    modbus.Baudrate = value;


            }
        }
        private System.IO.Ports.Parity parity;

        public System.IO.Ports.Parity Parity
        {
            get { return parity; }
            set
            {
                parity = value; NotifyPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                if (modbus != null & ConType == ConType.Serial)
                    modbus.Parity = value;
            }
        }

        private System.IO.Ports.StopBits stopbits;

        public System.IO.Ports.StopBits Stopbits
        {
            get { return stopbits; }
            set
            {
                stopbits = value;
                NotifyPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                if (modbus != null & ConType == ConType.Serial)
                    modbus.StopBits = value;

            }
        }

        public IEnumerable<Datapoint> DataPoints => DB.DataPointler.Where(x => x.DeviceID == id);
        public static string Path => Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + DB.Folder + typeof(Device).Name + ".json";

        System.Timers.Timer timer;
        [JsonIgnore]
        public System.Timers.Timer Timer => timer = timer ?? new System.Timers.Timer() { Enabled = false };

        private bool reporting;
        [JsonIgnore]
        public bool Reporting { get { return reporting; } set { reporting = value; NotifyPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4)); } }

    }
    public enum ConType
    {
        TCP,
        Serial
    }

}
