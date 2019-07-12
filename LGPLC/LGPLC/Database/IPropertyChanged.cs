using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LGPLC.Database
{
    public class IPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string p)
        {
            //string PropName = MethodBase.GetCurrentMethod().Name.Substring(4);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));        
        }

        
    }
}
