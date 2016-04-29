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
        public string deviceType { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public aDevice()
        {
            deviceName = "Device";
            deviceMAC = "No MAC Address Available";
            deviceIP = "No IP Address Available";

            devicePCName = "Unknown Device Name";
            deviceManufacturer = "Unavailable";
            deviceModel = "Unavailable";
            deviceUser = "Unavailable";
            deviceOS = "Unavailable";
            deviceType = "Unavailable";

            wmiManageable = false;

            wmiManageableText = "Unavailable";
            wmiManageableColor = "#FFFFFFFF";
        }

        public aDevice(string name, string mac, string ip)
        {
            deviceName = name;
            deviceMAC = mac;
            deviceIP = ip;

            wmiManageable = false;

            devicePCName = "Unknown Device Name";
            deviceManufacturer = "Unavailable";
            deviceModel = "Unavailable";
            deviceUser = "Unavailable";
            deviceOS = "Unavailable";
            deviceType = "Unavailable";
        }

        public aDevice(string wmi, string ip, string mac, string manu, string pcmanu, string model, string name, string os, string type, string user)
        {
            deviceName = manu;
            deviceMAC = mac;
            deviceIP = ip;

            wmiManageableText = wmi;

            wmiManageable = false;

            deviceManufacturer = pcmanu;
            deviceModel = model;
            devicePCName = name;
            deviceOS = os;
            deviceType = type;
            deviceUser = user;
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
                wmiManageableText = "Unavailable";
                wmiManageableColor = "#FFFFFFFF";
            }

            devicePCName = "Unknown Device Name";
            deviceManufacturer = "Unavailable";
            deviceModel = "Unavailable";
            deviceUser = "Unavailable";
            deviceOS = "Unavailable";
            deviceType = "Unavailable";
        }

        public aDevice(string name, string pcName, string mac, string ip, bool manageable)
        {
            deviceName = name;
            devicePCName = pcName;
            deviceMAC = mac;
            deviceIP = ip;

            wmiManageable = manageable;

            if (manageable)
            {
                wmiManageableText = "Available";
                wmiManageableColor = "#FFea8535";
            }
            else
            {
                wmiManageableText = "Unavailable";
                wmiManageableColor = "#FFFFFFFF";
            }

            deviceManufacturer = "Unavailable";
            deviceModel = "Unavailable";
            deviceUser = "Unavailable";
            deviceOS = "Unavailable";
            deviceType = "Unavailable";
        }
    }
}
