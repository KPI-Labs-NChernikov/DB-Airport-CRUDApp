using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class PassengerModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The passport code is too long")]
        public string PassportCode { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "The first name is too long")]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "The last name is too long")]
        [Required]
        public string LastName { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "The patronymic is too long")]
        public string? Patronymic { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public bool InternallyWanted { get; set; }

        [Required]
        public bool InternationallyWanted { get; set; }
    }
}
