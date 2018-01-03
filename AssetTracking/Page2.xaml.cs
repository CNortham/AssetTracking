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
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace AssetTracking
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();

            string connectionString = @"Data Source=RICHARDP-PC\AMS;Initial Catalog=TestData;Integrated Security=True;";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Select * from Asset_Table", con);
            con.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            con.Close();
            dtGridAss.DataContext = dt;

           // var instance = new SQLInformation();
            //instance.fillComboBox(@"Data Source=RICHARDP-PC\AMS;Initial Catalog=TestData;Integrated Security=True", System.Windows.Controls.ComboBox. , "select distinct [location] from [dbo].[locations]", null, "[location]");
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
