namespace Data.Models
{
    public partial class Baggage
    {
        public int Id { get; set; }
        public decimal Volume { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public int TicketId { get; set; }

        public virtual Ticket Ticket { get; set; } = null!;
    }
}
