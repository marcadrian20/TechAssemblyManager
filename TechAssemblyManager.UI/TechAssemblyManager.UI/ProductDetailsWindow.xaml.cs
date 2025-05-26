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
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    /// <summary>
    /// Interaction logic for ProductDetailsWindow.xaml
    /// </summary>
    public partial class ProductDetailsWindow : Window
    {
        public ProductDetailsWindow(CatalogProductViewModel product)
        {
            InitializeComponent();
            DataContext = product;
        }
        private void StarPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is StackPanel starPanel && DataContext is CatalogProductViewModel product)
            {
                var stars = starPanel.Children.OfType<Path>().ToList();
                double rating = product.Rating;

                for (int i = 0; i < stars.Count; i++)
                {
                    double starValue = rating - i;

                    if (starValue >= 1)
                        stars[i].Fill = Brushes.Gold;
                    else if (starValue >= 0.5)
                        stars[i].Fill = new LinearGradientBrush(Colors.Gold, Colors.Gray, 0);
                    else
                        stars[i].Fill = Brushes.Gray;
                }
            }
        }

    }
}
