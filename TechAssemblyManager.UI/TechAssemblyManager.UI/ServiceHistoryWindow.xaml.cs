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
    /// Interaction logic for ServiceHistoryWindow.xaml
    /// </summary>
    public partial class ServiceHistoryWindow : Window
    {
        private readonly OrderManagerBLL _orderManager;
        public ServiceHistoryWindow(OrderManagerBLL orderManager)
        {
            InitializeComponent();
            _orderManager = orderManager;
            LoadServiceHistory();
        }
        private async void LoadServiceHistory()
        {
            var user = SessionManager.LoggedInUser;
            if (user == null || user.userType != "customer")
            {
                MessageBox.Show("Trebuie să fii logat ca și client.");
                this.Close();
                return;
            }

            List<ServiceRequest> requests = await _orderManager.GetServiceRequestsByClientAsync(user.userName);
            ServiceGrid.ItemsSource = requests;
        }
    }
}
