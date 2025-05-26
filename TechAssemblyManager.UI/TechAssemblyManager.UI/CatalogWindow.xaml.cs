using System;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TechAssemblyManager.BLL;
using TechAssemblyManager.DAL.FirebaseHelper;
using TechAssemblyManager.Models;
using System.Globalization;
using System.Windows.Controls.Primitives;

namespace TechAssemblyManager.UI
{
    public partial class CatalogWindow : Window
    {
        private readonly ProductManagerBLL _productManager;
        private readonly CartManagerBLL _cartManager;
        private readonly OrderManagerBLL _orderManager;
        private List<ProductCategory> _categories = new();
        private List<Product> _allProducts = new();
        private List<Product> _filteredProducts = new();
        private string _selectedType = "PREBUILT";
        private string _selectedCategoryId = null;
        private string _selectedFilter = null;

        public CatalogWindow(ProductManagerBLL productManager, CartManagerBLL cartManager, OrderManagerBLL orderManager)
        {
            InitializeComponent();
            _productManager = productManager;
            _cartManager = cartManager;
            _orderManager = orderManager;
            LoadTypes();
            LoadFilters();
        }

        private void LoadTypes()
        {
            TypeComboBox.ItemsSource = new List<string> { "PREBUILT", "DIY" };
            TypeComboBox.SelectedIndex = 0;
        }

        private void LoadFilters()
        {
            FilterComboBox.ItemsSource = new List<string>
            {
                "Niciun filtru",
                "Category [A -> Z]",
                "Category [Z -> A]",
                "Price [Low -> High]",
                "Price [High -> Low]",
                "Name [A -> Z]",
                "Name [Z -> A]"
            };
            FilterComboBox.SelectedIndex = 0;
        }

        private async void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedType = TypeComboBox.SelectedItem as string;
            _categories = await _productManager.GetCategoriesByTypeAsync(_selectedType == "PREBUILT" ? "system" : "component");
            CategoryComboBox.ItemsSource = _categories;
            CategoryComboBox.DisplayMemberPath = "name";
            CategoryComboBox.SelectedValuePath = "categoryId";
            CategoryComboBox.SelectedIndex = 0;
        }

        private async void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryComboBox.SelectedValue is string categoryId)
            {
                _selectedCategoryId = categoryId;
                _allProducts = await _productManager.GetProductsByCategoryAsync(categoryId);
                ApplyFiltersAndSearch();
            }
        }

        private async void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedFilter = FilterComboBox.SelectedItem as string;
            ApplyFiltersAndSearch();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFiltersAndSearch();
        }

        private async void ApplyFiltersAndSearch()
        {
            var products = _allProducts;

            // Filter by search
            string search = SearchBox.Text?.Trim().ToLower() ?? "";
            if (!string.IsNullOrWhiteSpace(search))
            {
                products = products.Where(p => p.name.ToLower().Contains(search)).ToList();
            }

            // Apply sorting/filter
            if (!string.IsNullOrWhiteSpace(_selectedFilter) && _selectedFilter != "Niciun filtru")
            {
                products = await _productManager.GetProductsOrderedBy(_selectedFilter);
                // Only keep products from the selected category
                if (!string.IsNullOrWhiteSpace(_selectedCategoryId))
                    products = products.Where(p => p.categoryId == _selectedCategoryId).ToList();
            }

            // Prepare for DataGrid
            var user = SessionManager.LoggedInUser;
            ProductGrid.ItemsSource = products.Select(p => new CatalogProductViewModel
            {
                ProductId = p.productId,
                Name = p.name,
                Price = p.price,
                Rating = p.rating,
                Description = p.description,
                CanAdd = user != null && user.userType == "customer"
            }).ToList();
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

                await _cartManager.AddProductToCartAsync(user.userName, product.ProductId, 1);
                MessageBox.Show("Produs adăugat în coș!");
            }
        }
        private void StarPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is StackPanel starPanel)
            {
                if (starPanel.DataContext is CatalogProductViewModel product)
                {
                    var stars = starPanel.Children.OfType<Path>().ToList();
                    double rating = product.Rating;

                    for (int i = 0; i < stars.Count; i++)
                    {
                        double starValue = rating - i;

                        if (starValue >= 1)
                            stars[i].Fill = Brushes.Gold;
                        else if (starValue >= 0.5)
                            stars[i].Fill = new LinearGradientBrush(Colors.Gold, Colors.Gray, 0);
                        else
                            stars[i].Fill = Brushes.Gray;
                    }
                }
            }
        }
        private void BtnSeeCart_Click(object sender, RoutedEventArgs e)
        {
            var user = SessionManager.LoggedInUser;
            if (user == null || user.userType != "customer")
            {
                MessageBox.Show("Trebuie să fii logat ca client pentru a vedea coșul.");
                return;
            }
            new CartWindow(_cartManager, _productManager, _orderManager).ShowDialog();
        }
        private void BtnSeeDetails_Click(object sender, RoutedEventArgs e)
        {
            if (ProductGrid.SelectedItem is CatalogProductViewModel selectedProduct)
            {
                var detailsWindow = new ProductDetailsWindow(selectedProduct);
                detailsWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selectează un produs pentru a vedea detaliile.");
            }
        }

    }

}