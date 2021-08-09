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

namespace _3iCamera.UControl
{
    /// <summary>
    /// Interaction logic for ImageControl.xaml
    /// </summary>
    public partial class ImageControl : UserControl
    {
        private string __imagepath;
        public string ImagePath
        {
            get { return __imagepath; }
            set { __imagepath = value; }
        }
        public ImageControl()
        {
            InitializeComponent();
           
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ImagePath.Contains("OD"))
                { txt_eye.Text = "OD"; }
                else { txt_eye.Text = "OS"; }
                thumb_image.Source = new BitmapImage(new Uri(ImagePath));
            }
            catch (Exception ex) { }
        }
    }
}
