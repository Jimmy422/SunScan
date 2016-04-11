using SunScan.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void button_manage_Click(object sender, RoutedEventArgs e)
        {
            ManageDevicePage manageSelectedDevicePage = new ManageDevicePage();
            NavigationService.Navigate(manageSelectedDevicePage);
        }
    }
}
