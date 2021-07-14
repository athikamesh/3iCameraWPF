using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
    /// Interaction logic for testform.xaml
    /// </summary>
    public partial class testform : Window
    {
        public testform()
        {
            InitializeComponent();
            loadimage();
           
        }
        void loadimage()
        {
            Bitmap bmp = new Bitmap(@"E:\VideoRecording\001Rashid\1023-04-11-2019\Photo\2020-01-24 01-03-04-OD.jpg");
            //Mirror filter = new Mirror(false, true);
            //filter.Apply(bmp);
            System.Drawing.Image img = bmp;
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = ms;
            bi.EndInit();
            bi.Freeze();

           
                img1.Source = bi;
            
        }
        void loadFilpimage()
        {
            Bitmap bmp = new Bitmap(@"E:\VideoRecording\001Rashid\1023-04-11-2019\Photo\2020-01-24 01-03-04-OD.jpg");
            Mirror filter = new Mirror(false, true);
            bmp= filter.Apply(bmp);
            System.Drawing.Image img = bmp;
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = ms;
            bi.EndInit();
            bi.Freeze();
            img2.Source = bi;

        }

        private void btn_flip_Click(object sender, RoutedEventArgs e)
        {
            loadFilpimage();
        }
    }
}
