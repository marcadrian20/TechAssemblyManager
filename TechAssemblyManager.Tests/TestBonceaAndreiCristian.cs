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
    public class TestBonceaAndreiCristian
    {
        private Mock<IFirebaseHelper>? _mockFirebaseHelper;
        private PromotionManagerBLL? _promotionManager;
        private User? _testManager;
        private User? _testEmployee;
        private User? _testCustomer;
        private Promotion? _testPromotion;
        private List<Promotion>? _testPromotions;

        [TestInitialize]
        public void Setup()
        {
            _mockFirebaseHelper = new Mock<IFirebaseHelper>();
            _promotionManager = new PromotionManagerBLL(_mockFirebaseHelper.Object);

            _testManager = new User
            {
                userName = "manager1",
                email = "manager@company.com",
                userType = "manager",
                firstName = "John",
                lastName = "Manager"
            };

            _testEmployee = new User
            {
                userName = "employee1",
                email = "employee@company.com",
                userType = "employee",
                firstName = "Jane",
                lastName = "Employee",
                employeeData = new EmployeeData { isSenior = false }
            };

            _testCustomer = new User
            {
                userName = "customer1",
                email = "customer@example.com",
                userType = "customer",
                firstName = "Bob",
                lastName = "Customer"
            };

            _testPromotion = new Promotion
            {
                promotionId = "PROMO001",
                name = "Summer Sale",
                description = "Great summer discounts",
                discountPercentage = 20,
                startDate = DateTime.Now.ToString("o"),
                endDate = DateTime.Now.AddDays(30).ToString("o"),
                isActive = true,
                createdBy = "manager1"
            };

            _testPromotions = new List<Promotion>
            {
                _testPromotion,
                new Promotion
                {
                    promotionId = "PROMO002",
                    name = "Winter Sale",
                    discountPercentage = 15,
                    isActive = false,
                    createdBy = "manager1"
                }
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockFirebaseHelper = null;
            _promotionManager = null;
            _testManager = null;
            _testEmployee = null;
            _testCustomer = null;
            _testPromotion = null;
            _testPromotions = null;
        }

        #region AddPromotionAsync Tests (Black Box)

        [TestMethod]
        [TestCategory("BlackBox")]
        public async Task AddPromotionAsync_ValidManagerAndPromotion_ReturnsTrue()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.AddPromotionAsync(It.IsAny<Promotion>()))
                              .ReturnsAsync(true);

            // Act
            var result = await _promotionManager.AddPromotionAsync(_testPromotion, _testManager);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("manager1", _testPromotion.createdBy);
            Assert.IsTrue(_testPromotion.isActive);
            _mockFirebaseHelper.Verify(x => x.AddPromotionAsync(_testPromotion), Times.Once);
        }

        [TestMethod]
        [TestCategory("BlackBox")]
        public async Task AddPromotionAsync_NullManager_ReturnsFalse()
        {
            // Act
            var result = await _promotionManager.AddPromotionAsync(_testPromotion, null);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.AddPromotionAsync(It.IsAny<Promotion>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("BlackBox")]
        public async Task AddPromotionAsync_NonManagerUser_ReturnsFalse()
        {
            // Act
            var result = await _promotionManager.AddPromotionAsync(_testPromotion, _testEmployee);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.AddPromotionAsync(It.IsAny<Promotion>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("BlackBox")]
        public async Task AddPromotionAsync_CustomerUser_ReturnsFalse()
        {
            // Act
            var result = await _promotionManager.AddPromotionAsync(_testPromotion, _testCustomer);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.AddPromotionAsync(It.IsAny<Promotion>()), Times.Never);
        }

        #endregion

        #region GetAllPromotionsAsync Tests (Black Box)

        [TestMethod]
        [TestCategory("BlackBox")]
        public async Task GetAllPromotionsAsync_ReturnsAllPromotions()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.GetAllPromotionsAsync())
                              .ReturnsAsync(_testPromotions);

            // Act
            var result = await _promotionManager.GetAllPromotionsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("PROMO001", result.First().promotionId);
            _mockFirebaseHelper.Verify(x => x.GetAllPromotionsAsync(), Times.Once);
        }

        [TestMethod]
        [TestCategory("BlackBox")]
        public async Task GetAllPromotionsAsync_EmptyList_ReturnsEmptyList()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.GetAllPromotionsAsync())
                              .ReturnsAsync(new List<Promotion>());

            // Act
            var result = await _promotionManager.GetAllPromotionsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        #endregion

        #region DeletePromotionAsync Tests (White Box)

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task DeletePromotionAsync_ValidManagerAndPromotionId_ReturnsTrue()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.DeleteAsync("Promotions/PROMO001"))
                              .Returns(Task.CompletedTask);

            // Act
            var result = await _promotionManager.DeletePromotionAsync("PROMO001", _testManager);

            // Assert
            Assert.IsTrue(result);
            _mockFirebaseHelper.Verify(x => x.DeleteAsync("Promotions/PROMO001"), Times.Once);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task DeletePromotionAsync_NonManager_ReturnsFalse()
        {
            // Act
            var result = await _promotionManager.DeletePromotionAsync("PROMO001", _testEmployee);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.DeleteAsync(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public async Task DeletePromotionAsync_InvalidPromotionId_ReturnsFalse(string promotionId)
        {
            // Act
            var result = await _promotionManager.DeletePromotionAsync(promotionId, _testManager);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.DeleteAsync(It.IsAny<string>()), Times.Never);
        }

        #endregion

        #region UpdatePromotionAsync Tests (White Box)

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task UpdatePromotionAsync_ValidPromotionAndUser_ReturnsTrue()
        {
            // Arrange
            var validPromotion = new Promotion
            {
                promotionId = "PROMO001",
                name = "Updated Sale",
                discountPercentage = 25,
                startDate = DateTime.Now.ToString("o"),
                endDate = DateTime.Now.AddDays(15).ToString("o")
            };

            _mockFirebaseHelper.Setup(x => x.GetAsync<Promotion>("Promotions/PROMO001"))
                              .ReturnsAsync(_testPromotion);
            _mockFirebaseHelper.Setup(x => x.UpdateAsync("Promotions/PROMO001", validPromotion))
                              .Returns(Task.CompletedTask);

            // Act
            var result = await _promotionManager.UpdatePromotionAsync(validPromotion, _testManager);

            // Assert
            Assert.IsTrue(result);
            _mockFirebaseHelper.Verify(x => x.UpdateAsync("Promotions/PROMO001", validPromotion), Times.Once);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task UpdatePromotionAsync_NullUser_ReturnsFalse()
        {
            // Act
            var result = await _promotionManager.UpdatePromotionAsync(_testPromotion, null);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<Promotion>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task UpdatePromotionAsync_NullPromotion_ReturnsFalse()
        {
            // Act
            var result = await _promotionManager.UpdatePromotionAsync(null, _testManager);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<Promotion>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public async Task UpdatePromotionAsync_InvalidPromotionId_ReturnsFalse(string promotionId)
        {
            // Arrange
            var invalidPromotion = new Promotion { promotionId = promotionId };

            // Act
            var result = await _promotionManager.UpdatePromotionAsync(invalidPromotion, _testManager);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<Promotion>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public async Task UpdatePromotionAsync_InvalidPromotionName_ReturnsFalse(string name)
        {
            // Arrange
            var invalidPromotion = new Promotion
            {
                promotionId = "PROMO001",
                name = name,
                discountPercentage = 20,
                startDate = DateTime.Now.ToString("o"),
                endDate = DateTime.Now.AddDays(30).ToString("o")
            };

            // Act
            var result = await _promotionManager.UpdatePromotionAsync(invalidPromotion, _testManager);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<Promotion>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        [DataRow(0)]
        [DataRow(-10)]
        [DataRow(101)]
        [DataRow(150)]
        public async Task UpdatePromotionAsync_InvalidDiscountPercentage_ReturnsFalse(int discount)
        {
            // Arrange
            var invalidPromotion = new Promotion
            {
                promotionId = "PROMO001",
                name = "Valid Name",
                discountPercentage = discount,
                startDate = DateTime.Now.ToString("o"),
                endDate = DateTime.Now.AddDays(30).ToString("o")
            };

            // Act
            var result = await _promotionManager.UpdatePromotionAsync(invalidPromotion, _testManager);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<Promotion>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task UpdatePromotionAsync_StartDateAfterEndDate_ReturnsFalse()
        {
            // Arrange
            var invalidPromotion = new Promotion
            {
                promotionId = "PROMO001",
                name = "Valid Name",
                discountPercentage = 20,
                startDate = DateTime.Now.AddDays(30).ToString("o"),
                endDate = DateTime.Now.ToString("o") // End date before start date
            };

            // Act
            var result = await _promotionManager.UpdatePromotionAsync(invalidPromotion, _testManager);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<Promotion>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task UpdatePromotionAsync_InvalidDateFormat_ReturnsFalse()
        {
            // Arrange
            var invalidPromotion = new Promotion
            {
                promotionId = "PROMO001",
                name = "Valid Name",
                discountPercentage = 20,
                startDate = "invalid-date",
                endDate = "also-invalid"
            };

            // Act
            var result = await _promotionManager.UpdatePromotionAsync(invalidPromotion, _testManager);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<Promotion>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task UpdatePromotionAsync_NonExistentPromotion_ReturnsFalse()
        {
            // Arrange
            var validPromotion = new Promotion
            {
                promotionId = "NONEXISTENT",
                name = "Valid Name",
                discountPercentage = 20,
                startDate = DateTime.Now.ToString("o"),
                endDate = DateTime.Now.AddDays(30).ToString("o")
            };

            _mockFirebaseHelper.Setup(x => x.GetAsync<Promotion>("Promotions/NONEXISTENT"))
                              .ReturnsAsync((Promotion)null);

            // Act
            var result = await _promotionManager.UpdatePromotionAsync(validPromotion, _testManager);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<Promotion>()), Times.Never);
        }

        #endregion

        #region GetActivePromotionsAsync Tests (Black Box)

        [TestMethod]
        [TestCategory("BlackBox")]
        public async Task GetActivePromotionsAsync_ReturnsOnlyActivePromotions()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.GetAllPromotionsAsync())
                              .ReturnsAsync(_testPromotions);

            // Act
            var result = await _promotionManager.GetActivePromotionsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result.All(p => p.isActive));
            Assert.AreEqual("PROMO001", result.First().promotionId);
        }

        [TestMethod]
        [TestCategory("BlackBox")]
        public async Task GetActivePromotionsAsync_NoActivePromotions_ReturnsEmptyList()
        {
            // Arrange
            var inactivePromotions = new List<Promotion>
            {
                new Promotion { promotionId = "PROMO1", isActive = false },
                new Promotion { promotionId = "PROMO2", isActive = false }
            };
            _mockFirebaseHelper.Setup(x => x.GetAllPromotionsAsync())
                              .ReturnsAsync(inactivePromotions);

            // Act
            var result = await _promotionManager.GetActivePromotionsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        #endregion
    }
}