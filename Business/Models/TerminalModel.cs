using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class TerminalModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The name is too long")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "International")]
        public bool IsInternational { get; set; }
    }
}
