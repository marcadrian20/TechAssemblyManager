using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TechAssemblyManager.BLL;
using TechAssemblyManager.DAL.FirebaseHelper;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    public partial class ProductManagementWindow : Window
    {
        private readonly ProductManagerBLL _productManager;
        private readonly PromotionManagerBLL _promotionManager;
        private List<ProductCategory> _categories = new();
        private List<Product> _products = new();
        private string _selectedType = "PREBUILT";
        private ProductCategory _selectedCategory;

        public ProductManagementWindow(ProductManagerBLL productManager, PromotionManagerBLL promotionManager)
        {
            InitializeComponent();
            _productManager = productManager;
            _promotionManager = promotionManager;
            LoadTypes();
        }

        private void LoadTypes()
        {
            TypeComboBox.ItemsSource = new List<string> { "PREBUILT", "DIY" };
            TypeComboBox.SelectedIndex = 0;
        }

        private async void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedType = TypeComboBox.SelectedItem as string;
            _categories = await _productManager.GetCategoriesByTypeAsync(_selectedType == "PREBUILT" ? "system" : "component");
            CategoryComboBox.ItemsSource = _categories;
            CategoryComboBox.SelectedIndex = 0;
        }

        private async void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedCategory = CategoryComboBox.SelectedItem as ProductCategory;
            if (_selectedCategory != null)
            {
                _products = await _productManager.GetProductsByCategoryAsync(_selectedCategory.categoryId);
                ProductsGrid.ItemsSource = _products;
            }
        }

        private async void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEditProductDialog(_categories, null);
            if (dialog.ShowDialog() == true)
            {
                var product = dialog.Product;
                var currentUser = SessionManager.LoggedInUser;
                bool result = await _productManager.AddProductAsync(product, currentUser);
                if (result)
                {
                    MessageBox.Show("Produs adăugat cu succes!");
                    CategoryComboBox_SelectionChanged(null, null);
                }
                else
                {
                    MessageBox.Show("Eroare la adăugarea produsului.");
                }
            }
        }

        private async void BtnEditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem is Product selected)
            {
                var dialog = new AddEditProductDialog(_categories, selected);
                if (dialog.ShowDialog() == true)
                {
                    var product = dialog.Product;
                    var currentUser = SessionManager.LoggedInUser;
                    bool result = await _productManager.UpdateProductAsync(product, currentUser);
                    if (result)
                    {
                        MessageBox.Show("Produs modificat cu succes!");
                        CategoryComboBox_SelectionChanged(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Eroare la modificarea produsului.");
                    }
                }
            }
        }

        private async void BtnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem is Product selected)
            {
                var currentUser = SessionManager.LoggedInUser;
                var confirm = MessageBox.Show("Sigur vrei să ștergi acest produs?", "Confirmare", MessageBoxButton.YesNo);
                if (confirm == MessageBoxResult.Yes)
                {
                    bool result = await _productManager.DeleteProductAsync(selected.productId, currentUser);
                    if (result)
                    {
                        MessageBox.Show("Produs șters cu succes!");
                        CategoryComboBox_SelectionChanged(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Eroare la ștergerea produsului.");
                    }
                }
            }
        }

        private async void BtnSetActive_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem is Product selected)
            {
                var currentUser = SessionManager.LoggedInUser;
                selected.isActive = !selected.isActive;
                bool result = await _productManager.UpdateProductAsync(selected, currentUser);
                if (result)
                {
                    MessageBox.Show("Statusul produsului a fost actualizat!");
                    CategoryComboBox_SelectionChanged(null, null);
                }
                else
                {
                    MessageBox.Show("Eroare la actualizarea statusului.");
                }
            }
        }

        private async void BtnAssignPromotion_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem is Product selected)
            {
                var dialog = new AssignPromotionDialog(_promotionManager);
                if (dialog.ShowDialog() == true)
                {
                    selected.hasPromotion = true;
                    selected.promotionId = dialog.SelectedPromotionId;
                    var currentUser = SessionManager.LoggedInUser;
                    bool result = await _productManager.UpdateProductAsync(selected, currentUser);

                    // --- Update Promotion's includedProductIds ---
                    if (result)
                    {
                        // Get the promotion
                        var promotion = (await _promotionManager.GetAllPromotionsAsync())
                            .FirstOrDefault(p => p.promotionId == dialog.SelectedPromotionId);
                        if (promotion != null)
                        {
                            // Ensure the includedProductIds list is initialized
                            if (promotion.includedProductIds == null)
                                promotion.includedProductIds = new Dictionary<string, bool>();

                            if (!promotion.includedProductIds.ContainsKey(selected.productId))
                            {
                                promotion.includedProductIds[selected.productId] = true;
                                await _promotionManager.UpdatePromotionAsync(promotion, currentUser);
                            }
                        }
                        MessageBox.Show("Promoție asignată!");
                        CategoryComboBox_SelectionChanged(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Eroare la asignarea promoției.");
                    }
                }
            }
        }
        private async void BtnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEditCategoryDialog(_selectedType == "PREBUILT" ? "system" : "component");
            if (dialog.ShowDialog() == true)
            {
                var category = dialog.Category;
                var currentUser = SessionManager.LoggedInUser;
                bool result = await _productManager.AddProductCategoryAsync(category, currentUser);
                if (result)
                {
                    MessageBox.Show("Categorie adăugată cu succes!");
                    // Reload categories
                    _categories = await _productManager.GetCategoriesByTypeAsync(_selectedType == "PREBUILT" ? "system" : "component");
                    CategoryComboBox.ItemsSource = _categories;
                }
                else
                {
                    MessageBox.Show("Eroare la adăugarea categoriei.");
                }
            }
        }
    }
}