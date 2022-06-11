using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            Baggages = new HashSet<Baggage>();
        }

        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public decimal? Price { get; set; }
        public string? Type { get; set; }
        public int PassengerId { get; set; }
        public int FlightId { get; set; }

        public virtual Flight Flight { get; set; } = null!;
        public virtual Passenger Passenger { get; set; } = null!;
        public virtual ICollection<Baggage> Baggages { get; set; }
    }
}
