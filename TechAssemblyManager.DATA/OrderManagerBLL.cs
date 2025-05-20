using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechAssemblyManager.Models;
using FirebaseWrapper;

namespace TechAssemblyManager.BLL
{
    public class OrderManagerBLL
    {
        private FirebaseHelper _firebaseHelper;
        public OrderManagerBLL(FirebaseHelper firebaseHelper)
        {
            _firebaseHelper = firebaseHelper;
        }

        public async Task<bool> PlaceOrderAsync(Order order, User client) => throw new NotImplementedException();
        public async Task<bool> PlaceServiceRequestAsync(ServiceRequest request, User client) => throw new NotImplementedException();
        public async Task<List<Order>> GetOrdersByClientAsync(string clientUserName) => throw new NotImplementedException();
        public async Task<bool> UpdateOrderStatusAsync(string orderId, string status, User employee) => throw new NotImplementedException();
        public async Task<List<Order>> GetAllOrdersAsync() => throw new NotImplementedException();
        public async Task<bool> DeleteOrderAsync(string orderId) => throw new NotImplementedException();
        public async Task<List<ServiceRequest>> GetServiceRequestsByClientAsync(string clientUserName) => throw new NotImplementedException();
        public async Task<bool> UpdateServiceRequestStatusAsync(string requestId, string status, User employee) => throw new NotImplementedException();

        public async Task<List<ServiceRequest>> GetAllServiceRequestsAsync()
        {
            return await _firebaseHelper.GetAllServiceRequestsAsync();
        }
    }
}
