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
        int DSno = 0;
        public Doctorpage(int Sno)
        {
            InitializeComponent();
            if(Sno==0)
            {
                btn_save.Tag = "Save";
            }
            else
            {
                var Doctordetail = FNC.GetDoctor(Sno);
                if(Doctordetail!=null)
                {
                    txt_doctorname.Text = Doctordetail.doctorname;
                    cmb_specality.Text = Doctordetail.speciality;
                    DSno = Doctordetail.Sno;
                }
                btn_save.Tag = "Update";
            }

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
                string indication = ((Button)sender).Tag.ToString();
                if (indication == "Save")
                {
                    if (txt_doctorname.Text != string.Empty && cmb_specality.Text != string.Empty)
                    {
                        DoctorClass DC = new DoctorClass();
                        DC.doctorname = txt_doctorname.Text;
                        DC.speciality = cmb_specality.Text;
                        DC.setDefault = false;
                        String Message = FNC.SaveDoctor(DC);
                        MessageBox.Show(Message, "Information Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else { MessageBox.Show("Please fill requiered fields","Warning Message",MessageBoxButton.OK,MessageBoxImage.Warning); }
                }
                else
                {
                    if (txt_doctorname.Text != string.Empty && cmb_specality.Text != string.Empty)
                    {
                        DoctorClass DC = new DoctorClass();
                        DC.Sno = DSno;
                        DC.doctorname = txt_doctorname.Text;
                        DC.speciality = cmb_specality.Text;                      
                        String Message = FNC.UpdateDoctor(DC);
                        MessageBox.Show(Message, "Information Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else { MessageBox.Show("Please fill requiered fields", "Warning Message", MessageBoxButton.OK, MessageBoxImage.Warning); }
                }
            }
            catch(Exception ex) { }
        }
    }
}
