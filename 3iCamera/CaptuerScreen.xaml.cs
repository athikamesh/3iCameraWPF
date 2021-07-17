using AForge.Imaging.Filters;
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
        private AForge.Video.DirectShow.VideoCapabilities[] snapshotCapabilities;
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
            if (LoaclWebCamsCollection.Count == 0)
            {
                MessageBox.Show("Device Not Found..!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                btn_start.IsEnabled = false;
                btn_start_recording.IsEnabled = false;
                btn_stop_recording.IsEnabled = false;
                btn_snap.IsEnabled = false;
                btn_save.IsEnabled = false;
                btn_analysis.IsEnabled = false;
                btn_fullscreen.IsEnabled = false;
                
            }
            else
            {
                LocalWebCam = new VideoCaptureDevice(LoaclWebCamsCollection[CommanHelper.Cm_CameraId].MonikerString);
                videoCapabilities = LocalWebCam.VideoCapabilities;
                snapshotCapabilities = LocalWebCam.SnapshotCapabilities;
                LocalWebCam.VideoResolution = videoCapabilities[CommanHelper.Cm_VResolution];
                LocalWebCam.ProvideSnapshots = true;
                LocalWebCam.NewFrame += new NewFrameEventHandler(Cam_NewFrame);
                if (snapshotCapabilities.Count() > 0)
                {
                    LocalWebCam.SnapshotResolution = snapshotCapabilities[CommanHelper.Cm_IResolution];
                    LocalWebCam.SnapshotFrame += new NewFrameEventHandler(videoDevice_SnapshotFrame);
                }                
              
                btn_od.IsDefault = true;

                btn_od_Click(null, null);
            }
        }
        void Cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                //Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
                //Mirror filter = new Mirror(false, true);
                //bmp = filter.Apply(bmp);
                //System.Drawing.Image img = bmp;
                //MemoryStream ms = new MemoryStream();
                //img.Save(ms, ImageFormat.Bmp);
                //ms.Seek(0, SeekOrigin.Begin);
                //BitmapImage bi = new BitmapImage();
                //bi.BeginInit();
                //bi.StreamSource = ms;
                //bi.EndInit();
                //bi.Freeze();
                //Dispatcher.BeginInvoke(new ThreadStart(delegate
                //{
                //    frameHolder.Source = bi;
                //}));
                if (CommanHelper.Cm_Mirror == true)
                {
                    eventArgs.Frame.RotateFlip(RotateFlipType.Rotate180FlipY);
                }
                else { eventArgs.Frame.Clone(); }
            }
            catch (Exception ex)
            {
            }
        }

        // New snapshot frame is available
        private void videoDevice_SnapshotFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                Console.WriteLine(eventArgs.Frame.Size);
                if (CommanHelper.Cm_Mirror == true)
                {
                    eventArgs.Frame.RotateFlip(RotateFlipType.Rotate180FlipY);
                }
                Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
                ShowSnapshot(bmp);
            }
            catch(Exception ex) { }

        }

        private void ShowSnapshot(Bitmap image_snapshot_from_camera)
        {
            if (!Dispatcher.CheckAccess())
            {

                Dispatcher.Invoke(new Action<Bitmap>(ShowSnapshot), image_snapshot_from_camera);
            }
            else
            {
                string path = CommanHelper.Cm_Spath;
                try
                {

                    image_snapshot_from_camera.Save(path + "\\NewCametra-" + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + "-" + gen + ".jpg", ImageFormat.Jpeg);

                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Warning"); }


            }
        }

        private void Btn_start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (camera_status == false)
                {
                    player.VideoSource = LocalWebCam;
                    LocalWebCam.Start();
                    player.Start();
                    videoresolution.Content = LocalWebCam.VideoResolution.FrameSize.Width + "x" + LocalWebCam.VideoResolution.FrameSize.Height;
                    txt_stso.Text = "Stop Camera";
                    camera_status = true;
                    btn_back.IsEnabled = false;
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
                    btn_back.IsEnabled = true;
                    player.VideoSource = null;
                    LocalWebCam.Stop();
                    player.Stop();
                    GC.Collect();

                }
            }
            catch(Exception ex) { }
            
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

        private void btn_snap_Click(object sender, RoutedEventArgs e)
        {
            if ((LocalWebCam != null) && (LocalWebCam.ProvideSnapshots))
            {
                LocalWebCam.SimulateTrigger();

            }
        }
    }
}
