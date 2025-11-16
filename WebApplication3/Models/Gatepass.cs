using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Gatepass
    {
        [Key]
        public int Id { get; set; }

        // User Information (Auto-filled)
        public string UserId { get; set; } // FK to ApplicationUser
        public string Name { get; set; }
        public string CustomId { get; set; } // StudentId or FacultyId

        // Application Details
        [Required]
        public string SchoolYear { get; set; } // e.g., "2024-2025"

        [Required]
        public string Address { get; set; }

        [Required]
        public DateTime ApplicationDate { get; set; }

        // User Type (Non-Teaching Staff or Faculty)
        [Required]
        public string UserType { get; set; } // "Faculty" or "Non-Teaching Staff"

        // Faculty/Staff specific
        public string? Department { get; set; }

        // Student specific fields
        public string? Course { get; set; }
        public int? Year { get; set; }

        // Vehicle Information
        [Required]
        public string VehiclePlateNo { get; set; }

        [Required]
        public DateTime RegistrationExpiryDate { get; set; }

        [Required]
        public string VehicleType { get; set; } // e.g., Car, Motorcycle, etc.

        [Required]
        public string Maker { get; set; } // Vehicle brand/manufacturer

        [Required]
        public string Color { get; set; }

        // Document Attachments (file paths)
        public string? StudyLoadAttachment { get; set; }
        public string? RegistrationAttachment { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.Now;

        // Approval status
        public string Status { get; set; } = "Pending"; // Pending, Approved, Denied
        public string? AdminRemarks { get; set; }
        public DateTime? ApprovalDate { get; set; }

        // Navigation property
        public virtual ApplicationUser User { get; set; }
    }
}