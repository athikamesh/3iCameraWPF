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

namespace _3iCamera.UControl
{
    /// <summary>
    /// Interaction logic for SwitchTG.xaml
    /// </summary>
    public partial class SwitchTG : UserControl
    {
        private bool _ODChecked;
        public bool ODChecked
        {
            get { return _ODChecked; }
            set { _ODChecked = value; }
        }
        private string __checkvalue;
        public string Checkvalue
        {
            get { return __checkvalue; }
            set { __checkvalue = value; }
        }
        public SwitchTG()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Event to indicate UserControl Button Clicked
        /// </summary>
        public event EventHandler ODClicked;
        public event EventHandler OSClicked;
        /// <summary>
        /// Called to signal to subscribers that UserControl Button Clicked
        /// </summary>
        /// <param name="e"></param>
        protected virtual void ODOnClicked(EventArgs e)
        {
            EventHandler eh = ODClicked;
            if (eh != null)
            {
                eh(this, e);
            }
        }
        protected virtual void OSOnClicked(EventArgs e)
        {
            EventHandler eh = OSClicked;
            if (eh != null)
            {
                eh(this, e);
            }
        }
        private void btn_od_Click(object sender, RoutedEventArgs e)
        {
            
            Checkvalue = "OD";
        }

        private void btn_os_Click(object sender, RoutedEventArgs e)
        {
            Checkvalue = "OS";
        }
    }
}
