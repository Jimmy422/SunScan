using Microsoft.Win32;
using SunScan.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Xml;

namespace SunScan.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    /// 
    public partial class HomePage : Page
    {
        OpenFileDialog getXmlDialog = new OpenFileDialog();

        List<aDevice> deviceScanResults = new List<aDevice>();

        BackgroundWorker scanBackgroundWorker = new BackgroundWorker();

        XmlTextReader xmlReader;

        bool scanStopped = false;

        string nmapCommandFile = "test1.txt";
        string nmapXMLFile = "nmapScan.xml";

        public HomePage()
        {
            InitializeComponent();
            setupFileDialog();
            scanTimeLabel.Text = Properties.Settings.Default.lastScanRunTime.ToString();

            scanBackgroundWorker.WorkerSupportsCancellation = true;
            scanBackgroundWorker.WorkerReportsProgress = true;
            scanBackgroundWorker.DoWork += ScanBackgroundWorker_DoWork;
            scanBackgroundWorker.RunWorkerCompleted += ScanBackgroundWorker_RunWorkerCompleted;

            Properties.Settings.Default.windowNotLocked = true;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Sets up the file dialog to open a XML scan
        /// </summary>
        public void setupFileDialog()
        {
            getXmlDialog.FileName = "Scan";
            getXmlDialog.DefaultExt = ".xml";
            getXmlDialog.Filter = "SunScan Results (.xml)|*.xml";
        }

        /// <summary>
        /// Sets the values within the status box
        /// </summary>
        /// <param name="scanStatus">The main header of the box</param>
        /// <param name="errorLabelText">The text below the header, usually used for errors</param>
        /// <param name="isProgressIndterminate">Is the progress bar scrolling?</param>
        /// <param name="progress">The value of the progress bar (0-100)</param>
        public void setupProgressSection(string scanStatus, string errorLabelText, bool isProgressIndeterminate, int progress)
        {
            scanStatusLabel.Text = scanStatus;
            errorMessageLabel.Text = errorLabelText;
            errorMessageLabel.Visibility = Visibility.Visible;
            scanProgressStackPanel.Visibility = Visibility.Visible;
            scanProgressBar.IsIndeterminate = isProgressIndeterminate;

            if(!isProgressIndeterminate)
            {
                scanProgressBar.Value = progress;
            }
        }

        /// <summary>
        /// Opens the XML file, checks that it's valid, then runs the scan
        /// </summary>
        public void openFile()
        {
            if (getXmlDialog.ShowDialog() == true)
            {
                xmlReader = new XmlTextReader(getXmlDialog.FileName);
            }
        }

        /// <summary>
        /// Scans the xml file provided, and generates a list of aDevice objects based on the results.
        /// Returns true if any devices are found, false if none were found
        /// </summary>
        /// <param name="xmlToScan">The XML file to scan, in the form of an XmlTextReader</param>
        public bool scanXML(XmlTextReader xmlToScan)
        {
            if(xmlToScan == null)
            {
                return false;
            }

            //Initalizes values to no data
            string foundIP = "No IP Address Available";
            string foundMac = "No MAC Address Available";
            string foundName = "No Manufacturer Available";
            string foundAddress = "No Data Available";

            int addressCount = 0;
            int deviceCount = 0;

            bool hostFound = false;

            bool hasWMI = false;

            try
            {
                while (xmlToScan.Read())
                {
                    if (xmlToScan.NodeType == XmlNodeType.Element)
                    {
                        if (xmlToScan.Name == "host")
                        {
                            hostFound = true;
                        }

                        if (xmlToScan.Name == "state")
                        {
                            if (hostFound) //We want to make sure that we're getting data for one host at a time
                            {
                                while (xmlToScan.MoveToNextAttribute())
                                {
                                    switch (xmlToScan.Name)
                                    {
                                        case "state":
                                            if (xmlToScan.Value != "open")
                                            {
                                                hasWMI = false;
                                            }
                                            else
                                            {
                                                hasWMI = true;
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                deviceScanResults.Add(new aDevice(foundName, foundMac, foundIP, hasWMI));
                                deviceCount++;

                                //Initalizes values to no data for next device
                                foundIP = "No IP Address Available";
                                foundMac = "No MAC Address Available";
                                foundName = "No Manufacturer Available";
                                foundAddress = "No Data Available";
                                hasWMI = false;
                            }
                        }
                        if (xmlToScan.Name == "address")
                        {
                            addressCount++;

                            if (hostFound) //We want to make sure that we're getting data for one host at a time
                            {
                                while (xmlToScan.MoveToNextAttribute())
                                {
                                    switch (xmlToScan.Name)
                                    {
                                        case "addr": //The XML file gives us the address before the type, so we store it and categorize later.
                                            foundAddress = xmlToScan.Value;
                                            break;
                                        case "addrtype":
                                            switch (xmlToScan.Value)
                                            {
                                                case "ipv4":
                                                    foundIP = foundAddress;
                                                    break;
                                                case "mac":
                                                    foundMac = foundAddress;
                                                    break;
                                            }
                                            break;
                                        case "vendor":
                                            foundName = xmlToScan.Value + " Device";
                                            break;
                                    }
                                }
                            }

                            if (addressCount == 2) //Once we've found the IP and MAC address, we're done
                            {
                                addressCount = 0;
                            }
                        }
                    }
                }
                if (deviceCount > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// The "Open Scan" button was clicked, a scan xml file will be opened
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_openScan_Click(object sender, RoutedEventArgs e)
        {
            if(Properties.Settings.Default.windowNotLocked)
            {
                openFile();

                if (xmlReader != null)
                {
                    if (scanXML(xmlReader))
                    {
                        (App.Current as App).deviceList = deviceScanResults; //Get the list of devices ready to pass to the next page
                        (App.Current as App).freshScan = false; //This is not a new scan, don't show the save button

                        Properties.Settings.Default.windowNotLocked = true;
                        Properties.Settings.Default.Save();

                        ResultsPage scanResultsPage = new ResultsPage();
                        NavigationService.Navigate(scanResultsPage);
                    }
                    else
                    {
                        //Display "No devices found in this scan."
                        setupProgressSection("Error Processing Scan", "Something went wrong reading the scan file. Try again.", false, 0);
                        scanProgressStackPanel.Visibility = Visibility.Collapsed;

                        Properties.Settings.Default.windowNotLocked = true;
                        Properties.Settings.Default.Save();
                    }
                }
                else
                {
                    //Display error message that the file was not opened successfully.
                    setupProgressSection("Error Processing Scan", "Something went wrong reading the scan file. Try again.", false, 0);
                    scanProgressStackPanel.Visibility = Visibility.Collapsed;

                    Properties.Settings.Default.windowNotLocked = true;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void ScanBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            NMAPScan.ReadCommands(nmapCommandFile, nmapXMLFile);
        }

        private void ScanBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!scanStopped)
            {
                setupProgressSection("Scan Complete", "Processing results.", false, 0);

                xmlReader = new XmlTextReader(nmapXMLFile);

                if (xmlReader != null)
                {
                    if (scanXML(xmlReader))
                    {
                        (App.Current as App).deviceList = deviceScanResults; //Get the list of devices ready to pass to the next page
                        (App.Current as App).freshScan = true;
                        //Update scan date and time
                        Properties.Settings.Default.lastScanRunTime = "Last Scan Ran: " + DateTime.Now.Date.ToLongDateString() + " at " + DateTime.Now.ToShortTimeString().ToString();
                        Properties.Settings.Default.Save();

                        Properties.Settings.Default.windowNotLocked = true;
                        Properties.Settings.Default.Save();

                        ResultsPage scanResultsPage = new ResultsPage();
                        NavigationService.Navigate(scanResultsPage);
                    }
                    else
                    {
                        //Display "No devices found in this scan."
                        setupProgressSection("No Devices Found", "Either your network is empty, or the scan failed. Try again.", false, 0);
                        Properties.Settings.Default.windowNotLocked = true;
                        Properties.Settings.Default.Save();
                    }
                }
                else
                {
                    //Display error message that the file was not opened successfully.
                    setupProgressSection("Error Processing Scan", "Something went wrong reading the scan result. Try again.", false, 0);
                    Properties.Settings.Default.windowNotLocked = true;
                    Properties.Settings.Default.Save();
                }
            }
            scanStopped = false;

        }

        private void button_newScan_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.windowNotLocked)
            {
                Properties.Settings.Default.windowNotLocked = false;
                Properties.Settings.Default.Save();

                if ((!Properties.Settings.Default.overwriteIP) && (Properties.Settings.Default.ipScanRange == 0) || !File.Exists(nmapCommandFile))
                {
                    NMAPScan.GetIPConfig(nmapCommandFile);
                }

                setupProgressSection("Scanning " + Properties.Settings.Default.ipToScan, "Please be patient while we scan for devices. This may take a while.", true, 0);
                scanBackgroundWorker.RunWorkerAsync();
            }
        }

        private void button_favoriteDevices_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.windowNotLocked)
            {
                (App.Current as App).freshScan = false; //This is not a new scan, don't show the save button

                FavoritesPage favoritesResultsPage = new FavoritesPage();
                NavigationService.Navigate(favoritesResultsPage);
            }
        }

        private void button_settings_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.windowNotLocked)
            {
                SettingsPage settingsPage = new SettingsPage();
                NavigationService.Navigate(settingsPage);
            }
        }

        private void button_stop_scan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                scanStopped = true;

                Process[] proc = Process.GetProcessesByName("nmap");
                
                foreach(Process process in proc)
                {
                    process.Kill();
                }

                setupProgressSection("Scan Cancelled", "The scan was cancelled by the user.", false, 0);
                Properties.Settings.Default.windowNotLocked = true;
            }
            catch
            {
                setupProgressSection("Error", "The nmap process was unable to stop.", false, 0);

                scanStopped = false;
            }
            
        }
    }
}
