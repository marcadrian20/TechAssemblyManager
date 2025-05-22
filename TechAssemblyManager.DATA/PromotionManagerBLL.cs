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
        private FirebaseHelper _firebaseHelper;

        public PromotionManagerBLL(FirebaseHelper firebaseHelper)
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
