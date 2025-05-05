
namespace TechAssemblyManager.UI
{
    partial class MainForm
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
            TechAssemblyManager = new GroupBox();
            pictureBox1 = new PictureBox();
            CerereService = new Button();
            ViewCatalog = new Button();
            Promotii = new Button();
            cos = new Button();
            Myaccount = new Button();
            Searchbar = new TextBox();
            TechAssemblyManager.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // TechAssemblyManager
            // 
            TechAssemblyManager.Controls.Add(pictureBox1);
            TechAssemblyManager.Controls.Add(CerereService);
            TechAssemblyManager.Controls.Add(ViewCatalog);
            TechAssemblyManager.Controls.Add(Promotii);
            TechAssemblyManager.Controls.Add(cos);
            TechAssemblyManager.Controls.Add(Myaccount);
            TechAssemblyManager.Controls.Add(Searchbar);
            TechAssemblyManager.Location = new Point(27, 12);
            TechAssemblyManager.Name = "TechAssemblyManager";
            TechAssemblyManager.Size = new Size(427, 250);
            TechAssemblyManager.TabIndex = 0;
            TechAssemblyManager.TabStop = false;
            TechAssemblyManager.Text = "TechAssemblyManage";
            TechAssemblyManager.Enter += TechAssemblyManager_Enter;
    
      // Fix for CS1061: The issue is that `TechAssemblyManager.UI.Properties.Resources.pcgarage` is being accessed incorrectly.  
            // The `UI` namespace is not part of the `GroupBox` class. Instead, it should be accessed directly from the `Properties.Resources` namespace.  
            // Correcting the line to properly reference the resource.  

            pictureBox1.Image = Properties.Resources.pcgarage;
            pictureBox1.Location = new Point(18, 22);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(77, 112);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // CerereService
            // 
            CerereService.Location = new Point(101, 192);
            CerereService.Name = "CerereService";
            CerereService.Size = new Size(193, 38);
            CerereService.TabIndex = 6;
            CerereService.Text = "Cerere service";
            CerereService.UseVisualStyleBackColor = true;
            CerereService.Click += CerereService_Click;
            // 
            // ViewCatalog
            // 
            ViewCatalog.Location = new Point(101, 135);
            ViewCatalog.Name = "ViewCatalog";
            ViewCatalog.Size = new Size(193, 42);
            ViewCatalog.TabIndex = 5;
            ViewCatalog.Text = "View Catalog";
            ViewCatalog.UseVisualStyleBackColor = true;
            ViewCatalog.Click += ViewCatalog_Click;
            // 
            // Promotii
            // 
            Promotii.Location = new Point(101, 78);
            Promotii.Name = "Promotii";
            Promotii.Size = new Size(198, 45);
            Promotii.TabIndex = 4;
            Promotii.Text = "Promotii";
            Promotii.UseVisualStyleBackColor = true;
            Promotii.Click += Promotii_Click;
            // 
            // cos
            // 
            cos.Location = new Point(314, 64);
            cos.Name = "cos";
            cos.Size = new Size(75, 23);
            cos.TabIndex = 3;
            cos.Text = "cos";
            cos.UseVisualStyleBackColor = true;
            cos.Click += cos_Click;
            // 
            // Myaccount
            // 
            Myaccount.Location = new Point(297, 9);
            Myaccount.Name = "Myaccount";
            Myaccount.Size = new Size(110, 49);
            Myaccount.TabIndex = 2;
            Myaccount.Text = "my account";
            Myaccount.UseVisualStyleBackColor = true;
            Myaccount.Click += Myaccount_Click;
            // 
            // Searchbar
            // 
            Searchbar.Location = new Point(147, 23);
            Searchbar.Name = "Searchbar";
            Searchbar.Size = new Size(133, 23);
            Searchbar.TabIndex = 1;
            Searchbar.Text = "Searchbar";
            Searchbar.TextChanged += Searchbar_TextChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(TechAssemblyManager);
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            TechAssemblyManager.ResumeLayout(false);
            TechAssemblyManager.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private GroupBox TechAssemblyManager;
        private Button cos;
        private Button Myaccount;
        private TextBox Searchbar;
        private Button CerereService;
        private Button ViewCatalog;
        private Button Promotii;
        private PictureBox pictureBox1;
    }
}