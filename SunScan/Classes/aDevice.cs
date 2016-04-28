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
        private string _deviceName;
        private string _deviceMAC;
        private string _deviceIP;
        private string _devicePCName;

        private int _dataUsed;

        private bool _smnpManageable;
        private bool _wmiManageable;

        private string _wmiManageableText;
        private string _wmiManageableColor;

        public event PropertyChangedEventHandler PropertyChanged;

        public aDevice()
        {
            _deviceName = "Device";
            _deviceMAC = "No MAC Address Available";
            _deviceIP = "No IP Address Available";

            _smnpManageable = false;
            _wmiManageable = false;

            _wmiManageableText = "unavailable";
            _wmiManageableColor = "#FFFFFFFF";
        }

        public aDevice(string name, string mac, string ip)
        {
            _deviceName = name;
            _deviceMAC = mac;
            _deviceIP = ip;

            _smnpManageable = false;
            _wmiManageable = false;
        }

        public aDevice(string name, string mac, string ip, bool manageable)
        {
            _deviceName = name;
            _deviceMAC = mac;
            _deviceIP = ip;

            _smnpManageable = false;
            _wmiManageable = manageable;

            if(manageable)
            {
                _wmiManageableText = "Available";
                _wmiManageableColor = "#FFea8535";
            }
            else
            {
                _wmiManageableText = "unavailable";
                _wmiManageableColor = "#FFFFFFFF";
            }
        }

        public string deviceName
        {
            get
            {
                return _deviceName;
            }
            set
            {
                _deviceName = value;
            }
        }

        public string deviceIP
        {
            get
            {
                return _deviceIP;
            }
        }

        public string deviceMAC
        {
            get
            {
                return _deviceMAC;
            }
        }

        public bool wmiManageable
        {
            get
            {
                return _wmiManageable;
            }
            set
            {
                _wmiManageable = value;
            }
        }

        public string wmiManageableText
        {
            get
            {
                return _wmiManageableText;
            }
            set
            {
                _wmiManageableText = value;
            }
        }

        public string wmiManageableColor
        {
            get
            {
                return _wmiManageableColor;
            }
            set
            {
                _wmiManageableColor = value;
            }
        }

        public bool smnpManageable
        {
            get
            {
                return _smnpManageable;
            }
            set
            {
                _smnpManageable = value;
            }
        }

        public string devicePCName
        {
            get
            {
                return _devicePCName;
            }
            set
            {
                _devicePCName = value;
            }
        }

        public int dataUsed
        {
            get
            {
                return _dataUsed;
            }
            set
            {
                _dataUsed = value;
            }
        }
    }
}
