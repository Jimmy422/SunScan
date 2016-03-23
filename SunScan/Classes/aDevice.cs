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

        private bool _smnpManageable;
        private bool _wmiManageable;

        public event PropertyChangedEventHandler PropertyChanged;

        public aDevice()
        {
            _deviceName = "Device";
            _deviceMAC = "No MAC Address Available";
            _deviceIP = "No IP Address Available";

            _smnpManageable = false;
            _wmiManageable = false;
        }

        public aDevice(string name, string mac, string ip)
        {
            _deviceName = name;
            _deviceMAC = mac;
            _deviceIP = ip;

            _smnpManageable = false;
            _wmiManageable = false;
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
    }
}
