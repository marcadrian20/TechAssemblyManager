using System;
using System.Windows;
using TechAssemblyManager.DAL.FirebaseHelper;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    public partial class AddEditPromotionDialog : Window
    {
        public Promotion Promotion { get; private set; }
        private bool _isEditMode = false;

        public AddEditPromotionDialog(Promotion promo = null)
        {
            InitializeComponent();
            
            if (promo != null)
            {
                _isEditMode = true;
                Title = "Editează Promoție";
                
                promotionIdTextBox.Text = promo.promotionId;
                promotionIdTextBox.IsReadOnly = true; // Don't allow ID changes in edit mode
                promotionNameTextBox.Text = promo.name;
                promotionDescriptionTextBox.Text = promo.description;
                discountPercentageTextBox.Text = promo.discountPercentage.ToString();
                promotionIsActiveCheckBox.IsChecked = promo.isActive;
                
                if (DateTime.TryParse(promo.startDate, out var start))
                    promotionStartDatePicker.SelectedDate = start;
                if (DateTime.TryParse(promo.endDate, out var end))
                    promotionEndDatePicker.SelectedDate = end;
            }
            else
            {
                Title = "Adaugă Promoție";
                // Generate a unique ID for new promotions
                promotionIdTextBox.Text = "PROMO_" + Guid.NewGuid().ToString("N")[..8].ToUpper();
            }
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(promotionIdTextBox.Text) ||
                string.IsNullOrWhiteSpace(promotionNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(discountPercentageTextBox.Text) ||
                !promotionStartDatePicker.SelectedDate.HasValue ||
                !promotionEndDatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Completează toate câmpurile obligatorii!");
                return;
            }

            if (!float.TryParse(discountPercentageTextBox.Text, out var discount) || discount <= 0 || discount > 100)
            {
                MessageBox.Show("Reducerea trebuie să fie un număr valid între 1 și 100!");
                return;
            }

            // Date validation
            if (promotionStartDatePicker.SelectedDate >= promotionEndDatePicker.SelectedDate)
            {
                MessageBox.Show("Data de început trebuie să fie înainte de data de sfârșit!");
                return;
            }

            Promotion = new Promotion
            {
                promotionId = promotionIdTextBox.Text.Trim(),
                isActive = promotionIsActiveCheckBox.IsChecked == true,
                discountPercentage = discount,
                startDate = promotionStartDatePicker.SelectedDate.Value.ToString("MM/dd/yyyy"),
                endDate = promotionEndDatePicker.SelectedDate.Value.ToString("MM/dd/yyyy"),
                name = promotionNameTextBox.Text.Trim(),
                createdBy = SessionManager.LoggedInUser?.userName ?? "",
                description = promotionDescriptionTextBox.Text?.Trim() ?? ""
            };
            
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