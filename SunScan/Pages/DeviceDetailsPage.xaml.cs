using SunScan.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SunScan.Pages
{
    /// <summary>
    /// Interaction logic for DeviceDetailsPage.xaml
    /// </summary>
    public partial class DeviceDetailsPage : Page
    {
        aDevice selectedDevice = (App.Current as App).selectedDevice;
        /*
        ConnectionOptions options =
                 new ConnectionOptions();

        ManagementObjectSearcher managementSearch;*/

        public DeviceDetailsPage()
        {
            InitializeComponent();

            /*
            options.Username = "James";
            options.Password = "";
            //options.Authority = "ntdlmdomain:WORKGROUP";

            //string s = "\\\\" + selectedDevice.deviceIP + "\\root\\cimv2";

            ManagementScope scope =
               new ManagementScope(s, options);
             scope.Connect();

            //           managementSearch = new ManagementObjectSearcher(s, "SELECT * FROM  Win32_ComputerSystem"); //Where it says "\\\\localhost\\root\\...", replace this with an IP address or computer name
            //           ManagementObjectCollection queryCollection;

            */

            textBlock_ipAddress.Text = selectedDevice.deviceIP;
            textBlock_macAddress.Text = selectedDevice.deviceMAC;
            textBlock_deviceTitle.Text = selectedDevice.deviceName;
            textBlock_wmiAvailable.Text = selectedDevice.wmiManageableText;

            if(selectedDevice.wmiManageable)
            {
                //queryCollection = managementSearch.Get();

                /*foreach (ManagementObject m in queryCollection)
                {
                    // Display the remote computer information
                    textBlock_opsys.Text = m["Manufacturer"].ToString();
                    textBlock_pcname.Text = m["Name"].ToString();
                }*/
            }

            Color wmiColor = (Color)ColorConverter.ConvertFromString(selectedDevice.wmiManageableColor);
            textBlock_wmiAvailable.Foreground = new SolidColorBrush(wmiColor);

            if (!(App.Current as App).freshScan)
            {
                button_manage.Visibility = Visibility.Collapsed;
            }
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void button_manage_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://" + (App.Current as App).scanGateway);
        }

        private void button_favorites_Click(object sender, RoutedEventArgs e)
        {
            // This does not work properly! Fix this before release. Needs to be multiple lists under the settings file.
            if(Properties.Settings.Default.favoriteIP == null)
            {
                Properties.Settings.Default.favoriteIP = new List<string>();
            }

            if (Properties.Settings.Default.favoriteMAC == null)
            {
                Properties.Settings.Default.favoriteMAC = new List<string>();
            }

            if (Properties.Settings.Default.favoriteManufacturer == null)
            {
                Properties.Settings.Default.favoriteManufacturer = new List<string>();
            }

            if (Properties.Settings.Default.favoriteWMI == null)
            {
                Properties.Settings.Default.favoriteWMI = new List<string>();
            }

            Properties.Settings.Default.favoriteIP.Add(selectedDevice.deviceIP);
            Properties.Settings.Default.favoriteMAC.Add(selectedDevice.deviceMAC);
            Properties.Settings.Default.favoriteManufacturer.Add(selectedDevice.deviceName);
        }
    }
}
