using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for CreateTicket.xaml
    /// </summary>
    public partial class CreateTicket : Page
    {
        SqlCommand cmd;
        SqlConnection cs;
        SqlDataReader rdr;
        DataTable dt;

        //Getters And Setters
        public string myString { get; set; }

        // Connection string
        private string con = @"Data Source=RICHARDP-PC\AMS;Initial Catalog=TestData;Integrated Security=True;";
        //getters and setters
        public int ColumnIndex { get; private set; }
        private string CellValue;

        public CreateTicket(String Cellvalue)
        {
            //Create SQL connection and open it 
            cs = new SqlConnection(con);
            cs.Open();

            InitializeComponent();
            //Call on New Ticket ID
            uniqueValue();
            FillGrid();

            //Use Customer Key to find Selected Customer Name
            myString = Cellvalue;
            cmd = new SqlCommand("Select Customer_Name FROM Account Where Customer_ID='" + Cellvalue + "'", cs);
            rdr = cmd.ExecuteReader();
            rdr.Read();
            lbl.Content = rdr["Customer_Name"].ToString();
            rdr.Close();
        }
        private void uniqueValue()
        {
            //Create unique value and print to txtboxTicket
            cmd = new SqlCommand("Select Count(Ticket_ID) from Tickets", cs);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            i++;
            txtTicketNo.Text = i.ToString();
        }
        //Form Load Event
        private void FillGrid()
        {
            cmd = new SqlCommand("Select * from Return_Table", cs);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            ReturnDG.ItemsSource = dt.DefaultView;
        }
        private void ReturnDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell RowColumn = dataGrid.Columns[ColumnIndex].GetCellContent(row).Parent as DataGridCell;
            CellValue = ((TextBlock)RowColumn.Content).Text;

            lblReturnNo.Content = CellValue.ToString();
        }

        private void btnAddReturn_Click(object sender, RoutedEventArgs e)
        {
            AddAsset frm = new AddAsset(txtTicketNo.Text.ToString());
            frm.Show();
        }
        //Close connection when window is closed       <---- maybe a problem is wondows dont close.
        private void CleanYourMess(object sender, System.ComponentModel.CancelEventArgs e)
        {
            cs.Close();
        }

      
    }
}
