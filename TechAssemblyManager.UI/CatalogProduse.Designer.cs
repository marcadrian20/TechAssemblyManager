namespace TechAssemblyManager.UI
{
    partial class CatalogProduse
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Button Preasamblate;
        private System.Windows.Forms.Button PiesePC;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Panel sidebar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Preasamblate = new Button();
            PiesePC = new Button();
            Back = new Button();
            sidebar = new Panel();
            Myaccount = new Button();
            cos = new Button();
            SiglaBtn = new Label();
            SiglaBtn1 = new Label();
            SuspendLayout();
            // 
            // Preasamblate
            // 
            Preasamblate.Location = new Point(50, 100);
            Preasamblate.Name = "Preasamblate";
            Preasamblate.Size = new Size(150, 60);
            Preasamblate.TabIndex = 0;
            Preasamblate.Text = "Preasamblate";
            Preasamblate.UseVisualStyleBackColor = true;
            Preasamblate.Click += Preasamblate_Click;
            // 
            // PiesePC
            // 
            PiesePC.Location = new Point(250, 100);
            PiesePC.Name = "PiesePC";
            PiesePC.Size = new Size(150, 60);
            PiesePC.TabIndex = 1;
            PiesePC.Text = "Piese PC";
            PiesePC.UseVisualStyleBackColor = true;
            PiesePC.Click += PiesePC_Click;
            // 
            // Back
            // 
            Back.Location = new Point(150, 200);
            Back.Name = "Back";
            Back.Size = new Size(150, 60);
            Back.TabIndex = 2;
            Back.Text = "Back";
            Back.UseVisualStyleBackColor = true;
            Back.Click += Back_Click;
            // 
            // sidebar
            // 
            sidebar.Location = new Point(600, 0);
            sidebar.Name = "sidebar";
            sidebar.Size = new Size(200, 450);
            sidebar.TabIndex = 3;
            // 
            // Myaccount
            // 
            Myaccount.Location = new Point(483, 0);
            Myaccount.Name = "Myaccount";
            Myaccount.Size = new Size(98, 37);
            Myaccount.TabIndex = 4;
            Myaccount.Text = "Myaccount";
            Myaccount.UseVisualStyleBackColor = true;
            Myaccount.Click += Myaccount_Click;
            // 
            // cos
            // 
            cos.Location = new Point(483, 43);
            cos.Name = "cos";
            cos.Size = new Size(98, 39);
            cos.TabIndex = 5;
            cos.Text = "cos";
            cos.UseVisualStyleBackColor = true;
            cos.Click += cos_Click;
            // 
            // SiglaBtn
            // 
            SiglaBtn.AutoSize = true;
            SiglaBtn.Location = new Point(274, 31);
            SiglaBtn.Name = "SiglaBtn";
            SiglaBtn.Size = new Size(48, 15);
            SiglaBtn.TabIndex = 6;
            SiglaBtn.Text = "Catalog";
            // 
            // SiglaBtn1
            // 
            SiglaBtn1.AutoSize = true;
            SiglaBtn1.Location = new Point(48, 16);
            SiglaBtn1.Name = "SiglaBtn1";
            SiglaBtn1.Size = new Size(32, 15);
            SiglaBtn1.TabIndex = 7;
            SiglaBtn1.Text = "back";
            SiglaBtn1.Click += SiglaBtn1_Click;
            // 
            // CatalogProduse
            // 
            ClientSize = new Size(800, 450);
            Controls.Add(SiglaBtn1);
            Controls.Add(SiglaBtn);
            Controls.Add(cos);
            Controls.Add(Myaccount);
            Controls.Add(sidebar);
            Controls.Add(Back);
            Controls.Add(PiesePC);
            Controls.Add(Preasamblate);
            Name = "CatalogProduse";
            Text = "Catalog Produse";
            Load += CatalogProduse_Load;
            ResumeLayout(false);
            PerformLayout();
        }
        private Button Myaccount;
        private Button cos;
        private Label SiglaBtn;
        private Label SiglaBtn1;
    }
}
