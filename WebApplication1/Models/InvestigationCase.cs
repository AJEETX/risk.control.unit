using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class InvestigationCase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string InvestigationId { get; set; } =Guid.NewGuid().ToString(); 
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [Display(Name = "Case type")]
        public string LineOfBusinessId { get; set; }
        [Display(Name = "Case type")]
        public LineOfBusiness LineOfBusiness { get; set; }
        [Required]
        [Display(Name = "Case status")]
        public string InvestigationCaseStatusId { get; set; }
        [Display(Name = "Case status")]
        public InvestigationCaseStatus InvestigationCaseStatus { get; set; }
        public DateTime Created { get; set; }

    }
}
