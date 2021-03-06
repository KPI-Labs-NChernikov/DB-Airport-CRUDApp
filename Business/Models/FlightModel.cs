using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class FlightModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(100, ErrorMessage = "The destination name is too long")]
        [Display(Name = "Destination")]
        public string To { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Plane")]
        public int PlaneId { get; set; }

        [Required]
        [Display(Name = "Terminal")]
        public int TerminalId { get; set; }
    }
}
