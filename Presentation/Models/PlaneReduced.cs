using Presentation.Interfaces;

namespace Presentation.Models
{
    public class PlaneReduced : IReducedModel
    {
        public int Id { get; set; }

        public string RegistrationNumber { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Info => ToString();

        public override string ToString()
        {
            return $"{RegistrationNumber} {Name} (Id: {Id})";
        }
    }
}
