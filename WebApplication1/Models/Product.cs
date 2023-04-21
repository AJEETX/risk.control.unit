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
        [Display(Name = "Case type")]
        [Required]
        public string RiskCaseTypeId { get; set; }
        public RiskCaseType RiskCaseType { get; set; }
        [Display(Name = "Case status")]
        [Required]
        public string RiskCaseStatusId { get; set; }
        public RiskCaseStatus RiskCaseStatus { get; set; }
        public DateTime Created { get; set; }

    }

    public class RiskCaseType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string RiskCaseTypeId { get; set; }
        [Display(Name = "Case type")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Case type code")]
        [Required]
        public string Code { get; set; }
        public DateTime Created { get; set; }

    }

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
