using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        // For Students
        [Display(Name = "Student ID")]
        public string? StudentNumber { get; set; }

        [Display(Name = "Program")]
        public string? Program { get; set; }

        [Display(Name = "Year Level")]
        [Range(1, 4)]
        public int? YearLevel { get; set; }

        // For Faculty
        [Display(Name = "Employee ID")]
        public string? EmployeeId { get; set; }

        [Display(Name = "Department")]
        public string? Department { get; set; }

        // Common
        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";
    }
}