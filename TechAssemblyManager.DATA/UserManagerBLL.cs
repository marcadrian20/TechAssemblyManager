using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebaseWrapper;
using TechAssemblyManager.Models;


namespace TechAssemblyManager.BLL
{
    public class UserManagerBLL
    {
        private readonly FirebaseHelper _firebaseHelper;

        public UserManagerBLL(FirebaseHelper firebaseHelper)
        {
            _firebaseHelper = firebaseHelper;
        }

        public async Task<bool> RegisterUserAsync(
            string email, string password, string userName, string firstName, string lastName,
            string address, string phoneNumber)
        {
            // Check if username or email already exists
            var allUsers = await _firebaseHelper.GetAsync<Dictionary<string, User>>("Users") ?? new Dictionary<string, User>();
            if (allUsers.Values.Any(u => u.userName == userName || u.email == email))
                return false;

            var user = new User
            {
                createdAt = DateTime.UtcNow.ToString("o"),
                email = email,
                firstName = firstName,
                lastName = lastName,
                userName = userName,
                passwordHash = BCrypt.Net.BCrypt.HashPassword(password),
                userType = "customer",
                customerData = new CustomerData
                {
                    address = address,
                    phoneNumber = phoneNumber
                },
                employeeData = new EmployeeData
                {
                    isSenior = false
                },
                selectedProducts = new Dictionary<string, SelectedProduct>()
            };

            await _firebaseHelper.SetAsync($"Users/{user.userName}", user);
            return true;
        }
        public async Task<bool> RegisterUserAsync(User user, string password)
        {
            // Check if username or email already exists
            var allUsers = await _firebaseHelper.GetAsync<Dictionary<string, User>>("Users") ?? new Dictionary<string, User>();
            if (allUsers.Values.Any(u => u.userName == user.userName || u.email == user.email))
                return false;

            // Hash password
            user.passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            user.createdAt = DateTime.UtcNow.ToString("o");

            // Add user to Firebase
            await _firebaseHelper.SetAsync($"Users/{user.userName}", user);
            return true;
        }
        public async Task<User?> LoginAsync(string emailOrUsername, string password)
        {
            var user = await _firebaseHelper.LoginAsync(emailOrUsername, password);
            if (user == null)
                return null;
            return user;
        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _firebaseHelper.GetAsync<User>($"Users/{username}");
        }
        public async Task<List<User>> GetAllEmployeesAsync()
        {
            return await _firebaseHelper.GetAllEmployeesAsync();
        }

        public async Task<bool> AddEmployeeAsync(User employee)
        {
            if (employee == null || string.IsNullOrWhiteSpace(employee.userName))
                return false;
            employee.userType = "employee";
            return await RegisterUserAsync(employee, employee.passwordHash); // passwordHash here should be the plain password }
        }
        // Update employee role (senior/junior)
        public async Task<bool> UpdateEmployeeRoleAsync(string username, bool isSenior)
        {
            var user = await GetUserByUsernameAsync(username);
            if (user == null || user.userType != "employee")
                return false;
            user.employeeData.isSenior = isSenior;
            await _firebaseHelper.UpdateEmployeeAsync(user);
            return true;
        }

        // Delete user by username
        public async Task<bool> DeleteUserAsync(string username)
        {
            var user = await GetUserByUsernameAsync(username);
            if (user == null)
                return false;
            await _firebaseHelper.DeleteAsync($"Users/{username}");
            return true;
        }

        // Role checks
        public bool IsManager(User user)
        {
            return user != null && user.userType == "manager";
        }

        public bool IsSenior(User user)
        {
            return user != null && user.userType == "employee" && user.employeeData != null && user.employeeData.isSenior;
        }

        public bool IsJunior(User user)
        {
            return user != null && user.userType == "employee" && user.employeeData != null && !user.employeeData.isSenior;
        }

        public bool IsCustomer(User user)
        {
            return user != null && user.userType == "customer";
        }

        // Update user profile
        public async Task<bool> UpdateUserAsync(User user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.userName))
                return false;
            await _firebaseHelper.UpdateAsync($"Users/{user.userName}", user);
            return true;
        }
    }
}
