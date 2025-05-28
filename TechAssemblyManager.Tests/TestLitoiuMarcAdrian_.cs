using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechAssemblyManager.BLL;
using TechAssemblyManager.Models;
using FirebaseWrapper;
using System.Linq;

namespace TechAssemblyManager.Tests
{
    [TestClass]
    public class TestLitoiuMarcAdrian_
    {
        private Mock<IFirebaseHelper>? _mockFirebaseHelper;
        private CartManagerBLL? _cartManager;
        private string _testUserName = "testuser";
        private string _testProductId = "prod1";
        private string _testPromotionId = "promo1";
        private Promotion? _testPromotion;
        private Dictionary<string, SelectedProduct>? _testCart;
        private List<Promotion>? _testPromotions;

        [TestInitialize]
        public void Setup()
        {
            _mockFirebaseHelper = new Mock<IFirebaseHelper>();
            _cartManager = new CartManagerBLL(_mockFirebaseHelper.Object);

            _testPromotion = new Promotion
            {
                promotionId = _testPromotionId,
                name = "Test Promo",
                description = "Test Desc",
                discountPercentage = 10,
                isActive = true,
                includedProductIds = new Dictionary<string, bool> { { _testProductId, true } }
            };

            _testCart = new Dictionary<string, SelectedProduct>
            {
                { _testProductId, new SelectedProduct { quantity = 1 } }
            };

            _testPromotions = new List<Promotion> { _testPromotion };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockFirebaseHelper = null;
            _cartManager = null;
            _testPromotion = null;
            _testCart = null;
            _testPromotions = null;
        }

        #region AddProductToCartAsync Tests

        [TestMethod]
        public async Task AddProductToCartAsync_ValidInput_ReturnsTrue()
        {
            _mockFirebaseHelper.Setup(x => x.AddProductToCartAsync(_testUserName, _testProductId, 2))
                .Returns(Task.CompletedTask);

            var result = await _cartManager.AddProductToCartAsync(_testUserName, _testProductId, 2);

            Assert.IsTrue(result);
            _mockFirebaseHelper.Verify(x => x.AddProductToCartAsync(_testUserName, _testProductId, 2), Times.Once);
        }

        [TestMethod]
        [DataRow(null, "prod1", 1)]
        [DataRow("", "prod1", 1)]
        [DataRow("testuser", null, 1)]
        [DataRow("testuser", "", 1)]
        [DataRow("testuser", "prod1", 0)]
        [DataRow("testuser", "prod1", -1)]
        public async Task AddProductToCartAsync_InvalidInput_ReturnsFalse(string userName, string productId, int quantity)
        {
            var result = await _cartManager.AddProductToCartAsync(userName, productId, quantity);

            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.AddProductToCartAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Never);
        }

        #endregion

        #region RemoveProductFromCartAsync Tests

        [TestMethod]
        public async Task RemoveProductFromCartAsync_ValidInput_ReturnsTrue()
        {
            _mockFirebaseHelper.Setup(x => x.RemoveProductFromCartAsync(_testUserName, _testProductId))
                .Returns(Task.CompletedTask);

            var result = await _cartManager.RemoveProductFromCartAsync(_testUserName, _testProductId);

            Assert.IsTrue(result);
            _mockFirebaseHelper.Verify(x => x.RemoveProductFromCartAsync(_testUserName, _testProductId), Times.Once);
        }

        [TestMethod]
        [DataRow(null, "prod1")]
        [DataRow("", "prod1")]
        [DataRow("testuser", null)]
        [DataRow("testuser", "")]
        public async Task RemoveProductFromCartAsync_InvalidInput_ReturnsFalse(string userName, string productId)
        {
            var result = await _cartManager.RemoveProductFromCartAsync(userName, productId);

            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.RemoveProductFromCartAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        #endregion

        #region GetUserCartAsync Tests

        [TestMethod]
        public async Task GetUserCartAsync_ValidUser_ReturnsCart()
        {
            _mockFirebaseHelper.Setup(x => x.GetUserCartAsync(_testUserName))
                .ReturnsAsync(_testCart);

            var result = await _cartManager.GetUserCartAsync(_testUserName);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result.ContainsKey(_testProductId));
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        public async Task GetUserCartAsync_InvalidUser_ReturnsEmpty(string userName)
        {
            var result = await _cartManager.GetUserCartAsync(userName);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        #endregion

        #region AddPromotionToCartAsync Tests

        [TestMethod]
        public async Task AddPromotionToCartAsync_ValidPromotion_AddsProductsAndPromotionItem()
        {
            _mockFirebaseHelper.Setup(x => x.GetAllPromotionsAsync())
                .ReturnsAsync(_testPromotions);
            _mockFirebaseHelper.Setup(x => x.GetUserCartAsync(_testUserName))
                .ReturnsAsync(new Dictionary<string, SelectedProduct>());
            _mockFirebaseHelper.Setup(x => x.AddProductToCartAsync(_testUserName, _testProductId, 1))
                .Returns(Task.CompletedTask);
            _mockFirebaseHelper.Setup(x => x.GetAsync<Product>($"Products/{_testProductId}"))
                .ReturnsAsync(new Product { productId = _testProductId, price = 100 });
            _mockFirebaseHelper.Setup(x => x.SetAsync(
                $"Users/{_testUserName}/PromotionCartItem",
                It.IsAny<PromotionCartItem>()))
                .Returns(Task.CompletedTask);

            var result = await _cartManager.AddPromotionToCartAsync(_testUserName, _testPromotionId);

            Assert.IsTrue(result);
            _mockFirebaseHelper.Verify(x => x.AddProductToCartAsync(_testUserName, _testProductId, 1), Times.Once);
            _mockFirebaseHelper.Verify(x => x.SetAsync(
                $"Users/{_testUserName}/PromotionCartItem",
                It.Is<PromotionCartItem>(p => p.PromotionId == _testPromotionId)), Times.Once);
        }

        [TestMethod]
        public async Task AddPromotionToCartAsync_InvalidPromotion_ReturnsFalse()
        {
            _mockFirebaseHelper.Setup(x => x.GetAllPromotionsAsync())
                .ReturnsAsync(new List<Promotion>());

            var result = await _cartManager.AddPromotionToCartAsync(_testUserName, "invalidPromo");

            Assert.IsFalse(result);
            _mockFirebaseHelper.Verify(x => x.SetAsync(It.IsAny<string>(), It.IsAny<PromotionCartItem>()), Times.Never);
        }

        #endregion

        #region RemovePromotionFromCartAsync Tests

        [TestMethod]
        public async Task RemovePromotionFromCartAsync_Valid_RemovesPromotionAndProducts()
        {
            var promoCartItem = new PromotionCartItem { PromotionId = _testPromotionId };
            _mockFirebaseHelper.Setup(x => x.GetAsync<PromotionCartItem>($"Users/{_testUserName}/PromotionCartItem"))
                .ReturnsAsync(promoCartItem);
            _mockFirebaseHelper.Setup(x => x.GetAllPromotionsAsync())
                .ReturnsAsync(_testPromotions);
            _mockFirebaseHelper.Setup(x => x.RemoveProductFromCartAsync(_testUserName, _testProductId))
                .Returns(Task.CompletedTask);
            _mockFirebaseHelper.Setup(x => x.DeleteAsync($"Users/{_testUserName}/PromotionCartItem"))
                .Returns(Task.CompletedTask);

            var result = await _cartManager.RemovePromotionFromCartAsync(_testUserName);

            Assert.IsTrue(result);
            _mockFirebaseHelper.Verify(x => x.RemoveProductFromCartAsync(_testUserName, _testProductId), Times.Once);
            _mockFirebaseHelper.Verify(x => x.DeleteAsync($"Users/{_testUserName}/PromotionCartItem"), Times.Once);
        }

        [TestMethod]
        public async Task RemovePromotionFromCartAsync_NoPromotionItem_DeletesNothing()
        {
            _mockFirebaseHelper.Setup(x => x.GetAsync<PromotionCartItem>($"Users/{_testUserName}/PromotionCartItem"))
                .ReturnsAsync((PromotionCartItem)null);
            _mockFirebaseHelper.Setup(x => x.DeleteAsync($"Users/{_testUserName}/PromotionCartItem"))
                .Returns(Task.CompletedTask);

            var result = await _cartManager.RemovePromotionFromCartAsync(_testUserName);

            Assert.IsTrue(result);
            _mockFirebaseHelper.Verify(x => x.RemoveProductFromCartAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _mockFirebaseHelper.Verify(x => x.DeleteAsync($"Users/{_testUserName}/PromotionCartItem"), Times.Once);
        }

        #endregion

        #region GetPromotionCartItemAsync Tests

        [TestMethod]
        public async Task GetPromotionCartItemAsync_ValidUser_ReturnsItem()
        {
            var promoCartItem = new PromotionCartItem { PromotionId = _testPromotionId };
            _mockFirebaseHelper.Setup(x => x.GetAsync<PromotionCartItem>($"Users/{_testUserName}/PromotionCartItem"))
                .ReturnsAsync(promoCartItem);

            var result = await _cartManager.GetPromotionCartItemAsync(_testUserName);

            Assert.IsNotNull(result);
            Assert.AreEqual(_testPromotionId, result.PromotionId);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        public async Task GetPromotionCartItemAsync_InvalidUser_ReturnsNull(string userName)
        {
            var result = await _cartManager.GetPromotionCartItemAsync(userName);

            Assert.IsNull(result);
        }

        #endregion

        #region ClearCartAsync Tests

        [TestMethod]
        public async Task ClearCartAsync_RemovesAllProducts()
        {
            var cart = new Dictionary<string, SelectedProduct>
            {
                { "prod1", new SelectedProduct { quantity = 1 } },
                { "prod2", new SelectedProduct { quantity = 2 } }
            };
            _mockFirebaseHelper.Setup(x => x.GetUserCartAsync(_testUserName))
                .ReturnsAsync(cart);
            _mockFirebaseHelper.Setup(x => x.RemoveProductFromCartAsync(_testUserName, It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var result = await _cartManager.ClearCartAsync(_testUserName);

            Assert.IsTrue(result);
            _mockFirebaseHelper.Verify(x => x.RemoveProductFromCartAsync(_testUserName, "prod1"), Times.Once);
            _mockFirebaseHelper.Verify(x => x.RemoveProductFromCartAsync(_testUserName, "prod2"), Times.Once);
        }

        #endregion
    }
}