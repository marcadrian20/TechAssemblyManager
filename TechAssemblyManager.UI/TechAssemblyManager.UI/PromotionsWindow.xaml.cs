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
    /// Interaction logic for PromotionsWindow.xaml
    /// </summary>
    public partial class PromotionsWindow : Window
    {
        private readonly ProductManagerBLL _productManager;
        private readonly FirebaseHelper _firebaseHelper;
        public PromotionsWindow(ProductManagerBLL productManager)
        {
            InitializeComponent();
            _productManager = productManager;

            // extragem helperul din BLL (într-un workaround ușor, temporar)
            _firebaseHelper = typeof(ProductManagerBLL)
                .GetField("_firebaseHelper", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?
                .GetValue(_productManager) as FirebaseHelper;

            if (SessionManager.LoggedInUser?.userType != "manager")
            {
                MessageBox.Show("Această secțiune este disponibilă doar managerilor.");
                Close();
            }

            LoadPromotions();
        }
        private async void LoadPromotions()
        {
            var list = await _firebaseHelper.GetAllPromotionsAsync();
            PromotionsGrid.ItemsSource = list;
        }
        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var id = Guid.NewGuid().ToString().Substring(0, 8);
            var user = SessionManager.LoggedInUser;

            var newPromo = new Promotion
            {
                promotionId = id,
                name = $"Promo {DateTime.Now:ddMMyy_HHmm}",
                description = "Reducere de test",
                discountPercentage = 10,
                startDate = DateTime.Now.ToShortDateString(),
                endDate = DateTime.Now.AddDays(7).ToShortDateString(),
                createdBy = user.userName,
                isActive = true
            };

            bool success = await _firebaseHelper.AddPromotionAsync(newPromo);
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
                await _firebaseHelper.DeleteAsync($"Promotions/{selected.promotionId}");
                MessageBox.Show("Promoția a fost ștearsă.");
                LoadPromotions();
            }
            else
            {
                MessageBox.Show("Selectează o promoție pentru a o șterge.");
            }
        }
    }
}
