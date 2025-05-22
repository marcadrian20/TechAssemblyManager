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
using TechAssemblyManager.BLL;
using TechAssemblyManager.DAL.FirebaseHelper;

namespace TechAssemblyManager.UI
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private UserManagerBLL userManagerBLL;
        public RegisterWindow(UserManagerBLL userManagerBLL)
        {
            InitializeComponent();
            this.userManagerBLL = userManagerBLL;
        }

        private async void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = TxtUsername.Text.Trim();
            string email = TxtEmail.Text.Trim();
            string password = TxtPassword.Password;
            string confirmPassword = TxtConfirmPassword.Password;
            string firstName = TxtFirstName.Text.Trim();
            string lastName = TxtLastName.Text.Trim();
            string address = TxtAddress.Text.Trim();
            string phone = TxtPhone.Text.Trim();


            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword) ||
                string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(phone) ||
                password != confirmPassword)
            {
                LblError.Text = "Date invalide sau parolele nu coincid!";
                LblError.Visibility = Visibility.Visible;
                return;
            }

            bool success = await userManagerBLL.RegisterUserAsync(
                   email, password, username, firstName, lastName, address, phone
                );
            if (!success)
            {
                LblError.Text = "Înregistrare eșuată. Încearcă alt username/email.";
                LblError.Visibility = Visibility.Visible;
                return;
            }

            // Auto-login after registration
            var loggedUser = await userManagerBLL.LoginAsync(username, password);
            if (loggedUser != null)
            {
                SessionManager.LoggedInUser = loggedUser;
                MessageBox.Show("Cont creat și autentificat cu succes!", "Succes");

                var mainWindow = Application.Current.Windows
                    .OfType<MainWindow>()
                    .FirstOrDefault();

                if (mainWindow != null)
                {
                    mainWindow.UpdateDashboard();
                    mainWindow.Activate();
                }
                else
                {
                    mainWindow = new MainWindow();
                    mainWindow.Show();
                }
            }
            else
            {
                MessageBox.Show("Cont creat, dar autentificarea automată a eșuat.", "Eroare");
            }

            this.Close();
        }
    }
}