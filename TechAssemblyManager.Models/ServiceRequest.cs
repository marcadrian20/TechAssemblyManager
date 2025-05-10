using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechAssemblyManager.Models
{
    public class ServiceRequest
    {
        [Key]
        public int ServiceRequestId { get; set; }
        
        [Required]
        [StringLength(500)]
        public string ProblemDescription { get; set; }
        
        public DateTime RequestDate { get; set; } = DateTime.Now;
        
        public DateTime ScheduledDate { get; set; }
        
        public ServiceStatus Status { get; set; } = ServiceStatus.Requested;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal ServiceFee { get; set; }
        
        [StringLength(1000)]
        public string DiagnosisNotes { get; set; }
        
        public int CustomerId { get; set; }
        
        public int? EmployeeId { get; set; }
        
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        
        [ForeignKey("EmployeeId")]
        public virtual Employee HandledBy { get; set; }
    }
}