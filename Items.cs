using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace STATIONERY_SHOP
{
    public partial class Items : Form
    {
        public Items()
        {
            InitializeComponent();
            Methods obj = new Methods();
            obj.DisplayData("ItemTbl", ProductGDV);
            GetCategory();
            //MessageBox.Show(Login.UName);
            string U = Login.UName;
            QuantityTb.Validating += new System.ComponentModel.CancelEventHandler(QuantityTb_Validating);
            BPriceTb.Validating += new System.ComponentModel.CancelEventHandler(BPriceTb_Validating);
            SPriceTb.Validating += new System.ComponentModel.CancelEventHandler(SPriceTb_Validating);
            ProdDate.Validating += new CancelEventHandler(ProdDate_Validating);

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFileName=C:\Users\sivas\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\STATIONERYDB.mdf;Initial Catalog=STATIONERYDB;Integrated Security=True;Connect Timeout=30;");


        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void GetCategory()
        {
            try
            {
                using (SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFileName=C:\Users\sivas\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\STATIONERYDB.mdf;Initial Catalog=STATIONERYDB;Integrated Security=True;Connect Timeout=30;"))
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("select CatId, CatName from CategoryTbl", Con);
                    SqlDataReader Rdr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(Rdr);

                    // Add an initial row for "Select Category"
                    DataRow newRow = dt.NewRow();
                    newRow["CatId"] = 0;
                    newRow["CatName"] = "Select Category";
                    dt.Rows.InsertAt(newRow, 0);

                    // Bind data to ComboBox
                    CatCb.ValueMember = "CatId";
                    CatCb.DisplayMember = "CatName";
                    CatCb.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            string prodname = ProdNameTb.Text;
            if (ProdNameTb.Text == "" || CatCb.Text == "Select Category" || ProdDetailsTb.Text == "" || SPriceTb.Text == "" || BPriceTb.Text == "" || ProdDetailsTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                if (IsProdNameValid(prodname))
                {
                    try
                    {
                        int Profit = Convert.ToInt32(SPriceTb.Text) - Convert.ToInt32(BPriceTb.Text);
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("insert into ItemTbl(ItName,ItCat,ItQty,ItBPrice,ItSPrice,ItProfit,ItDetails,ItAddDate) values (@IN,@IC,@IQ,@IBP,@ISP,@IP,@ID,@IADate)", Con);
                        cmd.Parameters.AddWithValue("@IN", prodname);
                        cmd.Parameters.AddWithValue("@IC", CatCb.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@IQ", QuantityTb.Text);
                        cmd.Parameters.AddWithValue("@IBP", BPriceTb.Text);
                        cmd.Parameters.AddWithValue("@ISP", SPriceTb.Text);
                        cmd.Parameters.AddWithValue("@IP", Profit);
                        cmd.Parameters.AddWithValue("@ID", ProdDetailsTb.Text);
                        cmd.Parameters.AddWithValue("@IADate", ProdDate.Value.Date);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Item Added!!!");
                        Con.Close();
                        ProdNameTb.Text = "";
                        CatCb.Text = "Select Category";
                        QuantityTb.Text = "";
                        BPriceTb.Text = "";
                        SPriceTb.Text = "";
                        ProdDetailsTb.Text = " ";
                        ProdDate.Value = DateTime.Today;
                        Methods obj = new Methods();
                        obj.DisplayData("ItemTbl", ProductGDV);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        int Key = 0;
        private void ProductGDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdNameTb.Text = ProductGDV.SelectedRows[0].Cells[1].Value.ToString();
            CatCb.Text = ProductGDV.SelectedRows[0].Cells[2].Value.ToString();
            QuantityTb.Text = ProductGDV.SelectedRows[0].Cells[3].Value.ToString();
            BPriceTb.Text = ProductGDV.SelectedRows[0].Cells[4].Value.ToString();
            SPriceTb.Text = ProductGDV.SelectedRows[0].Cells[5].Value.ToString();
            ProdDetailsTb.Text = ProductGDV.SelectedRows[0].Cells[7].Value.ToString();
            ProdDate.Text = ProductGDV.SelectedRows[0].Cells[8].Value.ToString();
            if (ProdNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ProductGDV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from ItemTbl where ItId=@PK", Con);
                    cmd.Parameters.AddWithValue("@PK", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Deleted!!!");
                    Con.Close();
                    ProdNameTb.Text = "";
                    CatCb.Text = "Select Category";
                    QuantityTb.Text = "";
                    BPriceTb.Text = "";
                    SPriceTb.Text = "";
                    ProdDetailsTb.Text = " ";
                    ProdDate.Value = DateTime.Today;
                    Methods obj = new Methods();
                    obj.DisplayData("ItemTbl", ProductGDV);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {

        }

        private void EditBtn_Click_1(object sender, EventArgs e)
        {
            string prodname = ProdNameTb.Text;
            if (prodname == "" || CatCb.SelectedValue == null || CatCb.SelectedValue.ToString() == "0" || ProdDetailsTb.Text == "" || SPriceTb.Text == "" || BPriceTb.Text == "" || QuantityTb.Text == "")
            {
                MessageBox.Show("Missing or Invalid Information!!!");
                return;
            }

            if (!IsProdNameValid(prodname))
            {
                return;
            }

            if (Key == 0)
            {
                MessageBox.Show("Please select a product to edit.");
                return;
            }

            try
            {
                int Profit = Convert.ToInt32(SPriceTb.Text) - Convert.ToInt32(BPriceTb.Text);
                Con.Open();
                SqlCommand cmd = new SqlCommand("update ItemTbl set ItName=@IN,ItCat=@IC,ItQty=@IQ,ItBPrice=@IBP,ItSPrice=@ISP,ItProfit=@IP,ItDetails=@ID,ItAddDate=@IADate where ItId=@PKey", Con);
                cmd.Parameters.AddWithValue("@IN", prodname);
                cmd.Parameters.AddWithValue("@IC", CatCb.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@IQ", QuantityTb.Text);
                cmd.Parameters.AddWithValue("@IBP", BPriceTb.Text);
                cmd.Parameters.AddWithValue("@ISP", SPriceTb.Text);
                cmd.Parameters.AddWithValue("@IP", Profit);
                cmd.Parameters.AddWithValue("@ID", ProdDetailsTb.Text);
                cmd.Parameters.AddWithValue("@IADate", ProdDate.Value.Date);
                cmd.Parameters.AddWithValue("@PKey", Key);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Item Updated!!!");

                    ProdNameTb.Text = "";
                    CatCb.Text = "Select Category";
                    QuantityTb.Text = "";
                    BPriceTb.Text = "";
                    SPriceTb.Text = "";
                    ProdDetailsTb.Text = " ";
                    ProdDate.Value = DateTime.Today;


                    Methods obj = new Methods();
                    obj.DisplayData("ItemTbl", ProductGDV);
                }
                else
                {
                    MessageBox.Show("Update failed. No rows affected.");
                }
                Con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred: " + Ex.Message);
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Category Obj = new Category();
            Obj.Show();
            //this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Billing Obj = new Billing();
            Obj.Show();
            this.Hide();
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
        private void SearchItem()
        {
            Con.Open();
            String Query = "select * from ItemTbl where ItName='" + SearchTb.Text + "'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductGDV.DataSource = ds.Tables[0];
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
            ProductGDV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            SearchItem();
        }

        private void RefreshImg_Click(object sender, EventArgs e)
        {
            ShowItem();
            SearchTb.Text = "";
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
        // Product Name Validation
        private bool IsProdNameValid(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Product Name cannot be empty.");
                return false;
            }
            string pattern = @"^[a-zA-Z1-9 ]+$";

            if (!Regex.IsMatch(name, pattern))
            {
                MessageBox.Show("Product Name should contain only alphabetic characters and spaces.");
                return false;
            }
            if (name.Length < 4 || name.Length > 30)
            {
                MessageBox.Show("Product Name must be between 4 and 30 characters long.");
                return false;
            }

            return true;
        }

        private void ProdNameTb_TextChanged(object sender, EventArgs e)
        {

        }
        // Quantity Validation....

        private void QuantityTb_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string quantityText = QuantityTb.Text.Trim();
            int quantity;

            if (string.IsNullOrEmpty(quantityText))
            {
                MessageBox.Show("Quantity cannot be empty.");
                e.Cancel = true;
            }
            else if (!int.TryParse(quantityText, out quantity))
            {
                MessageBox.Show("Invalid Quantity! Please enter a valid number.");
                e.Cancel = true;
            }
            else if (quantity < 0)
            {
                MessageBox.Show("Quantity cannot be negative.");
                e.Cancel = true;
            }
            else if (quantity > 1000)
            {
                MessageBox.Show("Quantity cannot exceed 1,000.");
                e.Cancel = true;
            }
        }


        //Box Price Validation.....
        private void BPriceTb_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string priceText = BPriceTb.Text.Trim();
            double productPrice;

            if (string.IsNullOrEmpty(priceText))
            {
                MessageBox.Show("BoxPrice cannot be empty.");
                e.Cancel = true;
            }
            else if (!double.TryParse(priceText, out productPrice))
            {
                MessageBox.Show("Invalid BoxPrice! Please enter a valid number.");
                e.Cancel = true;
            }
            else if (productPrice < 0)
            {
                MessageBox.Show("Price cannot be negative.");
                e.Cancel = true;
            }
            else if (productPrice > 10000)
            {
                MessageBox.Show("Price cannot exceed 10,000.");
                e.Cancel = true;
            }
        }

        //Sales Price Validation....
        private void SPriceTb_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string priceText = SPriceTb.Text.Trim();
            double salesPrice;

            if (string.IsNullOrEmpty(priceText))
            {
                MessageBox.Show("Sales Price cannot be empty.");
                e.Cancel = true;
            }
            else if (!double.TryParse(priceText, out salesPrice))
            {
                MessageBox.Show("Invalid Sales Price! Please enter a valid number.");
                e.Cancel = true;
            }
            else if (salesPrice < 0)
            {
                MessageBox.Show("Sales Price cannot be negative.");
                e.Cancel = true;
            }
            else if (salesPrice > 10000)
            {
                MessageBox.Show("Sales Price cannot exceed 10,000.");
                e.Cancel = true;
            }
        }

        private void CatCb_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
        //Product Adding date Validation...
        private void ProdDate_Validating(object sender, CancelEventArgs e)
        {
            DateTime selectedDate = ProdDate.Value.Date;
            DateTime today = DateTime.Today;
            DateTime minDate = new DateTime(1990, 1, 1);

            if (selectedDate > today)
            {
                MessageBox.Show("Invalid Date!!! Product Add Date cannot be in the future.");
                e.Cancel = true;
            }
            else if (selectedDate < minDate)
            {
                MessageBox.Show("Invalid Date!!! Product Add Date cannot be before January 1, 1990.");
                e.Cancel = true;
            }
        }

        private void OutstockGDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SearchTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            Stock Obj = new Stock();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Customer Obj = new Customer();
            Obj.Show();
            this.Hide();
        }
    }
}
