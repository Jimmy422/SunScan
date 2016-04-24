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
        }

        private void button_clearFavorites_Click(object sender, RoutedEventArgs e)
        {
            //Put in code for clearing lists here
        }
    }
}
