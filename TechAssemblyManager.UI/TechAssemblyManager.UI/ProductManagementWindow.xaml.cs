using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for ProductManagementWindow.xaml
    /// </summary>
    public partial class ProductManagementWindow : Window
    {
        private readonly ProductManagerBLL _productManager;
        private List<ProductCategory> _categories;
        public ProductManagementWindow(ProductManagerBLL productManager)
        {
            InitializeComponent();
            _productManager = productManager;
            LoadCategories();
        }
        private async void LoadCategories()
        {
            _categories = await _productManager.GetProductCategoriesAsync();
            CategoryComboBox.ItemsSource = _categories;
        }

        private async void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = SessionManager.LoggedInUser;

            if (currentUser == null || currentUser.userType != "employee" || currentUser.employeeData?.isSenior != true)
            {
                LblStatus.Text = "Doar angajații seniori pot adăuga produse.";
                return;
            }

            if (!float.TryParse(ProductPriceTextBox.Text.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out float price))
            {
                LblStatus.Text = "Preț invalid.";
                return;
            }

            var product = new Product
            {
                productId = ProductIdTextBox.Text.Trim(),
                name = ProductNameTextBox.Text.Trim(),
                description = ProductDescriptionTextBox.Text.Trim(),
                price = price,
                rating = 0,
                isActive = ProductIsActiveCheckBox.IsChecked == true,
                hasPromotion = false,
                promotionId = "",
                categoryId = CategoryComboBox.SelectedValue?.ToString()
            };

            bool success = await _productManager.AddProductAsync(product, currentUser);
            LblStatus.Text = success ? "Produs adăugat cu succes." : "Eroare la adăugare produs.";
        }
    }
 }
