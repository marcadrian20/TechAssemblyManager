namespace TechAssemblyManager.UI
{
    partial class CerereServiceForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lblDescriere;
        //private System.Windows.Forms.TextBox txtDescriere;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.DateTimePicker datePicker;
        //private System.Windows.Forms.Button btnTrimite;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblUser = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblDescriere = new System.Windows.Forms.Label();
            this.txtDescriere = new System.Windows.Forms.TextBox();
            this.lblData = new System.Windows.Forms.Label();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.btnTrimite = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblUser
            //
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblUser.Location = new System.Drawing.Point(50, 30);
            this.lblUser.Text = "Nume utilizator:";
            //
            // txtUser
            //
            this.txtUser.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUser.Location = new System.Drawing.Point(50, 55);
            this.txtUser.Size = new System.Drawing.Size(300, 25);
            //
            // lblDescriere
            //
            this.lblDescriere.AutoSize = true;
            this.lblDescriere.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDescriere.Location = new System.Drawing.Point(50, 95);
            this.lblDescriere.Text = "Descriere problemă:";
            //
            // txtDescriere
            //
            this.txtDescriere.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescriere.Location = new System.Drawing.Point(50, 120);
            this.txtDescriere.Multiline = true;
            this.txtDescriere.Size = new System.Drawing.Size(300, 80);
            //
            // lblData
            //
            this.lblData.AutoSize = true;
            this.lblData.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblData.Location = new System.Drawing.Point(50, 210);
            this.lblData.Text = "Data dorită:";
            //
            // datePicker
            //
            this.datePicker.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.datePicker.Location = new System.Drawing.Point(50, 235);
            this.datePicker.Size = new System.Drawing.Size(300, 25);
            //
            // btnTrimite
            //
            this.btnTrimite.Text = "Trimite Cerere";
            this.btnTrimite.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTrimite.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnTrimite.ForeColor = System.Drawing.Color.White;
            this.btnTrimite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrimite.Location = new System.Drawing.Point(110, 280);
            this.btnTrimite.Size = new System.Drawing.Size(180, 35);
            this.btnTrimite.Click += new System.EventHandler(this.BtnTrimite_Click);
            //
            // CerereServiceForm
            //
            this.ClientSize = new System.Drawing.Size(400, 350);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.lblDescriere);
            this.Controls.Add(this.txtDescriere);
            this.Controls.Add(this.lblData);
            this.Controls.Add(this.datePicker);
            this.Controls.Add(this.btnTrimite);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "CerereServiceForm";
            this.Text = "Cerere Service";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
