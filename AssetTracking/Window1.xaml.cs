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
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace AssetTracking
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
      
        public Window1()
        {
            InitializeComponent();
            string connectionString = @"Data Source=RICHARDP-PC\AMS;Initial Catalog=TestData;Integrated Security=True;";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Select * from Asset_Table", con);
            con.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            con.Close();
            dtGrid.DataContext = dt;


            //  dtGrid.DataContext = dt;
        }

        public void getSerialNo()
        {
      
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            MainWindow frm = new MainWindow();
            frm.Show();
            this.Close();
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            getSerialNo();        
        }
    }
}
