using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class PlaneModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "The registration number is too long")]
        public string RegistrationNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "The name is too long")]
        public string Name { get; set; } = string.Empty;

        [Required]
        public short Capacity { get; set; }
    }
}
