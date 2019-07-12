using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace LGPLC.Database
{
    public static class DB
    {
        static BindingList<Device> cihazlar;
        static BindingList<Datapoint> points;
        static BindingList<DataValue> datavalues;


        public static BindingList<Device> Cihazlar
        {
            get
            {
                if (File.Exists(Device.Path))
                    return cihazlar = cihazlar ?? JsonConvert.DeserializeObject<BindingList<Device>>(File.ReadAllText(Device.Path));
                else
                    return cihazlar = cihazlar ?? new BindingList<Device>();
            }
        }

        public static BindingList<Datapoint> DataPointler
        {
            get
            {
                if (File.Exists(Datapoint.Path))
                    return points = points ?? JsonConvert.DeserializeObject<BindingList<Datapoint>>(File.ReadAllText(Datapoint.Path));
                else
                    return points = points ?? new BindingList<Datapoint>();
            }
        }

        public static BindingList<DataValue> DataValues
        {
            get
            {
                if (File.Exists(DataValue.Path))
                    return datavalues = datavalues ?? JsonConvert.DeserializeObject<BindingList<DataValue>>(File.ReadAllText(DataValue.Path)) ?? new BindingList<DataValue>();
                else
                    return datavalues = datavalues ?? new BindingList<DataValue>();
            }
        }

      public static   string Folder = "\\LGPLC\\";
        static void CheckPath()
        {
            System.IO.FileInfo file = new System.IO.FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + Folder);
            file.Directory.Create();
        }
        public static void Save()
        {
            CheckPath();
            File.WriteAllText(Device.Path, JsonConvert.SerializeObject(Cihazlar, Formatting.None));
            File.WriteAllText(Datapoint.Path, JsonConvert.SerializeObject(DataPointler, Formatting.None));
            //File.WriteAllText(DataValue.Path, JsonConvert.SerializeObject(DataValues, Formatting.Indented));


        }
        public static void SaveDatavalue()
        {
            try
            {
                CheckPath();
                File.WriteAllText(DataValue.Path, JsonConvert.SerializeObject(DataValues, Formatting.None));

            }
            catch (System.Exception)
            {

            }

        }
    }
}
