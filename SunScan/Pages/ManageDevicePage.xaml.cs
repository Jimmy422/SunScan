using SunScan.Classes;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ManageDevicePage.xaml
    /// </summary>
    public partial class ManageDevicePage : Page
    {
        aDevice selectedDevice = (App.Current as App).selectedDevice;

        public ManageDevicePage()
        {
            InitializeComponent();
            textBlock_devicename.Text = selectedDevice.devicePCName;
            textBlock_data.Text = selectedDevice.dataUsed.ToString() + " KB since WMI Query Ran";
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
