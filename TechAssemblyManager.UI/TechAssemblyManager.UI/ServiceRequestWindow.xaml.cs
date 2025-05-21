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
    /// Interaction logic for ServiceRequestWindow.xaml
    /// </summary>
    public partial class ServiceRequestWindow : Window
    {
        private readonly OrderManagerBLL _orderManager;
        public ServiceRequestWindow(OrderManagerBLL orderManager)
        {
            InitializeComponent();
            _orderManager = orderManager;
        }

        private async void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var user = SessionManager.LoggedInUser;
            if (user == null || user.userType != "customer")
            {
                MessageBox.Show("Trebuie să fii autentificat ca și client.");
                return;
            }

            var problemDescription = TxtProblem.Text.Trim();
            var scheduledDate = DatePickerScheduled.SelectedDate;

            if (string.IsNullOrWhiteSpace(problemDescription) || scheduledDate == null)
            {
                LblError.Text = "Completează toate câmpurile!";
                LblError.Visibility = Visibility.Visible;
                return;
            }

            var request = new ServiceRequest
            {
                ProblemDescription = problemDescription,
                ScheduledDate = scheduledDate.Value,
                CustomerUserName = user.userName,
                Status = "Requested",
                RequestDate = DateTime.Now,
                ServiceFee = 0, // Poate fi calculat mai târziu
                DiagnosisNotes = ""
            };

            bool result = await _orderManager.PlaceServiceRequestAsync(request, user);
            if (result)
            {
                MessageBox.Show("Cererea de service a fost trimisă cu succes!", "Succes");
                this.Close();
            }
            else
            {
                MessageBox.Show("A apărut o eroare la trimiterea cererii.", "Eroare");
            }
        }
    }
}
