using Microsoft.Win32;
using SunScan.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public HomePage()
        {
            InitializeComponent();
            setupFileDialog();
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

        // TODO: There is an issue where the following method freezes the app.
        // The method works, however, the stream is locking the app up

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
            string foundIP = "";
            string foundMac = "";
            string foundName = "";
            string foundAddress = "";

            int addressCount = 0;
            int deviceCount = 0;

            bool hostFound = false;

            while(xmlToScan.Read())
            {
                if (xmlToScan.NodeType == XmlNodeType.Element)
                {
                    if(xmlToScan.Name == "host")
                    {
                        hostFound = true;
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
                                        foundName = xmlToScan.Value;
                                        break;
                                }
                            }
                        }

                        if(addressCount == 2) //Once we've found the IP and MAC address, we're done
                        {
                            addressCount = 0;
                            hostFound = false;

                            //Console.WriteLine("New Device: " + foundName + ". IP: " + foundIP + " MAC: " + foundMac);
                            deviceScanResults.Add(new aDevice(foundName, foundMac, foundIP));
                            deviceCount++;

                            foundIP = "";
                            foundMac = "";
                            foundName = "";
                            foundAddress = "";
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
        /// The "Open Scan" button was clicked
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
    }
}
