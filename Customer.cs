using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Runtime.InteropServices.JavaScript.JSType;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Fonts;



namespace STATIONERY_SHOP
{
    public partial class Customer : Form
    {
        private List<BillDetail> billDetails = new List<BillDetail>();
        private string printCustomer = "";
        private DateTime printDate = DateTime.MinValue;
        private string printUser = "";
        private decimal printAmount = 0;
        private long printPhone = 0;
        private int printInvoice = 0;
        public Customer()
        {
            InitializeComponent();
            DisplayCustomerDetails();
            printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument1_PrintPage);
            // Initialize printDocument1
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument1_PrintPage_1);

            // Set default page settings
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A5", 583, 827); // A5 size in hundredths of an inch (583 x 827)
            printDocument1.DefaultPageSettings.Landscape = false;
            

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFileName=C:\Users\sivas\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\STATIONERYDB.mdf;Initial Catalog=STATIONERYDB;Integrated Security=True;Connect Timeout=30;");

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Stock Obj = new Stock();
            Obj.Show();
            this.Hide();
        }

        private void DisplayCustomerDetails()
        {
            using (SqlConnection con = new SqlConnection(Con.ConnectionString))
            {
                string query = @"
                    SELECT SNum AS [Invoice Number], SCustomer AS [Customer Name], SDate AS [Billing Date], UserTbl.UName AS [Sales Man], SAmount AS [Total Amount]
                    FROM SalesTbl 
                    INNER JOIN UserTbl ON SalesTbl.SUser = UserTbl.UId";

                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                SalesDGV.DataSource = ds.Tables[0];
            }
        }






        private void SearchImg_Click(object sender, EventArgs e)
        {

        }




        private void PrintBtn_Click(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void SearchImg_Click_1(object sender, EventArgs e)
        {
            string searchQuery = @"
        SELECT SNum AS [Invoice Number], SCustomer AS [Customer Name], SDate AS [Billing Date], UserTbl.UName AS [Sales Man], SAmount AS [Total Amount]
        FROM SalesTbl 
        INNER JOIN UserTbl ON SalesTbl.SUser = UserTbl.UId
        WHERE 1 = 1";

            string condition = "";

            // Check if SearchTb.Text is numeric before adding @SNum parameter
            int sNum;
            if (!string.IsNullOrWhiteSpace(SearchTb.Text) && int.TryParse(SearchTb.Text, out sNum))
            {
                condition += " AND SNum = @SNum";
            }

            if (SartDate.Value != DateTime.MinValue)
            {
                condition += " AND CONVERT(date, SDate) = @SDate";
            }

            searchQuery += condition;

            using (SqlConnection con = new SqlConnection(Con.ConnectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(searchQuery, con);

                if (!string.IsNullOrWhiteSpace(SearchTb.Text) && int.TryParse(SearchTb.Text, out sNum))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@SNum", sNum);
                }

                if (SartDate.Value != DateTime.MinValue)
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@SDate", SartDate.Value.Date);
                }

                DataSet ds = new DataSet();
                try
                {
                    adapter.Fill(ds);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        SalesDGV.DataSource = ds.Tables[0];
                    }
                    else
                    {
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
        private void PrintBill(int sNum)
        {
            string customer = "";
            DateTime date = DateTime.MinValue;
            string user = "";
            decimal amount = 0;

            using (SqlConnection con = new SqlConnection(Con.ConnectionString))
            {
                string query = @"
                    SELECT SCustomer, SDate, UT.UName AS SUser, SAmount 
                    FROM SalesTbl ST 
                    INNER JOIN UserTbl UT ON ST.SUser = UT.UId 
                    WHERE ST.SNum = @SalesId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@SalesId", sNum);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        customer = reader["SCustomer"].ToString();
                        date = Convert.ToDateTime(reader["SDate"]);
                        user = reader["SUser"].ToString();
                        amount = Convert.ToDecimal(reader["SAmount"]);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching SalesTbl details: {ex.Message}");
                }
            }

            string billText = $"Bill Details :\n\n" +
                              $"Customer     : {customer}\n" +
                              $"Date         : {date}\n" +
                              $"Sale User    : {user}\n" +
                              $"Amount       : {amount}\n";

            MessageBox.Show(billText, "Print Bill", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void FillSalesGridByDate(DateTime date)
        {
            using (SqlConnection con = new SqlConnection(Con.ConnectionString))
            {
                string query = @"
                    SELECT SNum, SDate, SCustomer, SPhone, UT.UName AS SUser, SAmount 
                    FROM SalesTbl ST 
                    INNER JOIN UserTbl UT ON ST.SUser = UT.UId 
                    WHERE CONVERT(date, ST.SDate) = @SDate";

                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.SelectCommand.Parameters.AddWithValue("@SDate", date);

                DataSet ds = new DataSet();
                try
                {
                    adapter.Fill(ds);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        SalesDGV.DataSource = ds.Tables[0];
                    }
                    else
                    {
                        SalesDGV.DataSource = null;
                        MessageBox.Show("No results found for the selected date.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching SalesTbl data: {ex.Message}");
                }
            }
        }




        private void SartDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void RefreshImg_Click(object sender, EventArgs e)
        {


            // Refresh or re-fetch all data from SalesTbl
            DisplayCustomerDetails();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
        {     // Check if admin is logged in
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

        private void pictureBox11_Click(object sender, EventArgs e)
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

        private void ViewBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (SalesDGV.SelectedRows.Count > 0)
                {
                    // Assuming 'SNum' is the column name for the invoice number
                    int selectedSalesId = Convert.ToInt32(SalesDGV.SelectedRows[0].Cells["Invoice Number"].Value);
                    DisplayBillDetails(selectedSalesId);
                    PreparePrintDocument(selectedSalesId);

                    // Show print preview dialog
                    PrintPreviewDialog printPreview = new PrintPreviewDialog
                    {
                        Document = printDocument1
                    };
                    int totalAmount = CalculateTotalAmount(selectedSalesId);
                    GrdTotalLbl.Text = $"Total: Rs {totalAmount}";

                    // Show the print preview in printPreviewControl1
                    printPreviewControl1.Document = printDocument1;
                    printPreviewControl1.Zoom = 1.0; // Set initial zoom level if needed
                    printPreviewControl1.InvalidatePreview();

                    

                }
                else
                {
                    MessageBox.Show("Please select a row to view details.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in ViewBtn_Click: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private int CalculateTotalAmount(int salesId)
        {
            int totalAmount = 0;

            using (SqlConnection con = new SqlConnection(Con.ConnectionString))
            {
                string query = @"
            SELECT SUM(Total) AS TotalAmount
            FROM BillDetailsTbl
            WHERE SalesId = @SalesId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@SalesId", salesId);

                try
                {
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        totalAmount = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error calculating total amount: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return totalAmount;
        }
        private void DisplayBillDetails(int salesId)
        {
            using (SqlConnection con = new SqlConnection(Con.ConnectionString))
            {
                string query = @"
        SELECT ItemNo, ProductName, Quantity, Price, Total
        FROM BillDetailsTbl
        WHERE SalesId = @SalesId";

                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.SelectCommand.Parameters.AddWithValue("@SalesId", salesId);

                DataSet ds = new DataSet();
                try
                {
                    adapter.Fill(ds);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        SalesDGV.DataSource = ds.Tables[0];
                    }
                    else
                    {
                        SalesDGV.DataSource = null;
                        MessageBox.Show("No bill details found for the selected invoice.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching bill details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void PreparePrintDocument(int salesId)
        {
            FetchSalesDetails(salesId);

            using (SqlConnection con = new SqlConnection(Con.ConnectionString))
            {
                string query = @"
        SELECT ItemNo, ProductName, Quantity, Price, Total
        FROM BillDetailsTbl
        WHERE SalesId = @SalesId";

                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.SelectCommand.Parameters.AddWithValue("@SalesId", salesId);

                DataSet ds = new DataSet();
                try
                {
                    adapter.Fill(ds);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        billDetails.Clear();
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            billDetails.Add(new BillDetail
                            {
                                ItemNo = row["ItemNo"].ToString(),
                                ProductName = row["ProductName"].ToString(),
                                Quantity = Convert.ToInt32(row["Quantity"]),
                                Price = Convert.ToDecimal(row["Price"]),
                                Total = Convert.ToDecimal(row["Total"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching bill details for printing: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FetchSalesDetails(int salesId)
        {
            using (SqlConnection con = new SqlConnection(Con.ConnectionString))
            {
                string query = @"
                    SELECT SNum,SCustomer, SDate, UT.UName AS SUser, SAmount,SPhone 
                    FROM SalesTbl ST 
                    INNER JOIN UserTbl UT ON ST.SUser = UT.UId 
                    WHERE ST.SNum = @SalesId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@SalesId", salesId);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        printCustomer = reader["SCustomer"].ToString();
                        printInvoice = Convert.ToInt32(reader["SNum"]);
                        printDate = Convert.ToDateTime(reader["SDate"]);
                        printUser = reader["SUser"].ToString();
                        printAmount = Convert.ToDecimal(reader["SAmount"]);
                        printPhone = Convert.ToInt64(reader["SPhone"]);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching SalesTbl details: {ex.Message}");
                }
            }
        }

        private void SalesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int startX = 50; // X-coordinate for starting point
            int startY = 50; // Y-coordinate for starting point
            int lineSpacing = 20; // Spacing between lines

            // Center-aligned title
            string title = "STATIONERY SHOP";
            Font titleFont = new Font("Algerian", 32, FontStyle.Regular);
            SizeF titleSize = e.Graphics.MeasureString(title, titleFont);
            e.Graphics.DrawString(title, titleFont, Brushes.LimeGreen, new PointF((e.PageBounds.Width - titleSize.Width) / 2, startY));

            Font CustdetailsFont = new Font("Calibri Light (Headings)", 14, FontStyle.Bold);

            // Customer details section - left aligned
            string[] customerDetails = {
        "Customer Name: " + printCustomer,
        "Date: " + printDate.ToShortDateString(),
        "Customer Contact: " + printPhone.ToString()
    };
            Font detailsFont = new Font("Calibri Light (Headings)", 12, FontStyle.Regular);
            for (int i = 0; i < customerDetails.Length; i++)
            {
                e.Graphics.DrawString(customerDetails[i], detailsFont, Brushes.Black, new PointF(startX, startY + (3 + i) * lineSpacing));
            }

            // Right-aligned Invoice number
            string invoiceNumber = "Invoice Number: " + printInvoice.ToString();
            SizeF invoiceSize = e.Graphics.MeasureString(invoiceNumber, detailsFont);
            e.Graphics.DrawString(invoiceNumber, detailsFont, Brushes.Black, new PointF(e.PageBounds.Width - invoiceSize.Width - startX, startY + 3 * lineSpacing));

            string salesUser = "Sales User: " + printUser;
            SizeF salesUserSize = e.Graphics.MeasureString(salesUser, detailsFont);
            e.Graphics.DrawString(salesUser, detailsFont, Brushes.Black, new PointF(e.PageBounds.Width - salesUserSize.Width - startX, startY + 4 * lineSpacing));

            // Draw line separator
            e.Graphics.DrawLine(new Pen(Brushes.Black), startX, startY + 7 * lineSpacing, e.PageBounds.Width - startX, startY + 7 * lineSpacing);

            // Column headers
            e.Graphics.DrawString("ID     PRODUCT      QUANTITY           PRICE         TOTAL", new Font("Calibri Light (Headings)", 14, FontStyle.Bold), Brushes.Black, new Point(40, startY + 8 * lineSpacing));
            int pos = startY + 10 * lineSpacing;

            // Print each bill detail
            foreach (BillDetail detail in billDetails)
            {
                e.Graphics.DrawString($"{detail.ItemNo}", new Font("Calibri Light (Headings)", 12, FontStyle.Italic), Brushes.Black, new Point(43, pos));
                e.Graphics.DrawString($"{detail.ProductName}", new Font("Calibri Light (Headings)", 12, FontStyle.Italic), Brushes.Black, new Point(95, pos));
                e.Graphics.DrawString($"{detail.Quantity}", new Font("Calibri Light (Headings)", 12, FontStyle.Italic), Brushes.Black, new Point(260, pos));
                e.Graphics.DrawString($"{detail.Price:C}", new Font("Calibri Light (Headings)", 12, FontStyle.Italic), Brushes.Black, new Point(390, pos));
                e.Graphics.DrawString($"{detail.Total:C}", new Font("Calibri Light (Headings)", 12, FontStyle.Italic), Brushes.Black, new Point(490, pos));
                pos += lineSpacing;
            }

            // Grand Total and Thank You message
            e.Graphics.DrawString("Grand Total: Rs " + printAmount, new Font("Century Gothic", 14, FontStyle.Bold), Brushes.Black, new Point(220, pos + 20));
            e.Graphics.DrawString("*~*~*~*~*Thank You! for Your Purchase!*~*~*~*~*", new Font("Brush Script MT", 18, FontStyle.Italic), Brushes.DeepPink, new PointF(50, pos + 55));


        }
        public class BillDetail
        {
            public string ItemNo { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal Total { get; set; }
        }

        private void ClearImg_Click(object sender, EventArgs e)
        {
            SearchTb.Text = "";
        }

        private void GrdTotalLbl_Click(object sender, EventArgs e)
        {

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void printPreviewControl1_Click(object sender, EventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void BillPrint_Click(object sender, EventArgs e)
        {
            try
    {
        // Print the document
        printDocument1.Print();
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error printing bill: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
        }
    }
    
}

