using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace AssetTracking
{
    /// <summary>
    /// Interaction logic for CreateTicket.xaml
    /// </summary>
    public partial class ViewTicket : Page
    {
        DataTable dt;

        // Connection string
       // private string con = @"Data Source=RICHARDP-PC\AMS;Initial Catalog=TestData;Integrated Security=True;";
        //getters and setters used for getting the customer id of the selected cell in the data table
        public int ColumnIndex { get;  set; }
        //Used to get the reurnID in the data grid event-Selection_change
        public string returnNo;
        //pass customer ID to my add asset button
        public string CustomerId { get; set; }
        private Int32 TicketId = 0;

        public ViewTicket(String customerID)
        {
            InitializeComponent();       
            
            //Use Customer Key to find Selected Customer Name
            Global.cmd = new SqlCommand("Select Customer_Name FROM Account Where Customer_ID='" + customerID + "'", Global.cs);
            Global.rdr = Global.cmd.ExecuteReader();
            Global.rdr.Read();
            lbl.Content = Global.rdr["Customer_Name"].ToString();
            Global.rdr.Close();

            //Create ticketID value and print to txtboxTicket
            Global.cmd = new SqlCommand("Select * from Tickets WHERE Customer_ID='" + customerID + "'", Global.cs);
            int ticket_ID = Convert.ToInt32(Global.cmd.ExecuteScalar());
            ticket_ID++;
            txtTicketNo.Text = ticket_ID.ToString();
            TicketId = ticket_ID;
            CustomerId = customerID;
            //call on method that gets returns and pass it the ticket ID
            FillGrid(TicketId, CustomerId);
        }
       
        //Fill dataGRid
        void FillGrid(int ticketID_Passed, String CustomerID_Passed)
        {
            Global.cmd = new SqlCommand("Select * from Return_Table where Ticket_ID='"+ ticketID_Passed +"'", Global.cs);
            dt = new DataTable();
            dt.Load(Global.cmd.ExecuteReader());
            ReturnDG.ItemsSource = dt.DefaultView;


        }
        //Returns the selected return number from the datagrid and puts in textBox
        private void ReturnDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell RowColumn = dataGrid.Columns[ColumnIndex].GetCellContent(row).Parent as DataGridCell;
            returnNo = ((TextBlock)RowColumn.Content).Text;
           
            txtReturnNo.Text = returnNo.ToString();
        }
        //opens addAsset Window, passes ticket number ID
        private void btnAddReturn_Click(object sender, RoutedEventArgs e)
        {     
            AddAsset frm = new AddAsset(txtTicketNo.Text.ToString(), lbl.Content.ToString(), CustomerId );
            frm.Show();
        }
        private void btnSubmitTicket_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
