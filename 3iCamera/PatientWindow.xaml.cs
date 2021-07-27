using System;
using System.Collections.Generic;
using System.IO;
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
        FunctionalClass FNC = new FunctionalClass();
        string gender = "Male";
        public PatientWindow()
        {
            InitializeComponent();
            getDoctor();
            
        }
        void getDoctor()
        {
            var data= FNC.ListDoctor();
            cmb_examining.Items.Clear();
            cmb_examining.Text = "Select";
            foreach (var s in data)
            {
                cmb_examining.Items.Add(s.doctorname);
            }

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
            try
            {
                bool status = FNC.GetPatienidStatus(txt_patientid.Text);
                if(status==false)
                {
                    if (txt_patientid.Text != "" && txt_firstname.Text != "" && cmb_examining.Text!="" && txt_mobile.Text!="")
                    {
                        if (!Directory.Exists(CommanHelper.Cm_Spath + "\\" + txt_patientid.Text + txt_firstname.Text))
                        {
                           DirectoryInfo di= Directory.CreateDirectory(CommanHelper.Cm_Spath + "\\" + txt_patientid.Text + txt_firstname.Text);
                       
                        }

                        PatientClass PC = new PatientClass();
                        PC.PatientID = txt_patientid.Text;
                        PC.FirstName = txt_firstname.Text;
                        PC.LastName = txt_lastname.Text;
                        PC.DOB = txt_date.Text;
                        PC.Gender = gender;
                        PC.EDR = cmb_examining.Text.ToString();
                        PC.RDR = "";
                        PC.Archive = CommanHelper.Cm_Spath + "\\" + txt_patientid.Text + txt_firstname.Text;
                        PC.Address = "";
                        PC.Email = txt_email.Text;
                        PC.Mobile = txt_mobile.Text;
                        PC.DiagInfo = txt_diagnosis.Text;
                        PC.CVisit = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
                        PC.LVisit = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
                        String Message = FNC.SavePatient(PC);
                        MessageBox.Show(Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        PatientVisitClass PVC = new PatientVisitClass();
                        PVC.Visitid = FNC.GenerateVisitID().ToString();
                        PVC.PName = txt_firstname.Text + " " + txt_lastname.Text;
                        PVC.PGender = gender;
                        PVC.MRNO = txt_patientid.Text;
                        PVC.PAge = txt_age.Text;
                        PVC.EDR = cmb_examining.Text;
                        PVC.Proce = txt_diagnosis.Text;
                        PVC.PDOB = txt_date.Text;
                        PVC.PatientFolder = CommanHelper.Cm_Spath + "\\" + txt_patientid.Text + txt_firstname.Text;
                        CaptuerScreen captuerScreen = new CaptuerScreen(PVC);
                        captuerScreen.Show();
                        this.Close();
                    }
                }
                else
                {
                    PatientClass PC = new PatientClass();
                    PC.PatientID = txt_patientid.Text;
                    PC.FirstName = txt_firstname.Text;
                    PC.LastName = txt_lastname.Text;
                    PC.DOB = txt_date.Text;
                    PC.Gender = gender;
                    PC.EDR = cmb_examining.Text.ToString();
                    PC.RDR = "";
                    PC.Archive = CommanHelper.Cm_Spath + "\\" + txt_patientid.Text + txt_firstname.Text;
                    PC.Address = "";
                    PC.Email = txt_email.Text;
                    PC.Mobile = txt_mobile.Text;
                    PC.DiagInfo = txt_diagnosis.Text;
                    PC.CVisit = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
                    PC.LVisit = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
                    String Message = FNC.UpdatePatient(PC);
                    MessageBox.Show(Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch(Exception ex) { }
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

        private void ARCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            gender = ARCheckbox.CheckedContent.ToString();
        }

        private void ARCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
             gender = ARCheckbox.CheckedContent.ToString();
        }

        private void txt_patientid_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txt_patientid.Text != "")
                {
                    bool status = FNC.GetPatienidStatus(txt_patientid.Text);
                    if (status == true) { var data = FNC.GetPatientinfo(txt_patientid.Text); 
                        if (data != null) { btn_save.Tag = "Update"; txt_firstname.Text = data.FirstName; txt_lastname.Text = data.LastName;
                            txt_date.Text = data.DOB; 
                            int dt = DateTime.Now.Year - DateTime.Parse(txt_date.Text).Year;
                            txt_age.Text = dt.ToString();
                            if (data.Gender == "Male") { ARCheckbox.UncheckedContent = data.Gender; } else { ARCheckbox.CheckedContent = data.Gender; }
                            cmb_examining.Text = data.EDR; txt_diagnosis.Text = data.DiagInfo; txt_mobile.Text = data.Mobile; txt_email.Text = data.Email; }
                       
                    }
                    else
                    {
                        txt_firstname.Text = string.Empty; txt_lastname.Text = string.Empty;
                        txt_date.Text = string.Empty;
                        txt_age.Text = string.Empty;
                        ARCheckbox.UncheckedContent = "Male";
                        cmb_examining.Text = string.Empty; txt_diagnosis.Text = string.Empty; txt_mobile.Text = string.Empty; txt_email.Text = string.Empty;
                    }
                }
              
            }
            catch { }
        }
    }
}
