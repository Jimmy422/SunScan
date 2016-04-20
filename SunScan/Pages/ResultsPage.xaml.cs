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
    /// Interaction logic for ResultsPage.xaml
    /// </summary>
    public partial class ResultsPage : Page
    {
        List<aDevice> foundDevices = (App.Current as App).deviceList;
        public ResultsPage()
        {
            InitializeComponent();
            listBox_devices.ItemsSource = foundDevices;
        }

        private void listBox_devices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Get the selected device and head over to the device details page
            if(listBox_devices.SelectedIndex >= 0)
            {
                aDevice listSelectedDevice = (aDevice)listBox_devices.Items[listBox_devices.SelectedIndex];
                (App.Current as App).selectedDevice = listSelectedDevice;

                DeviceDetailsPage selectedDetailsPage = new DeviceDetailsPage();
                NavigationService.Navigate(selectedDetailsPage);
            }
            listBox_devices.SelectedItem = null;
        }

        private void button_sort_az_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<aDevice> query = foundDevices.OrderBy(aDevice => aDevice.deviceName);
            listBox_devices.ItemsSource = query;
        }

        private void button_sort_za_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<aDevice> query = foundDevices.OrderByDescending(aDevice => aDevice.deviceName);
            listBox_devices.ItemsSource = query;
        }

        private void button_search_Click(object sender, RoutedEventArgs e)
        {
            listBox_devices.ItemsSource = foundDevices;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(textBox.Text != "")
            {
                IEnumerable<aDevice> query = foundDevices.Where(
                    aDevice =>
                        aDevice.deviceIP.Contains(textBox.Text.ToString()) ||
                        aDevice.deviceMAC.ToLower().Contains(textBox.Text.ToLower().ToString()) ||
                        aDevice.deviceName.ToLower().Contains(textBox.Text.ToLower().ToString()));

                listBox_devices.ItemsSource = query;
            }
            else
            {
                listBox_devices.ItemsSource = foundDevices;
            }
        }
    }
}
