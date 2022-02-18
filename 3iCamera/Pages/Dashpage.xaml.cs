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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace _3iCamera.Pages
{
    /// <summary>
    /// Interaction logic for Dashpage.xaml
    /// </summary>
    public partial class Dashpage : Page
    {
        public static bool cam_Status = false;
        FunctionalClass FNC = new FunctionalClass();
        DispatcherTimer timer = new DispatcherTimer();
        BackgroundWorker workerL1 = new BackgroundWorker();
        int Previouscount = 0, Currentcount = 0;
        public Dashpage(bool camerastatus)
        {
            InitializeComponent();
            setgridsize();
            cam_Status = camerastatus;
            workerL1.DoWork += WorkerL1_DoWork;
            workerL1.RunWorkerCompleted += WorkerL1_RunWorkerCompleted;
        
            timer.Interval = TimeSpan.FromSeconds(0.2);
            timer.Tick += timer_Tick;
            timer.Start();

         
        }

      
        void ChangeCameraStatus()
        {
            if(cam_Status==false)
            {
                btn_live.IsEnabled = false;
                btn_captuer.IsEnabled = false;
                btn_patient.IsEnabled = false;
            }
            else
            {
                btn_live.IsEnabled = true;
                btn_captuer.IsEnabled = true;
                btn_patient.IsEnabled = true;
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!workerL1.IsBusy)
                {
                    workerL1.RunWorkerAsync();
                    
                }
              
            }
            catch(Exception ex) { }
        }

        private void WorkerL1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           // workerL1.CancelAsync();
        }

        private void WorkerL1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                LoadGrid();
            }
            catch(Exception ex) { }
        }

        private void SearchGridScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
           
                ScrollViewer scv = (ScrollViewer)sender;
                scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
                e.Handled = true;
           
        }

        private void Btn_live_Click(object sender, RoutedEventArgs e)
        {
            CaptuerScreen CS = new CaptuerScreen(null,"live");
            CS.ShowDialog();
        }

        void setgridsize()
        {
           
            double scrwd= System.Windows.SystemParameters.PrimaryScreenWidth;
            scrwd -= 200;
            mrnoColumn.Width = (scrwd / 100) * 10;
            patientnameColumn.Width = (scrwd / 100) * 20;
            ageColumn.Width = (scrwd / 100) * 5;
            genderColumn.Width = (scrwd / 100) * 10;
            vdateColumn.Width = (scrwd / 100) * 14;
            edoctorColumn.Width = (scrwd / 100) * 13;
            diagnosisColumn.Width = (scrwd / 100) * 10;
            eyeColumn.Width = (scrwd / 100) * 5;
            summarycolumn.Width = (scrwd / 100) * 10;
           
        }

        void LoadGrid()
        {
            try
            {
                Dispatcher.Invoke(() => {
                    // Code causing the exception or requires UI thread access
                    var data = FNC.GetPatientDetail();
                    if(Previouscount==0)
                    {
                        Previouscount = data.Count;
                        Searchgrid.ItemsSource = data;
                    }
                    Currentcount = data.Count;
                    if (Previouscount > data.Count)
                    {
                        Searchgrid.ItemsSource = data;
                        Previouscount = Currentcount;
                    }
                   
                });
               
            }
            catch(Exception ex) { }
        }

        private void Searchgrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void Searchgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var det = Searchgrid.SelectedItem;
                if(det!=null)
                {

                }
            }
            catch { }
        }

        private void btn_patient_Click(object sender, RoutedEventArgs e)
        {
            PatientWindow PW = new PatientWindow();
            PW.ShowDialog();
            LoadGrid();
        }
    }
}
