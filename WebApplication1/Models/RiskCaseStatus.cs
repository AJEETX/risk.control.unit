using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class RiskCaseStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string RiskCaseStatusId { get; set; }
        [Display(Name = "Case status")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Case status")]
        [Required]
        public string Code { get; set; }
        public DateTime Created { get; set; }

    }
}
