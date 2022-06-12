using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class PlaneModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(10, ErrorMessage = "The registration number is too long")]
        [Display(Name = "Registration number")]
        public string RegistrationNumber { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "The name is too long")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1, short.MaxValue, ErrorMessage = "The value must be positive")]
        public short Capacity { get; set; }
    }
}
