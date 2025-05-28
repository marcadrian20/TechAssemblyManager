using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebaseWrapper;
using TechAssemblyManager.Models;
namespace TechAssemblyManager.BLL
{
    public class PromotionManagerBLL
    {
        private IFirebaseHelper _firebaseHelper;

        public PromotionManagerBLL(IFirebaseHelper firebaseHelper)
        {
            _firebaseHelper = firebaseHelper;
        }
        public async Task<bool> AddPromotionAsync(Promotion promotion, User manager)
        {
            if (manager == null || manager.userType != "manager")
                return false;

            promotion.createdBy = manager.userName;
            promotion.isActive = true;

            return await _firebaseHelper.AddPromotionAsync(promotion);
        }
        public async Task<List<Promotion>> GetAllPromotionsAsync()
        {
            return await _firebaseHelper.GetAllPromotionsAsync();
        }
        public async Task<bool> DeletePromotionAsync(string promotionId, User manager)
        {
            if (manager.userType != "manager" || string.IsNullOrWhiteSpace(promotionId))
                return false;

            await _firebaseHelper.DeleteAsync($"Promotions/{promotionId}");
            return true;
        }
        public async Task<bool> UpdatePromotionAsync(Promotion promotion, User user)
        {
            if (user == null || promotion == null)
                return false;
            // Promotion validation
            if (promotion == null || string.IsNullOrWhiteSpace(promotion.promotionId))
                return false;

            // Basic field validation
            if (string.IsNullOrWhiteSpace(promotion.name) ||
                promotion.discountPercentage <= 0 ||
                promotion.discountPercentage > 100)
                return false;

            // Date validation
            if (DateTime.TryParse(promotion.startDate, out var start) &&
                DateTime.TryParse(promotion.endDate, out var end))
            {
                if (start >= end)
                    return false;
            }
            else
            {
                return false; // Invalid date format
            }

            // Check if promotion exists
            var existingPromotion = await _firebaseHelper.GetAsync<Promotion>($"Promotions/{promotion.promotionId}");
            if (existingPromotion == null)
                return false;
            await _firebaseHelper.UpdateAsync($"Promotions/{promotion.promotionId}", promotion);
            return true;
        }
        public async Task<bool> AddPromotionToCartAsync(string userName, string promotionId)
        {
            var promotions = await _firebaseHelper.GetAllPromotionsAsync();
            var promo = promotions.FirstOrDefault(p => p.promotionId == promotionId && p.isActive);
            if (promo == null) return false;

            // Use method from CartManagerBLL if needed
            var cartMgr = new CartManagerBLL(_firebaseHelper);
            return await cartMgr.AddPromotionToCartAsync(userName, promotionId);
        }
        public async Task<List<Promotion>> GetActivePromotionsAsync()
        {
            var promotions = await _firebaseHelper.GetAllPromotionsAsync();
            return promotions.Where(p => p.isActive).ToList();
        }
    }
}