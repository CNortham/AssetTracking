﻿using System;
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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class MainFrame : Page
    {
        // Connection string
        private string connectionString = @"Data Source=RICHARDP-PC\AMS;Initial Catalog=TestData;Integrated Security=True;";

        //getters and setters
        public int ColumnIndex { get; private set; }
        //initializze
        public string CellValue;


        //Initialise this shit
        SqlConnection con;
        SqlCommand cmd;
        //SqlCommand cmd2;   NOT USED
        DataTable dt;
        DataTable dt1;

        public MainFrame()
        {
            InitializeComponent();
            Console.WriteLine("test");
            FillGrid();
        }
        //Form Load Event
        private void FillGrid()
        {
            con = new SqlConnection(connectionString);
            con.Open();
            cmd = new SqlCommand("Select * from Account", con);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            CustomerDG.ItemsSource = dt.DefaultView;
        }
        //txt_SearchName TextChanged Event  
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            cmd = new SqlCommand("select * from Account where Customer_Name like '" + txtSearch.Text + "%'", con);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            CustomerDG.ItemsSource = dt.DefaultView;

        }
        //Outputs Customer_ID when row is selected
        private void CustomerDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell RowColumn = dataGrid.Columns[ColumnIndex].GetCellContent(row).Parent as DataGridCell;
            CellValue = ((TextBlock)RowColumn.Content).Text;

            //FILLs THE OTHER DG1 WITH TICKETS
            cmd = new SqlCommand("Select * from Tickets WHERE Customer_ID ='" + CellValue + "'", con);
            dt1 = new DataTable();
            dt1.Load(cmd.ExecuteReader());
            TicketDG.ItemsSource = dt1.DefaultView;
        }
        //ADD ASSET -Create Ticket Number and pass selected customer name
        private void btnCreateTicket_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // AddAsset frm = new AddAsset(lbl1.Content);
                CreateTicket page = new CreateTicket(CellValue.ToString());
                //page.Show();
                // frame.Content = new CreateTicket(CellValue.ToString());
                NavigationService.Navigate(page);



            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void CleanYourMess(object sender, System.ComponentModel.CancelEventArgs e)
        {
            con.Close();
        }
    }
}