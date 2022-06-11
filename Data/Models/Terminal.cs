namespace Data.Models
{
    public partial class Terminal
    {
        public Terminal()
        {
            Flights = new HashSet<Flight>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsInternational { get; set; }

        public virtual ICollection<Flight> Flights { get; set; }
    }
}
