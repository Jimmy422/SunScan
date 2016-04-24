using SunScan.Pages;
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

namespace SunScan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //NMAPScan.RunTests();
        }

        private void button_home_Click(object sender, RoutedEventArgs e)
        {
            Uri homePage = new Uri("Pages/HomePage.xaml", UriKind.Relative);
            frame_pages.Source = homePage;
        }

        private void button_favorites_Click(object sender, RoutedEventArgs e)
        {
            (App.Current as App).freshScan = false;
            Uri homePage = new Uri("Pages/FavoritesPage.xaml", UriKind.Relative);
            frame_pages.Source = homePage;
        }

        private void button_settings_Click(object sender, RoutedEventArgs e)
        {
            Uri settingsPage = new Uri("Pages/SettingsPage.xaml", UriKind.Relative);
            frame_pages.Source = settingsPage;
        }
    }
}
