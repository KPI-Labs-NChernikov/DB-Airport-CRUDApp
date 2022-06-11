namespace Data.Models
{
    public partial class Visa
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public DateTime Issue { get; set; }
        public DateTime Expiry { get; set; }
        public int PassengerId { get; set; }

        public virtual Passenger Passenger { get; set; } = null!;
    }
}
