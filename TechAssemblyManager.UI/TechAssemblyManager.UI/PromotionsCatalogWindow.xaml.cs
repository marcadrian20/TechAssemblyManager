using System;
using System.Linq;
using System.Windows;
using TechAssemblyManager.BLL;
using TechAssemblyManager.DAL.FirebaseHelper;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    public partial class PromotionsCatalogWindow : Window
    {
        private readonly PromotionManagerBLL _promotionManager;
        private readonly ProductManagerBLL _productManager;
        public PromotionsCatalogWindow(PromotionManagerBLL promotionManager, ProductManagerBLL productManager)
        {
            InitializeComponent();
            _promotionManager = promotionManager;
            _productManager = productManager;
            LoadPromotions();
        }

        private async void LoadPromotions()
        {
            var all = await _promotionManager.GetAllPromotionsAsync();
            var now = DateTime.UtcNow;
            var active = all.Where(p =>
                p.isActive &&
                DateTime.TryParse(p.startDate, out var start) &&
                DateTime.TryParse(p.endDate, out var end) &&
                now >= start && now <= end
            ).ToList();
            PromotionsGrid.ItemsSource = active;
        }
        private async void PromotionsGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (PromotionsGrid.SelectedItem is Promotion selectedPromotion)
            {
                var products = new List<Product>();
                foreach (var prodId in selectedPromotion.includedProductIds.Keys)
                {
                    var product = await _productManager.GetProductByIdAsync(prodId);
                    if (product != null)
                        products.Add(product);
                }
                PromotionProductsGrid.ItemsSource = products;
            }
            else
            {
                PromotionProductsGrid.ItemsSource = null;
            }
        }
        private async void BtnAddPromotionToCart_Click(object sender, RoutedEventArgs e)
        {
            if (PromotionsGrid.SelectedItem is Promotion selectedPromotion)
            {
                var user = SessionManager.LoggedInUser;
                if (user == null)
                {
                    MessageBox.Show("Trebuie să fii autentificat pentru a adăuga o promoție în coș.");
                    return;
                }

                var result = await _promotionManager.AddPromotionToCartAsync(user.userName, selectedPromotion.promotionId);
                if (result)
                    MessageBox.Show("Promoția și produsele incluse au fost adăugate în coș!");
                else
                    MessageBox.Show("Eroare la adăugarea promoției în coș.");
            }
            else
            {
                MessageBox.Show("Selectează o promoție.");
            }
        }
    }
}