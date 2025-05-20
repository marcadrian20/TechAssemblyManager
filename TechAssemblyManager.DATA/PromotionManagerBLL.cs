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

        public async Task<bool> AddPromotionAsync(Promotion promotion, User manager) => throw new NotImplementedException();
        public async Task<List<Promotion>> GetAllPromotionsAsync() => throw new NotImplementedException();
        public async Task<bool> DeletePromotionAsync(string promotionId, User manager) => throw new NotImplementedException();
        public async Task<bool> AddPromotionToCartAsync(string userName, string promotionId) => throw new NotImplementedException();
        public async Task<List<Promotion>> GetActivePromotionsAsync() => throw new NotImplementedException();

    }
}
