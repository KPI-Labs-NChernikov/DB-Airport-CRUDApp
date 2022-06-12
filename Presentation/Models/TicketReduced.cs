using Presentation.Interfaces;

namespace Presentation.Models
{
    public class TicketReduced : IReducedModel
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Info => ToString();

        public override string ToString()
        {
            return $"{Code} (Id: {Id})";
        }
    }
}
