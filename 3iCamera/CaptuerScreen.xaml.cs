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
        public VideoCaptureDevice LocalWebCam;
        public FilterInfoCollection LoaclWebCamsCollection;
        private AForge.Video.DirectShow.VideoCapabilities[] videoCapabilities;
        private AForge.Video.DirectShow.VideoCapabilities[] snapshotCapabilities;
        string Eye = "OD"; string temp_path = ""; string resolution = ""; string _Mode = "";
        private string __checkvalue; 
        public string Checkvalue
        {
            get { return __checkvalue; }
            set { __checkvalue = value; }
        }
        public bool camera_status = false;
        PatientVisitClass PVC = new PatientVisitClass();
        FunctionalClass FNC = new FunctionalClass();
        public double PlayerWidth = 0;
        public CaptuerScreen(PatientVisitClass patientVisitClass,String Mode)
        {
            InitializeComponent();
            PlayerWidth = System.Windows.SystemParameters.PrimaryScreenWidth-410;
            try
            {
                Checkvalue = "OD";
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

                    if (patientVisitClass != null)
                    {
                        PVC.Visitid = FNC.GenerateVisitID().ToString();
                        PVC.MRNO = patientVisitClass.MRNO;
                        PVC.PName = patientVisitClass.PName;
                        PVC.PAge = patientVisitClass.PAge;
                        PVC.PGender = patientVisitClass.PGender;
                        PVC.VDate = patientVisitClass.VDate;
                        PVC.EDR = patientVisitClass.EDR;
                        PVC.Proce = patientVisitClass.Proce;
                        PVC.EEye = patientVisitClass.EEye;
                        PVC.Summary = patientVisitClass.Summary;
                        PVC.PatientFolder = patientVisitClass.PatientFolder + "\\" + PVC.Visitid + "-" + PVC.VDate;
                        if (!Directory.Exists(PVC.PatientFolder))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(PVC.PatientFolder);
                            di.CreateSubdirectory("Video");
                            di.CreateSubdirectory("Photo");
                            di.CreateSubdirectory("Dicom");
                        }
                        txt_age.Text = patientVisitClass.PAge;
                        txt_dob.Text = patientVisitClass.PDOB;
                        txt_mrno.Text = patientVisitClass.MRNO;
                        txt_patientname.Text = patientVisitClass.PName;
                    }
                    else
                    {
                        temp_path = CommanHelper.Cm_Spath + "\\temp";
                        if (!Directory.Exists(temp_path))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(temp_path);

                        }
                        Load_Image(temp_path);
                    }
                    var text = (ComboBoxItem)cmb_mode.SelectedItem;
                    Load_Camerasetting(text.Content.ToString());
                }
            }
            catch(Exception ex) { }
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
                videoresolution.Content = LocalWebCam.VideoResolution.FrameSize.Width + "x" + LocalWebCam.VideoResolution.FrameSize.Height;
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

        //Load CameraSetting
        public void Load_Camerasetting(string Mode)
        {
            CameraSettingClass Data = FNC.GetCameraSetting(Mode);
            if(Data.Mode!=null)
            {
                trc_gamma.Minimum = 72;
                trc_whitebalance.Minimum = 2800;
                //Video Property
                trc_backlight.Value = Data.BacklightCompensation;
                trc_brightness.Value = Data.Brightness;
                trc_contrast.Value = Data.Contrast;
                trc_gain.Value = Data.Gain;
                trc_gamma.Value = Data.Gamma;
                trc_hue.Value = Data.Hue;
                trc_saturation.Value = Data.Saturation;
                trc_sharpness.Value = Data.Sharpness;
                trc_whitebalance.Value = Data.WhiteBalance;

                txt_backlight.Text = Data.BacklightCompensation.ToString();
                txt_brightness.Text = Data.Brightness.ToString();
                txt_contrast.Text = Data.Contrast.ToString();
                txt_gain.Text = Data.Gain.ToString();
                txt_gamma.Text = Data.Gamma.ToString();
                txt_hue.Text = Data.Hue.ToString();
                txt_saturation.Text = Data.Saturation.ToString();
                txt_sharpness.Text = Data.Sharpness.ToString();
                txt_whitebalance.Text = Data.WhiteBalance.ToString();

                //Camera Property
                trc_exposure.Value = Data.Exposure;
                trc_focus.Value = Data.Focus;
                trc_iris.Value = Data.Iris;
                trc_pan.Value = Data.Pan;
                trc_roll.Value = Data.Roll;
                trc_tilt.Value = Data.Tilt;
                trc_zoom.Value = Data.Zoom;

                txt_exposure.Text = Data.Exposure.ToString();
                txt_focus.Text = Data.Focus.ToString();
                txt_iris.Text = Data.Iris.ToString();
                txt_pan.Text = Data.Pan.ToString();
                txt_roll.Text = Data.Roll.ToString();
                txt_tilt.Text = Data.Tilt.ToString();
                txt_zoom.Text = Data.Zoom.ToString();

              
            }
        }

        //Load Image Panel
        public void Load_Image(string Path)
        {
            try
            {    string[] array2 = Directory.GetFiles(Path, "*.jpg");

                ImageListPanel.Children.Clear();
                foreach (FileInfo s in array2.Select(f => new FileInfo(f)).OrderBy(f => f.CreationTime))
                {
                    UControl.ImageControl imageControl = new UControl.ImageControl();
                    imageControl.ImagePath = s.FullName;
                    imageControl.Width = 88;imageControl.Height = 88;
                    imageControl.Margin = new Thickness(2);
                    ImageListPanel.Children.Add(imageControl);
                }
            }
            catch (Exception ex) { }
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
               
                try
                {
                    if (_Mode == "Captuer")
                    {
                        image_snapshot_from_camera.Save(PVC.PatientFolder + "\\Photo\\" + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + "-" + Eye + ".jpg", ImageFormat.Jpeg);
                        Load_Image(PVC.PatientFolder + "\\Photo\\");
                    }
                    else
                    {
                        image_snapshot_from_camera.Save(temp_path + "\\" + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + "-" + Eye + ".jpg", ImageFormat.Jpeg);
                        Load_Image(temp_path + "\\");
                    }

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
                    resolution= LocalWebCam.VideoResolution.FrameSize.Width + "x" + LocalWebCam.VideoResolution.FrameSize.Height;
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
                    resolution="";
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
            if (Checkvalue.Contains("OD") == false)
            {
                Checkvalue += "OD";
              
            }
            Eye = "OD";
            txt_os.Foreground = System.Windows.Media.Brushes.White;
            txt_od.Foreground = System.Windows.Media.Brushes.Black;
            btn_od.IsDefault = true;
            btn_os.IsDefault = false;
        }

        private void btn_os_Click(object sender, RoutedEventArgs e)
        {
            if (Checkvalue.Contains("OS") == false)
            {
                Checkvalue += "OS";
             
            }
            Eye = "OS";
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

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PVC.EEye = Checkvalue;
                PVC.VDate= DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
                String Message = FNC.SavePatientVisit(PVC);
                MessageBox.Show(Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception ex) { }
        }

        private void btn_setting_Click(object sender, RoutedEventArgs e)
        {
            if(setting_grid.Width==0)
            {
                setting_grid.Width = 400;
                var text = (ComboBoxItem)cmb_mode.SelectedItem;
                Load_Camerasetting(text.Content.ToString());
            }
            else
            {
                setting_grid.Width = 0;
            }
        }

        private void btn_setting_close_Click(object sender, RoutedEventArgs e)
        {
            if (setting_grid.Width == 0)
            {
               
            }
            else
            {
                setting_grid.Width = 0;
            }
        }

       

        private void player_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            try
            {
                e.Graphics.DrawString(resolution, new Font("Arial", 14), System.Drawing.Brushes.LimeGreen, new PointF(float.Parse(PlayerWidth.ToString())-120,10));
            }
            catch(Exception ex) { }
        }

        private void cmb_mode_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
          
        }

        private void btn_settingsave_Click(object sender, RoutedEventArgs e)
        {
            CameraSettingClass CSC = new CameraSettingClass();
            CSC.Mode = cmb_mode.Text;
            CSC.BacklightCompensation =trc_backlight.Value;
            CSC.Brightness = trc_brightness.Value;
            CSC.Contrast = trc_contrast.Value;
            CSC.Gain = trc_gain.Value;
            CSC.Gamma = trc_gamma.Value;
            CSC.Hue = trc_hue.Value;
            CSC.Saturation = trc_saturation.Value;
            CSC.Sharpness = trc_sharpness.Value;
            CSC.WhiteBalance =trc_whitebalance.Value;
            CSC.Exposure = trc_exposure.Value;
            CSC.Focus = trc_focus.Value;
            CSC.Iris = trc_iris.Value;
            CSC.Pan = trc_pan.Value;
            CSC.Roll =trc_roll.Value;
            CSC.Tilt = trc_tilt.Value;
            CSC.Zoom =trc_zoom.Value;

            String Message = FNC.SaveCameraSetting(CSC);

            var text = (ComboBoxItem)cmb_mode.SelectedItem;
            Load_Camerasetting(text.Content.ToString());
        }

        private void trc_backlight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txt_backlight.Text = trc_backlight.Value.ToString();
        }

        private void trc_brightness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txt_brightness.Text = trc_brightness.Value.ToString();
        }

        private void trc_contrast_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txt_contrast.Text = trc_contrast.Value.ToString();
        }

        private void trc_gain_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txt_gain.Text = trc_gain.Value.ToString();
        }

        private void trc_gamma_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txt_gamma.Text = trc_gamma.Value.ToString();
        }

        private void trc_hue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txt_hue.Text = trc_hue.Value.ToString();
        }

        private void trc_saturation_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txt_saturation.Text = trc_saturation.Value.ToString();
        }

        private void trc_sharpness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txt_sharpness.Text = trc_sharpness.Value.ToString();
        }

        private void trc_whitebalance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           txt_whitebalance.Text = trc_whitebalance.Value.ToString();
        }

        private void trc_exposure_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txt_exposure.Text = trc_exposure.Value.ToString();
        }

        private void trc_focus_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txt_focus.Text = trc_focus.Value.ToString();
        }

        private void trc_iris_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txt_iris.Text = trc_iris.Value.ToString();
        }

        private void trc_pan_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txt_pan.Text = trc_pan.Value.ToString();
        }

        private void trc_roll_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txt_roll.Text = trc_roll.Value.ToString();
        }

        private void trc_tilt_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txt_tilt.Text = trc_tilt.Value.ToString();
        }

        private void trc_zoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txt_zoom.Text = trc_zoom.Value.ToString();
        }
    }
}
