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
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        private readonly CartManagerBLL _cartManager;
        private readonly ProductManagerBLL _productManager;
        private readonly OrderManagerBLL _orderManager;
        private Dictionary<string, SelectedProduct> cartProducts = new();

        public CartWindow(CartManagerBLL cartManager, ProductManagerBLL productManager, OrderManagerBLL orderManager)
        {
            InitializeComponent();
            _cartManager = cartManager;
            _productManager = productManager;
            _orderManager = orderManager;
            LoadCart();
        }
        private async void LoadCart()
        {
            var user = SessionManager.LoggedInUser;
            if (user == null)
            {
                MessageBox.Show("Trebuie să fii autentificat.");
                return;
            }

            var cart = await _cartManager.GetUserCartAsync(user.userName);
            cartProducts.Clear();
            decimal total = 0;

            var displayItems = new List<object>();

            foreach (var kvp in cart)
            {
                var product = await _productManager.GetProductByIdAsync(kvp.Key);
                if (product != null)
                {
                    var subtotal = (decimal)product.price * kvp.Value.quantity;
                    cartProducts[kvp.Key] = kvp.Value;

                    displayItems.Add(new
                    {
                        ProductId = product.productId,
                        Name = product.name,
                        Quantity = kvp.Value.quantity,
                        Price = product.price,
                        Subtotal = subtotal
                    });

                    total += subtotal;
                }
            }

            CartGrid.ItemsSource = displayItems;
            TxtTotal.Text = $"{total} lei";
        }
        private async void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var productId = button?.Tag?.ToString();
            if (!string.IsNullOrEmpty(productId))
            {
                await _cartManager.RemoveProductFromCartAsync(SessionManager.LoggedInUser.userName, productId);
                LoadCart();
            }
        }
        private async void BtnPlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            var user = SessionManager.LoggedInUser;
            if (user == null)
            {
                MessageBox.Show("Trebuie să fii autentificat.");
                return;
            }

            if (!cartProducts.Any())
            {
                MessageBox.Show("Coșul este gol.");
                return;
            }

            var orderItems = new List<OrderItem>();

            foreach (var kvp in cartProducts)
            {
                var product = await _productManager.GetProductByIdAsync(kvp.Key);
                if (product != null)
                {
                    orderItems.Add(new OrderItem
                    {
                        ProductId = product.productId,
                        Quantity = kvp.Value.quantity,
                        Price = (decimal)product.price,
                        IsPromotion = false
                    });
                }
            }

            var order = new Order
            {
                ClientUserName = user.userName,
                OrderItems = orderItems,
                OrderDate = DateTime.Now,
                TotalCost = orderItems.Sum(i => i.Price * i.Quantity),
                OrderStatus = "Pending",
                OrderType = "Cumpărare"
            };

            var result = await _orderManager.PlaceOrderAsync(order, user);
            if (result)
            {
                await _cartManager.ClearCartAsync(user.userName);
                MessageBox.Show("Comanda a fost plasată cu succes!", "Succes");
                LoadCart();
            }
            else
            {
                MessageBox.Show("Eroare la plasarea comenzii.", "Eroare");
            }
        }

        private async void BtnAddToCart_Click(object sender, RoutedEventArgs e)
        {
           var user = SessionManager.LoggedInUser;
            if (user == null)
            {
                MessageBox.Show("Trebuie să fii autentificat.");
                return;
            }

            string productId = "PRODUS_EXEMPLU"; // înlocuiește cu ID real
            int quantity = 1;

            var cart = await _cartManager.GetUserCartAsync(user.userName);
            if (cart.ContainsKey(productId))
            {
                // Dacă produsul e deja în coș, actualizează cantitatea
                int newQuantity = cart[productId].quantity + quantity;
                await _cartManager.AddProductToCartAsync(user.userName, productId, newQuantity);
            }
            else
            {
                // Dacă nu există, adaugă cu cantitatea specificată
                await _cartManager.AddProductToCartAsync(user.userName, productId, quantity);
            }

            MessageBox.Show("Produs adăugat/actualizat în coș.");
            LoadCart();
        }

        private async void BtnRemoveSelected_Click(object sender, RoutedEventArgs e)
        {
            var user = SessionManager.LoggedInUser;
            if (user == null)
            {
                MessageBox.Show("Trebuie să fii autentificat.");
                return;
            }

            dynamic selectedItem = CartGrid.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Selectează un produs din coș.");
                return;
            }

            string productId = selectedItem.ProductId;
            var cart = await _cartManager.GetUserCartAsync(user.userName);
            if (!cart.ContainsKey(productId))
            {
                MessageBox.Show("Produsul nu există în coș.");
                return;
            }

            await _cartManager.RemoveProductFromCartAsync(user.userName, productId);
            MessageBox.Show("Produs eliminat din coș.");
            LoadCart();
        }
    }
}
