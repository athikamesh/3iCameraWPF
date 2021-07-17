using Microsoft.Win32;
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
    /// Interaction logic for Reportpage.xaml
    /// </summary>
    public partial class Reportpage : Page
    {
        BitmapImage logo = new BitmapImage();
        ReportSettingClass RSC = new ReportSettingClass();
        FunctionalClass FNC = new FunctionalClass();
        
        public Reportpage()
        {
            InitializeComponent();
            var data = FNC.GetReportSetting();
            if(data.ReportType==null)
            {
                btn_save.Tag = "Save";
            }
            else
            {
                RTCheckbox.IsChecked = Convert.ToBoolean(data.ReportType);
                txt_address.Text = data.Address;
                txt_doctorname.Text = data.Doctorname;
                txt_hspname.Text = data.Hospitalname;
                txt_mobno.Text = data.Mobile;
                txt_telno.Text = data.Phone;
                txt_time.Text = data.Time;
                txt_email.Text = data.Emailid;
                logo = Image_Convertion.ToBitmapImage(data.Logo);
                imglogo.Source = logo;
                btn_save.Tag = "Update";
            }
        }

        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string indication = ((Button)sender).Tag.ToString();
                if (logo != null && txt_hspname.Text != "" && txt_doctorname.Text != "" && txt_address.Text != "" && txt_telno.Text != "" && txt_mobno.Text != "" && txt_email.Text != "" && txt_time.Text != "")
                {
                    if (indication == "Save")
                    {
                        RSC = new ReportSettingClass();
                        RSC.ReportType = RTCheckbox.IsChecked.ToString();
                        RSC.Hospitalname = txt_hspname.Text;
                        RSC.Doctorname = txt_doctorname.Text;
                        RSC.Address = txt_address.Text;
                        RSC.Phone = txt_telno.Text;
                        RSC.Mobile = txt_mobno.Text;
                        RSC.Emailid = txt_email.Text;
                        RSC.Time = txt_time.Text;
                        RSC.Logo = Image_Convertion.ImagetoByte(logo);
                        var Message = FNC.SaveReportSetting(RSC);
                        MessageBox.Show(Message, "Information Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        RSC = new ReportSettingClass();
                        RSC.ReportType = RTCheckbox.IsChecked.ToString();
                        RSC.Hospitalname = txt_hspname.Text;
                        RSC.Doctorname = txt_doctorname.Text;
                        RSC.Address = txt_address.Text;
                        RSC.Phone = txt_telno.Text;
                        RSC.Mobile = txt_mobno.Text;
                        RSC.Emailid = txt_email.Text;
                        RSC.Time = txt_time.Text;
                        RSC.Logo = Image_Convertion.ImagetoByte(logo);
                        var Message = FNC.UpdateReportSetting(RSC);
                        MessageBox.Show(Message, "Information Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex) { }
        }

        
        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch(Exception ex) { }
        }

        private void imglogo_Drop(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);              
                logo.BeginInit();
                logo.UriSource = new Uri(files[0].ToString());
                logo.EndInit();
                imglogo.Source = logo;
            }
        }

        private void btn_browse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create OpenFileDialog 
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



                // Set filter for file extension and default file extension 
                dlg.DefaultExt = ".png";
                dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


                // Display OpenFileDialog by calling ShowDialog method 
                Nullable<bool> result = dlg.ShowDialog();


                // Get the selected file name and display in a TextBox 
                if (result == true)
                {
                    // Open document 
                    string filename = dlg.FileName;
                    logo.BeginInit();
                    logo.UriSource = new Uri(filename);
                    logo.EndInit();
                    imglogo.Source = logo;
                }
            }
            catch (Exception ex) { }
        }
    }
}
