namespace TechAssemblyManager.UI
{
    partial class OrderInformation
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
            Trimitere = new Button();
            Account = new Button();
            SuspendLayout();
            // 
            // Trimitere
            // 
            Trimitere.Location = new Point(319, 304);
            Trimitere.Name = "Trimitere";
            Trimitere.Size = new Size(192, 43);
            Trimitere.TabIndex = 1;
            Trimitere.Text = "Trimitere comanda";
            Trimitere.UseVisualStyleBackColor = true;
            //Trimitere.Click += Trimitere_Click;
            // 
            // Account
            // 
            Account.Location = new Point(354, 98);
            Account.Name = "Account";
            Account.Size = new Size(75, 23);
            Account.TabIndex = 2;
            Account.Text = "Account";
            Account.UseVisualStyleBackColor = true;
            //Account.Click += Account_Click;
            // 
            // OrderInformation
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Account);
            Controls.Add(Trimitere);
            Name = "OrderInformation";
            Text = "OrderInformation";
            Load += OrderInformation_Load;
            Resize += OrderInformation_Resize;
            ResumeLayout(false);
        }

        #endregion
        private Button Trimitere;
        private Button Account;
    }
}