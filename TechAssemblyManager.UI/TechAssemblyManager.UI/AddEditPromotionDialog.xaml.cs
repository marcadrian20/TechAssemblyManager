using System;
using System.Windows;
using TechAssemblyManager.DAL.FirebaseHelper;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    public partial class AddEditPromotionDialog : Window
    {
        public Promotion Promotion { get; private set; }

        public AddEditPromotionDialog(Promotion promo = null)
        {
            InitializeComponent();
            if (promo != null)
            {
                promotionIdTextBox.Text = promo.promotionId;
                promotionNameTextBox.Text = promo.name;
                promotionDescriptionTextBox.Text = promo.description;
                discountPercentageTextBox.Text = promo.discountPercentage.ToString();
                promotionIsActiveCheckBox.IsChecked = promo.isActive;
                if (DateTime.TryParse(promo.startDate, out var start))
                    promotionStartDatePicker.SelectedDate = start;
                if (DateTime.TryParse(promo.endDate, out var end))
                    promotionEndDatePicker.SelectedDate = end;
            }
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(promotionIdTextBox.Text) ||
                string.IsNullOrWhiteSpace(promotionNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(discountPercentageTextBox.Text) ||
                !promotionStartDatePicker.SelectedDate.HasValue ||
                !promotionEndDatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Completează toate câmpurile obligatorii!");
                return;
            }

            if (!float.TryParse(discountPercentageTextBox.Text, out var discount))
            {
                MessageBox.Show("Reducerea trebuie să fie un număr valid!");
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
                description = promotionDescriptionTextBox.Text.Trim()
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