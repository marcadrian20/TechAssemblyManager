namespace TechAssemblyManager.UI
{
    partial class AccountForm
    {
        private Label lblUser;
        private Button btnLogout;

        private void InitializeComponent()
        {
            lblUser = new Label();
            btnLogout = new Button();
            SuspendLayout();

            // lblUser
            lblUser.AutoSize = true;
            lblUser.Font = new Font("Segoe UI", 10F);
            lblUser.Location = new Point(20, 20);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(150, 23);
            lblUser.Text = "Utilizator: Guest";

            // btnLogout
            btnLogout.Location = new Point(20, 60);
            btnLogout.Size = new Size(120, 30);
            btnLogout.Text = "Logout";
            btnLogout.Click += btnLogout_Click;

            // AccountForm
            ClientSize = new Size(250, 120);
            Controls.Add(lblUser);
            Controls.Add(btnLogout);
            Name = "AccountForm";
            Text = "Contul Meu";
            Load += AccountForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
