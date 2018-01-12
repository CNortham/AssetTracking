using System;
using System.Data.SqlClient;
using System.Windows;


namespace AssetTracking
{
    /// <summary>
    /// Interaction logic for AddAsset.xaml
    /// </summary>
    public partial class AddAsset : Window
    {
        public string ticketIDScope { get; set; }
        public string CustomerNameScope { get; set; }
        public string CustomerIDScope { get; set; }

      

        public AddAsset(string ticketID, string customerName, string customerID)
        {
            //Create SQL connection and open it 
            Global.cs = new SqlConnection(Global.con);
            Global.cs.Open();

            InitializeComponent();
            //set Scope for other methods
            ticketIDScope = ticketID;
            CustomerNameScope = customerName;
            CustomerIDScope = customerID;
            //Call for New Return ID pass the ticket ID to count no of returns for this ticket
            uniqueValue(ticketID);
            //Display value of objects passed to this class
            lbl.Content = lbl.Content + ticketID;
            lblCustomerName.Content = lblCustomerName.Content + customerName;
        }
        private void AddAsset_Load(object sender, EventArgs e)
        {
            //Need to create list 
            cmbModel.Items.Clear();
            try { } catch (Exception) { MessageBox.Show("Can not open connection ! "); }
        }
        private void uniqueValue(string ticketID)
        {
            //Create unique value and print to txtboxTicket
            Global.cmd = new SqlCommand("Select Count(Return_ID) from Return_Table Where Ticket_ID='" + ticketID + "'", Global.cs);
            int i = Convert.ToInt32(Global.cmd.ExecuteScalar());
            i++;
            txtReturnNo.Text = i.ToString();
        }
        private void btnSearch_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                //convert search textbox to string variable
                string searchValue = txtSerial.Text;
                //Get ISSI
                Global.cmd = new SqlCommand("Select ISSI from Asset_Table where Serial_ID ='" + searchValue + "'", Global.cs);
                txtISSI.Text = Global.cmd.ExecuteScalar().ToString();
                //Get TEI
                Global.cmd = new SqlCommand("Select TEI from Asset_Table where Serial_ID ='" + searchValue + "'", Global.cs);
                txtTEI.Text = Global.cmd.ExecuteScalar().ToString();
                //Get Date
                /*  cmd = new SqlCommand("Select Date from Asset_Table where Serial_ID ='" + searchValue + "'", cs);
                    txtDate.Text = cmd.ExecuteScalar().ToString();*/
                //Get Decription
                Global.cmd = new SqlCommand("Select AssetDescription from Asset_Table where Serial_ID ='" + searchValue + "'", Global.cs);
                txtAssetDescription.Text = Global.cmd.ExecuteScalar().ToString();
            }catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            try
            {/*  LEAVE THIS UNTIL YOU CAN WORK LISTS OUT PROPERLY
                //Get Model
                cmd = new SqlCommand("Select Model from Asset_Table where Serial_ID ='" + searchValue + "'", cs);
                cmbModel.add = cmd.ExecuteScalar().ToString();

                //Get And SEt Status
                cmd = new SqlCommand("Select Status from Asset_Table where Serial_ID ='" + searchValue + "'", cs);
                cmbstatus.Text = cmd.ExecuteScalar().ToString();*/
            }catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //UPADTE:>Asset   ISSI, TEI, Date, Asset Description, Model

                //INSERT:>Return  Problem Description, Resolution, status, Return ID, Customer_ID
                Global.cmd = new SqlCommand("INSERT INTO Return_Table (Return_ID, Ticket_ID, Serial_ID, Issue, Resolution) Values('" + txtReturnNo.Text + "','" + ticketIDScope + "','" + txtSerial.Text + "','" + txtReported.Text + "','" + txtResult.Text + "')", Global.cs);
                Global.cmd.ExecuteNonQuery();
                MessageBox.Show("Return Added to " + CustomerNameScope);
                Application.Current.Windows[1].Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //CATCH:  if return not saved, delete the data just inserted        !!!!!UPDATED DATA MIGHT HAVE TO BE CHANGED OR LOCKED!!!!!!!!!!
            }
        }
    }
}
