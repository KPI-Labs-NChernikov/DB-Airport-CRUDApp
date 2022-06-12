using Presentation.Interfaces;

namespace Presentation.Models
{
    public class TerminalReduced : IReducedModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Info => ToString();

        public override string ToString()
        {
            return $"{Name} (Id: {Id})";
        }
    }
}
