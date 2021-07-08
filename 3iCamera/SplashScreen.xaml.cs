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
        bool isBGWorking = false;
        BackgroundWorker bgWorker;
        int increase = 0; DispatcherTimer DS = new DispatcherTimer();
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
            if(increase==4)
            {             
                if (File.Exists(CommanHelper.databasepath)==true)
                {
                    txt_status.Text = "Database connected";
                }
            }
            if (increase >=10)
            {
                DS.Stop();
                this.Hide();
                bgWorker.CancelAsync();
                isBGWorking = false;
                MainWindow md = new MainWindow();
                md.ShowDialog();
               

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
              
            
           
        }
    }
}
