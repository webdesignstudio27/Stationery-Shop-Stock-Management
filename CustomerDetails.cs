using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STATIONERY_SHOP
{
    public partial class CustomerDetails : Form
    {
        public CustomerDetails()
        {
            InitializeComponent();
        }
         SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFileName=C:\Users\sivas\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\STATIONERYDB.mdf;Initial Catalog=STATIONERYDB;Integrated Security=True;Connect Timeout=30;");

        public void PerformSearch(string salesId)
        {
            // Base query to select data from BillDetailsTbl
            string searchQuery = @"
                SELECT SalesId, ItemNo, ProductName, Quantity, Price, Total
                FROM BillDetailsTbl
                WHERE 1 = 1";

            // Optional: Constructing dynamic conditions based on user input
            string condition = "";

            // Example: Search by SNum (SalesId)
            if (!string.IsNullOrWhiteSpace(salesId))
            {
                condition += " AND SalesId = @SNum";
            }

            // Combine conditions if there are any
            searchQuery += condition;

            using (SqlConnection con = new SqlConnection(Con.ConnectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(searchQuery, con);

                // Add parameters to the adapter
                if (!string.IsNullOrWhiteSpace(salesId))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@SNum", int.Parse(salesId));
                }

                DataSet ds = new DataSet();
                try
                {
                    adapter.Fill(ds);

                    // Check if any rows were returned
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        SalesDGV.DataSource = ds.Tables[0];
                    }
                    else
                    {
                        // Clear the DataGridView if no results found
                        SalesDGV.DataSource = null;
                        MessageBox.Show("No results found matching the criteria.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching data: {ex.Message}");
                }
            }
        }
        private void SearchImg_Click(object sender, EventArgs e)
        {

        }

        private void SalesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
