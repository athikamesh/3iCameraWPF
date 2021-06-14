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
        public Reportpage()
        {
            InitializeComponent();
        }

        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void imglogo_Drop(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                logo.UriSource = new Uri(files[0].ToString());
                logo.EndInit();
                imglogo.Source = logo;
            }
        }
    }
}
