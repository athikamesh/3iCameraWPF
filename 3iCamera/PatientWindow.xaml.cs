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
using System.Windows.Shapes;

namespace _3iCamera
{
    /// <summary>
    /// Interaction logic for PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {
        public PatientWindow()
        {
            InitializeComponent();
        }

        private void dp_dop_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                txt_date.Text = dp_dop.SelectedDate.Value.Date.ToString("dd-MM-yyyy");                
                int dt = DateTime.Now.Year - DateTime.Parse(txt_date.Text).Year;
                txt_age.Text = dt.ToString();
            }
            catch(Exception ex) { }
        }

        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txt_age_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txt_age.Text != null)
                {
                    int dt = DateTime.Now.Year - int.Parse(txt_age.Text);
                    txt_date.Text = DateTime.Now.Date.ToString("dd") + "-" + DateTime.Now.Month.ToString() + "-" + dt.ToString();
                }
            }
            catch { }
        }

        private void Btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
