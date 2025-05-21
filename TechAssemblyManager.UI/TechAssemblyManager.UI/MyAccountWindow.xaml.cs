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
using TechAssemblyManager.DAL.FirebaseHelper;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    /// <summary>
    /// Interaction logic for MyAccountWindow.xaml
    /// </summary>
    public partial class MyAccountWindow : Window
    {
        private readonly User _currentUser;
        private readonly FirebaseHelper _firebaseHelper;
        public MyAccountWindow()
        {
            InitializeComponent();
            _currentUser = SessionManager.LoggedInUser;
            _firebaseHelper = new FirebaseHelper(
                "https://techassemblymanager-default-rtdb.firebaseio.com/",
                "ky7wJX7Iu46hjBHWqDJNWjJW19NeYQurX4Z9VeUv",
                "AIzaSyBxq3J01JqE6yonLc9plkzA6c3-Gi1r1eU"
            );

            LoadUserData();

        }
        private void LoadUserData()
        {
            if (_currentUser == null)
            {
                MessageBox.Show("Trebuie să fii autentificat.");
                Close();
                return;
            }

            TxtUsername.Text = _currentUser.userName;
            TxtEmail.Text = _currentUser.email;
            TxtFirstName.Text = _currentUser.firstName;
            TxtLastName.Text = _currentUser.lastName;
            TxtPhone.Text = _currentUser.customerData?.phoneNumber ?? "";
            TxtAddress.Text = _currentUser.customerData?.address ?? "";
        }
        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            _currentUser.firstName = TxtFirstName.Text.Trim();
            _currentUser.lastName = TxtLastName.Text.Trim();

            if (_currentUser.userType == "customer")
            {
                _currentUser.customerData.phoneNumber = TxtPhone.Text.Trim();
                _currentUser.customerData.address = TxtAddress.Text.Trim();
            }

            await _firebaseHelper.UpdateEmployeeAsync(_currentUser); // Folosit și pentru useri normali

            MessageBox.Show("Datele au fost actualizate cu succes.");
            SessionManager.LoggedInUser = _currentUser;
        }

        private void TxtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
