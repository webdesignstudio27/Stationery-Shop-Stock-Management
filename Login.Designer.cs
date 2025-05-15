namespace STATIONERY_SHOP
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            pictureBox1 = new PictureBox();
            pictureBox9 = new PictureBox();
            panel1 = new Panel();
            pictureBox4 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            PasswordTb = new TextBox();
            UNameTb = new TextBox();
            LoginBtn = new Button();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(711, 224);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(70, 70);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox9
            // 
            pictureBox9.Image = (Image)resources.GetObject("pictureBox9.Image");
            pictureBox9.Location = new Point(1362, 21);
            pictureBox9.Name = "pictureBox9";
            pictureBox9.Size = new Size(53, 58);
            pictureBox9.TabIndex = 2;
            pictureBox9.TabStop = false;
            pictureBox9.Click += pictureBox9_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(pictureBox4);
            panel1.Controls.Add(pictureBox3);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(PasswordTb);
            panel1.Controls.Add(UNameTb);
            panel1.Controls.Add(LoginBtn);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox9);
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(62, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1482, 853);
            panel1.TabIndex = 19;
            panel1.BackColorChanged += LoginBtn_Click;
            panel1.ForeColorChanged += pictureBox1_Click;
            panel1.Paint += panel1_Paint;
            panel1.Enter += pictureBox1_Click;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(837, 565);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(48, 48);
            pictureBox4.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox4.TabIndex = 12;
            pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(1075, 460);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(48, 48);
            pictureBox3.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox3.TabIndex = 11;
            pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(1075, 374);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(48, 48);
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox2.TabIndex = 10;
            pictureBox2.TabStop = false;
            // 
            // PasswordTb
            // 
            PasswordTb.Font = new Font("Arial Unicode MS", 18F, FontStyle.Italic, GraphicsUnit.Point, 0);
            PasswordTb.ForeColor = Color.Red;
            PasswordTb.Location = new Point(693, 459);
            PasswordTb.Multiline = true;
            PasswordTb.Name = "PasswordTb";
            PasswordTb.PasswordChar = '*';
            PasswordTb.PlaceholderText = "Password";
            PasswordTb.Size = new Size(367, 49);
            PasswordTb.TabIndex = 9;
            // 
            // UNameTb
            // 
            UNameTb.Font = new Font("Arial Unicode MS", 13.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            UNameTb.ForeColor = Color.Red;
            UNameTb.Location = new Point(693, 374);
            UNameTb.Multiline = true;
            UNameTb.Name = "UNameTb";
            UNameTb.PlaceholderText = "User Name";
            UNameTb.Size = new Size(367, 49);
            UNameTb.TabIndex = 8;
            UNameTb.TextChanged += UNameTb_TextChanged;
            // 
            // LoginBtn
            // 
            LoginBtn.BackColor = Color.Purple;
            LoginBtn.Font = new Font("Arial Unicode MS", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            LoginBtn.ForeColor = Color.White;
            LoginBtn.Location = new Point(644, 565);
            LoginBtn.Name = "LoginBtn";
            LoginBtn.Size = new Size(187, 58);
            LoginBtn.TabIndex = 7;
            LoginBtn.Text = "LOG IN";
            LoginBtn.UseVisualStyleBackColor = false;
            LoginBtn.BackColorChanged += LoginBtn_Click;
            LoginBtn.Click += LoginBtn_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Arial Unicode MS", 13.8F, FontStyle.Italic | FontStyle.Underline, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Red;
            label4.Location = new Point(633, 673);
            label4.Name = "label4";
            label4.Size = new Size(211, 31);
            label4.TabIndex = 6;
            label4.Text = "Continue As Admin";
            label4.Click += label4_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Arial Unicode MS", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(0, 192, 0);
            label3.Location = new Point(496, 459);
            label3.Name = "label3";
            label3.Size = new Size(158, 40);
            label3.TabIndex = 5;
            label3.Text = "Password";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial Unicode MS", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(0, 192, 0);
            label2.Location = new Point(487, 382);
            label2.Name = "label2";
            label2.Size = new Size(179, 40);
            label2.TabIndex = 4;
            label2.Text = "User Name";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Algerian", 48F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(369, 116);
            label1.Name = "label1";
            label1.Size = new Size(735, 89);
            label1.TabIndex = 3;
            label1.Text = "STATIONERY SHOP";
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 192, 0);
            ClientSize = new Size(1544, 853);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox pictureBox1;
        private PictureBox pictureBox9;
        private Panel panel1;
        private Label label1;
        private Label label4;
        private Label label3;
        private Button LoginBtn;
        private TextBox PasswordTb;
        private TextBox UNameTb;
        private Label label2;
        private PictureBox pictureBox4;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
    }
}