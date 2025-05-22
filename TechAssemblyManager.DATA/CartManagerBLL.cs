using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechAssemblyManager.Models;
using FirebaseWrapper;

namespace TechAssemblyManager.BLL
{
    public class CartManagerBLL
    {
        private FirebaseHelper _firebaseHelper;
        public CartManagerBLL(FirebaseHelper firebaseHelper) { _firebaseHelper = firebaseHelper; }

        public async Task<bool> AddProductToCartAsync(string userName, string productId, int quantity)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(productId) || quantity <= 0)
                return false;

            await _firebaseHelper.AddProductToCartAsync(userName, productId, quantity);
            return true;
        }
        public async Task<bool> RemoveProductFromCartAsync(string userName, string productId)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(productId))
                return false;

            await _firebaseHelper.RemoveProductFromCartAsync(userName, productId);
            return true;
        }
        public async Task<Dictionary<string, SelectedProduct>> GetUserCartAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return new();

            return await _firebaseHelper.GetUserCartAsync(userName);
        }
        public async Task<bool> AddPromotionToCartAsync(string userName, string promotionId)
        {
            var promotions = await _firebaseHelper.GetAllPromotionsAsync();
            var promotion = promotions.FirstOrDefault(p => p.promotionId == promotionId && p.isActive);
            if (promotion == null) return false;

            var userCart = await _firebaseHelper.GetUserCartAsync(userName);
            decimal totalPrice = 0;
            foreach (var item in userCart)
            {
                var product = await _firebaseHelper.GetAsync<Product>($"Products/{item.Key}");
                if (product != null)
                {
                    totalPrice += (decimal)product.price * item.Value.quantity;
                }
            }

            var discountAmount = -(totalPrice * ((decimal)promotion.discountPercentage / 100));

            // Add the promotion to the cart
            await _firebaseHelper.SetAsync($"Users/{userName}/PromotionCartItem", new PromotionCartItem
            {
                PromotionId = promotionId,
                DiscountAmount = discountAmount,
                Description = promotion.description
            });

            return true;
        }
        public async Task<bool> ClearCartAsync(string userName)
        {
            var cart = await GetUserCartAsync(userName);
            foreach (var productId in cart.Keys)
            {
                await _firebaseHelper.RemoveProductFromCartAsync(userName, productId);
            }
            return true;
        }

    }
}