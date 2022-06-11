using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class BaggageModel
    {
        public int Id { get; set; }

        [Required]
        public decimal Volume { get; set; }

        [Required]
        public decimal Weight { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int TicketId { get; set; }
    }
}
