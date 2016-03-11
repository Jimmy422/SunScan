using SunScan.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SunScan
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //These are objects that get passed between pages. If you need something to go
        //across multiple pages, put it below.

        public List<aDevice> deviceList { get; set; }
        public aDevice selectedDevice { get; set; }
    }
}
