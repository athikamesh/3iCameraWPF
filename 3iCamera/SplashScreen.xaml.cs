using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        bool isBGWorking = false;
        BackgroundWorker bgWorker;
        public SplashScreen()
        {
            InitializeComponent();
            bgWorker = new BackgroundWorker();
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.WorkerReportsProgress = true;
            bgWorker.DoWork += new DoWorkEventHandler(DoWork);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnWorkCompleted);
            bgWorker.ProgressChanged += new ProgressChangedEventHandler(OnProgress);
        }

        private void OnProgress(object sender, ProgressChangedEventArgs e)
        {
            
        }

        private void OnWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            
        }
    }
}
