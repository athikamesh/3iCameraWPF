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
using System.Windows.Shapes;

namespace _3iCamera
{
    /// <summary>
    /// Interaction logic for Toolwindows.xaml
    /// </summary>
    public partial class Toolwindows : Window
    {
        public Toolwindows(Page page)
        {
            InitializeComponent();
            
            frameholder.Navigate(page);
            this.Width = page.Width;
            this.Height = page.Height;
            string title= page.Title.Replace("page", "");
            lbl_title.Content = "Toolswindow -" + title.Replace("page","");
            lbl_title.Content = "Toolswindow -" + title.Replace("list", " List");
        }

        private void Btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
