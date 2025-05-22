using System.Windows;
using System.Windows.Controls;
using TechAssemblyManager.BLL;
using TechAssemblyManager.Models;
using System.Collections.Generic;
using System.Linq;
using TechAssemblyManager.DAL.FirebaseHelper;

namespace TechAssemblyManager.UI
{
    public partial class OrderAndServiceHistoryWindow : Window
    {
        private readonly OrderManagerBLL _orderManager;
        private readonly User _user;

        public OrderAndServiceHistoryWindow(OrderManagerBLL orderManager)
        {
            InitializeComponent();
            _orderManager = orderManager;
            _user = SessionManager.LoggedInUser;
            HistoryTypeComboBox.SelectedIndex = 0; // Default to orders
        }

        private async void HistoryTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HistoryTypeComboBox.SelectedIndex == 0)
            {
                // Orders
                var orders = await _orderManager.GetOrdersByClientAsync(_user.userName);
                HistoryGrid.Columns.Clear();
                HistoryGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new System.Windows.Data.Binding("OrderId") });
                HistoryGrid.Columns.Add(new DataGridTextColumn { Header = "Data", Binding = new System.Windows.Data.Binding("OrderDate") { StringFormat = "dd.MM.yyyy HH:mm" } });
                HistoryGrid.Columns.Add(new DataGridTextColumn { Header = "Total", Binding = new System.Windows.Data.Binding("TotalCost") });
                HistoryGrid.Columns.Add(new DataGridTextColumn { Header = "Tip", Binding = new System.Windows.Data.Binding("OrderType") });
                HistoryGrid.Columns.Add(new DataGridTextColumn { Header = "Status", Binding = new System.Windows.Data.Binding("OrderStatus") });
                HistoryGrid.Columns.Add(new DataGridTextColumn { Header = "Descriere", Binding = new System.Windows.Data.Binding("OrderDescription") });
                HistoryGrid.Columns.Add(new DataGridTextColumn { Header = "Finalizare", Binding = new System.Windows.Data.Binding("CompletionDate") { StringFormat = "dd.MM.yyyy HH:mm" } });
                HistoryGrid.ItemsSource = orders;
            }
            else
            {
                // Service Requests
                var requests = await _orderManager.GetServiceRequestsByClientAsync(_user.userName);
                HistoryGrid.Columns.Clear();
                HistoryGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new System.Windows.Data.Binding("ServiceRequestId") });
                HistoryGrid.Columns.Add(new DataGridTextColumn { Header = "Data Cerere", Binding = new System.Windows.Data.Binding("RequestDate") { StringFormat = "dd.MM.yyyy HH:mm" } });
                HistoryGrid.Columns.Add(new DataGridTextColumn { Header = "Status", Binding = new System.Windows.Data.Binding("Status") });
                HistoryGrid.Columns.Add(new DataGridTextColumn { Header = "Descriere Problemă", Binding = new System.Windows.Data.Binding("ProblemDescription") });
                HistoryGrid.Columns.Add(new DataGridTextColumn { Header = "Data Programare", Binding = new System.Windows.Data.Binding("ScheduledDate") { StringFormat = "dd.MM.yyyy HH:mm" } });
                HistoryGrid.Columns.Add(new DataGridTextColumn { Header = "Diagnostic", Binding = new System.Windows.Data.Binding("DiagnosisNotes") });
                HistoryGrid.Columns.Add(new DataGridTextColumn { Header = "Taxă Service", Binding = new System.Windows.Data.Binding("ServiceFee") });
                HistoryGrid.ItemsSource = requests;
            }
        }
    }
}