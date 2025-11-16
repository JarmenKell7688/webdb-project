using Microsoft.AspNetCore.Identity;

namespace WebApplication3.Models
{
    // Base user class - inherits from IdentityUser
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfilePicturePath { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Discriminator for user type
        public string UserType { get; set; } // "Student", "Faculty", "Admin"
    }

    // Student-specific properties
    public class Student : ApplicationUser
    {
        public string StudentId { get; set; } // Format: 200000+
        public string Program { get; set; } // Course
        public int Year { get; set; } // 1-4

        public Student()
        {
            UserType = "Student";
        }
    }

    // Faculty-specific properties
    public class Faculty : ApplicationUser
    {
        public string FacultyId { get; set; } // Format: 300000+
        public string Department { get; set; }

        public Faculty()
        {
            UserType = "Faculty";
        }
    }
}