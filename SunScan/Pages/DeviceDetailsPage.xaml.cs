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

            checkFavorites();

            textBlock_ipAddress.Text = selectedDevice.deviceIP;
            textBlock_macAddress.Text = selectedDevice.deviceMAC;

            if (selectedDevice.devicePCName == "Unknown Device Name")
            {
                textBlock_deviceTitle.Text = selectedDevice.deviceName;
            }
            else
            {
                textBlock_deviceTitle.Text = selectedDevice.deviceName + " | " + selectedDevice.devicePCName;
            }
            
            textBlock_wmiAvailable.Text = selectedDevice.wmiManageableText;

            refreshDeviceDetails();

            if (!(App.Current as App).freshScan)
            {
                button_manage.Visibility = Visibility.Collapsed;
                button_manage_wmi.Visibility = Visibility.Collapsed;
            }
            if (selectedDevice.wmiManageable == false)
            {
                button_manage_wmi.Visibility = Visibility.Collapsed;
            }
        }

        private void checkFavorites()
        {
            if(Properties.Settings.Default.favoriteIP == null)
            {
                Properties.Settings.Default.favoriteIP = new List<string>();
            }

            IEnumerable<string> query = Properties.Settings.Default.favoriteMAC.Where(
                    s => s.Equals(selectedDevice.deviceMAC));

            if(query.Any())
            {
                button_favorites.Visibility = Visibility.Collapsed;
            }
        }

        private void refreshDeviceDetails()
        {
            textBlock_manufacturer.Text = selectedDevice.deviceManufacturer;
            textBlock_devtype.Text = selectedDevice.deviceType;
            textBlock_opsys.Text = selectedDevice.deviceOS;
            textBlock_pcname.Text = selectedDevice.devicePCName;
            textBlock_devmodel.Text = selectedDevice.deviceModel;
            textBlock_currentuser.Text = selectedDevice.deviceUser;

            if (selectedDevice.devicePCName == "Unknown Device Name")
            {
                textBlock_deviceTitle.Text = selectedDevice.deviceName;
            }
            else
            {
                textBlock_deviceTitle.Text =  selectedDevice.devicePCName;
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

            if (Properties.Settings.Default.favoritePCManufacturer == null)
            {
                Properties.Settings.Default.favoritePCManufacturer = new List<string>();
            }

            if (Properties.Settings.Default.favoritePCModel == null)
            {
                Properties.Settings.Default.favoritePCModel = new List<string>();
            }

            if (Properties.Settings.Default.favoritePCName == null)
            {
                Properties.Settings.Default.favoritePCName = new List<string>();
            }

            if (Properties.Settings.Default.favoritePCOS == null)
            {
                Properties.Settings.Default.favoritePCOS = new List<string>();
            }

            if (Properties.Settings.Default.favoritePCType == null)
            {
                Properties.Settings.Default.favoritePCType = new List<string>();
            }

            if (Properties.Settings.Default.favoritePCUser == null)
            {
                Properties.Settings.Default.favoritePCUser = new List<string>();
            }

            if (Properties.Settings.Default.favoriteWMI == null)
            {
                Properties.Settings.Default.favoriteWMI = new List<string>();
            }

            

            Properties.Settings.Default.favoriteIP.Add(selectedDevice.deviceIP);
            Properties.Settings.Default.favoriteMAC.Add(selectedDevice.deviceMAC);
            Properties.Settings.Default.favoriteManufacturer.Add(selectedDevice.deviceName);
            Properties.Settings.Default.favoritePCManufacturer.Add(selectedDevice.deviceManufacturer);
            Properties.Settings.Default.favoritePCModel.Add(selectedDevice.deviceModel);
            Properties.Settings.Default.favoritePCName.Add(selectedDevice.devicePCName);
            Properties.Settings.Default.favoritePCOS.Add(selectedDevice.deviceOS);
            Properties.Settings.Default.favoritePCType.Add(selectedDevice.deviceType);
            Properties.Settings.Default.favoritePCUser.Add(selectedDevice.deviceUser);
            Properties.Settings.Default.favoriteWMI.Add(selectedDevice.wmiManageableText);

            Properties.Settings.Default.Save();

            checkFavorites();
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
            }
        }
    }
}
