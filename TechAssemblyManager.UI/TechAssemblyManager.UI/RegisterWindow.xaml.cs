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
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private FirebaseHelper _firebaseHelper;
        public RegisterWindow()
        {
            InitializeComponent();
            _firebaseHelper = new FirebaseHelper(
              "https://techassemblymanager-default-rtdb.firebaseio.com/",
              "ky7wJX7Iu46hjBHWqDJNWjJW19NeYQurX4Z9VeUv",
              "AIzaSyBxq3J01JqE6yonLc9plkzA6c3-Gi1r1eU"
          );
        }

        private async void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = TxtUsername.Text.Trim();
            string email = TxtEmail.Text.Trim();
            string password = TxtPassword.Password;
            string confirmPassword = TxtConfirmPassword.Password;
            string firstName = TxtFirstName.Text.Trim();
            string lastName = TxtLastName.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword) ||
                password != confirmPassword)
            {
                LblError.Text = "Date invalide sau parolele nu coincid!";
                LblError.Visibility = Visibility.Visible;
                return;
            }

            bool success = await _firebaseHelper.SignUpAsync(email, password, username);
            if (!success)
            {
                LblError.Text = "Înregistrare eșuată. Încearcă alt username/email.";
                LblError.Visibility = Visibility.Visible;
                return;
            }

            // Creăm obiectul User
            var user = new User
            {
                userName = username,
                email = email,
                firstName = firstName,
                lastName = lastName,
                userType = "customer",
                passwordHash = BCrypt.Net.BCrypt.HashPassword(password),
                createdAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            await _firebaseHelper.SetAsync($"Users/{username}", user);
            MessageBox.Show("Cont creat cu succes!", "Succes");

            //
            //login.Show();
            this.Close();

        }
    }
}
