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
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void button_saveSettings_Click(object sender, RoutedEventArgs e)
        {
            int scanRange = 0;

            Properties.Settings.Default.ipToOverwrite = overwriteIPBox.Text.ToString(); //This is the custom IP address

            scanRange = int.Parse(ipRangeBox.Text);

            Properties.Settings.Default.ipScanRange = scanRange; //This is the +/- range specified in the text box
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
            NMAPScan.GetIPConfig(nmapCommandFile);

            string line = "";

            using (StreamReader rdr = new StreamReader(nmapCommandFile))
            {
                line = rdr.ReadLine();
            }

            string[] nmapCommandParts = line.Split(' ');

            line = nmapCommandParts[nmapCommandParts.Length - 1];

            string[] ipParts = line.Split('.');

            int defaultIP = 0;

            string defaultNMAPCommand = Properties.Settings.Default.nmapCommand;

            string newIPAddressRange = defaultNMAPCommand;

            string ipRangeNoNmap = "";

            for (int i = 0; i < ipParts.Length-1; ++i)
            {

                if (ipParts[i+1].Contains("*"))
                {
                    int range = Properties.Settings.Default.ipScanRange;

                    int high = 0;
                    int low = 0;

                    int difference = 0;

                    if (ipParts[i].Contains("-"))
                    {
                        string[] rangeParts = ipParts[i].Split('-');

                        //NMAPScan.WriteFile(rangeParts.Length + " " + rangeParts[0] + " " + rangeParts[1], "rangeparts.txt");

                        low = int.Parse(rangeParts[0]);
                        high = int.Parse(rangeParts[1]);

                        difference = (high - low) / 2;
                    }
                    else
                    {
                        difference = int.Parse(ipParts[i]);

                    }

                    defaultIP = low + difference;

                    low = defaultIP - range;

                    if (low < 0)
                        low = 0;

                    high = defaultIP + range;

                    string newRange = low + "-" + high;

                    if (range == 0)
                    {
                        newRange = ( (high + low)/2 ) + "";
                    }

                    ipParts[i] = newRange;
                }

                ipRangeNoNmap += ipParts[i];
                newIPAddressRange += ipParts[i];

                if (i < ipParts.Length - 1)
                {
                    ipRangeNoNmap += ".";
                    newIPAddressRange += ".";
                }

            }
            ipRangeNoNmap += "*";
            newIPAddressRange += "*";
            //NMAPScan.WriteFile(Properties.Settings.Default.overwriteIP + " " + Properties.Settings.Default.ipToOverwrite +  " " + newIPAddressRange, "testBoolean.txt");

            SunScan.Properties.Settings.Default.ipToScan = ipRangeNoNmap;
            SunScan.Properties.Settings.Default.Save();

            NMAPScan.WriteFile(newIPAddressRange, "test1.txt");
        }

        private void button_clearFavorites_Click(object sender, RoutedEventArgs e)
        {
            //Put in code for clearing lists here
            Properties.Settings.Default.favoritesList.Clear();
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
