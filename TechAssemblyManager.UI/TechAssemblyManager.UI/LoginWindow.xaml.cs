using FirebaseWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TechAssemblyManager.DAL;
using TechAssemblyManager.DAL.FirebaseHelper;
using TechAssemblyManager.Models;


namespace TechAssemblyManager.UI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private FirebaseHelper _firebaseHelper;     
        public LoginWindow()
        {
            InitializeComponent();
            _firebaseHelper = new FirebaseHelper(
               "https://techassemblymanager-default-rtdb.firebaseio.com/",
               "ky7wJX7Iu46hjBHWqDJNWjJW19NeYQurX4Z9VeUv",
               "AIzaSyBxq3J01JqE6yonLc9plkzA6c3-Gi1r1eU"
           );
        }

        public async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = TxtUsername.Text.Trim();
            string password = TxtPassword.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                LblError.Text = "Introduceți toate câmpurile!";
                LblError.Visibility = Visibility.Visible;
                return;
            }

            User user = await _firebaseHelper.LoginAsync(username, password);
            if (user != null)
            {
                SessionManager.LoggedInUser = user;
                MessageBox.Show($"Bun venit, {user.firstName}!", "Succes");

                var main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                LblError.Text = "Autentificare eșuată. Verifică datele.";
                LblError.Visibility = Visibility.Visible;
            }
        }
    }
}
