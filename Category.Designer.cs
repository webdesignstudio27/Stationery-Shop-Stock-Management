namespace STATIONERY_SHOP
{
    partial class Category
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Category));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            pictureBox1 = new PictureBox();
            pictureBox9 = new PictureBox();
            label1 = new Label();
            CatNameTb = new TextBox();
            label2 = new Label();
            CategoryGDV = new DataGridView();
            AddBtn = new Button();
            EditBtn = new Button();
            DeleteBtn = new Button();
            pictureBox10 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CategoryGDV).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(129, 79);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(73, 77);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // pictureBox9
            // 
            pictureBox9.Image = (Image)resources.GetObject("pictureBox9.Image");
            pictureBox9.Location = new Point(644, 25);
            pictureBox9.Name = "pictureBox9";
            pictureBox9.Size = new Size(53, 58);
            pictureBox9.TabIndex = 3;
            pictureBox9.TabStop = false;
            pictureBox9.Click += pictureBox9_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial Rounded MT Bold", 22.2F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(208, 113);
            label1.Name = "label1";
            label1.Size = new Size(344, 43);
            label1.TabIndex = 4;
            label1.Text = "Manage Category ";
            // 
            // CatNameTb
            // 
            CatNameTb.Font = new Font("Arial Unicode MS", 16.2F, FontStyle.Italic, GraphicsUnit.Point, 0);
            CatNameTb.ForeColor = Color.Red;
            CatNameTb.Location = new Point(312, 234);
            CatNameTb.Multiline = true;
            CatNameTb.Name = "CatNameTb";
            CatNameTb.PlaceholderText = "Enter Item";
            CatNameTb.Size = new Size(230, 49);
            CatNameTb.TabIndex = 10;
            CatNameTb.TextChanged += CatNameTb_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial Unicode MS", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(0, 192, 0);
            label2.Location = new Point(99, 234);
            label2.Name = "label2";
            label2.Size = new Size(173, 40);
            label2.TabIndex = 9;
            label2.Text = "Item Name";
            // 
            // CategoryGDV
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(255, 128, 128);
            dataGridViewCellStyle1.Font = new Font("Arial", 16.2F, FontStyle.Italic, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.Red;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            CategoryGDV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            CategoryGDV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            CategoryGDV.BackgroundColor = Color.White;
            CategoryGDV.BorderStyle = BorderStyle.None;
            CategoryGDV.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.Red;
            dataGridViewCellStyle2.Font = new Font("Arial", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.Red;
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            CategoryGDV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            CategoryGDV.ColumnHeadersHeight = 32;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(255, 128, 128);
            dataGridViewCellStyle3.Font = new Font("Arial", 16.2F, FontStyle.Italic, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = Color.Red;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            CategoryGDV.DefaultCellStyle = dataGridViewCellStyle3;
            CategoryGDV.GridColor = Color.Red;
            CategoryGDV.ImeMode = ImeMode.On;
            CategoryGDV.Location = new Point(71, 340);
            CategoryGDV.Name = "CategoryGDV";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = Color.Red;
            dataGridViewCellStyle4.Font = new Font("Arial", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = Color.Red;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            CategoryGDV.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            CategoryGDV.RowHeadersWidth = 51;
            CategoryGDV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            CategoryGDV.Size = new Size(626, 396);
            CategoryGDV.TabIndex = 11;
            CategoryGDV.CellContentClick += CategoryGDV_CellContentClick;
            // 
            // AddBtn
            // 
            AddBtn.BackColor = Color.Green;
            AddBtn.Font = new Font("Arial Unicode MS", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            AddBtn.ForeColor = Color.White;
            AddBtn.Location = new Point(85, 807);
            AddBtn.Name = "AddBtn";
            AddBtn.Size = new Size(153, 58);
            AddBtn.TabIndex = 12;
            AddBtn.Text = "ADD";
            AddBtn.UseVisualStyleBackColor = false;
            AddBtn.Click += AddBtn_Click;
            // 
            // EditBtn
            // 
            EditBtn.BackColor = Color.Black;
            EditBtn.Font = new Font("Arial Unicode MS", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            EditBtn.ForeColor = Color.White;
            EditBtn.Location = new Point(276, 807);
            EditBtn.Name = "EditBtn";
            EditBtn.Size = new Size(175, 58);
            EditBtn.TabIndex = 13;
            EditBtn.Text = "EDIT";
            EditBtn.UseVisualStyleBackColor = false;
            EditBtn.Click += EditBtn_Click;
            // 
            // DeleteBtn
            // 
            DeleteBtn.BackColor = Color.Red;
            DeleteBtn.Font = new Font("Arial Unicode MS", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            DeleteBtn.ForeColor = Color.White;
            DeleteBtn.Location = new Point(492, 807);
            DeleteBtn.Name = "DeleteBtn";
            DeleteBtn.Size = new Size(205, 58);
            DeleteBtn.TabIndex = 14;
            DeleteBtn.Text = "DELETE";
            DeleteBtn.UseVisualStyleBackColor = false;
            DeleteBtn.Click += DeleteBtn_Click;
            // 
            // pictureBox10
            // 
            pictureBox10.BackColor = Color.Green;
            pictureBox10.Image = (Image)resources.GetObject("pictureBox10.Image");
            pictureBox10.Location = new Point(99, 823);
            pictureBox10.Name = "pictureBox10";
            pictureBox10.Size = new Size(25, 29);
            pictureBox10.TabIndex = 43;
            pictureBox10.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Black;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(292, 823);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(30, 29);
            pictureBox2.TabIndex = 44;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Red;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(503, 823);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(30, 29);
            pictureBox3.TabIndex = 45;
            pictureBox3.TabStop = false;
            // 
            // Category
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(755, 939);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox10);
            Controls.Add(DeleteBtn);
            Controls.Add(EditBtn);
            Controls.Add(AddBtn);
            Controls.Add(CategoryGDV);
            Controls.Add(CatNameTb);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox9);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Category";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Category";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)CategoryGDV).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox9;
        private Label label1;
        private TextBox CatNameTb;
        private Label label2;
        private DataGridView CategoryGDV;
        private Button AddBtn;
        private Button EditBtn;
        private Button DeleteBtn;
        private PictureBox pictureBox10;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
    }
}