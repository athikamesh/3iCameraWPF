using AForge.Video.DirectShow;
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
    /// Interaction logic for Utility.xaml
    /// </summary>
    public partial class Utility : Page
    {
        private AForge.Video.DirectShow.FilterInfoCollection videoDevices;
        private AForge.Video.DirectShow.VideoCaptureDevice videoDevice;
        private AForge.Video.DirectShow.VideoCapabilities[] videoCapabilities;
        private AForge.Video.DirectShow.VideoCapabilities[] snapshotCapabilities;
        public Utility()
        {
            InitializeComponent();           
            intiaHdcamera();
        }
        void intiaHdcamera()
        {
            try
            {
                videoDevices = new AForge.Video.DirectShow.FilterInfoCollection(AForge.Video.DirectShow.FilterCategory.VideoInputDevice);
                for (int i = 0; i <= videoDevices.Count - 1; i++)
                {
                    cmb_cameralist.Items.Add(videoDevices[i].Name);
                }
                //DataSet dt = new DataSet();
                //dt.ReadXml(DBClass.path);
                //DataRow resultRow = dt.Tables[0].AsEnumerable().FirstOrDefault();
                //cmb_cameralist.Text = resultRow.Field<String>("DeviceName");
                //txtpath.Text = resultRow.Field<String>("StoragePath");
                //if (videoCapabilities.Count() > 0 && videoCapabilities != null && cmb_PRVRES.Items.Count >= Convert.ToInt16(resultRow.Field<string>("PResolution").ToString()))
                //{
                //    cmb_PRVRES.SelectedIndex = Convert.ToInt16(resultRow.Field<string>("PResolution").ToString());
                //}
                //if (snapshotCapabilities.Count() > 0 && snapshotCapabilities != null && cmb_STLRES.Items.Count >= Convert.ToInt16(resultRow.Field<string>("PResolution").ToString()))
                //{
                //    cmb_STLRES.SelectedIndex = Convert.ToInt16(resultRow.Field<string>("SResolution").ToString());
                //}
                //chk_aspect.Checked = Convert.ToBoolean(resultRow.Field<String>("ScreenAspect").ToString());
               
            }
            catch { }
        }
        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {

        }
        // Collect supported video and snapshot sizes
        private void EnumeratedSupportedFrameSizes(AForge.Video.DirectShow.VideoCaptureDevice videoDevice)
        {
            this.Cursor = Cursors.Wait;

            cmb_videoresolution.Items.Clear();
            cmb_imageresolution.Items.Clear();

            try
            {
                videoCapabilities = videoDevice.VideoCapabilities;
                snapshotCapabilities = videoDevice.SnapshotCapabilities;


                foreach (VideoCapabilities capabilty in videoCapabilities)
                {
                    cmb_videoresolution.Items.Add(string.Format("{0} x {1} ({2})", capabilty.FrameSize.Width, capabilty.FrameSize.Height, capabilty.MaximumFrameRate));
                }
                if (snapshotCapabilities.Count() > 0)
                {
                    txb_image_resolution.Text = "";
                    foreach (VideoCapabilities capabilty in snapshotCapabilities)
                    {
                        cmb_imageresolution.Items.Add(string.Format("{0} x {1}", capabilty.FrameSize.Width, capabilty.FrameSize.Height));
                    }
                }
                else
                {
                    txb_image_resolution.Text = "(Still Pin Not Available)";
                    foreach (VideoCapabilities capabilty in videoCapabilities)
                    {
                        cmb_imageresolution.Items.Add(string.Format("{0} x {1}", capabilty.FrameSize.Width, capabilty.FrameSize.Height));
                    }
                }

                if (videoCapabilities.Length == 0)
                {
                    cmb_videoresolution.Items.Add("Not supported");
                }
                //if (snapshotCapabilities.Length == 0)
                //{
                //    cmb_imageresolution.Items.Add("Not supported");
                //}

                cmb_videoresolution.SelectedIndex = 0;
                cmb_imageresolution.SelectedIndex = 0;
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        private void cmb_cameralist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            videoDevice = new AForge.Video.DirectShow.VideoCaptureDevice(videoDevices[cmb_cameralist.SelectedIndex].MonikerString);
            EnumeratedSupportedFrameSizes(videoDevice);
        }
    }
}
