using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Plane
    {
        public Plane()
        {
            Flights = new HashSet<Flight>();
        }

        public int Id { get; set; }
        public string RegistrationNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public short Capacity { get; set; }

        public virtual ICollection<Flight> Flights { get; set; }
    }
}
