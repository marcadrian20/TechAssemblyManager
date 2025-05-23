using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TechAssemblyManager.BLL;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    public partial class EmployeeManagementWindow : Window
    {
        private readonly UserManagerBLL _userManagerBLL;
        private List<User> _allEmployees = new List<User>();

        public EmployeeManagementWindow(UserManagerBLL userManagerBLL)
        {
            InitializeComponent();
            _userManagerBLL = userManagerBLL;
            LoadEmployees();
        }

        private async void LoadEmployees()
        {
            _allEmployees = await _userManagerBLL.GetAllEmployeesAsync();
            EmployeesGrid.ItemsSource = _allEmployees;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = TxtSearch.Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(search))
            {
                EmployeesGrid.ItemsSource = _allEmployees;
            }
            else
            {
                EmployeesGrid.ItemsSource = _allEmployees
                    .Where(emp =>
                        emp.userName.ToLower().Contains(search) ||
                        emp.firstName.ToLower().Contains(search) ||
                        emp.lastName.ToLower().Contains(search) ||
                        emp.email.ToLower().Contains(search)
                    ).ToList();
            }
        }

        private async void BtnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEmployeeDialog();
            if (dialog.ShowDialog() == true)
            {
                var newUser = new User
                {
                    userName = dialog.UserName,
                    email = dialog.Email,
                    firstName = dialog.FirstName,
                    lastName = dialog.LastName,
                    userType = "employee",
                    passwordHash= dialog.Password,
                    employeeData = new EmployeeData { isSenior = dialog.IsSenior }
                };
                bool result = await _userManagerBLL.AddEmployeeAsync(newUser);
                if (result)
                {
                    MessageBox.Show("Angajat adăugat cu succes!");
                    LoadEmployees();
                }
                else
                {
                    MessageBox.Show("Eroare la adăugarea angajatului (username/email deja există).");
                }
            }
        }

        private async void BtnPromote_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesGrid.SelectedItem is User selected)
            {
                if (!selected.employeeData.isSenior)
                {
                    bool result = await _userManagerBLL.UpdateEmployeeRoleAsync(selected.userName, true);
                    if (result)
                    {
                        MessageBox.Show("Angajat promovat la senior!");
                        LoadEmployees();
                    }
                }
            }
        }

        private async void BtnDemote_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesGrid.SelectedItem is User selected)
            {
                if (selected.employeeData.isSenior)
                {
                    bool result = await _userManagerBLL.UpdateEmployeeRoleAsync(selected.userName, false);
                    if (result)
                    {
                        MessageBox.Show("Angajat retrogradat la junior!");
                        LoadEmployees();
                    }
                }
            }
        }
    }
}