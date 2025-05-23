using System.Windows;

namespace TechAssemblyManager.UI
{
    public partial class AddEmployeeDialog : Window
    {
        public string UserName => TxtUserName.Text.Trim();
        public string Email => TxtEmail.Text.Trim();
        public string FirstName => TxtFirstName.Text.Trim();
        public string LastName => TxtLastName.Text.Trim();
        public string Password => TxtPassword.Password;
        public bool IsSenior => ChkIsSenior.IsChecked == true;

        public AddEmployeeDialog()
        {
            InitializeComponent();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserName) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(FirstName) ||
                string.IsNullOrWhiteSpace(LastName) ||
                string.IsNullOrWhiteSpace(Password))
            {
                LblError.Text = "Completează toate câmpurile!";
                LblError.Visibility = Visibility.Visible;
                return;
            }
            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}