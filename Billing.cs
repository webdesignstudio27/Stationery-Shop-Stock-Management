using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace STATIONERY_SHOP
{
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            Methods obj = new Methods();
            obj.DisplayData("ItemTbl", ItemsDGV);
            Methods obj1 = new Methods();
            obj1.DisplayData("SalesTbl", BillsDGV);
            ULabel.Text = Login.UName;
            GetUID();
            QuantityTb.Validating += new System.ComponentModel.CancelEventHandler(QuantityTb_Validating);
            PhoneTb.KeyPress += new KeyPressEventHandler(PhoneTb_KeyPress);
            BillDate.Value = DateTime.Today;
            BillDate.Enabled = false;
        }

        private void Billing_Load(object sender, EventArgs e)
        {
            BillDate.Value = DateTime.Today;
            BillDate.Enabled = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFileName=C:\Users\sivas\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\STATIONERYDB.mdf;Initial Catalog=STATIONERYDB;Integrated Security=True;Connect Timeout=30;");

        int n = 0;
        int GrdTotal = 0;
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
        private void UpdateItem()
        {
            int NewQty = Stock - Convert.ToInt32(QuantityTb.Text);
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update ItemTbl set ItQty=@IQ where ItID=@ProdKey ", Con);
                cmd.Parameters.AddWithValue("@IQ", NewQty);
                cmd.Parameters.AddWithValue("@ProdKey", ProdKey);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Added to Bill!!!");
                Con.Close();
                PriceTb.Text = "";
                QuantityTb.Text = "";
                ProdNameTb.Text = "";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void AddToBillBtn_Click(object sender, EventArgs e)
        {
            string customer = BCustomer.Text;
            string phoneNumber = PhoneTb.Text;

            // Validate phone number
            if (!IsPhoneNumberValid(phoneNumber))
            {
                MessageBox.Show("Please enter a valid phone number.");
                return;
            }

            // Add Product to Client Bill
            if (QuantityTb.Text == "" || Convert.ToInt32(QuantityTb.Text) > Stock)
            {
                MessageBox.Show("Enter Valid Quantity!!!");
            }
            else
            {
                if (IsNameValid(customer))
                {
                    int total = Convert.ToInt32(QuantityTb.Text) * Convert.ToInt32(PriceTb.Text);
                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(ClientBillDGV);
                    newRow.Cells[0].Value = n + 1;
                    newRow.Cells[1].Value = ProdNameTb.Text;
                    newRow.Cells[2].Value = PriceTb.Text;
                    newRow.Cells[3].Value = QuantityTb.Text;
                    newRow.Cells[4].Value = total;
                    ClientBillDGV.Rows.Add(newRow);
                    GrdTotal = GrdTotal + total;
                    GrdTotalLbl.Text = "Rs " + GrdTotal;
                    n++;
                    UpdateItem();
                    Methods obj = new Methods();
                    obj.DisplayData("ItemTbl", ItemsDGV);

                    // The SaveBill method will handle the actual insertion into BillDetailsTbl
                }
            }

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

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        int ProdKey = 0;
        int Stock = 0;
        private void ItemsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdNameTb.Text = ItemsDGV.SelectedRows[0].Cells[1].Value.ToString();
            Stock = Convert.ToInt32(ItemsDGV.SelectedRows[0].Cells[3].Value.ToString());
            PriceTb.Text = ItemsDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (ProdNameTb.Text == "")
            {
                ProdKey = 0;
            }
            else
            {
                ProdKey = Convert.ToInt32(ItemsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            BCustomer.Text = "";
            PriceTb.Text = "";
            QuantityTb.Text = "";
            ProdNameTb.Text = "";
            PhoneTb.Text = "";
            Stock = 0;
            GrdTotalLbl.Text = "Rs ";
            ClientBillDGV.Rows.Clear();
        }
        private void SaveBill()
        {
            try
            {
                // Open connection
                Con.Open();

                // Fetch UName based on ULabel.Text (assuming ULabel.Text is the username)
                string query = "SELECT UId FROM UserTbl WHERE UName = @UName";
                SqlCommand userCmd = new SqlCommand(query, Con);
                userCmd.Parameters.AddWithValue("@UName", ULabel.Text);
                int userId = (int)userCmd.ExecuteScalar();

                // Insert into SalesTbl with username and retrieve the SalesId (SNum) of the newly inserted row
                SqlCommand cmd = new SqlCommand("INSERT INTO SalesTbl (SDate, SCustomer, SPhone, SUser, SAmount) OUTPUT INSERTED.SNum VALUES (@SD, @SC, @PC, @SU, @SA)", Con);
                cmd.Parameters.AddWithValue("@SD", BillDate.Value.Date);
                cmd.Parameters.AddWithValue("@SC", BCustomer.Text);
                cmd.Parameters.AddWithValue("@PC", PhoneTb.Text);
                cmd.Parameters.AddWithValue("@SU", userId); // Use fetched username here
                cmd.Parameters.AddWithValue("@SA", GrdTotal);
                int salesId = (int)cmd.ExecuteScalar(); // Retrieve the SalesId

                // Ensure salesId is valid
                if (salesId <= 0)
                {
                    throw new Exception("Failed to retrieve SalesId after inserting into SalesTbl.");
                }

                // Insert each item in the ClientBillDGV into BillDetailsTbl with the SalesId
                foreach (DataGridViewRow row in ClientBillDGV.Rows)
                {
                    if (row.IsNewRow) continue; // Skip the new row placeholder

                    string itemNo = row.Cells[0].Value.ToString();
                    string productName = row.Cells[1].Value.ToString();
                    int quantity = Convert.ToInt32(row.Cells[3].Value);
                    int price = Convert.ToInt32(row.Cells[2].Value);
                    int total = Convert.ToInt32(row.Cells[4].Value);

                    string insertBillDetailQuery = "INSERT INTO BillDetailsTbl (SalesId, ItemNo, ProductName, Quantity, Price, Total) VALUES (@SalesId, @ItemNo, @ProductName, @Quantity, @Price, @Total)";
                    using (SqlCommand billDetailCmd = new SqlCommand(insertBillDetailQuery, Con))
                    {
                        billDetailCmd.Parameters.AddWithValue("@SalesId", salesId);
                        billDetailCmd.Parameters.AddWithValue("@ItemNo", itemNo);
                        billDetailCmd.Parameters.AddWithValue("@ProductName", productName);
                        billDetailCmd.Parameters.AddWithValue("@Quantity", quantity);
                        billDetailCmd.Parameters.AddWithValue("@Price", price);
                        billDetailCmd.Parameters.AddWithValue("@Total", total);
                        billDetailCmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Bill Saved!!!");

                // Close connection
                Con.Close();

                // Display updated data in BillsDGV
                Methods obj = new Methods();
                obj.DisplayData("SalesTbl", BillsDGV);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Con.Close(); // Ensure the connection is closed in case of an error
            }
        }





        int UserId;
        private void GetUID()
        {
            Con.Open();
            string query = "select UId from UserTbl where UName='" + ULabel.Text + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                UserId = Convert.ToInt32(dr["UId"].ToString());
            }
            Con.Close();
        }


        private void PrintBtn_Click(object sender, EventArgs e)
        {
            SaveBill();
            PaperSize A5PaperSize = new PaperSize("A5", 583, 827);

            // Assign the custom paper size to a PrintDocument
            printDocument1.DefaultPageSettings.PaperSize = A5PaperSize;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }

            ClientBillDGV.Rows.Clear();
        }

        int prodid, prodqty, prodprice, total, pos = 100;
        string prodname;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Constants for positioning
            int startX = 50; // X-coordinate for starting point
            int startY = 50; // Y-coordinate for starting point
            int lineSpacing = 20; // Spacing between lines

            // Header section
            e.Graphics.DrawString("STATIONERY SHOP", new Font("Algerian", 20, FontStyle.Bold), Brushes.LimeGreen, new Point(startX + 170, startY));

            // Customer details section
            e.Graphics.DrawString("Customer Details:", new Font("Calibri", 12, FontStyle.Bold), Brushes.Black, new Point(startX, startY + lineSpacing));
            e.Graphics.DrawString("Customer Name: " + BCustomer.Text, new Font("Calibri", 12, FontStyle.Regular), Brushes.Black, new Point(startX, startY + 2 * lineSpacing));
            e.Graphics.DrawString("Date: " + DateTime.Now.ToShortDateString(), new Font("Calibri", 12, FontStyle.Regular), Brushes.Black, new Point(startX, startY + 3 * lineSpacing));
            e.Graphics.DrawString("Sales User: " + ULabel.Text, new Font("Calibri", 12, FontStyle.Regular), Brushes.Black, new Point(startX, startY + 4 * lineSpacing));
            e.Graphics.DrawString("Invoice Number: " + GetSalesNumberForCurrentBill().ToString(), new Font("Calibri", 12, FontStyle.Regular), Brushes.Black, new Point(startX, startY + 5 * lineSpacing));
            e.Graphics.DrawString("Customer Phone Number: " + PhoneTb.Text, new Font("Calibri", 12, FontStyle.Regular), Brushes.Black, new Point(startX, startY + 6 * lineSpacing));

            // Separate line
            e.Graphics.DrawLine(new Pen(Brushes.Black), startX, startY + 7 * lineSpacing, startX + 600, startY + 7 * lineSpacing);

            // Billing items section
            // Table header
            e.Graphics.DrawString("ID      PRODUCT              PRICE    QUANTITY    TOTAL", new Font("Calibri Light (Headings)", 14, FontStyle.Bold), Brushes.Black, new Point(40, 200));
            int pos = startY + 10 * lineSpacing;

            // Table content
            foreach (DataGridViewRow row in ClientBillDGV.Rows)
            {
                if (!row.IsNewRow) // Check if it's not a new row placeholder
                {
                    int prodid = 0;
                    string prodname = "";
                    int prodprice = 0;
                    int prodqty = 0;
                    int total = 0;

                    // Example: Check if the cell value is not null before accessing
                    if (row.Cells["Column1"].Value != null)
                        prodid = Convert.ToInt32(row.Cells["Column1"].Value);
                    if (row.Cells["Column2"].Value != null)
                        prodname = row.Cells["Column2"].Value.ToString();
                    if (row.Cells["Column3"].Value != null)
                        prodprice = Convert.ToInt32(row.Cells["Column3"].Value);
                    if (row.Cells["Column4"].Value != null)
                        prodqty = Convert.ToInt32(row.Cells["Column4"].Value);
                    if (row.Cells["Column5"].Value != null)
                        total = Convert.ToInt32(row.Cells["Column5"].Value);

                    e.Graphics.DrawString("" + prodid, new Font("Calibri Light (Headings)", 10, FontStyle.Italic), Brushes.Black, new Point(43, pos));
                    e.Graphics.DrawString("" + prodname, new Font("Calibri Light (Headings)", 10, FontStyle.Italic), Brushes.Black, new Point(97, pos));
                    e.Graphics.DrawString("" + prodprice, new Font("Calibri Light (Headings)", 10, FontStyle.Italic), Brushes.Black, new Point(286, pos));
                    e.Graphics.DrawString("" + prodqty, new Font("Calibri Light (Headings)", 10, FontStyle.Italic), Brushes.Black, new Point(400, pos));
                    e.Graphics.DrawString("" + total, new Font("Calibri Light (Headings)", 10, FontStyle.Italic), Brushes.Black, new Point(500, pos));
                    pos = pos + 20;

                }
            }

            // Grand total
            e.Graphics.DrawString("Grand Total: Rs " + GrdTotal, new Font("Century Gothic", 14, FontStyle.Bold), Brushes.Black, new Point(190, pos + 60));
            e.Graphics.DrawString("**************Thank You! for Your Purchase**************", new Font("Brush Script MT", 18, FontStyle.Italic), Brushes.DeepPink, new Point(50, pos + 95));
            ClientBillDGV.Rows.Clear();
            ClientBillDGV.Refresh();
            pos = 100;
            GrdTotal = 0;
            n = 0;
            BCustomer.Text = "";
            PriceTb.Text = "";
            QuantityTb.Text = "";
            ProdNameTb.Text = "";
            PhoneTb.Text = "";
            Stock = 0;
            GrdTotalLbl.Text = "Rs ";
            ClientBillDGV.Rows.Clear();


        }

        private int GetSalesNumberForCurrentBill()
        {
            try
            {
                Con.Open();
                // Assuming you store the SalesId (SNum) of the current bill in a variable accessible here
                string query = "SELECT SNum AS[Invoice Number] FROM SalesTbl WHERE SDate = @SD AND SCustomer = @SC AND SPhone = @PC AND SAmount = @SA";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.Parameters.AddWithValue("@SD", BillDate.Value.Date);
                cmd.Parameters.AddWithValue("@SC", BCustomer.Text);
                cmd.Parameters.AddWithValue("@PC", PhoneTb.Text);
                cmd.Parameters.AddWithValue("@SA", GrdTotal);
                int salesNumber = (int)cmd.ExecuteScalar();
                Con.Close();
                return salesNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching Sales Number: " + ex.Message);
                Con.Close();
                return -1; // or handle the error accordingly
            }
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

        private void BCustomer_TextChanged(object sender, EventArgs e)
        {

        }

        // Quantity Validation...
        private void QuantityTb_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string quantityText = QuantityTb.Text.Trim();
            int quantity;


            if (!int.TryParse(quantityText, out quantity))
            {
                MessageBox.Show("Invalid Quantity! Please enter a valid number.");
                e.Cancel = false;
            }
            else if (quantity < 0)
            {
                MessageBox.Show("Quantity cannot be negative.");
                e.Cancel = false;
            }
            else
            {
                int existingQuantity = GetExistingQuantity();
                if (existingQuantity < 0)
                {
                    MessageBox.Show("Out of Stock.");
                    e.Cancel = false;
                }
                else if (quantity > existingQuantity)
                {
                    MessageBox.Show($"Quantity cannot be less than the already stored quantity of {existingQuantity}.");
                    PriceTb.Text = "";
                    QuantityTb.Text = "";
                    ProdNameTb.Text = "";
                    e.Cancel = false;


                }
            }
        }

        private int GetExistingQuantity()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ItQty FROM ItemTbl WHERE ItName = @ItemName", Con);
                cmd.Parameters.AddWithValue("@ItemName", ProdNameTb.Text);
                object result = cmd.ExecuteScalar();
                Con.Close();

                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while fetching existing quantity: ");
                Con.Close();
                return -1;
            }
        }
        //Customer Name....
        private bool IsNameValid(string customer)
        {
            if (string.IsNullOrWhiteSpace(customer))
            {
                MessageBox.Show("Name cannot be empty.");
                return false;
            }

            // Check if name contains only alphabetic characters and optional spaces
            string pattern = @"^[a-zA-Z ]+$";

            if (!Regex.IsMatch(customer, pattern))
            {
                MessageBox.Show("Name should contain only alphabetic characters and spaces.");
                return false;
            }

            // Check if name length is between 4 and 10 characters
            if (customer.Length < 3 || customer.Length > 20)
            {
                MessageBox.Show("Name must be between 4 and 20 characters long.");
                return false;
            }

            return true;
        }

        private void BillsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Stock Obj = new Stock();
            Obj.Show();
            this.Hide();
        }

        private void ProdNameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void PhoneTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Invalid input! Please enter digits only.");
            }
        }
        private bool IsPhoneNumberValid(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                MessageBox.Show("Phone number cannot be empty.");
                return false;
            }

            if (phoneNumber.Length != 10)
            {
                MessageBox.Show("Phone number must be exactly 10 digits long.");
                return false;
            }

            if (!Regex.IsMatch(phoneNumber, @"^\d{10}$"))
            {
                MessageBox.Show("Phone number must contain only digits.");
                return false;
            }

            return true;
        }

        private void BillDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Customer Obj = new Customer();
            Obj.Show();
            this.Hide();
        }
        private void SearchItem()
        {
            Con.Open();
            String Query = "select * from ItemTbl where ItName='" + SearchTb.Text + "'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void ShowItem()
        {
            Con.Open();
            String Query = "select * from ItemTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void SearchImg_Click(object sender, EventArgs e)
        {
            SearchItem();
        }

        private void RefreshImg_Click(object sender, EventArgs e)
        {
            ShowItem();
            SearchTb.Text = "";
        }

        private void GrdTotalLbl_Click(object sender, EventArgs e)
        {

        }
    }
}
