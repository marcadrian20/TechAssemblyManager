using TechAssemblyManager.Models;
namespace FirebaseWrapper
{
    public interface IFirebaseHelper
    {
       Task<T> GetAsync<T>(string path) where T : class;
        Task SetAsync<T>(string path, T data) where T : class;
        Task UpdateAsync<T>(string path, T data) where T : class;
        Task PushAsync<T>(string path, T data) where T : class;
        Task DeleteAsync(string path);
        Task<bool> SignUpAsync(string email, string password, string username);
        Task<User?> LoginAsync(string emailOrUsername, string password);
        Task<List<User>> GetAllEmployeesAsync();
        Task UpdateEmployeeAsync(User employee);
        Task<string> AddOrderAsync(Order order);
        Task<List<Order>> GetOrdersByClientAsync(string clientUserName);
        Task UpdateOrderStatusAsync(string orderId, string status);
        Task<string> AddServiceRequestAsync(ServiceRequest request);
        Task<List<ServiceRequest>> GetServiceRequestsByClientAsync(string clientUserName);
        Task UpdateServiceRequestStatusAsync(string requestId, string status);
        Task<List<ServiceRequest>> GetAllServiceRequestsAsync();
        Task<List<Product>> GetProductsByCategoryAsync(string categoryId);
        Task<List<Promotion>> GetAllPromotionsAsync();
        Task AddProductToCartAsync(string userName, string productId, int quantity);
        Task<Dictionary<string, SelectedProduct>> GetUserCartAsync(string userName);
        Task RemoveProductFromCartAsync(string userName, string productId);
        Task<bool> AddPromotionAsync(Promotion promotion);
        Task<bool> AddProductCategoryAsync(ProductCategory category);
        Task<List<ProductCategory>> GetCategoriesByTypeAsync(string type);
        Task<bool> AddProductAsync(Product product);
        Task<List<Product>> GetAllActiveProductsAsync();
    }
}