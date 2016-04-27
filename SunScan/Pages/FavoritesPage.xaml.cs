using Microsoft.Win32;
using SunScan.Classes;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class FavoritesPage : Page
    {
        SaveFileDialog saveXMLFileDialog = new SaveFileDialog();

        List<aDevice> foundDevices;
        List<string> favIPList;
        List<string> favMACList;


        public FavoritesPage()
        {
            InitializeComponent();
            calculateFavorites();

            foundDevices = Properties.Settings.Default.favoritesList;

            listBox_devices.ItemsSource = foundDevices;
            if((App.Current as App).freshScan)
            {
                button_saveScan.Visibility = Visibility.Visible;
            }
        }

        public void calculateFavorites()
        {
            if()
            {
                List<string> favIPList = Properties.Settings.Default.favoriteIP;
                List<string> favMACList = Properties.Settings.Default.favoriteMAC;
                List<string> favManuList = Properties.Settings.Default.favoriteManufacturer;

                for (int i = 0; i < favIPList.Count; i++)
                {
                    aDevice newDevice = new aDevice(favManuList[i], favMACList[i], favIPList[i]);
                    Properties.Settings.Default.favoritesList.Add(newDevice);
                }
            }
            
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

        public void setupFileDialog()
        {
            saveXMLFileDialog.Filter = "XML Files (*.xml)|*.xml";
            saveXMLFileDialog.FilterIndex = 1;
            saveXMLFileDialog.RestoreDirectory = true;
        }

        private void button_saveScan_Click(object sender, RoutedEventArgs e)
        {
            setupFileDialog();

            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.LoadXml(System.IO.File.ReadAllText("nmapScan.xml"));

            Stream myStream;
            
            if (saveXMLFileDialog.ShowDialog() == true)
            {
                if ((myStream = saveXMLFileDialog.OpenFile()) != null)
                {
                    xmlDoc.Save(myStream);
                    myStream.Close();
                }
            }
        }
    }
}
