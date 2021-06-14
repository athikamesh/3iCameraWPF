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

namespace _3iCamera
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Pages.Dashpage Dpage = new Pages.Dashpage();
            Pageframe.Navigate(Dpage);
        }

        private void Btn_close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void tool_adddoctor_Click(object sender, RoutedEventArgs e)
        {
            Pages.Doctorpage UP = new Pages.Doctorpage();
            Toolwindows TW = new Toolwindows(UP);
            TW.ShowDialog();
        }

        private void tool_viewdoctor_Click(object sender, RoutedEventArgs e)
        {
            Pages.Doctorlist UP = new Pages.Doctorlist();
            Toolwindows TW = new Toolwindows(UP);
            TW.ShowDialog();
        }

        private void tool_utility_Click(object sender, RoutedEventArgs e)
        {
            Pages.Utility UP = new Pages.Utility();
            Toolwindows TW = new Toolwindows(UP);
            TW.ShowDialog();
        }

        private void tool_report_setting_Click(object sender, RoutedEventArgs e)
        {
            Pages.Reportpage UP = new Pages.Reportpage();
            Toolwindows TW = new Toolwindows(UP);
            TW.ShowDialog();
        }

        private void tool_about_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow RW = new ReportWindow();
            RW.ShowDialog();
        }
    }
}
