using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class GatePass
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Student ID")]
        public int StudentId { get; set; }

        public Student? Student { get; set; }

        [Required]
        [Display(Name = "Application for School Year")]
        public string SchoolYear { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Application Date")]
        [DataType(DataType.Date)]
        public DateTime ApplicationDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Staff Type")]
        public string StaffType { get; set; } = string.Empty; // "Non-Teaching" or "Faculty"

        [Required]
        public string Department { get; set; } = string.Empty;

        [Display(Name = "Course & Year")]
        public string? CourseAndYear { get; set; }

        [Required]
        [Display(Name = "Vehicle Plate No.")]
        public string VehiclePlateNo { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Registration Expiry Date")]
        [DataType(DataType.Date)]
        public DateTime RegistrationExpiryDate { get; set; }

        [Required]
        [Display(Name = "Vehicle Type")]
        public string VehicleType { get; set; } = string.Empty;

        [Required]
        public string Maker { get; set; } = string.Empty;

        [Required]
        public string Color { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "Pending";
    }
}