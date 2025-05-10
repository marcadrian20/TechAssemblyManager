using System.ComponentModel.DataAnnotations;

namespace TechAssemblyManager.Models
{
    public abstract class User
    {
        [Key]
        public int UserId { get; set; }
        
        ///USER CREDITENTIALS
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string PasswordHash { get; set; }


        ///USER ROLES
        public string Discriminator { get; set; }
        //public UserType userType { get; set; }

        ///USER INFO
        [Required]
        public DateTime CreatedDate { get; set; }=DateTime.Now;

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
    }

    //public enum UserType
    //{
    //    CLIENT,
    //    MANAGER,
    //    EMPLOYEE,
    //}
}
