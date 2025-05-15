using Microsoft.VisualBasic.ApplicationServices;
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
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;



namespace STATIONERY_SHOP
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            LoadDashboardData();

        }
        private void LoadDashboardData()
        {
            try
            {
                DateTime currentDate = DateTime.Today;

                // Count items for the current month
                CountItems(null, new DateTime(currentDate.Year, currentDate.Month, 1), currentDate);

                // Sum of sales for all users
                SumSales();

                // Show chart for sales over time
                ShowChart();

                // Display all sales
                DisplaySales();

                // Display salary for the current month
                DisplaySalary(currentDate.Month);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading dashboard data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Dashboard_Load(object sender, EventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFileName=C:\Users\sivas\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\STATIONERYDB.mdf;Initial Catalog=STATIONERYDB;Integrated Security=True;Connect Timeout=30;");
        private void CountItems(string userName = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            using (SqlConnection con = new SqlConnection(Con.ConnectionString))
            {
                string query = @"
        SELECT COUNT(*)
        FROM SalesTbl
        INNER JOIN UserTbl ON SalesTbl.SUser = UserTbl.UId";

                SqlCommand cmd = new SqlCommand(query, con);

                // Append conditions based on parameters
                if (!string.IsNullOrEmpty(userName))
                {
                    query += " WHERE UserTbl.UName = @UName";
                    cmd.Parameters.AddWithValue("@UName", userName);
                }

                if (startDate != null && endDate != null)
                {
                    if (!string.IsNullOrEmpty(userName))
                    {
                        query += " AND";
                    }
                    else
                    {
                        query += " WHERE";
                    }

                    query += " SDate >= @StartDate AND SDate <= @EndDate";
                    cmd.Parameters.AddWithValue("@StartDate", startDate.Value);
                    cmd.Parameters.AddWithValue("@EndDate", endDate.Value);
                }
                else
                {
                    // If no specific user or date range, count for the current month
                    query += " WHERE DATEPART(MONTH, SDate) = @Month";
                    cmd.Parameters.AddWithValue("@Month", DateTime.Today.Month);
                }

                cmd.CommandText = query;

                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    StockLbl.Text = result.ToString() + " Items";
                }
            }
        }



        private void SumSales(string userName = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            using (SqlConnection con = new SqlConnection(Con.ConnectionString))
            {
                string query = "SELECT SUM(SAmount) AS TotalSales FROM SalesTbl WHERE 1 = 1";

                // Append conditions based on parameters
                if (!string.IsNullOrEmpty(userName))
                {
                    query += " AND SUser = (SELECT UId FROM UserTbl WHERE UName = @UName)";
                }

                if (startDate != null)
                {
                    query += " AND SDate >= @StartDate";
                }

                if (endDate != null)
                {
                    query += " AND SDate <= @EndDate";
                }

                SqlCommand cmd = new SqlCommand(query, con);

                // Add parameters based on conditions
                if (!string.IsNullOrEmpty(userName))
                {
                    cmd.Parameters.AddWithValue("@UName", userName);
                }

                if (startDate != null)
                {
                    cmd.Parameters.AddWithValue("@StartDate", startDate.Value);
                }

                if (endDate != null)
                {
                    cmd.Parameters.AddWithValue("@EndDate", endDate.Value);
                }

                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    SalesLbl.Text = "Rs " + result.ToString();
                }
                else
                {
                    SalesLbl.Text = "Rs 0";
                }
            }
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
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
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Users Obj = new Users();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Billing Obj = new Billing();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Dashboard Obj = new Dashboard();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            AdminLogin Obj = new AdminLogin();
            Obj.Show();
            this.Hide();
        }
        private void ShowChart()
        {
            using (SqlConnection con = new SqlConnection(Con.ConnectionString))
            {
                string query = "SELECT SUM(SAmount) AS Amount, SDate FROM SalesTbl GROUP BY SDate";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                Chart1.Series.Clear();
                Chart1.ChartAreas.Clear();
                Chart1.ChartAreas.Add(new ChartArea("ChartArea1"));

                Series series = new Series("SalesSeries");
                series.ChartType = SeriesChartType.SplineArea;
                series.XValueType = ChartValueType.DateTime;
                series.YValueType = ChartValueType.Double;

                foreach (DataRow row in dt.Rows)
                {
                    DateTime date = Convert.ToDateTime(row["SDate"]);
                    double amount = Convert.ToDouble(row["Amount"]);
                    series.Points.AddXY(date, amount);
                }

                Chart1.Series.Add(series);
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Chart1_Click(object sender, EventArgs e)
        {

        }

        private void StockLbl_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            Stock Obj = new Stock();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {

        }
        private void DisplaySales()
        {
            using (SqlConnection con = new SqlConnection(Con.ConnectionString))
            {
                string query = @"
                    SELECT SCustomer, SDate, UserTbl.UName AS SUser, SAmount
                    FROM SalesTbl 
                    INNER JOIN UserTbl ON SalesTbl.SUser = UserTbl.UId";

                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                SalesDGV.DataSource = ds.Tables[0];
            }
        }

        private void SearchUser(string userName, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Con.ConnectionString))
                {
                    string query = @"
                SELECT SCustomer, SDate, UserTbl.UName AS SUser, SAmount
                FROM SalesTbl 
                INNER JOIN UserTbl ON SalesTbl.SUser = UserTbl.UId 
                WHERE UserTbl.UName = @UName
                AND SDate >= @StartDate
                AND SDate <= @EndDate";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@UName", userName);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    SalesDGV.DataSource = ds.Tables[0];

                    // Refresh StockLbl and SalesLbl after updating SalesDGV
                    CountItems(userName, startDate, endDate); // Refresh item count for the specified date range
                    SumSales(userName, startDate, endDate);  // Refresh sales sum for the specified user
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while searching user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }







        private void UpdateChart(string userName, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Con.ConnectionString))
                {
                    string query = @"
                SELECT SDate, SUM(SAmount) AS Amount
                FROM SalesTbl 
                INNER JOIN UserTbl ON SalesTbl.SUser = UserTbl.UId 
                WHERE UserTbl.UName = @UName
                AND SDate >= @StartDate
                AND SDate <= @EndDate
                GROUP BY SDate";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@UName", userName);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    Chart1.Series.Clear();
                    Chart1.ChartAreas.Clear();
                    Chart1.ChartAreas.Add(new ChartArea("ChartArea1"));

                    Series series = new Series("SalesSeries");
                    series.ChartType = SeriesChartType.SplineArea;
                    series.XValueType = ChartValueType.DateTime;
                    series.YValueType = ChartValueType.Double;

                    while (reader.Read())
                    {
                        DateTime date = Convert.ToDateTime(reader["SDate"]);
                        double amount = Convert.ToDouble(reader["Amount"]);
                        series.Points.AddXY(date, amount);
                    }

                    Chart1.Series.Add(series);
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating chart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchImg_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = SearchTb.Text;
                DateTime startDate = DateTime.Parse(SartDate.Text); // Parse string to DateTime
                DateTime endDate = DateTime.Parse(EndDate.Text); // Parse string to DateTime

                SearchUser(userName, startDate, endDate);
                UpdateChart(userName, startDate, endDate);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Invalid date format entered. Please enter a valid date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void ShowSales()
        {
            try
            {
                Con.Open();
                string Query = @"
            SELECT 
                SCustomer, 
                SDate, 
                UserTbl.UName AS SUser, 
                SAmount 
            FROM SalesTbl 
            INNER JOIN UserTbl ON SalesTbl.SUser = UserTbl.UId";
                SqlDataAdapter stockSda = new SqlDataAdapter(Query, Con);
                SqlCommandBuilder stockBuilder = new SqlCommandBuilder(stockSda);
                var stockDs = new DataSet();
                stockSda.Fill(stockDs);
                SalesDGV.DataSource = stockDs.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading sales data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Con.Close();
            }
        }

        private void RefreshImg_Click(object sender, EventArgs e)
        {
            try
            {
                // Refresh the chart
                ShowChart();

                // Refresh the sales data
                DisplaySales();

                // Clear search text box
                SearchTb.Text = "";

                // Count items for the current month
                DateTime currentMonth = DateTime.Today;
                CountItems(null, new DateTime(currentMonth.Year, currentMonth.Month, 1), currentMonth);

                // Sum sales for all users
                SumSales();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while refreshing data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void DisplaySalary(int monthNumber)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Con.ConnectionString))
                {
                    string query = @"
            SELECT 
                UserTbl.UName AS SUser,
                DATENAME(MONTH, SalesTbl.SDate) AS MonthName,
                SUM(SalesTbl.SAmount) AS TotalSales,
                CASE 
                    WHEN SUM(SalesTbl.SAmount) > 1000 THEN 2000
                    ELSE 500
                END AS Salary
            FROM SalesTbl
            INNER JOIN UserTbl ON SalesTbl.SUser = UserTbl.UId
            WHERE DATEPART(MONTH, SalesTbl.SDate) = @MonthNumber
            GROUP BY UserTbl.UName, DATENAME(MONTH, SalesTbl.SDate)
            ORDER BY UserTbl.UName";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@MonthNumber", monthNumber);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    // Clear any existing columns
                    SalaryDGV.Columns.Clear();

                    // Bind the dataset to the DataGridView
                    SalaryDGV.DataSource = ds.Tables[0];

                    // Optionally, set column headers
                    SalaryDGV.Columns["SUser"].HeaderText = "User";
                    SalaryDGV.Columns["MonthName"].HeaderText = "Month";
                    SalaryDGV.Columns["TotalSales"].HeaderText = "Total Sales";
                    SalaryDGV.Columns["Salary"].HeaderText = "Salary";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while displaying salary: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            Customer Obj = new Customer();
            Obj.Show();
            this.Hide();
        }

        private void SearchTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
