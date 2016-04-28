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

        public DeviceDetailsPage()
        {
            InitializeComponent();

            textBlock_ipAddress.Text = selectedDevice.deviceIP;
            textBlock_macAddress.Text = selectedDevice.deviceMAC;
            textBlock_deviceTitle.Text = selectedDevice.deviceName;
            textBlock_wmiAvailable.Text = selectedDevice.wmiManageableText;

            refreshDeviceDetails();

            if (!(App.Current as App).freshScan)
            {
                button_manage.Visibility = Visibility.Collapsed;
            }
        }

        private void refreshDeviceDetails()
        {
            textBlock_manufacturer.Text = selectedDevice.deviceManufacturer;
            textBlock_opsys.Text = selectedDevice.deviceOS;
            textBlock_pcname.Text = selectedDevice.devicePCName;
            textBlock_devmodel.Text = selectedDevice.deviceModel;
            textBlock_currentuser.Text = selectedDevice.deviceUser;
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
            Properties.Settings.Default.Save();
        }

        private void button_manage_wmi_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.wmiIPScan = selectedDevice.deviceIP;
            Properties.Settings.Default.Save();

            WMILoginWindow loginWindow = new WMILoginWindow();

            Nullable<bool> dialogResult = loginWindow.ShowDialog();

            if((bool)dialogResult)
            {
                refreshDeviceDetails();
                textBlock_deviceTitle.Text = selectedDevice.devicePCName;
            }
        }
    }
}
