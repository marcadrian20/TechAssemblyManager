using System.Windows;
using System.Threading.Tasks;
using FirebaseWrapper;
using System.Linq;

namespace TechAssemblyManager.UI
{
    public partial class DebugWindow : Window
    {
        private readonly FirebaseHelper _firebaseHelper;

        public DebugWindow(FirebaseHelper firebaseHelper)
        {
            InitializeComponent();
            _firebaseHelper = firebaseHelper;
        }

        private async void DeleteAllOrders_Click(object sender, RoutedEventArgs e)
        {
            await _firebaseHelper.DeleteAsync("Orders");
            LblStatus.Text = "All orders deleted.";
        }

        private async void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var username = TxtUserName.Text.Trim();
            if (string.IsNullOrWhiteSpace(username))
            {
                LblStatus.Text = "Enter a username!";
                return;
            }
            await _firebaseHelper.DeleteAsync($"Users/{username}");
            LblStatus.Text = $"User '{username}' deleted.";
        }

        private async void DeleteAllServiceRequests_Click(object sender, RoutedEventArgs e)
        {
            await _firebaseHelper.DeleteAsync("ServiceRequests");
            LblStatus.Text = "All service requests deleted.";
        }

        private async void DeleteAllProducts_Click(object sender, RoutedEventArgs e)
        {
            await _firebaseHelper.DeleteAsync("Products");
            await _firebaseHelper.DeleteAsync("ProductCategories");
            LblStatus.Text = "All products and categories deleted.";
        }

        private async void DeleteAllPromotions_Click(object sender, RoutedEventArgs e)
        {
            await _firebaseHelper.DeleteAsync("Promotions");
            LblStatus.Text = "All promotions deleted.";
        }
    }
}