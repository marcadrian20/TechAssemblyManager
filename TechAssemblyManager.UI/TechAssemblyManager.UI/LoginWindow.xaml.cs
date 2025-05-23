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
using TechAssemblyManager.BLL;
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
        //private FirebaseHelper _firebaseHelper;
        private UserManagerBLL _userManagerBLL;
        public LoginWindow(UserManagerBLL userManagerBLL)
        {
            InitializeComponent();

            _userManagerBLL = userManagerBLL;
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

            var user = await _userManagerBLL.LoginAsync(username, password);
            if (user != null)
            {
                SessionManager.LoggedInUser = user;
                MessageBox.Show($"Bun venit, {user.userName}!", "Succes");

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
