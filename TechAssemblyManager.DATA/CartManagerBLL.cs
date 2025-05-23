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
        public async Task<bool> RemovePromotionFromCartAsync(string userName)
        {
            // Remove the promotion cart item
            await _firebaseHelper.DeleteAsync($"Users/{userName}/PromotionCartItem");

            // Remove the products associated with the promotion
            var promotions = await _firebaseHelper.GetAllPromotionsAsync();
            var promoItem = await GetPromotionCartItemAsync(userName);
            if (promoItem != null)
            {
                var promotion = promotions.FirstOrDefault(p => p.promotionId == promoItem.PromotionId);
                if (promotion != null && promotion.includedProductIds != null)
                {
                    foreach (var prodId in promotion.includedProductIds.Keys)
                    {
                        await _firebaseHelper.RemoveProductFromCartAsync(userName, prodId);
                    }
                }
            }
            return true;
        }

        public async Task<bool> AddPromotionToCartAsync(string userName, string promotionId)
        {
            var promotions = await _firebaseHelper.GetAllPromotionsAsync();
            var promotion = promotions.FirstOrDefault(p => p.promotionId == promotionId && p.isActive);
            if (promotion == null || promotion.includedProductIds == null) return false;

            // Add all products in the promotion to the cart
            foreach (var prodId in promotion.includedProductIds.Keys)
            {
                var cart = await _firebaseHelper.GetUserCartAsync(userName);
                if (!cart.ContainsKey(prodId))
                {
                    await _firebaseHelper.AddProductToCartAsync(userName, prodId, 1);
                }
            }

            // Calculate total price of included products
            decimal totalPrice = 0;
            foreach (var prodId in promotion.includedProductIds.Keys)
            {
                var product = await _firebaseHelper.GetAsync<Product>($"Products/{prodId}");
                if (product != null)
                    totalPrice += (decimal)product.price;
            }
            var discountAmount = -(totalPrice * ((decimal)promotion.discountPercentage / 100));

            // Add the promotion as a negative price item
            await _firebaseHelper.SetAsync($"Users/{userName}/PromotionCartItem", new PromotionCartItem
            {
                PromotionId = promotionId,
                DiscountAmount = discountAmount,
                Description = promotion.description
            });

            return true;
        }
        public async Task<PromotionCartItem?> GetPromotionCartItemAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return null;
            return await _firebaseHelper.GetAsync<PromotionCartItem>($"Users/{userName}/PromotionCartItem");
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