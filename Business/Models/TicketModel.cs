using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class TicketModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The code is too long")]
        public string Code { get; set; } = string.Empty;
        public decimal? Price { get; set; }

        [StringLength(50, ErrorMessage = "The type name is too long")]
        public string? Type { get; set; }

        [Required]
        public int PassengerId { get; set; }

        [Required]
        public int FlightId { get; set; }
    }
}
