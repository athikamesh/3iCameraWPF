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
    /// Interaction logic for Doctorlist.xaml
    /// </summary>
    public partial class Doctorlist : Page
    {
        List<DoctorClass> DoctorList = new List<DoctorClass>();
        FunctionalClass FCN = new FunctionalClass();
        public Doctorlist()
        {
            InitializeComponent();
            LoadGrid();
        }
        void LoadGrid()
        {
            DoctorList = FCN.ListDoctor();
            Searchgrid.ItemsSource = DoctorList;
        }
        private void SearchGridScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void editbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object item = Searchgrid.SelectedItem;
                int Sno = int.Parse((Searchgrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                Pages.Doctorpage UP = new Pages.Doctorpage(Sno);
                Toolwindows TW = new Toolwindows(UP);
                TW.ShowDialog();
                LoadGrid();
            }
            catch (Exception ex) { }
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object item = Searchgrid.SelectedItem;
                int Sno = int.Parse((Searchgrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                MessageBoxResult MBR = MessageBox.Show("Do you want delete this doctor?", "Confirm Message", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (MBR == MessageBoxResult.Yes)
                {
                    String Message = FCN.RemoveDoctor(Sno);
                    MessageBox.Show(Message, "Information Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadGrid();
                }
            }
            catch(Exception ex) { }
        }

        private void Searchgrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        void OnChecked(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
