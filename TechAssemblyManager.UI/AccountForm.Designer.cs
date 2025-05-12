namespace TechAssemblyManager.UI
{
    partial class AccountForm
    {
        private Button btnLogout;

        private void InitializeComponent()
        {
            btnLogout = new Button();
            back = new Button();
            SuspendLayout();
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
            Controls.Add(btnLogout);
            Name = "AccountForm";
            Text = "Contul Meu";
            Load += AccountForm_Load;
            ResumeLayout(false);
        }
        private Button back;
    }
}
