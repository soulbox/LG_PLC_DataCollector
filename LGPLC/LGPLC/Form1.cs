using EasyModbus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LGPLC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int[] Gelenler;
        BindingList<string> Coils = new BindingList<string>();
        private void button1_Click(object sender, EventArgs e)
        {

            EasyModbus.ModbusClient sdast = new ModbusClient();            
            ModbusClient modbus = new ModbusClient()
            {
                SerialPort="COM4",
                Baudrate = 9600,
                Parity = System.IO.Ports.Parity.None,
                StopBits = System.IO.Ports.StopBits.One,
                UnitIdentifier = 1    
            };
            modbus.Connect();
            Stopwatch sw = new Stopwatch();
            
            Task.Factory.StartNew(() => 
            {
                while (modbus.Connected)
                {
                    sw.Start();
                    //coils = modbus.ReadCoils(1, 1);
                    Gelenler = modbus.ReadHoldingRegisters(0,70);
                    //var sad = modbus.ReadCoils(0, 100);
                    if (Coils.Count == 0)
                    {
                        int index = 0;
                        modbus.ReadCoils(0, 256).ToList().ForEach(x => 
                        {
                            Coils.Add(index.ToString("{M}-000") + "=>"+ string.Format("{0:TRUE;0;FALSE}",x.GetHashCode()));
                            index++;
                        });
                    }

                       
                    this.InvokeIfRequired(() => 
                    {

                        label1.Text = string.Join("\n", Gelenler);
                        listBox1.DataSource = Coils;

                    });
                }
            });

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
