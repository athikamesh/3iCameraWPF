using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for CaptuerScreen.xaml
    /// </summary>
    public partial class CaptuerScreen : Window
    {
        VideoCaptureDevice LocalWebCam;
        public FilterInfoCollection LoaclWebCamsCollection;
        private AForge.Video.DirectShow.VideoCapabilities[] videoCapabilities;
        string gen = "";
        private string __checkvalue;
        public string Checkvalue
        {
            get { return __checkvalue; }
            set { __checkvalue = value; }
        }
        public bool camera_status = false;
        public CaptuerScreen()
        {
            InitializeComponent();

           
            LoaclWebCamsCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            LocalWebCam = new VideoCaptureDevice(LoaclWebCamsCollection[0].MonikerString);
            videoCapabilities = LocalWebCam.VideoCapabilities;
            LocalWebCam.VideoResolution = videoCapabilities[0];
            LocalWebCam.NewFrame += new NewFrameEventHandler(Cam_NewFrame);
            
            btn_od.IsDefault = true;
           
            btn_od_Click(null, null);
        }
        void Cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                System.Drawing.Image img = (Bitmap)eventArgs.Frame.Clone();

                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = ms;
                bi.EndInit();

                bi.Freeze();
                Dispatcher.BeginInvoke(new ThreadStart(delegate
                {
                    frameHolder.Source = bi;
                }));
            }
            catch (Exception ex)
            {
            }
        }

        private void Btn_start_Click(object sender, RoutedEventArgs e)
        {
            if (camera_status == false)
            {
                LocalWebCam.Start();
                videoresolution.Content = LocalWebCam.VideoResolution.FrameSize.Width + "x" + LocalWebCam.VideoResolution.FrameSize.Height;
                txt_stso.Text = "Stop Camera";
                camera_status=true;
            }
            else
            {
                camera_status = false;
                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                logo.UriSource = new Uri("pack://application:,,,/Images/PVS2.png");
                logo.EndInit();              
                LocalWebCam.SignalToStop();
                LocalWebCam.WaitForStop();                
                txt_stso.Text = "Start Camera";                
                GC.Collect();
                frameHolder.Source = logo;
            }
            
        }

        private void Btn_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_od_Click(object sender, RoutedEventArgs e)
        {
            Checkvalue = "OD";
            txt_os.Foreground = System.Windows.Media.Brushes.White;
            txt_od.Foreground = System.Windows.Media.Brushes.Black;
            btn_od.IsDefault = true;
            btn_os.IsDefault = false;
        }

        private void btn_os_Click(object sender, RoutedEventArgs e)
        {
            Checkvalue = "OS";
            txt_os.Foreground = System.Windows.Media.Brushes.Black;
            txt_od.Foreground = System.Windows.Media.Brushes.White;
            btn_os.IsDefault = true;
            btn_od.IsDefault = false;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
          
        }
    }
}
