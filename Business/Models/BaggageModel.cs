using Business.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class BaggageModel
    {
        public int Id { get; set; }

        [Required]
        [GreaterThanZeroDecimal]
        public decimal Volume { get; set; }

        [Required]
        [GreaterThanZeroDecimal]
        public decimal Weight { get; set; }

        [Required]
        [Range(0, double.PositiveInfinity, ErrorMessage = "The value should be positive or equal 0")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Ticket Id")]
        public int TicketId { get; set; }
    }
}
