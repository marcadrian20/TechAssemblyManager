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
            back = new Button();
            SuspendLayout();
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Font = new Font("Segoe UI", 10F);
            lblUser.Location = new Point(20, 20);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(107, 19);
            lblUser.TabIndex = 0;
            lblUser.Text = "Utilizator: Guest";
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(20, 60);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(120, 30);
            btnLogout.TabIndex = 1;
            btnLogout.Text = "Logout";
            btnLogout.Click += btnLogout_Click;
            // 
            // back
            // 
            back.Location = new Point(147, 17);
            back.Name = "back";
            back.Size = new Size(75, 23);
            back.TabIndex = 2;
            back.Text = "button1";
            back.UseVisualStyleBackColor = true;
            back.Click += back_Click;
            // 
            // AccountForm
            // 
            ClientSize = new Size(250, 120);
            Controls.Add(back);
            Controls.Add(lblUser);
            Controls.Add(btnLogout);
            Name = "AccountForm";
            Text = "Contul Meu";
            Load += AccountForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
        private Button back;
    }
}
