using Microsoft.Win32;
using SunScan.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class HomePage : Page
    {
        OpenFileDialog getXmlDialog = new OpenFileDialog();

        List<aDevice> deviceScanResults = new List<aDevice>();

        BackgroundWorker scanBackgroundWorker = new BackgroundWorker();

        XmlTextReader xmlReader;

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
        }

        private void ScanBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            
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

            while(xmlToScan.Read())
            {
                if (xmlToScan.NodeType == XmlNodeType.Element)
                {
                    if(xmlToScan.Name == "host")
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
                    if(xmlToScan.Name == "address")
                    {
                        addressCount++;

                        if (hostFound) //We want to make sure that we're getting data for one host at a time
                        {
                            while (xmlToScan.MoveToNextAttribute())
                            {
                                switch(xmlToScan.Name)
                                {
                                    case "addr": //The XML file gives us the address before the type, so we store it and categorize later.
                                        foundAddress = xmlToScan.Value;
                                        break;
                                    case "addrtype":
                                        switch(xmlToScan.Value)
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

                        if(addressCount == 2) //Once we've found the IP and MAC address, we're done
                        {
                            addressCount = 0;
                            //hostFound = false; //Disable this right now for port scan

                            //Console.WriteLine("New Device: " + foundName + ". IP: " + foundIP + " MAC: " + foundMac);
                            
                        }
                    }
                }
            }
            if(deviceCount > 0)
            {
                return true;
            }
            else
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
            openFile();

            if (xmlReader != null)
            {
                if(scanXML(xmlReader))
                {
                    (App.Current as App).deviceList = deviceScanResults; //Get the list of devices ready to pass to the next page
                    ResultsPage scanResultsPage = new ResultsPage();
                    NavigationService.Navigate(scanResultsPage);
                }
                else
                {
                    //Display "No devices found in this scan."
                    Console.WriteLine("No Devices found in scan. Is this a valid XML file?");
                }
            }
            else
            {
                //Display error message that the file was not opened successfully.
                Console.WriteLine("The file was not able to be read. Check your XML file.");
            }
        }

        private void button_newScan_Click(object sender, RoutedEventArgs e)
        {
            NMAPScan.GetIPConfig(nmapCommandFile, nmapXMLFile);

            //NMAPScan.GetIPConfig(nmapXMLFile);

            NMAPScan.ReadCommands2(nmapCommandFile, nmapXMLFile);

            xmlReader = new XmlTextReader(nmapXMLFile);

            if (xmlReader != null)
            {
                if (scanXML(xmlReader))
                {
                    (App.Current as App).deviceList = deviceScanResults; //Get the list of devices ready to pass to the next page

                    //Update scan date and time
                    Properties.Settings.Default.lastScanRunTime = "Last Scan Ran: " + DateTime.Now.Date.ToLongDateString() + " at " + DateTime.Now.ToShortTimeString().ToString();
                    Properties.Settings.Default.Save();

                    ResultsPage scanResultsPage = new ResultsPage();
                    NavigationService.Navigate(scanResultsPage);
                }
                else
                {
                    //Display "No devices found in this scan."
                    Console.WriteLine("No Devices found in scan. Is this a valid XML file?");
                }
            }
            else
            {
                //Display error message that the file was not opened successfully.
                Console.WriteLine("The file was not able to be read. Check your XML file.");
            }
            /*
            if (scanBackgroundWorker.IsBusy != true)
            {
                scanBackgroundWorker.RunWorkerAsync();
            }*/
        }
    }
}
