using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Passenger
    {
        public Passenger()
        {
            Tickets = new HashSet<Ticket>();
            Visas = new HashSet<Visa>();
        }

        public int Id { get; set; }
        public string PassportCode { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public bool InternallyWanted { get; set; }
        public bool InternationallyWanted { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Visa> Visas { get; set; }
    }
}
