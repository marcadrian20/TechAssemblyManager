using FirebaseWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TechAssemblyManager.DAL.FirebaseHelper;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    /// <summary>
    /// Interaction logic for CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        private FirebaseHelper _firebaseHelper;
        private List<Product> _allProducts = new List<Product>();
        private List<ProductCategory> _categories = new List<ProductCategory>();
        public CatalogWindow()
        {
            InitializeComponent();
            _firebaseHelper = new FirebaseHelper(
              "https://techassemblymanager-default-rtdb.firebaseio.com/",
              "ky7wJX7Iu46hjBHWqDJNWjJW19NeYQurX4Z9VeUv",
              "AIzaSyBxq3J01JqE6yonLc9plkzA6c3-Gi1r1eU"
          );
            LoadData();
        }
        private async void LoadData()
        {
            _allProducts = await _firebaseHelper.GetAllActiveProductsAsync();
            var categoryDict = await _firebaseHelper.GetAsync<Dictionary<string, ProductCategory>>("ProductCategories");
            _categories = categoryDict?.Values.ToList() ?? new List<ProductCategory>();

            CmbCategorie.ItemsSource = _categories;
            CmbCategorie.DisplayMemberPath = "name";
            CmbCategorie.SelectedValuePath = "categoryId";
        }


        private void CmbCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbCategorie.SelectedValue is string selectedCategoryId)
            {
                var filtered = _allProducts
                    .Where(p => p.categoryId == selectedCategoryId)
                    .Select(p => new CatalogProductViewModel
                    {
                        ProductId = p.productId,
                        Name = p.name,
                        Price = p.price,
                        Rating = p.rating,
                        Description = p.description,
                        CanAdd = SessionManager.LoggedInUser?.userType == "customer"
                    }).ToList();

                ProductGrid.ItemsSource = filtered;
            }
        }
        private async void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is CatalogProductViewModel product)
            {
                var user = SessionManager.LoggedInUser;
                if (user == null || user.userType != "customer")
                {
                    MessageBox.Show("Trebuie să fii logat ca client pentru a adăuga în coș.");
                    return;
                }

                await _firebaseHelper.AddProductToCartAsync(user.userName, product.ProductId, 1);
                MessageBox.Show("Produs adăugat în coș!");
            }
        }
    }
}
