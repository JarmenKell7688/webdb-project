using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class LockerRequest
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Student ID")]
        public int StudentId { get; set; }

        public Student? Student { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "ID Number")]
        public string IdNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Locker Number")]
        public string LockerNumber { get; set; } = string.Empty;

        [Required]
        public string Semester { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Contact Number")]
        [Phone]
        public string ContactNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Application Date")]
        [DataType(DataType.Date)]
        public DateTime ApplicationDate { get; set; } = DateTime.Now;

        [Required]
        public string Status { get; set; } = "Pending";
    }
}