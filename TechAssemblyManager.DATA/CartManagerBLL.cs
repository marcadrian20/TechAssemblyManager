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

        public async Task<bool> AddProductToCartAsync(string userName, string productId, int quantity) => throw new NotImplementedException();
        public async Task<bool> RemoveProductFromCartAsync(string userName, string productId) => throw new NotImplementedException();
        public async Task<Dictionary<string, SelectedProduct>> GetUserCartAsync(string userName) => throw new NotImplementedException();
        public async Task<bool> AddPromotionToCartAsync(string userName, string promotionId) => throw new NotImplementedException();
        public async Task<bool> ClearCartAsync(string userName) => throw new NotImplementedException();
    }
}