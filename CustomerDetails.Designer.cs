namespace STATIONERY_SHOP
{
    partial class CustomerDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerDetails));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            pictureBox12 = new PictureBox();
            label6 = new Label();
            RefreshImg = new PictureBox();
            SearchImg = new PictureBox();
            SearchTb = new TextBox();
            SalesDGV = new DataGridView();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RefreshImg).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SearchImg).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SalesDGV).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox12
            // 
            pictureBox12.Image = (Image)resources.GetObject("pictureBox12.Image");
            pictureBox12.Location = new Point(639, 200);
            pictureBox12.Name = "pictureBox12";
            pictureBox12.Size = new Size(54, 52);
            pictureBox12.TabIndex = 77;
            pictureBox12.TabStop = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Arial Unicode MS", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Purple;
            label6.Location = new Point(204, 170);
            label6.Name = "label6";
            label6.Size = new Size(173, 40);
            label6.TabIndex = 76;
            label6.Text = "Item Name";
            // 
            // RefreshImg
            // 
            RefreshImg.Image = (Image)resources.GetObject("RefreshImg.Image");
            RefreshImg.Location = new Point(564, 200);
            RefreshImg.Name = "RefreshImg";
            RefreshImg.Size = new Size(69, 52);
            RefreshImg.TabIndex = 75;
            RefreshImg.TabStop = false;
            // 
            // SearchImg
            // 
            SearchImg.Image = (Image)resources.GetObject("SearchImg.Image");
            SearchImg.Location = new Point(474, 200);
            SearchImg.Name = "SearchImg";
            SearchImg.Size = new Size(69, 52);
            SearchImg.TabIndex = 74;
            SearchImg.TabStop = false;
            SearchImg.Click += SearchImg_Click;
            // 
            // SearchTb
            // 
            SearchTb.Font = new Font("Arial Unicode MS", 13.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            SearchTb.ForeColor = Color.Red;
            SearchTb.Location = new Point(204, 213);
            SearchTb.Multiline = true;
            SearchTb.Name = "SearchTb";
            SearchTb.PlaceholderText = "Search Item Name";
            SearchTb.Size = new Size(264, 39);
            SearchTb.TabIndex = 73;
            // 
            // SalesDGV
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(255, 192, 192);
            dataGridViewCellStyle1.Font = new Font("Arial", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.Red;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            SalesDGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            SalesDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            SalesDGV.BackgroundColor = Color.White;
            SalesDGV.BorderStyle = BorderStyle.None;
            SalesDGV.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.Red;
            dataGridViewCellStyle2.Font = new Font("Arial", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.Red;
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            SalesDGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            SalesDGV.ColumnHeadersHeight = 45;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(255, 128, 128);
            dataGridViewCellStyle3.Font = new Font("Arial", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = Color.Red;
            dataGridViewCellStyle3.SelectionBackColor = Color.Red;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            SalesDGV.DefaultCellStyle = dataGridViewCellStyle3;
            SalesDGV.GridColor = Color.Red;
            SalesDGV.ImeMode = ImeMode.On;
            SalesDGV.Location = new Point(31, 317);
            SalesDGV.Name = "SalesDGV";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(255, 128, 128);
            dataGridViewCellStyle4.Font = new Font("Arial Narrow", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = Color.Red;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            SalesDGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            SalesDGV.RowHeadersWidth = 70;
            SalesDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            SalesDGV.Size = new Size(1102, 610);
            SalesDGV.TabIndex = 72;
            SalesDGV.CellContentClick += SalesDGV_CellContentClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial Rounded MT Bold", 22.2F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(97, 69);
            label1.Name = "label1";
            label1.Size = new Size(276, 43);
            label1.TabIndex = 71;
            label1.Text = "Buying Details";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(22, 35);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(69, 77);
            pictureBox1.TabIndex = 70;
            pictureBox1.TabStop = false;
            // 
            // CustomerDetatils
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1177, 1073);
            Controls.Add(pictureBox12);
            Controls.Add(label6);
            Controls.Add(RefreshImg);
            Controls.Add(SearchImg);
            Controls.Add(SearchTb);
            Controls.Add(SalesDGV);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CustomerDetatils";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CustomerDetatils";
            ((System.ComponentModel.ISupportInitialize)pictureBox12).EndInit();
            ((System.ComponentModel.ISupportInitialize)RefreshImg).EndInit();
            ((System.ComponentModel.ISupportInitialize)SearchImg).EndInit();
            ((System.ComponentModel.ISupportInitialize)SalesDGV).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox12;
        private Label label6;
        private PictureBox RefreshImg;
        private PictureBox SearchImg;
        private TextBox SearchTb;
        private DataGridView SalesDGV;
        private Label label1;
        private PictureBox pictureBox1;
    }
}