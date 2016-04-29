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
using System.IO;

namespace SunScan.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        string nmapCommandFile = "test1.txt";

        public SettingsPage()
        {
            InitializeComponent();

            overwriteIPBox.Text = Properties.Settings.Default.ipToOverwrite;
            ipRangeBox.Text = Properties.Settings.Default.ipScanRange.ToString();

            checkBox_overwrite.IsChecked = Properties.Settings.Default.overwriteIP;
            textbox_nmapcommand.Text = Properties.Settings.Default.nmapCommand + Properties.Settings.Default.ipToScan;

            wmiUsername.Text = Properties.Settings.Default.defaultWMIUsername;
            wmiPassword.Password = Properties.Settings.Default.defaultWMIPassword;
            wmiDomain.Text = Properties.Settings.Default.defaultWMIDomain;
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void button_saveSettings_Click(object sender, RoutedEventArgs e)
        {
            NMAPScan.GetIPConfig(nmapCommandFile);

            int scanRange = 0;

            Properties.Settings.Default.ipToOverwrite = overwriteIPBox.Text.ToString(); //This is the custom IP address

            scanRange = int.Parse(ipRangeBox.Text);

            Properties.Settings.Default.ipScanRange = scanRange; //This is the +/- range specified in the text box

            Properties.Settings.Default.defaultWMIUsername = wmiUsername.Text;
            Properties.Settings.Default.defaultWMIPassword = wmiPassword.Password;
            Properties.Settings.Default.defaultWMIDomain = wmiDomain.Text;

            Properties.Settings.Default.Save();

            if(!(Properties.Settings.Default.overwriteIP))
            {
                ChangeIPRange();
            }

            //now for the completely overriting the ip scan range

            //check to see if IP can be overwritten
            string defaultNMAPCommand = Properties.Settings.Default.nmapCommand;

            if (Properties.Settings.Default.overwriteIP)
            {
                SunScan.Properties.Settings.Default.ipToScan = Properties.Settings.Default.ipToOverwrite;
                SunScan.Properties.Settings.Default.Save();

                NMAPScan.WriteFile(defaultNMAPCommand + Properties.Settings.Default.ipToOverwrite, "test1.txt");
                
            }

            textbox_nmapcommand.Text = Properties.Settings.Default.nmapCommand + Properties.Settings.Default.ipToScan;

            textbox_settings_saved.Visibility = Visibility.Visible;
            
        }

        private void ChangeIPRange()
        {

            string defaultNMAPCommand = Properties.Settings.Default.nmapCommand;

            string ipRangeNoNmap = "";

            int range = Properties.Settings.Default.ipScanRange;

            string ip = NMAPScan.GetIPAddress();

            string[] addrParts = ip.Split('.');
 
            addrParts[addrParts.Length - 1] = "*";

            string rangeToChange = addrParts[addrParts.Length - 2];

            int ipconfigResult = int.Parse(rangeToChange);

            int low = ipconfigResult - range;
            int high = ipconfigResult + range;

            if (low < 0)
                low = 0;

            string newIPRange = "";

            if (range == 0)
                newIPRange = ipconfigResult + "";
            else
                newIPRange = low + "-" + high;

            for(int i=0; i<addrParts.Length; i++)
            {

                if(i == addrParts.Length-2)
                    ipRangeNoNmap += newIPRange;
                
                else
                    ipRangeNoNmap += addrParts[i];

                if (i < addrParts.Length - 1)
                    ipRangeNoNmap += ".";
            }

            //NMAPScan.WriteFile(Properties.Settings.Default.overwriteIP + " " + Properties.Settings.Default.ipToOverwrite +  " " + newIPAddressRange, "testBoolean.txt");

            SunScan.Properties.Settings.Default.ipToScan = ipRangeNoNmap;
            SunScan.Properties.Settings.Default.Save();

            NMAPScan.WriteFile(defaultNMAPCommand + ipRangeNoNmap, "test1.txt");
        }

        private void button_clearFavorites_Click(object sender, RoutedEventArgs e)
        {
            //Put in code for clearing lists here
            if(Properties.Settings.Default.favoritesList == null)
            {
                Properties.Settings.Default.favoritesList = new List<aDevice>();
            }
            if (Properties.Settings.Default.favoriteIP == null)
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

            Properties.Settings.Default.favoritesList.Clear();
            Properties.Settings.Default.favoriteIP.Clear();
            Properties.Settings.Default.favoriteMAC.Clear();
            Properties.Settings.Default.favoriteManufacturer.Clear();

            Properties.Settings.Default.Save();
        }

        private void checkBox_overwrite_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.overwriteIP = true;
        }

        private void checkBox_overwrite_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.overwriteIP = false;
        }
    }
}
