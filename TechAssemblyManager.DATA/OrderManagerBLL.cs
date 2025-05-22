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
        public async Task<string> PlaceOrderAsync(Order order, User client)
        {
            if (order == null || client == null || client.userType != "customer")
                return null;

            order.OrderDate = DateTime.Now;
            order.ClientUserName = client.userName;
            order.OrderStatus = "Placed";

            var orderId = await _firebaseHelper.AddOrderAsync(order);
            order.OrderId = orderId;

            await ClearCartAsync(client.userName);
            return orderId;
        }
        private async Task ClearCartAsync(string userName)
        {
            var cart = await _firebaseHelper.GetUserCartAsync(userName);
            foreach (var productId in cart.Keys)
            {
                await _firebaseHelper.RemoveProductFromCartAsync(userName, productId);
            }

            await _firebaseHelper.DeleteAsync($"Users/{userName}/PromotionCartItem");
        }
        public async Task<bool> PlaceServiceRequestAsync(ServiceRequest request, User client)
        {
            if (request == null || client == null || client.userType != "customer")
                return false;

            request.Status = "Requested";
            request.CustomerUserName = client.userName;

            await _firebaseHelper.AddServiceRequestAsync(request);
            return true;
        }
        public async Task<List<Order>> GetOrdersByClientAsync(string clientUserName)
        {
            return await _firebaseHelper.GetOrdersByClientAsync(clientUserName);
        }
        public async Task<bool> UpdateOrderStatusAsync(string orderId, string status, User employee)
        {
            if (employee.userType != "employee")
                return false;

            await _firebaseHelper.UpdateOrderStatusAsync(orderId, status);
            return true;
        }
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            var ordersDict = await _firebaseHelper.GetAsync<Dictionary<string, Order>>("Orders");
            if (ordersDict == null)
                return new List<Order>();

            foreach (var kvp in ordersDict)
            {
                if (kvp.Value != null)
                    kvp.Value.OrderId = kvp.Key;
            }
            return ordersDict.Values.ToList();
        }
        public async Task<bool> DeleteOrderAsync(string orderId)
        {
            if (string.IsNullOrWhiteSpace(orderId))
                return false;

            await _firebaseHelper.DeleteAsync($"Orders/{orderId}");
            return true;
        }
        public async Task<bool> UpdateOrderDetailsAsync(Order order, string newStatus, User employee)
        {
            if (order == null || employee == null || employee.userType != "employee")
                return false;

            order.OrderStatus = newStatus;
            if (newStatus == "Completed")
                order.CompletionDate = DateTime.Now;

            await _firebaseHelper.SetAsync($"Orders/{order.OrderId}", order);
            return true;
        }
        public async Task<List<ServiceRequest>> GetServiceRequestsByClientAsync(string clientUserName)
        {
            return await _firebaseHelper.GetServiceRequestsByClientAsync(clientUserName);
        }
        public async Task<bool> UpdateServiceRequestStatusAsync(string requestId, string status, User employee)
        {
            if (employee.userType != "employee")
                return false;

            await _firebaseHelper.UpdateServiceRequestStatusAsync(requestId, status);
            return true;
        }
        public async Task<List<ServiceRequest>> GetAllServiceRequestsAsync()
        {
            return await _firebaseHelper.GetAllServiceRequestsAsync();
        }
    }
}