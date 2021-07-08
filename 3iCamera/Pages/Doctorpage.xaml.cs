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
    /// Interaction logic for Utitlitypage.xaml
    /// </summary>
    public partial class Doctorpage : Page
    {
        FunctionalClass FNC = new FunctionalClass();
        public Doctorpage()
        {
            InitializeComponent();
            

        }

        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            txt_doctorname.Clear();
            cmb_specality.Text = "";
            txt_doctorname.Focusable = true;
            txt_doctorname.Focus();
        }

        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                DoctorClass DC = new DoctorClass();
                DC.Doctorname = "Sample";
                DC.Speciality = "IOL";
                DC.SetDefault = true;
                String Message= FNC.SaveDoctor(DC);
                MessageBox.Show(Message);
            }
            catch(Exception ex) { }
        }
    }
}
