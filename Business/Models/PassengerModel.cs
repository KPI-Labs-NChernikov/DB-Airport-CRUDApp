using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class PassengerModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(20, ErrorMessage = "The passport code is too long")]
        [Display(Name = "Passport code")]
        public string PassportCode { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "The first name is too long")]
        [Display(Name = "First name")]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "The last name is too long")]
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Last name")]
        public string LastName { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "The patronymic is too long")]
        public string? Patronymic { get; set; }

        [Required]
        [Display(Name = "Birth date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Internally wanted")]
        public bool InternallyWanted { get; set; }

        [Required]
        [Display(Name = "Internationally wanted")]
        public bool InternationallyWanted { get; set; }

        public string FullName => $"{FirstName} {Patronymic}".TrimEnd() + $" {LastName}";
    }
}
