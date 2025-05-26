using System.Collections.Generic;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
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
                TxtRating.Text = product.rating.ToString("0.0");
                CategoryComboBox.SelectedValue = product.categoryId;
                ChkActive.IsChecked = product.isActive;
            }
            else
            {
                TxtRating.Text = "0";
            }
            TxtRating.TextChanged += (s, e) => UpdateStars();

            UpdateStars();
        }
        private void UpdateStars()
        {
            StarPanel.Children.Clear();

            if (!double.TryParse(TxtRating.Text, out double rating))
                rating = 0;

            int fullStars = (int)rating;
            bool halfStar = (rating - fullStars) >= 0.5;

            for (int i = 1; i <= 5; i++)
            {
                Path star = CreateStarShape();
                if (i <= fullStars)
                    star.Fill = Brushes.Gold;
                else if (i == fullStars + 1 && halfStar)
                    star.Fill = new LinearGradientBrush(Colors.Gold, Colors.Gray, 0);
                else
                    star.Fill = Brushes.Gray;

                int starValue = i;
                star.MouseLeftButtonDown += (s, e) =>
                {
                    TxtRating.Text = starValue.ToString();
                };

                StarPanel.Children.Add(star);
            }
        }

        private Path CreateStarShape()
        {
            return new Path
            {
                Width = 20,
                Height = 20,
                Margin = new Thickness(2, 0, 2, 0),
                Data = Geometry.Parse("M10,1 L12,7 H18 L13,11 L15,17 L10,13 L5,17 L7,11 L2,7 H8 Z"),
                Stroke = Brushes.Black,
                StrokeThickness = 0.5
            };
        }
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtId.Text) ||
                string.IsNullOrWhiteSpace(TxtName.Text) ||
                string.IsNullOrWhiteSpace(TxtPrice.Text) ||
                string.IsNullOrWhiteSpace(TxtRating.Text) ||
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
                rating = double.TryParse(TxtRating.Text, out var rating) ? rating : 0,
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