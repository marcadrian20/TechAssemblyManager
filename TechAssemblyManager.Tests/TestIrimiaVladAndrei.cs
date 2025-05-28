using BCrypt.Net;
using TechAssemblyManager.DAL.FirebaseHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechAssemblyManager.BLL;
using TechAssemblyManager.Models;
using FirebaseWrapper;

namespace TechAssemblyManager.Tests
{
    [TestClass]
    public class TestIrimiaVladAndrei
    {
        private Mock<IFirebaseHelper>? _mockFirebaseHelper;
        private UserManagerBLL? _userManager;
        private User? _testUser;
        private Dictionary<string, User>? _testUsers;

        [TestInitialize]
        public void Setup()
        {
            // Mock the correct FirebaseHelper from FirebaseWrapper
            _mockFirebaseHelper = new Mock<IFirebaseHelper>();
            _userManager = new UserManagerBLL(_mockFirebaseHelper.Object);

            _testUser = new User
            {
                userName = "testuser",
                email = "test@example.com",
                firstName = "Jam",
                lastName = "Al",
                passwordHash = BCrypt.Net.BCrypt.HashPassword("12345678"),
                userType = "customer",
                createdAt = DateTime.UtcNow.ToString("o"),
                customerData = new CustomerData
                {
                    address = "Aleea studentilor",
                    phoneNumber = "0760491485"
                },
                employeeData = new EmployeeData { isSenior = false },
                selectedProducts = new Dictionary<string, SelectedProduct>()
            };

            _testUsers = new Dictionary<string, User>
            {
                ["testuser"] = _testUser
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockFirebaseHelper = null;
            _userManager = null;
            _testUser = null;
            _testUsers = null;
        }

        #region RegisterUserAsync Tests (Black Box)

        [TestMethod]
        [TestCategory("BlackBox")]
        public async Task RegisterUserAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var emptyUsers = new Dictionary<string, User>();
            _mockFirebaseHelper.Setup(x => x.GetAsync<Dictionary<string, User>>("Users"))
                              .ReturnsAsync(emptyUsers);
            _mockFirebaseHelper.Setup(x => x.SetAsync(It.IsAny<string>(), It.IsAny<User>()))
                              .Returns(Task.CompletedTask);

            // Act
            var result = await _userManager.RegisterUserAsync(
                "newuser@test.com", "12345678", "newuser",
                "Ben", "Dover", "Bulevardul ABC", "0760123456");

            // Assert
            Assert.IsTrue(result);
            _mockFirebaseHelper.Verify(x => x.SetAsync("Users/newuser", It.Is<User>(u =>
                u.userName == "newuser" &&
                u.email == "newuser@test.com" &&
                u.userType == "customer")), Times.Once);
        }

        [TestMethod]
        [TestCategory("BlackBox")]
        public async Task RegisterUserAsync_DuplicateUsername_ReturnsFalse()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.GetAsync<Dictionary<string, User>>("Users"))
                              .ReturnsAsync(_testUsers);

            // Act
            var result = await _userManager.RegisterUserAsync(
                "different@test.com", "12345678", "testuser",
                "Ben", "Dover", "Bulevardul ABC", "0760123456");

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.SetAsync(It.IsAny<string>(), It.IsAny<User>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("BlackBox")]
        public async Task RegisterUserAsync_DuplicateEmail_ReturnsFalse()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.GetAsync<Dictionary<string, User>>("Users"))
                              .ReturnsAsync(_testUsers);

            // Act
            var result = await _userManager.RegisterUserAsync(
                "test@example.com", "12345678", "newuser",
                "Ben", "Dover", "Bulevardul ABC", "0760123456");

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.SetAsync(It.IsAny<string>(), It.IsAny<User>()), Times.Never);
        }

        #endregion

        #region LoginAsync Tests (Black Box)

        [TestMethod]
        [TestCategory("BlackBox")]
        public async Task LoginAsync_ValidCredentials_ReturnsUser()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.LoginAsync("testuser", "12345678"))
                              .ReturnsAsync(_testUser);

            // Act
            var result = await _userManager.LoginAsync("testuser", "12345678");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("testuser", result.userName);
            Assert.AreEqual("test@example.com", result.email);
        }

        [TestMethod]
        [TestCategory("BlackBox")]
        public async Task LoginAsync_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.LoginAsync("wronguser", "wrongpass"))
                              .ReturnsAsync((User)null);

            // Act
            var result = await _userManager.LoginAsync("wronguser", "wrongpass");

            // Assert
            Assert.IsNull(result);
        }

        #endregion

        #region Role Checking Tests (White Box)

        [TestMethod]
        [TestCategory("WhiteBox")]
        [DataRow("manager", true)]
        [DataRow("employee", false)]
        [DataRow("customer", false)]
        [DataRow("", false)]
        public void IsManager_DifferentUserTypes_ReturnsExpectedResult(string userType, bool expected)
        {
            // Arrange
            var user = new User { userType = userType };

            // Act
            var result = _userManager.IsManager(user);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public void IsManager_NullUser_ReturnsFalse()
        {
            // Act
            var result = _userManager.IsManager(null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public void IsSenior_SeniorEmployee_ReturnsTrue()
        {
            // Arrange
            var user = new User
            {
                userType = "employee",
                employeeData = new EmployeeData { isSenior = true }
            };

            // Act
            var result = _userManager.IsSenior(user);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public void IsSenior_JuniorEmployee_ReturnsFalse()
        {
            // Arrange
            var user = new User
            {
                userType = "employee",
                employeeData = new EmployeeData { isSenior = false }
            };

            // Act
            var result = _userManager.IsSenior(user);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public void IsSenior_CustomerUser_ReturnsFalse()
        {
            // Arrange
            var user = new User
            {
                userType = "customer",
                employeeData = new EmployeeData { isSenior = true }
            };

            // Act
            var result = _userManager.IsSenior(user);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region AddEmployeeAsync Tests (White Box)

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task AddEmployeeAsync_ValidEmployee_SetsUserTypeAndCallsRegister()
        {
            // Arrange
            var employee = new User
            {
                userName = "newemployee",
                email = "emp@company.com",
                firstName = "Jane",
                lastName = "Smith",
                passwordHash = "plainpassword", // This is treated as plain password in AddEmployeeAsync
                employeeData = new EmployeeData { isSenior = false }
            };

            var emptyUsers = new Dictionary<string, User>();
            _mockFirebaseHelper.Setup(x => x.GetAsync<Dictionary<string, User>>("Users"))
                              .ReturnsAsync(emptyUsers);
            _mockFirebaseHelper.Setup(x => x.SetAsync(It.IsAny<string>(), It.IsAny<User>()))
                              .Returns(Task.CompletedTask);

            // Act
            var result = await _userManager.AddEmployeeAsync(employee);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("employee", employee.userType);
            _mockFirebaseHelper.Verify(x => x.SetAsync($"Users/{employee.userName}", It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task AddEmployeeAsync_NullEmployee_ReturnsFalse()
        {
            // Act
            var result = await _userManager.AddEmployeeAsync(null);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.SetAsync(It.IsAny<string>(), It.IsAny<User>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task AddEmployeeAsync_EmptyUsername_ReturnsFalse()
        {
            // Arrange
            var employee = new User { userName = "" };

            // Act
            var result = await _userManager.AddEmployeeAsync(employee);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region UpdateEmployeeRoleAsync Tests (White Box)

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task UpdateEmployeeRoleAsync_ValidEmployee_UpdatesRoleAndCallsUpdate()
        {
            // Arrange
            var employee = new User
            {
                userName = "emp1",
                userType = "employee",
                employeeData = new EmployeeData { isSenior = false }
            };

            _mockFirebaseHelper.Setup(x => x.GetAsync<User>("Users/emp1"))
                              .ReturnsAsync(employee);
            _mockFirebaseHelper.Setup(x => x.UpdateEmployeeAsync(It.IsAny<User>()))
                              .Returns(Task.CompletedTask);

            // Act
            var result = await _userManager.UpdateEmployeeRoleAsync("emp1", true);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(employee.employeeData.isSenior);
            _mockFirebaseHelper.Verify(x => x.UpdateEmployeeAsync(employee), Times.Once);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task UpdateEmployeeRoleAsync_NonEmployee_ReturnsFalse()
        {
            // Arrange
            var customer = new User { userName = "cust1", userType = "customer" };
            _mockFirebaseHelper.Setup(x => x.GetAsync<User>("Users/cust1"))
                              .ReturnsAsync(customer);

            // Act
            var result = await _userManager.UpdateEmployeeRoleAsync("cust1", true);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.UpdateEmployeeAsync(It.IsAny<User>()), Times.Never);
        }

        #endregion

        #region DeleteUserAsync Tests (Black Box)

        [TestMethod]
        [TestCategory("BlackBox")]
        public async Task DeleteUserAsync_ExistingUser_ReturnsTrue()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.GetAsync<User>("Users/testuser"))
                              .ReturnsAsync(_testUser);
            _mockFirebaseHelper.Setup(x => x.DeleteAsync("Users/testuser"))
                              .Returns(Task.CompletedTask);

            // Act
            var result = await _userManager.DeleteUserAsync("testuser");

            // Assert
            Assert.IsTrue(result);
            _mockFirebaseHelper.Verify(x => x.DeleteAsync("Users/testuser"), Times.Once);
        }

        [TestMethod]
        [TestCategory("BlackBox")]
        public async Task DeleteUserAsync_NonExistingUser_ReturnsFalse()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.GetAsync<User>("Users/nonexistent"))
                              .ReturnsAsync((User)null);

            // Act
            var result = await _userManager.DeleteUserAsync("nonexistent");

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.DeleteAsync(It.IsAny<string>()), Times.Never);
        }

        #endregion

        #region GetAccountType Tests (White Box - Decision Testing)

        [TestMethod]
        [TestCategory("WhiteBox")]
        [DataRow("manager", false, "Manager")]
        [DataRow("employee", true, "Senior")]
        [DataRow("employee", false, "Junior")]
        [DataRow("customer", false, "Utilizator")]
        [DataRow("unknown", false, "Utilizator")]
        public void GetAccountType_DifferentUserTypes_ReturnsCorrectType(string userType, bool isSenior, string expected)
        {
            // Arrange
            var user = new User
            {
                userType = userType,
                employeeData = new EmployeeData { isSenior = isSenior }
            };

            // Act
            var result = _userManager.GetAccountType(user);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public void GetAccountType_NullUser_ReturnsGuest()
        {
            // Act
            var result = _userManager.GetAccountType(null);

            // Assert
            Assert.AreEqual("Guest", result);
        }

        #endregion

        #region UpdatePasswordAsync Tests (Black Box)

        [TestMethod]
        [TestCategory("BlackBox")]
        public async Task UpdatePasswordAsync_ValidUser_UpdatesPasswordAndReturnsTrue()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.GetAsync<User>("Users/testuser"))
                              .ReturnsAsync(_testUser);
            _mockFirebaseHelper.Setup(x => x.UpdateAsync("Users/testuser", It.IsAny<User>()))
                              .Returns(Task.CompletedTask);

            // Act
            var result = await _userManager.UpdatePasswordAsync("testuser", "newpassword123");

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(BCrypt.Net.BCrypt.Verify("newpassword123", _testUser.passwordHash));
            _mockFirebaseHelper.Verify(x => x.UpdateAsync("Users/testuser", _testUser), Times.Once);
        }

        [TestMethod]
        [TestCategory("BlackBox")]
        [DataRow(null, "password")]
        [DataRow("", "password")]
        [DataRow("   ", "password")]
        [DataRow("user", null)]
        [DataRow("user", "")]
        [DataRow("user", "   ")]
        public async Task UpdatePasswordAsync_InvalidInput_ReturnsFalse(string userName, string password)
        {
            // Act
            var result = await _userManager.UpdatePasswordAsync(userName, password);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region UpdateCustomerDataAsync Tests (Black Box)

        [TestMethod]
        [TestCategory("BlackBox")]
        public async Task UpdateCustomerDataAsync_ValidCustomer_UpdatesDataAndReturnsTrue()
        {
            // Arrange
            var customerData = new CustomerData
            {
                address = "Str. Bulevard 123",
                phoneNumber = "0760491999",
            };

            _mockFirebaseHelper.Setup(x => x.GetAsync<User>("Users/testuser"))
                              .ReturnsAsync(_testUser);
            _mockFirebaseHelper.Setup(x => x.UpdateAsync("Users/testuser", It.IsAny<User>()))
                              .Returns(Task.CompletedTask);

            // Act
            var result = await _userManager.UpdateCustomerDataAsync("testuser", customerData);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("Str. Bulevard 123", _testUser.customerData.address);
            Assert.AreEqual("0760491999", _testUser.customerData.phoneNumber);
        }

        [TestMethod]
        [TestCategory("BlackBox")]
        public async Task UpdateCustomerDataAsync_NonCustomer_ReturnsFalse()
        {
            // Arrange
            var employee = new User { userName = "emp1", userType = "employee" };
            var customerData = new CustomerData { address = "Test", phoneNumber = "123" };

            _mockFirebaseHelper.Setup(x => x.GetAsync<User>("Users/emp1"))
                              .ReturnsAsync(employee);

            // Act
            var result = await _userManager.UpdateCustomerDataAsync("emp1", customerData);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<User>()), Times.Never);
        }

        #endregion
    }
}