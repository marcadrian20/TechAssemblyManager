using FirebaseWrapper;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TechAssemblyManager.BLL;

namespace TechAssemblyManager.UI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private FirebaseHelper _firebaseHelper;
    private CartManagerBLL _cartManager;
    private OrderManagerBLL _orderManager;
    private ProductManagerBLL _productManager;
    private UserManagerBLL _userManagerBLL;
    private ProductManagementWindow _productManagementWindow;
    private RegisterWindow _registerWindow;
    private CartWindow _cartWindow;
    public MainWindow()
    {
        InitializeComponent();
        _firebaseHelper = new FirebaseHelper(
            "https://techassemblymanager-default-rtdb.firebaseio.com/",
            "ky7wJX7Iu46hjBHWqDJNWjJW19NeYQurX4Z9VeUv",
            "AIzaSyBxq3J01JqE6yonLc9plkzA6c3-Gi1r1eU"
        );
        _cartManager = new CartManagerBLL(_firebaseHelper);
        _orderManager = new OrderManagerBLL(_firebaseHelper);
        _productManager = new ProductManagerBLL(_firebaseHelper);
        _userManagerBLL = new UserManagerBLL(_firebaseHelper);

    }

    private void BtnLogin_Click(object sender, RoutedEventArgs e)
    {
        new LoginWindow(_userManagerBLL).Show();
    }

    private void BtnCatalog_Click(object sender, RoutedEventArgs e)
    {
        new CatalogWindow(_productManager,_cartManager,_userManagerBLL,_productManagementWindow,_orderManager).Show();
    }

    private void BtnPromotions_Click(object sender, RoutedEventArgs e)
    {
        new PromotionsWindow(_productManager).Show();
    }

    private void BtnMyAccount_Click(object sender, RoutedEventArgs e)
    {
        new MyAccountWindow().Show();
    }

    private void BtnServiceRequest_Click(object sender, RoutedEventArgs e)
    {
        new ServiceRequestWindow(_orderManager).Show();
    }

    private void BtnServiceHistory_Click(object sender, RoutedEventArgs e)
    {
        new ServiceHistoryWindow(_orderManager).Show();
    }

    private void BtnCart_Click(object sender, RoutedEventArgs e)
    {
        new CartWindow(_cartManager, _productManager, _orderManager).Show();
    }

    private void BtnRegister_Click(object sender, RoutedEventArgs e)
    {
        new RegisterWindow(_userManagerBLL).Show();
    }
}