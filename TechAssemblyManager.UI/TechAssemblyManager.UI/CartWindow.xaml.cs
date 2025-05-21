//using FirebaseWrapper;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;
//using TechAssemblyManager.BLL;
//using TechAssemblyManager.DAL.FirebaseHelper;
//using TechAssemblyManager.Models;

//namespace TechAssemblyManager.UI
//{
//    /// <summary>
//    /// Interaction logic for CartWindow.xaml
//    /// </summary>
//    public partial class CartWindow : Window
//    {
//        private readonly CartManagerBLL _cartManager;
//        private readonly ProductManagerBLL _productManager;
//        private readonly OrderManagerBLL _orderManager;
//        private Dictionary<string, SelectedProduct> cartProducts = new();

//        public CartWindow(CartManagerBLL cartManager, ProductManagerBLL productManager, OrderManagerBLL orderManager)
//        {
//            InitializeComponent();
//            _cartManager = cartManager;
//            _productManager = productManager;
//            _orderManager = orderManager;
//            LoadCart();
//        }
//        private async void LoadCart()
//        {
//            var user = SessionManager.LoggedInUser;
//            if (user == null)
//            {
//                MessageBox.Show("Trebuie să fii autentificat.");
//                return;
//            }

//            var cart = await _cartManager.GetUserCartAsync(user.userName);
//            cartProducts.Clear();
//            decimal total = 0;

//            foreach (var kvp in cart)
//            {
//                var product = await _productManager.GetProductByIdAsync(kvp.Key);
//                if (product != null)
//                {
//                    var subtotal = (decimal)product.price * kvp.Value.quantity;
//                    cartProducts.Add(new CartItemViewModel
//                    {
//                        ProductId = product.productId,
//                        Name = product.name,
//                        Quantity = kvp.Value.quantity,
//                        Price = product.price,
//                        Subtotal = subtotal
//                    });
//                    total += subtotal;
//                }
//            }

//            CartGrid.ItemsSource = null;
//            CartGrid.ItemsSource = _cartItems;
//            TxtTotal.Text = $"{total} lei";
//        }
//        private async void BtnRemove_Click(object sender, RoutedEventArgs e)
//        {
//            if ((sender as Button)?.DataContext is CartItemViewModel item)
//            {
//                await _cartManager.RemoveProductFromCartAsync(SessionManager.LoggedInUser.userName, item.ProductId);
//                LoadCart();
//            }
//        }
//        private async void BtnPlaceOrder_Click(object sender, RoutedEventArgs e)
//        {
//            var user = SessionManager.LoggedInUser;
//            if (user == null)
//            {
//                MessageBox.Show("Trebuie să fii autentificat.");
//                return;
//            }

//            if (!_cartItems.Any())
//            {
//                MessageBox.Show("Coșul este gol.");
//                return;
//            }

//            var orderItems = _cartItems.Select(p => new OrderItem
//            {
//                ProductId = p.ProductId,
//                Quantity = p.Quantity,
//                Price = p.Price,
//                IsPromotion = false
//            }).ToList();

//            var order = new Order
//            {
//                ClientUserName = user.userName,
//                OrderItems = orderItems,
//                OrderDate = DateTime.Now,
//                TotalCost = orderItems.Sum(i => i.Price * i.Quantity),
//                OrderStatus = "Pending",
//                OrderType = "Cumpărare"
//            };

//            var result = await _orderManager.PlaceOrderAsync(order, user);
//            if (result)
//            {
//                await _cartManager.ClearCartAsync(user.userName);
//                MessageBox.Show("Comanda a fost plasată cu succes!", "Succes");
//                LoadCart();
//            }
//            else
//            {
//                MessageBox.Show("Eroare la plasarea comenzii.", "Eroare");
//            }
//        }
//    }*/
//}
