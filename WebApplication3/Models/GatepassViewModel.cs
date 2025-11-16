using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class GatepassViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Destination")]
        public string Destination { get; set; }

        [Required]
        [Display(Name = "Purpose")]
        public string Purpose { get; set; }

        [Required]
        [Display(Name = "Date Out")]
        [DataType(DataType.DateTime)]
        public DateTime DateOut { get; set; }

        [Required]
        [Display(Name = "Date In")]
        [DataType(DataType.DateTime)]
        public DateTime DateIn { get; set; }

        // Auto-filled fields (read-only in form)
        public string Name { get; set; }
        public string CustomId { get; set; }
        public string? Course { get; set; }
        public int? Year { get; set; }
        public string? Department { get; set; }

        // For display
        public string Status { get; set; }
        public DateTime RequestDate { get; set; }
    }
}