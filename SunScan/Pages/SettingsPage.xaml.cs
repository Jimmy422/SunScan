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
        public SettingsPage()
        {
            InitializeComponent();

            overwriteIPBox.Text = Properties.Settings.Default.ipToOverwrite;
            ipRangeBox.Text = Properties.Settings.Default.ipScanRange.ToString();
            radio_overwrite_checked.IsChecked = Properties.Settings.Default.overwriteIP;
            radio_overwrite_unchecked.IsChecked = !(Properties.Settings.Default.overwriteIP);
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void button_saveSettings_Click(object sender, RoutedEventArgs e)
        {
            bool? test = radio_overwrite_checked.IsChecked;
            int scanRange = 0;
            if (!test.HasValue) //check for a value
            {
                // Assume that IsInitialized
                // returns either true or false.
                test = IsInitialized;
            }

            Properties.Settings.Default.overwriteIP = ((bool)test); //This is what determines if we are going to use a custom ip address
            Properties.Settings.Default.ipToOverwrite = overwriteIPBox.Text.ToString(); //This is the custom IP address

            scanRange = int.Parse(ipRangeBox.Text);

            Properties.Settings.Default.ipScanRange = scanRange; //This is the +/- range specified in the text box
            Properties.Settings.Default.Save();

            //To get these values on a different page, call Properties.Settings.Default.(whatever it is above that you need). It will return the value saved above.

            if(scanRange > 0)
            {
                ChangeIPRange();
            }


            //now for the completely overriting the ip scan range

            //check to see if IP can be overwritten
            string defaultNMAPCommand = "nmap - p 135 - oX - ";

            if (Properties.Settings.Default.overwriteIP)
            { 
                NMAPScan.WriteFile(defaultNMAPCommand + Properties.Settings.Default.ipToOverwrite, "test1.txt");
            }
        }

        private void ChangeIPRange()
        {
            string line = "";
            using (StreamReader rdr = new StreamReader("test1.txt"))
            {
                line = rdr.ReadLine();
            }

            string[] nmapCommandParts = line.Split(' ');

            line = nmapCommandParts[nmapCommandParts.Length - 1];

            string[] ipParts = line.Split('.');

            int defaultIP = 0;

            string defaultNMAPCommand = "nmap - p 135 - oX - ";

            string newIPAddressRange = defaultNMAPCommand;

            for (int i = 0; i < ipParts.Length; ++i)
            {

                if (ipParts[i].Contains("-"))
                {
                    int range = Properties.Settings.Default.ipScanRange;

                    string[] rangeParts = ipParts[i].Split('-');

                    //NMAPScan.WriteFile(rangeParts.Length + " " + rangeParts[0] + " " + rangeParts[1], "rangeparts.txt");

                    int low = int.Parse(rangeParts[0]);
                    int high = int.Parse(rangeParts[1]);

                    int difference = (high - low) / 2;

                    defaultIP = low + difference;

                    low = defaultIP - range;

                    if (low < 1)
                        low = 1;

                    high = defaultIP + range;

                    string newRange = low + "-" + high;

                    ipParts[i] = newRange;
                }

                newIPAddressRange += ipParts[i];


                if (i < ipParts.Length - 1)
                {
                    newIPAddressRange += ".";
                }
            }

            //NMAPScan.WriteFile(Properties.Settings.Default.overwriteIP + " " + Properties.Settings.Default.ipToOverwrite +  " " + newIPAddressRange, "testBoolean.txt");

            NMAPScan.WriteFile(newIPAddressRange, "test1.txt");
        }

        private void button_clearFavorites_Click(object sender, RoutedEventArgs e)
        {
            //Put in code for clearing lists here
            Properties.Settings.Default.favoritesList.Clear();
            Properties.Settings.Default.Save();
        }
    }
}
