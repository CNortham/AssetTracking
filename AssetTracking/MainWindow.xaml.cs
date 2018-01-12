using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace AssetTracking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

//#warning Initialize SQL Connection Here

            //Create SQL connection and open it 
            Global.cs = new SqlConnection(Global.con);
            Global.cs.Open();


            frame1.Content = new MainFrame();           
        }
        private void CreatTicketbtn_Click(object sender, RoutedEventArgs e)
        {
        }
        private void CleanYourMess(object sender, System.ComponentModel.CancelEventArgs e)
        {
           // con.Close();
        }

        private void OnClosingEvent(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Global.cs.Close();
        }
    }
}
