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
using TechAssemblyManager.DAL.FirebaseHelper;
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
    private PromotionManagerBLL _promotionManager;
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
        _promotionManager = new PromotionManagerBLL(_firebaseHelper);
        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        UpdateDashboard();
    }

    public void UpdateDashboard()
    {
        var user = SessionManager.LoggedInUser;

        // Default: hide all except catalog, login, promotions
        BtnLogin.Visibility = Visibility.Visible;
        BtnRegister.Visibility = Visibility.Visible;
        BtnCatalog.Visibility = Visibility.Visible;
        BtnPromotions.Visibility = Visibility.Visible;
        BtnMyAccount.Visibility = Visibility.Collapsed;
        BtnCart.Visibility = Visibility.Collapsed;
        BtnServiceRequest.Visibility = Visibility.Collapsed;
        BtnServiceHistory.Visibility = Visibility.Collapsed;
        BtnEmployeeManagement.Visibility = Visibility.Collapsed;
        BtnProductManagement.Visibility = Visibility.Collapsed;
        BtnOrderManagement.Visibility = Visibility.Collapsed;
        BtnDebug.Visibility = Visibility.Collapsed;

        // If logged in, show/hide based on role
        if (user != null)
        {
            BtnLogin.Visibility = Visibility.Collapsed;
            BtnMyAccount.Visibility = Visibility.Visible;
            BtnRegister.Visibility = Visibility.Collapsed;
            if (_userManagerBLL.IsCustomer(user))
            {
                BtnCart.Visibility = Visibility.Visible;
                BtnServiceRequest.Visibility = Visibility.Visible;
                BtnServiceHistory.Visibility = Visibility.Visible;
            }
            else if (_userManagerBLL.IsManager(user))
            {
                BtnEmployeeManagement.Visibility = Visibility.Visible;
                // BtnProductManagement.Visibility = Visibility.Visible;
                // BtnOrderManagement.Visibility = Visibility.Visible;
                BtnDebug.Visibility = Visibility.Visible;
            }
            else if (_userManagerBLL.IsSenior(user))
            {
                BtnProductManagement.Visibility = Visibility.Visible;
                BtnOrderManagement.Visibility = Visibility.Visible;
            }
            else if (_userManagerBLL.IsJunior(user))
            {
                BtnOrderManagement.Visibility = Visibility.Visible;
            }
        }
    }
    private void BtnLogin_Click(object sender, RoutedEventArgs e)
    {
        new LoginWindow(_userManagerBLL).Show();
    }
    private void BtnRegister_Click(object sender, RoutedEventArgs e)
    {
        new RegisterWindow(_userManagerBLL).Show();
    }
    private void BtnCatalog_Click(object sender, RoutedEventArgs e)
    {
        new CatalogWindow(_productManager, _cartManager, _orderManager).Show();
    }

    private void BtnPromotions_Click(object sender, RoutedEventArgs e)
    {
        var user = SessionManager.LoggedInUser;
        if (user != null && user.userType == "manager")
        {
            // Manager: open management window
            new PromotionsWindow(_promotionManager).Show();
        }
        else
        {
            // Customer/employee/guest: open catalog window
            new PromotionsCatalogWindow(_promotionManager, _productManager).Show();
        }
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
        new OrderAndServiceHistoryWindow(_orderManager).Show();
    }

    private void BtnCart_Click(object sender, RoutedEventArgs e)
    {
        new CartWindow(_cartManager, _productManager, _orderManager).Show();
    }


    // Add click handlers for new buttons
    private void BtnEmployeeManagement_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Replace with your actual EmployeeManagementWindow
        // new EmployeeManagementWindow(_userManagerBLL).Show();
        new EmployeeManagementWindow(_userManagerBLL).Show();
    }

    private void BtnProductManagement_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Replace with your actual ProductManagementWindow
        new ProductManagementWindow(_productManager, _promotionManager).Show();
        // MessageBox.Show("Gestionare Produse/Componente (manager/senior).");
    }

    private void BtnOrderManagement_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Replace with your actual OrderManagementWindow
        new OrderManagementWindow(_orderManager).Show();
        // MessageBox.Show("Gestionare Comenzi/Service (manager/angajat).");
    }
    private void BtnDebug_Click(object sender, RoutedEventArgs e)
    {
        new DebugWindow(_firebaseHelper).Show();
    }
}