using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Student ID")]
        public int StudentId { get; set; }

        public Student? Student { get; set; }

        [Required]
        [Display(Name = "Application Date")]
        [DataType(DataType.Date)]
        public DateTime ApplicationDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Name of Organization")]
        public string OrganizationName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Activity Title")]
        public string ActivityTitle { get; set; } = string.Empty;

        [Required]
        public string Venue { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Date Needed")]
        [DataType(DataType.Date)]
        public DateTime DateNeeded { get; set; }

        [Required]
        [Display(Name = "Time From")]
        [DataType(DataType.Time)]
        public DateTime TimeFrom { get; set; }

        [Required]
        [Display(Name = "Time To")]
        [DataType(DataType.Time)]
        public DateTime TimeTo { get; set; }

        [Required]
        public string Participants { get; set; } = string.Empty;

        public string? Speaker { get; set; }

        [Required]
        [Display(Name = "Purpose/Objective")]
        public string Purpose { get; set; } = string.Empty;

        [Display(Name = "Equipment & Facilities Needed")]
        public string? EquipmentNeeded { get; set; }

        [Required]
        [Display(Name = "Nature of Activity")]
        public string NatureOfActivity { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Source of Funds")]
        public string SourceOfFunds { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "Pending";

        public string? CreatedBy { get; set; }
    }
}