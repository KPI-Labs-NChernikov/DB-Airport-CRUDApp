using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class VisaModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(20, ErrorMessage = "The code is too long")]
        public string Code { get; set; } = string.Empty;

        [Required]
        public DateTime Issue { get; set; }

        [Required]
        public DateTime Expiry { get; set; }

        [Required]
        [Display(Name = "Passenger")]
        public int PassengerId { get; set; }
    }
}
