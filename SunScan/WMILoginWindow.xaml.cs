using SunScan.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management;
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
using System.Windows.Shapes;

namespace SunScan
{
    /// <summary>
    /// Interaction logic for WMILoginWindow.xaml
    /// </summary>
    /// 

    public enum ChassisTypes
    {
        Other = 1,
        Unknown,
        Desktop,
        LowProfileDesktop,
        PizzaBox,
        MiniTower,
        Tower,
        Portable,
        Laptop,
        Notebook,
        Handheld,
        DockingStation,
        AllInOne,
        SubNotebook,
        SpaceSaving,
        LunchBox,
        MainSystemChassis,
        ExpansionChassis,
        SubChassis,
        BusExpansionChassis,
        PeripheralChassis,
        StorageChassis,
        RackMountChassis,
        SealedCasePC
    }

    public partial class WMILoginWindow : Window
    {
        aDevice selectedDevice = (App.Current as App).selectedDevice;

        string ipToLogin = Properties.Settings.Default.wmiIPScan;

        BackgroundWorker wmiLoginBgWorker = new BackgroundWorker();

        ConnectionOptions options =
                 new ConnectionOptions();

        ManagementObjectSearcher managementSearch;
        ManagementObjectSearcher managementSearch2;
        ManagementObjectSearcher managementSearch3;

        public WMILoginWindow()
        {
            InitializeComponent();

            textbox_username.Text = Properties.Settings.Default.defaultWMIUsername;
            textbox_password.Password = Properties.Settings.Default.defaultWMIPassword;
            textbox_domain.Text = Properties.Settings.Default.defaultWMIDomain;

            header_page.Text = "Login to " + ipToLogin;
        }


        private void button_connect_Click(object sender, RoutedEventArgs e)
        {
            options.Username = textbox_username.Text;
            options.Password = textbox_password.Password;
            options.Authority = "ntlmdomain:" + textbox_domain.Text;

            try
            {
                string connectionString = "\\\\" + ipToLogin + "\\root\\cimv2";
                
                ManagementScope scope =
                new ManagementScope(connectionString, options);
                scope.Connect();

                managementSearch = new ManagementObjectSearcher(connectionString, "SELECT * FROM  Win32_ComputerSystem");
                ManagementObjectCollection queryCollection;

                managementSearch2 = new ManagementObjectSearcher(connectionString, "SELECT * FROM  Win32_OperatingSystem");
                ManagementObjectCollection queryCollection2;

                managementSearch3 = new ManagementObjectSearcher(connectionString, "SELECT * FROM  Win32_SystemEnclosure");
                ManagementObjectCollection queryCollection3;

                textbox_status.Text = "Status: Login Successful!";

                queryCollection = managementSearch.Get();

                foreach (ManagementObject m in queryCollection)
                {
                    // Display the remote computer information
                    selectedDevice.deviceManufacturer = m["Manufacturer"].ToString();
                    selectedDevice.devicePCName = m["Name"].ToString();
                    selectedDevice.deviceModel = m["Model"].ToString();
                    selectedDevice.deviceUser = m["UserName"].ToString();
                }

                queryCollection2 = managementSearch2.Get();

                foreach (ManagementObject m in queryCollection2)
                {
                    // Display the remote computer information
                    selectedDevice.deviceOS = m["Caption"].ToString();
                }

                queryCollection3 = managementSearch3.Get();

                foreach (ManagementObject m in queryCollection3)
                {
                    foreach (int i in (UInt16[])(m["ChassisTypes"]))
                    {
                        if (i > 0 && i < 25)
                        {
                            selectedDevice.deviceType = ((ChassisTypes)i).ToString();
                        }
                    }
                }

                this.DialogResult = true;
                this.Close();

            }
            catch (System.UnauthorizedAccessException ae)
            {
                textbox_status.Text = "Status: " + ae.Message;
            }
            catch (System.Runtime.InteropServices.COMException ce)
            {
                textbox_status.Text = "Status: " + ce.Message;
            }
        }

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {

        }
    }
}
