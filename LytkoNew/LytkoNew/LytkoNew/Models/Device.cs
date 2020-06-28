using System.ComponentModel;
using Xamarin.Forms;

namespace LytkoNew.Models
{
    public class DeviceModel : INotifyPropertyChanged
    {
        private string currentTemp = "-1", targetTemp = "-1", zoneId = "0";
        Color hBtnColor = Color.FromHex("#282828");

        public string Ip { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public string ZoneId
        {
            get { return zoneId; }
            set
            {
                if (zoneId != value)
                {
                    zoneId = value;
                    OnPropertyChanged("ZoneId");
                }
            }
        }
        public string CurrentTemp 
        {
            get { return currentTemp; }
            set
            {
                if (currentTemp != value)
                {
                    currentTemp = value;
                    OnPropertyChanged("CurrentTemp");
                }
            }
        }
        public string TargetTemp
        {
            get { return targetTemp; }
            set
            {
                if (targetTemp != value)
                {
                    targetTemp = value;
                    OnPropertyChanged("TargetTemp");
                }
            }
        }
        public Color HBtnState
        {
            get { return hBtnColor; }
            set
            {
                if (hBtnColor != value)
                {
                    hBtnColor = value;
                    OnPropertyChanged("HBtnState");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class DeviceList : INotifyPropertyChanged
    {
        public string[] AllId { get; set; }

        private int count;
        public int Count {
            get { return count; }
            set {
                if (count != value)
                {
                    count = value;
                    OnPropertyChanged("Count");
                }
            }
        }

        public DeviceModel devicePerId { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
