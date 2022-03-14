using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
using System.Windows.Threading;
using WPFSpark;

namespace _3iCamera
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        bool isBGWorking = false; bool camera_status = false;
        BackgroundWorker bgWorker;
        int increase = 0; DispatcherTimer DS = new DispatcherTimer();
        FunctionalClass FNC = new FunctionalClass();
        public FilterInfoCollection LoaclWebCamsCollection;
        public SplashScreen()
        {
            InitializeComponent();
            CommanHelper.databasepath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\DBFile\\3CameraDB.db";
      
            bgWorker = new BackgroundWorker();
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.WorkerReportsProgress = true;
            bgWorker.DoWork += new DoWorkEventHandler(DoWork);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnWorkCompleted);
            bgWorker.ProgressChanged += new ProgressChangedEventHandler(OnProgress);        
            
            DS.Tick += DS_Tick;
            DS.Interval = new TimeSpan(0,0,1);
            DS.Start();
        }

        private void DS_Tick(object sender, EventArgs e)
        {
            if(!isBGWorking)
            {
                bgWorker.RunWorkerAsync();
                isBGWorking = true;

            }
          

        }

        private void OnProgress(object sender, ProgressChangedEventArgs e)
        {
            
        }

        private void OnWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           
            isBGWorking = false;
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {

            increase++;
            Dispatcher.Invoke(() => { 
            if (increase == 2)
            {
                if (File.Exists(CommanHelper.databasepath) == true)
                {
                    txt_status.Text = "Database connected";
                }
            }
            if (increase == 4)
            {
                LoaclWebCamsCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (LoaclWebCamsCollection.Count > 0)
                {
                    var LocalWebCam = new VideoCaptureDevice(LoaclWebCamsCollection[CommanHelper.Cm_CameraId].Name);
                    if (LocalWebCam.Source == "HD USB Camera") { Pages.Dashpage.cam_Status = true; } else { Pages.Dashpage.cam_Status = false; }
                }
            }
            if (increase == 6)
            {
                if (File.Exists(CommanHelper.databasepath) == true)
                {
                    var DR = FNC.GetUtiltiy();
                    if (DR != null)
                    {
                        CommanHelper.Cm_CameraId = int.Parse(DR.CameraId.ToString());
                        CommanHelper.Cm_Devicename = DR.Devicename.ToString();
                        CommanHelper.Cm_VResolution = int.Parse(DR.VResolution.ToString());
                        CommanHelper.Cm_IResolution = int.Parse(DR.IResolution.ToString());
                        CommanHelper.Cm_AspectRatio = Convert.ToBoolean(DR.AspectRatio.ToString());
                        CommanHelper.Cm_Spath = DR.Spath.ToString();
                        CommanHelper.Cm_Mirror = Convert.ToBoolean(DR.Mirror.ToString());
                    }
                    else
                    {
                        Pages.Utility UP = new Pages.Utility();
                        Toolwindows TW = new Toolwindows(UP);
                        TW.ShowDialog();
                    }
                }
            }
            if (increase == 8)
            {
                if (CommenMethod.Cleantemp() == false) { MessageBox.Show("Path not read", "Alert", MessageBoxButton.OK); }
            }
            if (increase >= 10)
            {
                DS.Stop();
                this.Hide();
                bgWorker.CancelAsync();
                isBGWorking = false;
                MainWindow md = new MainWindow(camera_status);
                md.ShowDialog();


            }
            });

        }
    }
}
