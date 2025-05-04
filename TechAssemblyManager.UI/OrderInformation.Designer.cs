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
            Introducereinformatii = new ListBox();
            Trimitere = new Button();
            SuspendLayout();
            // 
            // Introducereinformatii
            // 
            Introducereinformatii.FormattingEnabled = true;
            Introducereinformatii.ItemHeight = 15;
            Introducereinformatii.Location = new Point(162, 43);
            Introducereinformatii.Name = "Introducereinformatii";
            Introducereinformatii.Size = new Size(466, 244);
            Introducereinformatii.TabIndex = 0;
            Introducereinformatii.TabStop = false;
            // 
            // Trimitere
            // 
            Trimitere.Location = new Point(319, 304);
            Trimitere.Name = "Trimitere";
            Trimitere.Size = new Size(192, 43);
            Trimitere.TabIndex = 1;
            Trimitere.Text = "Trimitere comanda";
            Trimitere.UseVisualStyleBackColor = true;
            Trimitere.Click += Trimitere_Click;
            // 
            // OrderInformation
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Trimitere);
            Controls.Add(Introducereinformatii);
            Name = "OrderInformation";
            Text = "OrderInformation";
            Load += OrderInformation_Load;
            ResumeLayout(false);
            this.Load += new System.EventHandler(this.OrderInformation_Load);
            this.Resize += new System.EventHandler(this.OrderInformation_Resize);
        }

        #endregion

        private ListBox Introducereinformatii;
        private Button Trimitere;

    }
}