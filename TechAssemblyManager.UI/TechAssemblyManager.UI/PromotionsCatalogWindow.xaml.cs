using System;
using System.Linq;
using System.Windows;
using TechAssemblyManager.BLL;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    public partial class PromotionsCatalogWindow : Window
    {
        private readonly PromotionManagerBLL _promotionManager;

        public PromotionsCatalogWindow(PromotionManagerBLL promotionManager)
        {
            InitializeComponent();
            _promotionManager = promotionManager;
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
    }
}