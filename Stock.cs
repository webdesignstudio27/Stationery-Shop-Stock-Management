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
    public partial class Stock : Form
    {
        public Stock()
        {
            InitializeComponent();
            CountItems();
            DisplayStockData();
            DisplayOutStockData();
            DisplaySoonOutStockData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void StockDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void StockGDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFileName=C:\Users\sivas\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\STATIONERYDB.mdf;Initial Catalog=STATIONERYDB;Integrated Security=True;Connect Timeout=30;";

        private void SearchItem()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Search and display in StockDGV
                    string stockQuery = "SELECT ItId, ItName, ItQty FROM ItemTbl WHERE ItName=@ItName AND ItQty > 0";
                    SqlDataAdapter stockSda = new SqlDataAdapter(stockQuery, con);
                    stockSda.SelectCommand.Parameters.AddWithValue("@ItName", SearchTb.Text); // Assign @ItName parameter
                    DataTable stockDt = new DataTable();
                    stockSda.Fill(stockDt);
                    StockDGV.DataSource = stockDt;

                    // Search and display in OutstockDGV
                    string outstockQuery = "SELECT ItId, ItName FROM ItemTbl WHERE ItName=@ItName AND ItQty = 0";
                    SqlDataAdapter outstockSda = new SqlDataAdapter(outstockQuery, con);
                    outstockSda.SelectCommand.Parameters.AddWithValue("@ItName", SearchTb.Text); // Assign @ItName parameter
                    DataTable outstockDt = new DataTable();
                    outstockSda.Fill(outstockDt);
                    OutstockDGV.DataSource = outstockDt;

                    // Search and display in SoonOutstockDGV
                    string soonOutstockQuery = "SELECT ItId, ItName FROM ItemTbl WHERE ItName=@ItName AND ItQty < 50 AND ItQty > 0";
                    SqlDataAdapter soonOutstockSda = new SqlDataAdapter(soonOutstockQuery, con);
                    soonOutstockSda.SelectCommand.Parameters.AddWithValue("@ItName", SearchTb.Text); // Assign @ItName parameter
                    DataTable soonOutstockDt = new DataTable();
                    soonOutstockSda.Fill(soonOutstockDt);
                    MiniDGV.DataSource = soonOutstockDt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        // Method to display stock data
        private void DisplayStockData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ItId, ItName, ItQty FROM ItemTbl WHERE ItQty > 0"; ;
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    StockDGV.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private void DisplayOutStockData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ItId, ItName FROM ItemTbl WHERE ItQty = 0";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    OutstockDGV.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private void DisplaySoonOutStockData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ItId, ItName FROM ItemTbl WHERE ItQty <50 AND ItQty > 0";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    MiniDGV.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Stock Obj = new Stock();
            Obj.Show();
            this.Hide();
        }

        private void SearchImg_Click(object sender, EventArgs e)
        {
            SearchItem();

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void ShowItem()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Query for StockDGV
                    string stockQuery = "SELECT ItId, ItName, ItQty FROM ItemTbl WHERE ItQty > 0";
                    SqlDataAdapter stockSda = new SqlDataAdapter(stockQuery, con);
                    SqlCommandBuilder stockBuilder = new SqlCommandBuilder(stockSda);
                    var stockDs = new DataSet();
                    stockSda.Fill(stockDs);
                    StockDGV.DataSource = stockDs.Tables[0];

                    // Query for OutstockDGV
                    string outstockQuery = "SELECT ItId, ItName FROM ItemTbl WHERE ItQty = 0";
                    SqlDataAdapter outstockSda = new SqlDataAdapter(outstockQuery, con);
                    SqlCommandBuilder outstockBuilder = new SqlCommandBuilder(outstockSda);
                    var outstockDs = new DataSet();
                    outstockSda.Fill(outstockDs);
                    OutstockDGV.DataSource = outstockDs.Tables[0];

                    // Query for Soon OutstockDGV
                    string SoonoutstockQuery = "SELECT ItId, ItName FROM ItemTbl WHERE ItQty = 0";
                    SqlDataAdapter SoonoutstockSda = new SqlDataAdapter(SoonoutstockQuery, con);
                    SqlCommandBuilder SoonoutstockBuilder = new SqlCommandBuilder(SoonoutstockSda);
                    var SoonoutstockDs = new DataSet();
                    outstockSda.Fill(SoonoutstockDs);
                    OutstockDGV.DataSource = SoonoutstockDs.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }
        private void RefreshImg_Click(object sender, EventArgs e)
        {
            ShowItem();
            SearchTb.Text = "";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Items Obj = new Items();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

            Category Obj = new Category();
            Obj.Show();
            //this.Hide();
        }
        private bool isAdminLoggedIn = false;

        // Method to simulate admin login (you should replace this with your actual login mechanism)
        private void AdminLogin()
        {
            // Example: Assume login succeeds
            isAdminLoggedIn = true;
        }

        // Method to simulate admin logout (optional, if needed)
        private void AdminLogout()
        {
            isAdminLoggedIn = false;
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (isAdminLoggedIn)
            {
                // Admin is logged in, proceed to open Items form
                Users Obj = new Users();
                Obj.Show();
                this.Hide();
            }
            else
            {
                // Admin is not logged in, show message
                MessageBox.Show("Only administrators are allowed to access this page.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Billing Obj = new Billing();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (isAdminLoggedIn)
            {
                // Admin is logged in, proceed to open Items form
                Dashboard Obj = new Dashboard();
                Obj.Show();
                this.Hide();
            }
            else
            {
                // Admin is not logged in, show message
                MessageBox.Show("Only administrators are allowed to access this page.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Customer Obj = new Customer();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void CountItems()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Query to count items with ItQty > 0
                    string queryStock = "SELECT COUNT(*) FROM Itemtbl WHERE ItQty > 0";
                    using (SqlDataAdapter sdaStock = new SqlDataAdapter(queryStock, con))
                    {
                        DataTable dtStock = new DataTable();
                        sdaStock.Fill(dtStock);

                        if (dtStock.Rows.Count > 0)
                        {
                            StockLbl.Text = dtStock.Rows[0][0].ToString() + " Items";
                        }
                        else
                        {
                            StockLbl.Text = "0 Items";
                        }
                    }

                    // Query to count items with ItQty = 0
                    string queryOutStock = "SELECT COUNT(*) FROM Itemtbl WHERE ItQty = 0";
                    using (SqlDataAdapter sdaOutStock = new SqlDataAdapter(queryOutStock, con))
                    {
                        DataTable dtOutStock = new DataTable();
                        sdaOutStock.Fill(dtOutStock);

                        if (dtOutStock.Rows.Count > 0)
                        {
                            OutStockLbl.Text = dtOutStock.Rows[0][0].ToString() + " Items";
                        }
                        else
                        {
                            OutStockLbl.Text = "0 Items";
                        }
                    }

                    string querySoonOutStock = "SELECT COUNT(*) FROM Itemtbl WHERE ItQty < 50 AND ItQty > 0";
                    using (SqlDataAdapter sdaSoonOutStock = new SqlDataAdapter(querySoonOutStock, con))
                    {
                        DataTable dtSoonOutStock = new DataTable();
                        sdaSoonOutStock.Fill(dtSoonOutStock);

                        if (dtSoonOutStock.Rows.Count > 0)
                        {
                            MiniLbl.Text = dtSoonOutStock.Rows[0][0].ToString() + " Items";
                        }
                        else
                        {
                            MiniLbl.Text = "0 Items";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }


    }
}
