using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace _3iCamera
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow(bool Camerastatus)
        {
            InitializeComponent();
           
            Pages.Dashpage Dpage = new Pages.Dashpage(Camerastatus);
            Pageframe.Navigate(Dpage);
           if(CommenMethod.Cleantemp() == false) { MessageBox.Show("Path not read","Alert",MessageBoxButton.OK); }
        }

        
        private void Btn_close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void tool_adddoctor_Click(object sender, RoutedEventArgs e)
        {
            Pages.Doctorpage UP = new Pages.Doctorpage(0);
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

        private void Window_Activated(object sender, EventArgs e)
        {
            Pageframe.Refresh();
        }

        void cleantemp()
        {

        }
    }
}
