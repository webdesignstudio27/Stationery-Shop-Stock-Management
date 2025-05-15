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
    public partial class Category : Form
    {
        public Category()
        {
            InitializeComponent();
            Methods obj = new Methods();
            obj.DisplayData("CategoryTbl", CategoryGDV);
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFileName=C:\Users\sivas\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\STATIONERYDB.mdf;Initial Catalog=STATIONERYDB;Integrated Security=True;Connect Timeout=30;");
        private void AddBtn_Click(object sender, EventArgs e)
        {
            string categoryName = CatNameTb.Text;

            if (CatNameTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                if (IsCategoryNameValid(categoryName))
                {
                    try
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("insert into CategoryTbl(CatName) values (@CN)", Con);
                        cmd.Parameters.AddWithValue("@CN", categoryName);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Category Added!!!");
                        Con.Close();
                        Methods obj = new Methods();
                        obj.DisplayData("CategoryTbl", CategoryGDV);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            string categoryName = CatNameTb.Text;
            if (CatNameTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                if (IsCategoryNameValid(categoryName))
                {
                    try
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("update CategoryTbl set CatName=@CN where CatId=@CK", Con);
                        cmd.Parameters.AddWithValue("@CN", categoryName);
                        cmd.Parameters.AddWithValue("@CK", Key);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Category Updated!!!");
                        Con.Close();
                        Methods obj = new Methods();
                        obj.DisplayData("CategoryTbl", CategoryGDV);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
            }
        }

        int Key = 0;
        private void CategoryGDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CategoryGDV.SelectedRows.Count > 0)
            {
                if (CategoryGDV.SelectedRows[0].Cells.Count > 1)
                {
                    var cellValue1 = CategoryGDV.SelectedRows[0].Cells[1].Value;
                    CatNameTb.Text = cellValue1 != null ? cellValue1.ToString() : string.Empty;
                    if (string.IsNullOrEmpty(CatNameTb.Text))
                    {
                        Key = 0;
                    }
                    else
                    {
                        var cellValue0 = CategoryGDV.SelectedRows[0].Cells[0].Value;
                        if (cellValue0 != null && int.TryParse(cellValue0.ToString(), out int result))
                        {
                            Key = result;
                        }
                        else
                        {
                            Key = 0;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Selected row does not contain enough cells.");
                }
            }
            else
            {
                MessageBox.Show("No row is selected.");
                CatNameTb.Text = string.Empty;
                Key = 0;
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
                    SqlCommand cmd = new SqlCommand("delete from CategoryTbl where CatId=@CK", Con);
                    cmd.Parameters.AddWithValue("@CK", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Deleted!!!");
                    Con.Close();
                    Methods obj = new Methods();
                    obj.DisplayData("CategoryTbl", CategoryGDV);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private bool IsCategoryNameValid(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Category Name cannot be empty.");
                return false;
            }
            string pattern = @"^[a-zA-Z ]+$";

            if (!Regex.IsMatch(name, pattern))
            {
                MessageBox.Show("Category Name should contain only alphabetic characters and spaces.");
                return false;
            }
            if (name.Length < 4 || name.Length > 25)
            {
                MessageBox.Show("Category Name must be between 4 and 10 characters long.");
                return false;
            }

            return true;
        }

        private void CatNameTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
