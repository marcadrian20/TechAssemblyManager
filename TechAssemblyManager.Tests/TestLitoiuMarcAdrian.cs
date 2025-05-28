using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechAssemblyManager.BLL;
using TechAssemblyManager.Models;
using FirebaseWrapper;

namespace TechAssemblyManager.Tests
{
    [TestClass]
    public class TestLitoiuMarcAdrian
    {
        private Mock<IFirebaseHelper>? _mockFirebaseHelper;
        private ProductManagerBLL? _productManager;
        private Product? _testProduct;
        private User? _testUser;
        private User? _testJuniorEmployee;
        private User? _testCustomer;
        private List<Product>? _testProducts;
        private ProductCategory? _testCategory;
        private Dictionary<string, ProductCategory>? _testCategories;

        [TestInitialize]
        public void Setup()
        {
            _mockFirebaseHelper = new Mock<IFirebaseHelper>();
            _productManager = new ProductManagerBLL(_mockFirebaseHelper.Object);

            _testUser = new User
            {
                userName = "testuser",
                userType = "employee",
                employeeData = new EmployeeData { isSenior = true }
            };

            _testJuniorEmployee = new User
            {
                userName = "junior",
                userType = "employee",
                employeeData = new EmployeeData { isSenior = false }
            };

            _testCustomer = new User
            {
                userName = "customer",
                userType = "customer"
            };

            _testCategory = new ProductCategory
            {
                categoryId = "cat1",
                name = "Test Category",
                type = "component"
            };

            _testCategories = new Dictionary<string, ProductCategory>
            {
                ["cat1"] = _testCategory,
                ["cat2"] = new ProductCategory { categoryId = "cat2", name = "Category 2", type = "system" }
            };

            _testProduct = new Product
            {
                productId = "prod1",
                name = "Test Product",
                description = "A valid test product description",
                categoryId = "cat1",
                price = 100,
                rating = 4.5
            };

            _testProducts = new List<Product>
            {
                new Product { productId = "prod1", name = "Product A", categoryId = "cat1", price = 50, rating = 4 },
                new Product { productId = "prod2", name = "Product B", categoryId = "cat2", price = 150, rating = 5 },
                new Product { productId = "prod3", name = "Product C", categoryId = "cat1", price = 75, rating = 3.5 }
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockFirebaseHelper = null;
            _productManager = null;
            _testUser = null;
            _testJuniorEmployee = null;
            _testCustomer = null;
            _testProduct = null;
            _testProducts = null;
            _testCategory = null;
            _testCategories = null;
        }

        #region GetAllActiveProductsAsync Tests

        [TestMethod]
        [TestCategory("BlackBox")]
        public async Task GetAllActiveProductsAsync_ReturnsProducts()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.GetAllActiveProductsAsync())
                               .ReturnsAsync(_testProducts);

            // Act
            var result = await _productManager.GetAllActiveProductsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        [TestCategory("BlackBox")]
        public async Task GetAllActiveProductsAsync_EmptyList_ReturnsEmptyList()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.GetAllActiveProductsAsync())
                               .ReturnsAsync(new List<Product>());

            // Act
            var result = await _productManager.GetAllActiveProductsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        #endregion

        #region AddProductAsync Tests

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task AddProductAsync_ValidProduct_ReturnsTrue()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.GetAsync<Dictionary<string, ProductCategory>>("ProductCategories"))
                               .ReturnsAsync(_testCategories);
            _mockFirebaseHelper.Setup(x => x.GetAsync<Product>("Products/prod1"))
                               .ReturnsAsync((Product)null);
            _mockFirebaseHelper.Setup(x => x.AddProductAsync(It.IsAny<Product>()))
                               .ReturnsAsync(true);

            // Act
            var result = await _productManager.AddProductAsync(_testProduct, _testUser);

            // Assert
            Assert.IsTrue(result);
            _mockFirebaseHelper.Verify(x => x.AddProductAsync(_testProduct), Times.Once);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task AddProductAsync_NullProduct_ReturnsFalse()
        {
            // Act
            var result = await _productManager.AddProductAsync(null, _testUser);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.AddProductAsync(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task AddProductAsync_NullUser_ReturnsFalse()
        {
            // Act
            var result = await _productManager.AddProductAsync(_testProduct, null);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.AddProductAsync(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task AddProductAsync_NonEmployeeUser_ReturnsFalse()
        {
            // Act
            var result = await _productManager.AddProductAsync(_testProduct, _testCustomer);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.AddProductAsync(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task AddProductAsync_JuniorEmployee_ReturnsFalse()
        {
            // Act
            var result = await _productManager.AddProductAsync(_testProduct, _testJuniorEmployee);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.AddProductAsync(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public async Task AddProductAsync_InvalidProductId_ReturnsFalse(string productId)
        {
            // Arrange
            var invalidProduct = new Product { productId = productId, name = "Valid Name", description = "Valid description" };

            // Act
            var result = await _productManager.AddProductAsync(invalidProduct, _testUser);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.AddProductAsync(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        [DataRow(0f)]
        [DataRow(-10f)]
        [DataRow(-100.50f)]
        public async Task AddProductAsync_InvalidPrice_ReturnsFalse(float price)
        {
            // Arrange
            var invalidProduct = new Product
            {
                productId = "valid",
                name = "Valid Name",
                description = "Valid description",
                price = price
            };

            // Act
            var result = await _productManager.AddProductAsync(invalidProduct, _testUser);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.AddProductAsync(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        [DataRow(-1)]
        [DataRow(5.1)]
        [DataRow(10)]
        public async Task AddProductAsync_InvalidRating_ReturnsFalse(double rating)
        {
            // Arrange
            var invalidProduct = new Product
            {
                productId = "valid",
                name = "Valid Name",
                description = "Valid description",
                price = 100,
                rating = rating
            };

            // Act
            var result = await _productManager.AddProductAsync(invalidProduct, _testUser);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.AddProductAsync(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task AddProductAsync_ExistingProductId_ReturnsFalse()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.GetAsync<Dictionary<string, ProductCategory>>("ProductCategories"))
                               .ReturnsAsync(_testCategories);
            _mockFirebaseHelper.Setup(x => x.GetAsync<Product>("Products/prod1"))
                               .ReturnsAsync(_testProduct); // Product already exists

            // Act
            var result = await _productManager.AddProductAsync(_testProduct, _testUser);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.AddProductAsync(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task AddProductAsync_NonExistentCategory_ReturnsFalse()
        {
            // Arrange
            var productWithInvalidCategory = new Product
            {
                productId = "prod1",
                name = "Test Product",
                description = "Valid description",
                categoryId = "nonexistent",
                price = 100,
                rating = 4.5
            };

            _mockFirebaseHelper.Setup(x => x.GetAsync<Dictionary<string, ProductCategory>>("ProductCategories"))
                               .ReturnsAsync(_testCategories);
            _mockFirebaseHelper.Setup(x => x.GetAsync<Product>("Products/prod1"))
                               .ReturnsAsync((Product)null);

            // Act
            var result = await _productManager.AddProductAsync(productWithInvalidCategory, _testUser);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.AddProductAsync(It.IsAny<Product>()), Times.Never);
        }

        #endregion

        #region GetProductsByCategoryAsync Tests

        [TestMethod]
        [TestCategory("BlackBox")]
        public async Task GetProductsByCategoryAsync_ValidCategory_ReturnsFilteredProducts()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.GetAllActiveProductsAsync())
                               .ReturnsAsync(_testProducts);

            // Act
            var result = await _productManager.GetProductsByCategoryAsync("cat1");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(p => p.categoryId == "cat1"));
        }

        [TestMethod]
        [TestCategory("BlackBox")]
        public async Task GetProductsByCategoryAsync_NonExistentCategory_ReturnsEmptyList()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.GetAllActiveProductsAsync())
                               .ReturnsAsync(_testProducts);

            // Act
            var result = await _productManager.GetProductsByCategoryAsync("nonexistent");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        #endregion

        #region GetProductsOrderedBy Tests

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task GetProductsOrderedBy_PriceLowToHigh_ReturnsSortedProducts()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.GetAllActiveProductsAsync())
                               .ReturnsAsync(_testProducts);

            // Act
            var result = await _productManager.GetProductsOrderedBy("Price [Low -> High]");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(50, result.First().price);
            Assert.AreEqual(150, result.Last().price);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task GetProductsOrderedBy_PriceHighToLow_ReturnsSortedProducts()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.GetAllActiveProductsAsync())
                               .ReturnsAsync(_testProducts);

            // Act
            var result = await _productManager.GetProductsOrderedBy("Price [High -> Low]");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(150, result.First().price);
            Assert.AreEqual(50, result.Last().price);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task GetProductsOrderedBy_InvalidFilter_ReturnsUnsortedProducts()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.GetAllActiveProductsAsync())
                               .ReturnsAsync(_testProducts);

            // Act
            var result = await _productManager.GetProductsOrderedBy("Invalid Filter");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            // Should return original order
            Assert.AreEqual("prod1", result.First().productId);
        }

        #endregion

        #region UpdateProductAsync Tests

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task UpdateProductAsync_ValidProduct_ReturnsTrue()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.GetAsync<Dictionary<string, ProductCategory>>("ProductCategories"))
                               .ReturnsAsync(_testCategories);
            _mockFirebaseHelper.Setup(x => x.GetAsync<Product>("Products/prod1"))
                               .ReturnsAsync(_testProduct);
            _mockFirebaseHelper.Setup(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<Product>()))
                               .Returns(Task.CompletedTask);

            // Act
            var result = await _productManager.UpdateProductAsync(_testProduct, _testUser);

            // Assert
            Assert.IsTrue(result);
            _mockFirebaseHelper.Verify(x => x.UpdateAsync($"Products/{_testProduct.productId}", _testProduct), Times.Once);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task UpdateProductAsync_NonSeniorEmployee_ReturnsFalse()
        {
            // Act
            var result = await _productManager.UpdateProductAsync(_testProduct, _testJuniorEmployee);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task UpdateProductAsync_NonExistentProduct_ReturnsFalse()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.GetAsync<Dictionary<string, ProductCategory>>("ProductCategories"))
                               .ReturnsAsync(_testCategories);
            _mockFirebaseHelper.Setup(x => x.GetAsync<Product>("Products/prod1"))
                               .ReturnsAsync((Product)null);

            // Act
            var result = await _productManager.UpdateProductAsync(_testProduct, _testUser);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<Product>()), Times.Never);
        }

        #endregion

        #region DeleteProductAsync Tests

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task DeleteProductAsync_ValidProductId_ReturnsTrue()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.DeleteAsync(It.IsAny<string>()))
                               .Returns(Task.CompletedTask);

            // Act
            var result = await _productManager.DeleteProductAsync("prod1", _testUser);

            // Assert
            Assert.IsTrue(result);
            _mockFirebaseHelper.Verify(x => x.DeleteAsync("Products/prod1"), Times.Once);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        public async Task DeleteProductAsync_NonSeniorEmployee_ReturnsFalse()
        {
            // Act
            var result = await _productManager.DeleteProductAsync("prod1", _testJuniorEmployee);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.DeleteAsync(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("WhiteBox")]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public async Task DeleteProductAsync_InvalidProductId_ReturnsFalse(string productId)
        {
            // Act
            var result = await _productManager.DeleteProductAsync(productId, _testUser);

            // Assert
            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.DeleteAsync(It.IsAny<string>()), Times.Never);
        }

        #endregion

        #region GetProductByIdAsync Tests

        [TestMethod]
        [TestCategory("BlackBox")]
        public async Task GetProductByIdAsync_ValidProductId_ReturnsProduct()
        {
            // Arrange
            _mockFirebaseHelper.Setup(x => x.GetAsync<Product>("Products/prod1"))
                               .ReturnsAsync(_testProduct);

            // Act
            var result = await _productManager.GetProductByIdAsync("prod1");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("prod1", result.productId);
        }

        [TestMethod]
        [TestCategory("BlackBox")]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public async Task GetProductByIdAsync_InvalidProductId_ReturnsNull(string productId)
        {
            // Act
            var result = await _productManager.GetProductByIdAsync(productId);

            // Assert
            Assert.IsNull(result);
            _mockFirebaseHelper.Verify(x => x.GetAsync<Product>(It.IsAny<string>()), Times.Never);
        }

        #endregion
    }
}