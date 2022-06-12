using Presentation.Interfaces;

namespace Presentation.Models
{
    public class FlightReduced : IReducedModel
    {
        public int Id { get; set; }

        public string To { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Info => ToString();

        public override string ToString()
        {
            return $"Id: {Id}; Dest: {To}; Date: {Date}";
        }
    }
}
