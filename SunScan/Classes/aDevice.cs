using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunScan.Classes
{
    public class aDevice : INotifyPropertyChanged
    {
        public string deviceName { get; set; }
        public string deviceMAC { get; set; }
        public string deviceIP { get; set; }

        public bool wmiManageable { get; set; }
        public string wmiManageableText { get; set; }
        public string wmiManageableColor { get; set; }

        public string devicePCName { get; set; }
        public string deviceManufacturer { get; set; }
        public string deviceModel { get; set; }
        public string deviceUser { get; set; }
        public string deviceOS { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public aDevice()
        {
            deviceName = "Device";
            deviceMAC = "No MAC Address Available";
            deviceIP = "No IP Address Available";

            wmiManageable = false;

            wmiManageableText = "unavailable";
            wmiManageableColor = "#FFFFFFFF";
        }

        public aDevice(string name, string mac, string ip)
        {
            deviceName = name;
            deviceMAC = mac;
            deviceIP = ip;

            wmiManageable = false;
        }

        public aDevice(string name, string mac, string ip, bool manageable)
        {
            deviceName = name;
            deviceMAC = mac;
            deviceIP = ip;

            wmiManageable = manageable;

            if(manageable)
            {
                wmiManageableText = "Available";
                wmiManageableColor = "#FFea8535";
            }
            else
            {
                wmiManageableText = "unavailable";
                wmiManageableColor = "#FFFFFFFF";
            }
        }
    }
}
