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

namespace _3iCamera.Pages
{
    /// <summary>
    /// Interaction logic for Dashpage.xaml
    /// </summary>
    public partial class Dashpage : Page
    {
        public Dashpage()
        {
            InitializeComponent();
            setgridsize();
        }

        private void SearchGridScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
           
                ScrollViewer scv = (ScrollViewer)sender;
                scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
                e.Handled = true;
           
        }

        private void Btn_live_Click(object sender, RoutedEventArgs e)
        {
            CaptuerScreen CS = new CaptuerScreen();
            CS.ShowDialog();
        }

        void setgridsize()
        {
           
            double scrwd= System.Windows.SystemParameters.PrimaryScreenWidth;
            scrwd -= 200;
            mrnoColumn.Width = (scrwd / 100) * 10;
            patientnameColumn.Width = (scrwd / 100) * 20;
            ageColumn.Width = (scrwd / 100) * 5;
            genderColumn.Width = (scrwd / 100) * 10;
            vdateColumn.Width = (scrwd / 100) * 10;
            edoctorColumn.Width = (scrwd / 100) * 13;
            diagnosisColumn.Width = (scrwd / 100) * 10;
            eyeColumn.Width = (scrwd / 100) * 5;
            summarycolumn.Width = (scrwd / 100) * 10;
           
        }

        private void btn_patient_Click(object sender, RoutedEventArgs e)
        {
            PatientWindow PW = new PatientWindow();
            PW.ShowDialog();
        }
    }
}
