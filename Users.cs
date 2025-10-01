using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STATIONERY_SHOP
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            Methods obj = new Methods();
            obj.DisplayData("UserTbl", UserGDV);
            GenCb.Items.Add("Select Gender");
            GenCb.SelectedIndex = 0;
            EmailTb.TextChanged += EmailTb_TextChanged;

        }

        private SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFileName=C:\Users\sivas\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\STATIONERYDB.mdf;Initial Catalog=STATIONERYDB;Integrated Security=True;Connect Timeout=30;");

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            string name = UNameTb.Text;
            string email = EmailTb.Text;
            string password = PasswordTb.Text;
            string phoneNumber = PhoneTb.Text;
            string gender = GenCb.SelectedItem.ToString();


            if (UNameTb.Text == "" || GenCb.Text == "Select Gender " || EmailTb.Text == "" || PasswordTb.Text == "" || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                ValidatePassword(password);
                if (IsNameValid(name) && IsEmailValid(email) && IsPasswordValid(password) && IsPhoneNumberValid(phoneNumber))
                {
                    try
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("insert into UserTbl(UName,UGen,UDOB,UEmail,UPassword,Uphone) values (@UN,@UG,@UD,@UEM,@UPa,@UP)", Con);
                        cmd.Parameters.AddWithValue("@UN", name);
                        cmd.Parameters.AddWithValue("@UG", gender);
                        cmd.Parameters.AddWithValue("@UD", UDOB.Value.Date);
                        cmd.Parameters.AddWithValue("@UEM", email);
                        cmd.Parameters.AddWithValue("@UPa", password);
                        cmd.Parameters.AddWithValue("@UP", phoneNumber);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User Added!!!");
                        Con.Close();
                        Methods obj = new Methods();
                        obj.DisplayData("UserTbl", UserGDV);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
            }
        }

        int Key = 0;
        private void UserGDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UNameTb.Text = UserGDV.SelectedRows[0].Cells[1].Value.ToString();
            GenCb.Text = UserGDV.SelectedRows[0].Cells[2].Value.ToString();
            UDOB.Text = UserGDV.SelectedRows[0].Cells[3].Value.ToString();
            EmailTb.Text = UserGDV.SelectedRows[0].Cells[4].Value.ToString();
            PasswordTb.Text = UserGDV.SelectedRows[0].Cells[5].Value.ToString();
            PhoneTb.Text = UserGDV.SelectedRows[0].Cells[6].Value.ToString();
            if (UNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(UserGDV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            string name = UNameTb.Text;
            string password = PasswordTb.Text;
            string phoneNumber = PhoneTb.Text;
            string email = EmailTb.Text;

            if (UNameTb.Text == "" || GenCb.Text == "" || EmailTb.Text == "" || PasswordTb.Text == "" || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                ValidatePassword(password);
                if (IsNameValid(name) && IsPasswordValid(password) && IsPhoneNumberValid(phoneNumber) && IsEmailValid(email))
                {
                    try
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("update UserTbl set UName=@UN,UGen=@UG,UDOB=@UD,UEmail=@UEM,UPassword=@UPa,Uphone=@UP where UId=@UK", Con);
                        cmd.Parameters.AddWithValue("@UN", name);
                        cmd.Parameters.AddWithValue("@UG", value: GenCb.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@UD", UDOB.Value.Date);
                        cmd.Parameters.AddWithValue("@UEM", email);
                        cmd.Parameters.AddWithValue("@UPa", password);
                        cmd.Parameters.AddWithValue("@UP", phoneNumber);
                        cmd.Parameters.AddWithValue("@UK", Key);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User Updated!!!");
                        Con.Close();
                        Methods obj = new Methods();
                        obj.DisplayData("UserTbl", UserGDV);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
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
                    SqlCommand cmd = new SqlCommand("delete from UserTbl where UId=@UK", Con);
                    cmd.Parameters.AddWithValue("@UK", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Deleted!!!");
                    Con.Close();
                    Methods obj = new Methods();
                    obj.DisplayData("UserTbl", UserGDV);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox16_Click(object sender, EventArgs e)
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

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            //Users Obj = new Users();
            //Obj.Show();
            //this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        //User Name Validation....
        private bool IsNameValid(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Name cannot be empty.");
                return false;
            }

            // Check if name contains only alphabetic characters and optional spaces
            string pattern = @"^[a-zA-Z ]+$";

            if (!Regex.IsMatch(name, pattern))
            {
                MessageBox.Show("Name should contain only alphabetic characters and spaces.");
                return false;
            }

            // Check if name length is between 4 and 10 characters
            if (name.Length < 4 || name.Length > 25)
            {
                MessageBox.Show("Name must be between 4 and 10 characters long.");
                return false;
            }

            return true;
        }




        private void EmailTb_TextChanged(object sender, EventArgs e)
        {
            string originalText = EmailTb.Text;
            string modifiedText = ModifyText(originalText);
            EmailTb.Text = modifiedText;
        }
        private string ModifyText(string input)
        {

            return input.ToLower();
        }
        // E-Mail Address Validation....
        private bool IsEmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Email address cannot be empty.");
                return false;
            }
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            if (!Regex.IsMatch(email, pattern))
            {
                MessageBox.Show("Invalid email address!! format 'Username@gmail.com'.");
                return false;
            }

            return true;
        }


        //Password Validation.......
        private void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Password cannot be empty.");
            }
            else if (password.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long.");
            }
            else if (!Regex.IsMatch(password, @"[A-Z]"))
            {
                MessageBox.Show("Password must contain at least one uppercase letter.");
            }
            else if (!Regex.IsMatch(password, @"[a-z]"))
            {
                MessageBox.Show("Password must contain at least one lowercase letter.");
            }
            else if (!Regex.IsMatch(password, @"[0-9]"))
            {
                MessageBox.Show("Password must contain at least one number.");
            }
            else if (!Regex.IsMatch(password, @"[\W_]"))
            {
                MessageBox.Show("Password must contain at least one special character.");
            }

        }
        private bool IsPasswordValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password) ||
                password.Length < 8 ||
                !Regex.IsMatch(password, @"[A-Z]") ||
                !Regex.IsMatch(password, @"[a-z]") ||
                !Regex.IsMatch(password, @"[0-9]") ||
                !Regex.IsMatch(password, @"[\W_]"))
            {
                return false;
            }
            return true;
        }
        // Phone Validation.......
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

        private void pictureBox12_Click(object sender, EventArgs e)
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
