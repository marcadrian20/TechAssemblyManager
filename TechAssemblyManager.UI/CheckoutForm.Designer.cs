namespace TechAssemblyManager.UI
{
    partial class CheckoutForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnFinalizare;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            listView1 = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            lblTotal = new Label();
            btnFinalizare = new Button();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Location = new Point(12, 12);
            listView1.Name = "listView1";
            listView1.Size = new Size(360, 200);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Produs";
            columnHeader1.Width = 220;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Preț";
            columnHeader2.Width = 120;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotal.Location = new Point(12, 225);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(93, 19);
            lblTotal.TabIndex = 1;
            lblTotal.Text = "Total: 0 RON";
            // 
            // btnFinalizare
            // 
            btnFinalizare.Location = new Point(280, 220);
            btnFinalizare.Name = "btnFinalizare";
            btnFinalizare.Size = new Size(92, 30);
            btnFinalizare.TabIndex = 2;
            btnFinalizare.Text = "Finalizează";
            btnFinalizare.UseVisualStyleBackColor = true;
            btnFinalizare.Click += btnFinalizare_Click;
            // 
            // CheckoutForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(786, 327);
            Controls.Add(btnFinalizare);
            Controls.Add(lblTotal);
            Controls.Add(listView1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CheckoutForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Coș de cumpărături";
           /* Load += CheckoutForm_Load;*/
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
