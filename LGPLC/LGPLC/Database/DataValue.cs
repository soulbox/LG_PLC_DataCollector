using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LGPLC.Database
{
    public class DataValue : IPropertyChanged
    {
        public int id { get; set; }

        private int datapointid;

        public int DatapointID
        {
            get { return datapointid; }
            set { datapointid = value; NotifyPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4)); }
        }

        private int _value;

        public int Value
        {
            get { return _value; }
            set { _value = value; NotifyPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4)); }
        }

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; NotifyPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4)); }
        }
        private StatusType status;

        public StatusType Status
        {
            get { return status; }
            set { status = value; NotifyPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4)); }
        }
        [JsonIgnore]
        public Datapoint Datapoint => DB.DataPointler.FirstOrDefault(x => x.id == DatapointID);
        public static string Path=> Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +DB.Folder  + typeof(DataValue).Name + ".json";
          
        public enum StatusType
        {
            Ok,
            ConError
        }
    }

}
