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
using System.Windows.Shapes;

namespace AssetTracking
{
    /// <summary>
    /// Interaction logic for AddAsset.xaml
    /// </summary>
    public partial class AddAsset : Window
    {

        SqlCommand cmd;
        SqlConnection cs;
        SqlDataReader rdr;
        
        //Getters And Setters
        public string myString { get; set; }

        // Connection string
        private string con = @"Data Source=RICHARDP-PC\AMS;Initial Catalog=TestData;Integrated Security=True;";

        public AddAsset(string Cellvalue)
        {
            
            //Create SQL connection and open it 
            cs = new SqlConnection(con);
            cs.Open();
            
            InitializeComponent();
            //Call on New Return ID
            uniqueValue();
            
            //Object is Ticket number - passed from Create Ticket Page
            myString = Cellvalue;
            lbl.Content = lbl.Content + myString;

            //load customer name into thing with number preferable    ---< why again???
        }
        private void uniqueValue()
        {           
            //Create unique value and print to txtboxTicket
            cmd = new SqlCommand("Select Count(Return_ID) from Return_Table", cs);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            i++;
            txtReturnNo.Text = i.ToString();
            
        }
        //Close connection when window is closed       <---- maybe a problem is wondows dont close.
        private void CleanYourMess(object sender, System.ComponentModel.CancelEventArgs e)
        {
            cs.Close();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            cmd = new SqlCommand("Select ISSI FROM Asset_Table where Serial_ID = '"+txtSerial.Text +"'", cs);
            rdr=  cmd.ExecuteReader();
           // String ding = rdr.GetString("ISSI").ToString();
            txtISSI.Text = rdr.ToString();
        }
    }
}
