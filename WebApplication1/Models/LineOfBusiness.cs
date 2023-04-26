using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class LineOfBusiness
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string LineOfBusinessId { get; set; }
        [Display(Name = "Line of Business")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Line of Business code")]
        [Required]
        public string Code { get; set; }
        public DateTime Created { get; set; }
    }
}
