﻿using Microsoft.Win32;
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
        List<string> favManuList;
        List<string> favPCManuList;
        List<string> favPCNameList;
        List<string> favPCModelList;
        List<string> favPCOSList;
        List<string> favPCTypeList;
        List<string> favPCUserList;
        List<string> favWMIList;

        public FavoritesPage()
        {
            InitializeComponent();

            favIPList = Properties.Settings.Default.favoriteIP;
            favMACList = Properties.Settings.Default.favoriteMAC;
            favManuList = Properties.Settings.Default.favoriteManufacturer;

            favPCManuList = Properties.Settings.Default.favoritePCManufacturer;
            favPCModelList = Properties.Settings.Default.favoritePCModel;
            favPCNameList = Properties.Settings.Default.favoritePCName;
            favPCOSList = Properties.Settings.Default.favoritePCOS;
            favPCTypeList = Properties.Settings.Default.favoritePCType;
            favPCUserList = Properties.Settings.Default.favoritePCUser;
            favWMIList = Properties.Settings.Default.favoriteWMI;

            foundDevices = new List<aDevice>();

            calculateFavorites();

            listBox_devices.ItemsSource = foundDevices;
        }

        public void calculateFavorites()
        {
            if(favIPList != null)
            {
                for (int i = 0; i < favIPList.Count; i++)
                {
                    foundDevices.Add(new aDevice(favWMIList[i], favIPList[i], favMACList[i], favManuList[i], favPCManuList[i], favPCModelList[i], favPCNameList[i], favPCOSList[i], favPCTypeList[i], favPCUserList[i]));
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
                        aDevice.devicePCName.ToLower().Contains(textBox.Text.ToLower().ToString()) ||
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
