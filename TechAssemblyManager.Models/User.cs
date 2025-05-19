namespace TechAssemblyManager.Models
{
    public class User
    {
        public string createdAt { get; set; }
        public CustomerData customerData { get; set; } = new CustomerData();
        public string email { get; set; }
        public EmployeeData employeeData { get; set; } = new EmployeeData();
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string passwordHash { get; set; }
        public string userName { get; set; }
        public string userType { get; set; }// "manager", "employee", "customer"
        public Dictionary<string, SelectedProduct> selectedProducts { get; set; } = new Dictionary<string, SelectedProduct>();
    }
}
