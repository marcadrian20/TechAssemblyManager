using System.Windows;
using TechAssemblyManager.BLL;
using TechAssemblyManager.DAL.FirebaseHelper;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    public partial class PromotionsWindow : Window
    {
        private readonly PromotionManagerBLL _promotionManager;

        public PromotionsWindow(PromotionManagerBLL promotionManager)
        {
            InitializeComponent();
            _promotionManager = promotionManager;

            if (SessionManager.LoggedInUser?.userType != "manager")
            {
                MessageBox.Show("Această secțiune este disponibilă doar managerilor.");
                Close();
            }

            LoadPromotions();
        }

        private async void LoadPromotions()
        {
            var list = await _promotionManager.GetAllPromotionsAsync();
            PromotionsGrid.ItemsSource = list;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEditPromotionDialog();
            if (dialog.ShowDialog() == true)
            {
                var promo = dialog.Promotion;
                // promo.promotionId = Guid.NewGuid().ToString().Substring(0, 8);
                var user = SessionManager.LoggedInUser;
                _ = AddPromotionAsync(promo, user);
            }
        }

        private async System.Threading.Tasks.Task AddPromotionAsync(Promotion promo, User user)
        {
            bool success = await _promotionManager.AddPromotionAsync(promo, user);
            if (success)
            {
                MessageBox.Show("Promoția a fost adăugată.");
                LoadPromotions();
            }
            else
            {
                MessageBox.Show("Eroare la adăugare promoție.");
            }
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (PromotionsGrid.SelectedItem is Promotion selected)
            {
                var user = SessionManager.LoggedInUser;
                bool success = await _promotionManager.DeletePromotionAsync(selected.promotionId, user);
                if (success)
                {
                    MessageBox.Show("Promoția a fost ștearsă.");
                    LoadPromotions();
                }
                else
                {
                    MessageBox.Show("Eroare la ștergere promoție.");
                }
            }
            else
            {
                MessageBox.Show("Selectează o promoție pentru a o șterge.");
            }
        }
    }
}