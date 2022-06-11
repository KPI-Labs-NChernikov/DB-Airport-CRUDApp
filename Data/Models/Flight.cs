using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Flight
    {
        public Flight()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string To { get; set; } = null!;
        public DateTime Date { get; set; }
        public int PlaneId { get; set; }
        public int TerminalId { get; set; }

        public virtual Plane Plane { get; set; } = null!;
        public virtual Terminal Terminal { get; set; } = null!;
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
