using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class InvestigationCaseStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string InvestigationCaseStatusId { get; set; }
        [Display(Name = "Case status")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Case status")]
        [Required]
        public string Code { get; set; }
        public DateTime Created { get; set; }

    }
}
