namespace TechAssemblyManager.UI
{
    partial class CartForm
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
            Myaccount = new Button();
            cos = new Button();
            Cosdecumparaturi = new Button();
            backbutton = new Button();
            listBox1 = new ListBox();
            Total = new TextBox();
            PlasareComanda = new Button();
            SuspendLayout();
            // 
            // Myaccount
            // 
            Myaccount.Location = new Point(650, 31);
            Myaccount.Name = "Myaccount";
            Myaccount.Size = new Size(103, 23);
            Myaccount.TabIndex = 0;
            Myaccount.Text = "My account";
            Myaccount.UseVisualStyleBackColor = true;
            Myaccount.Click += Myaccount_Click;
            // 
            // cos
            // 
            cos.Location = new Point(664, 60);
            cos.Name = "cos";
            cos.Size = new Size(75, 23);
            cos.TabIndex = 1;
            cos.Text = "cos";
            cos.UseVisualStyleBackColor = true;
            // 
            // Cosdecumparaturi
            // 
            Cosdecumparaturi.Location = new Point(363, 49);
            Cosdecumparaturi.Name = "Cosdecumparaturi";
            Cosdecumparaturi.Size = new Size(137, 34);
            Cosdecumparaturi.TabIndex = 2;
            Cosdecumparaturi.Text = "Cos de cumparaturi";
            Cosdecumparaturi.UseVisualStyleBackColor = true;
            // 
            // backbutton
            // 
            backbutton.Location = new Point(75, 45);
            backbutton.Name = "backbutton";
            backbutton.Size = new Size(118, 38);
            backbutton.TabIndex = 3;
            backbutton.Text = "Tech Assembly Manager";
            backbutton.UseVisualStyleBackColor = true;
            backbutton.Click += backbutton_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(171, 103);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(501, 154);
            listBox1.TabIndex = 4;
            // 
            // Total
            // 
            Total.Location = new Point(287, 263);
            Total.Name = "Total";
            Total.Size = new Size(248, 23);
            Total.TabIndex = 5;
            Total.Text = "Totalul:.........";
            // 
            // PlasareComanda
            // 
            PlasareComanda.Location = new Point(324, 300);
            PlasareComanda.Name = "PlasareComanda";
            PlasareComanda.Size = new Size(176, 30);
            PlasareComanda.TabIndex = 6;
            PlasareComanda.Text = "Plaseaza comanda";
            PlasareComanda.UseVisualStyleBackColor = true;
            PlasareComanda.Click += PlasareComanda_Click;
            // 
            // CartForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(PlasareComanda);
            Controls.Add(Total);
            Controls.Add(listBox1);
            Controls.Add(backbutton);
            Controls.Add(Cosdecumparaturi);
            Controls.Add(cos);
            Controls.Add(Myaccount);
            Name = "CartForm";
            Text = "CartForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Myaccount;
        private Button cos;
        private Button Cosdecumparaturi;
        private Button backbutton;
        private ListBox listBox1;
        private TextBox Total;
        private Button PlasareComanda;
    }
}