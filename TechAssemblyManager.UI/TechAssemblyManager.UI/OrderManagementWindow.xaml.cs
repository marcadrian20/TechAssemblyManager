using System.Windows;
using System.Windows.Controls;
using TechAssemblyManager.BLL;
using TechAssemblyManager.Models;
using System.Collections.Generic;
using TechAssemblyManager.DAL.FirebaseHelper;

namespace TechAssemblyManager.UI
{
    public partial class OrderManagementWindow : Window
    {
        private readonly OrderManagerBLL _orderManager;
        private string _currentMode = "Comenzi"; // or "Cereri Service"

        public OrderManagementWindow(OrderManagerBLL orderManager)
        {
            InitializeComponent();
            _orderManager = orderManager;
            ManagementTypeComboBox.SelectedIndex = 0; // Default to orders
        }

        private async void ManagementTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currentMode = (ManagementTypeComboBox.SelectedItem as ComboBoxItem)?.Content as string ?? "Comenzi";
            StatusComboBox.ItemsSource = _currentMode == "Comenzi"
                ? new[] { "Placed", "Processing", "Completed", "Cancelled" }
                : new[] { "Requested", "InProgress", "Completed", "Cancelled" };
            await LoadData();
        }

        private async System.Threading.Tasks.Task LoadData()
        {
            ManagementGrid.Columns.Clear();
            if (_currentMode == "Comenzi")
            {
                var orders = await _orderManager.GetAllOrdersAsync();
                ManagementGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new System.Windows.Data.Binding("OrderId") });
                ManagementGrid.Columns.Add(new DataGridTextColumn { Header = "Client", Binding = new System.Windows.Data.Binding("ClientUserName") });
                ManagementGrid.Columns.Add(new DataGridTextColumn { Header = "Data", Binding = new System.Windows.Data.Binding("OrderDate") { StringFormat = "dd.MM.yyyy HH:mm" } });
                ManagementGrid.Columns.Add(new DataGridTextColumn { Header = "Total", Binding = new System.Windows.Data.Binding("TotalCost") });
                ManagementGrid.Columns.Add(new DataGridTextColumn { Header = "Tip", Binding = new System.Windows.Data.Binding("OrderType") });
                ManagementGrid.Columns.Add(new DataGridTextColumn { Header = "Status", Binding = new System.Windows.Data.Binding("OrderStatus") });
                ManagementGrid.Columns.Add(new DataGridTextColumn { Header = "Descriere", Binding = new System.Windows.Data.Binding("OrderDescription") });
                ManagementGrid.Columns.Add(new DataGridTextColumn { Header = "Finalizare", Binding = new System.Windows.Data.Binding("CompletionDate") { StringFormat = "dd.MM.yyyy HH:mm" } });
                ManagementGrid.ItemsSource = orders;
            }
            else
            {
                var requests = await _orderManager.GetAllServiceRequestsAsync();
                ManagementGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new System.Windows.Data.Binding("ServiceRequestId") });
                ManagementGrid.Columns.Add(new DataGridTextColumn { Header = "Client", Binding = new System.Windows.Data.Binding("CustomerUserName") });
                ManagementGrid.Columns.Add(new DataGridTextColumn { Header = "Data Cerere", Binding = new System.Windows.Data.Binding("RequestDate") { StringFormat = "dd.MM.yyyy HH:mm" } });
                ManagementGrid.Columns.Add(new DataGridTextColumn { Header = "Status", Binding = new System.Windows.Data.Binding("Status") });
                ManagementGrid.Columns.Add(new DataGridTextColumn { Header = "Descriere Problemă", Binding = new System.Windows.Data.Binding("ProblemDescription") });
                ManagementGrid.Columns.Add(new DataGridTextColumn { Header = "Data Programare", Binding = new System.Windows.Data.Binding("ScheduledDate") { StringFormat = "dd.MM.yyyy HH:mm" } });
                ManagementGrid.Columns.Add(new DataGridTextColumn { Header = "Diagnostic", Binding = new System.Windows.Data.Binding("DiagnosisNotes") });
                ManagementGrid.Columns.Add(new DataGridTextColumn { Header = "Taxă Service", Binding = new System.Windows.Data.Binding("ServiceFee") });
                ManagementGrid.ItemsSource = requests;
            }
        }

        private void BtnReload_Click(object sender, RoutedEventArgs e)
        {
            _ = LoadData();
        }

        private void ManagementGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_currentMode == "Comenzi" && ManagementGrid.SelectedItem is Order selectedOrder)
            {
                DescriptionTextBox.Text = selectedOrder.OrderDescription;
            }
            else if (_currentMode == "Cereri Service" && ManagementGrid.SelectedItem is ServiceRequest selectedRequest)
            {
                DescriptionTextBox.Text = selectedRequest.ProblemDescription;
            }
        }

        private async void BtnChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            var user = SessionManager.LoggedInUser;
            if (user == null || user.userType != "employee")
            {
                MessageBox.Show("Doar angajații pot schimba statusul.");
                return;
            }

            if (_currentMode == "Comenzi" && ManagementGrid.SelectedItem is Order selectedOrder && StatusComboBox.SelectedItem is string newStatus)
            {
                selectedOrder.OrderDescription = DescriptionTextBox.Text;
                bool result = await _orderManager.UpdateOrderDetailsAsync(selectedOrder, newStatus, user);
                if (result)
                {
                    MessageBox.Show("Comanda a fost actualizată.");
                    await LoadData();
                }
                else
                {
                    MessageBox.Show("Eroare la actualizarea comenzii.");
                }
            }
            else if (_currentMode == "Cereri Service" && ManagementGrid.SelectedItem is ServiceRequest selectedRequest && StatusComboBox.SelectedItem is string newStatusSR)
            {
                selectedRequest.ProblemDescription = DescriptionTextBox.Text;
                bool result = await _orderManager.UpdateServiceRequestStatusAsync(selectedRequest.ServiceRequestId, newStatusSR, user);
                if (result)
                {
                    MessageBox.Show("Cererea de service a fost actualizată.");
                    await LoadData();
                }
                else
                {
                    MessageBox.Show("Eroare la actualizarea cererii de service.");
                }
            }
            else
            {
                MessageBox.Show("Selectează un element și un status nou.");
            }
        }
    }
}