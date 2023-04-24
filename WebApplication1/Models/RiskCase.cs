using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class RiskCase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string RiskCaseId { get; set; } =Guid.NewGuid().ToString(); 
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [Display(Name = "Case type")]
        public string RiskCaseTypeId { get; set; }
        [Display(Name = "Case type")]
        public RiskCaseType RiskCaseType { get; set; }
        [Required]
        [Display(Name = "Case status")]
        public string RiskCaseStatusId { get; set; }
        [Display(Name = "Case status")]
        public RiskCaseStatus RiskCaseStatus { get; set; }
        public DateTime Created { get; set; }

    }
}
