using System.Windows;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    public partial class AddEditCategoryDialog : Window
    {
        public ProductCategory Category { get; private set; }
        private readonly string _type;

        public AddEditCategoryDialog(string type)
        {
            InitializeComponent();
            _type = type;
            TypeTextBlock.Text = _type == "system" ? "PREBUILT" : "DIY";
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtId.Text) ||
                string.IsNullOrWhiteSpace(TxtName.Text))
            {
                MessageBox.Show("Completează toate câmpurile obligatorii!");
                return;
            }

            Category = new ProductCategory
            {
                categoryId = TxtId.Text.Trim(),
                name = TxtName.Text.Trim(),
                type = _type,
                description = TxtDescription.Text.Trim()
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