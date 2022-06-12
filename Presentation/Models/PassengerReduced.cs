using Presentation.Interfaces;

namespace Presentation.Models
{
    public class PassengerReduced : IReducedModel
    {
        public int Id { get; set; }

        public string PassportCode { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Info => ToString();

        public override string ToString() => $"{FullName}; Passport: {PassportCode}; Id: {Id}";
    }
}
