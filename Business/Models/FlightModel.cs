using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class FlightModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The destination name is too long")]
        public string To { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int PlaneId { get; set; }

        [Required]
        public int TerminalId { get; set; }
    }
}
