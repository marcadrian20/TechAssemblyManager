using System.Collections.Generic;
using System.Runtime;
using System.Windows;
using TechAssemblyManager.BLL;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    public partial class AssignPromotionDialog : Window
    {
        public string SelectedPromotionId => PromotionComboBox.SelectedValue as string;
        private PromotionManagerBLL promotionManager;
        public AssignPromotionDialog(PromotionManagerBLL promotionManager)
        {
            this.promotionManager = promotionManager;
            InitializeComponent();
            LoadPromotions();
        }

        private async void LoadPromotions()
        {
            var promotions = await promotionManager.GetAllPromotionsAsync();
            PromotionComboBox.ItemsSource = promotions;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (PromotionComboBox.SelectedItem == null)
            {
                MessageBox.Show("Selectează o promoție!");
                return;
            }
            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}