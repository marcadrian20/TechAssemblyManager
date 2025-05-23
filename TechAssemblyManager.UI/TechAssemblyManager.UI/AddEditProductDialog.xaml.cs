using System.Collections.Generic;
using System.Windows;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    public partial class AddEditProductDialog : Window
    {
        public Product Product { get; private set; }

        public AddEditProductDialog(List<ProductCategory> categories, Product product)
        {
            InitializeComponent();
            CategoryComboBox.ItemsSource = categories;
            CategoryComboBox.DisplayMemberPath = "name";
            CategoryComboBox.SelectedValuePath = "categoryId";

            if (product != null)
            {
                TxtId.Text = product.productId;
                TxtId.IsEnabled = false;
                TxtName.Text = product.name;
                TxtDescription.Text = product.description;
                TxtPrice.Text = product.price.ToString();
                CategoryComboBox.SelectedValue = product.categoryId;
                ChkActive.IsChecked = product.isActive;
            }
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtId.Text) ||
                string.IsNullOrWhiteSpace(TxtName.Text) ||
                string.IsNullOrWhiteSpace(TxtPrice.Text) ||
                CategoryComboBox.SelectedItem == null)
            {
                MessageBox.Show("Completează toate câmpurile obligatorii!");
                return;
            }

            Product = new Product
            {
                productId = TxtId.Text.Trim(),
                name = TxtName.Text.Trim(),
                description = TxtDescription.Text.Trim(),
                price = float.TryParse(TxtPrice.Text, out var price) ? price : 0,
                categoryId = (CategoryComboBox.SelectedItem as ProductCategory).categoryId,
                isActive = ChkActive.IsChecked == true
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