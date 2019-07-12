using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LGPLC.Database
{
    public class Datapoint : IPropertyChanged
    {

        private string label;
        private int adres;
        private DataType type;
        private int deviceid;
        private object _value;
        public int id { get; set; }
        public string Label
        {
            get { return label; }
            set { label = value; NotifyPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4)); }
        }
        public int Address
        {
            get { return adres; }
            set { adres = value; NotifyPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4)); }
        }
        public DataType DataType
        {
            get { return type; }
            set { type = value; NotifyPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4)); }
        }
        public int DeviceID
        {
            get { return deviceid; }
            set { deviceid = value; NotifyPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4)); }
        }
        public object Value
        {
            get { return _value; }
            set { _value = value; NotifyPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4)); }
        }
    
        public static string Path => Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + DB.Folder  + typeof(Datapoint).Name + ".json";
        [JsonIgnore]
        public Device Device => DB.Cihazlar.FirstOrDefault(x => x.id == DeviceID);



    }
    public enum DataType
    {
        Bool,
        Word,
        DoubleWord,
        RaporBiti
    }



}

