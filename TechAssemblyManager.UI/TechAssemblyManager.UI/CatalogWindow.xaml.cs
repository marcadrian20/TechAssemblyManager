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
using TechAssemblyManager.BLL;
using TechAssemblyManager.DAL.FirebaseHelper;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    /// <summary>
    /// Interaction logic for CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        private readonly ProductManagerBLL _productManager;
        private readonly CartManagerBLL _cartManager;
        private List<Product> _allProducts = new();
        private List<ProductCategory> _categories = new();
        private dynamic selectedProduct;
        private UserManagerBLL userManagerBLL;
        private ProductManagementWindow _productManagementWindow;
        private CartWindow _cartWindow;
        private OrderManagerBLL orderManager;
        public CatalogWindow(ProductManagerBLL productManager, CartManagerBLL cartManager, UserManagerBLL userManagerBLL, ProductManagementWindow productManagementWindow, OrderManagerBLL orderManager)
        {
            InitializeComponent();
            _productManager = productManager;
            _cartManager = cartManager;
            this.userManagerBLL = userManagerBLL;
            _productManagementWindow = productManagementWindow;
            this._cartManager = cartManager;
            this.orderManager = orderManager;
            LoadData();
        }
        private async void LoadData()
        {
            _allProducts = await _productManager.GetAllActiveProductsAsync();
            _categories = await _productManager.GetProductCategoriesAsync();

            CmbCategorie.ItemsSource = _categories;
            CmbCategorie.DisplayMemberPath = "name";
            CmbCategorie.SelectedValuePath = "categoryId";

            BtnAddToCart.IsEnabled = false;
            LblInfo.Text = "";
        }




        private void CmbCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbCategorie.SelectedValue is string selectedCategoryId)
            {
                bool canAdd = SessionManager.LoggedInUser?.userType == "customer";

                var filtered = _allProducts
                    .Where(p => p.categoryId == selectedCategoryId)
                    .Select(p => new
                    {
                        ProductId = p.productId,
                        Name = p.name,
                        Price = p.price,
                        Rating = p.rating,
                        Description = p.description,
                        CanAdd = canAdd
                    }).ToList();

                ProductGrid.ItemsSource = filtered;

                BtnAddToCart.IsEnabled = false;
                LblInfo.Text = "";
                selectedProduct = null;
            }
        }
        private async void BtnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            var user = SessionManager.LoggedInUser;

            if (user == null || !userManagerBLL.IsCustomer(user))
            {
                MessageBox.Show("Trebuie să fii logat ca și client.");
                return;
            }

            if (selectedProduct == null)
            {
                MessageBox.Show("Selectează un produs.");
                return;
            }

            string productId = selectedProduct.ProductId;
            int quantityToAdd = 1;

            // Obține coșul curent al utilizatorului
            var currentCart = await _cartManager.GetUserCartAsync(user.userName);

            if (currentCart.ContainsKey(productId))
            {
                int existingQty = currentCart[productId].quantity;
                await _cartManager.AddProductToCartAsync(user.userName, productId, existingQty + quantityToAdd);
            }
            else
            {
                await _cartManager.AddProductToCartAsync(user.userName, productId, quantityToAdd);
            }

            MessageBox.Show($"Produsul '{selectedProduct.Name}' a fost adăugat/actualizat în coș!");
            _cartWindow=new CartWindow(_cartManager,_productManager,orderManager);
            _cartWindow.Show();
            this.Hide();

        }

        private void ProductGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductGrid.SelectedItem != null)
            {
                selectedProduct = ProductGrid.SelectedItem;
                bool canAdd = SessionManager.LoggedInUser?.userType == "customer";

                if (!canAdd)
                {
                    LblInfo.Text = "Trebuie să fii autentificat ca și client pentru a adăuga în coș.";
                    BtnAddToCart.IsEnabled = false;
                }
                else
                {
                    LblInfo.Text = "";
                    BtnAddToCart.IsEnabled = true;
                }
            }
            else
            {
                selectedProduct = null;
                BtnAddToCart.IsEnabled = false;
                LblInfo.Text = "";
            }
        }
        

    }
}
