namespace TechAssemblyManager.UI
{
    partial class ProductViewerForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            Filtre = new ComboBox();
            button2 = new Button();
            Account = new Button();
            btnVeziCos = new Button();
            button13 = new Button();
            SiglaMainForm = new Button();
            lblPagina = new Label();
            SuspendLayout();
            // 
            // Filtre
            // 
            Filtre.FormattingEnabled = true;
            Filtre.Items.AddRange(new object[] { "Filtre pret(Range list)" });
            Filtre.Location = new Point(12, 64);
            Filtre.Name = "Filtre";
            Filtre.Size = new Size(121, 23);
            Filtre.TabIndex = 1;
            Filtre.Text = "Filtre";
            Filtre.SelectedIndexChanged += Filtre_SelectedIndexChanged;
            // 
            // button2
            // 
            button2.Location = new Point(12, 12);
            button2.Name = "button2";
            button2.Size = new Size(121, 46);
            button2.TabIndex = 2;
            button2.Text = "back button";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Account
            // 
            Account.Location = new Point(666, 3);
            Account.Name = "Account";
            Account.Size = new Size(122, 34);
            Account.TabIndex = 4;
            Account.Text = "Account";
            Account.UseVisualStyleBackColor = true;
            Account.Click += Account_Click;
            // 
            // btnVeziCos
            // 
            btnVeziCos.Location = new Point(674, 43);
            btnVeziCos.Name = "btnVeziCos";
            btnVeziCos.Size = new Size(98, 36);
            btnVeziCos.TabIndex = 5;
            btnVeziCos.Text = "Cos";
            btnVeziCos.UseVisualStyleBackColor = true;
            btnVeziCos.Click += btnVeziCos_Click;
            // 
            // button13
            // 
            button13.Location = new Point(22, 218);
            button13.Name = "button13";
            button13.Size = new Size(104, 23);
            button13.TabIndex = 14;
            button13.Text = "Adauga in cos";
            button13.UseVisualStyleBackColor = true;
            //button13.Click += button13_Click;
            // 
            // SiglaMainForm
            // 
            SiglaMainForm.Location = new Point(371, 37);
            SiglaMainForm.Name = "SiglaMainForm";
            SiglaMainForm.Size = new Size(75, 23);
            SiglaMainForm.TabIndex = 15;
            SiglaMainForm.Text = "Sigla MainForm";
            SiglaMainForm.UseVisualStyleBackColor = true;
            SiglaMainForm.Click += SiglaMainForm_Click;
            // 
            // lblPagina
            // 
            lblPagina.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblPagina.AutoSize = true;
            lblPagina.Location = new Point(700, 430);
            lblPagina.Name = "lblPagina";
            lblPagina.Size = new Size(87, 15);
            lblPagina.TabIndex = 16;
            lblPagina.Text = "Pagina 1 din 10";
            // 
            // ProductViewerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblPagina);
            Controls.Add(SiglaMainForm);
            Controls.Add(button13);
            Controls.Add(btnVeziCos);
            Controls.Add(Account);
            Controls.Add(button2);
            Controls.Add(Filtre);
            Name = "ProductViewerForm";
            Text = "ProductViewerForm";
            Load += ProductViewerForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox Filtre;
        private Button button2;
        private Button Account;
        private Button btnVeziCos;
        private Button button13;
        private Button SiglaMainForm;
        private Label lblPagina;
    }
}
