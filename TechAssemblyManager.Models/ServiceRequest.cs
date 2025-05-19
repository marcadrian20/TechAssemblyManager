using System;

namespace TechAssemblyManager.Models
{
    public class ServiceRequest
    {
        public string ServiceRequestId { get; set; } 
        public string ProblemDescription { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public DateTime ScheduledDate { get; set; }
        public string Status { get; set; } = "Requested"; // e.g., "Requested", "InProgress", "Completed"
        public decimal ServiceFee { get; set; }
        public string DiagnosisNotes { get; set; }
        public string CustomerUserName { get; set; }
        public string EmployeeUserName { get; set; }
    }
}