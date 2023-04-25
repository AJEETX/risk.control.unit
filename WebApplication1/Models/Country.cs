
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;
public class Country
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string CountryId { get; set; }
    [Display(Name = "Country name")]
    public string Name { get; set; }
    [Display(Name = "Country code")]
    [Required]
    public string Code { get; set; }    
}
public class State
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string StateId { get; set; }
    [Display(Name = "State name")]
    public string Name { get; set; }
    [Display(Name = "State code")]
    [Required]
    public string Code { get; set; }      
    [Required]
    [Display(Name = "Country name")]
    public  string CountryId { get; set; }
    [Display(Name = "Country name")]
    public Country Country { get; set; }
}
public class PinCode
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string PinCodeId { get; set; }
    [Display(Name = "PinCode name")]
    public string Name { get; set; }
    [Display(Name = "PinCode")]
    [Required]
    public string Code { get; set; }      
    [Required]
    [Display(Name = "State name")]
    public  string StateId { get; set; }
    [Display(Name = "State name")]
    public State State { get; set; }
    [Required]
    [Display(Name = "Country name")]
    public string CountryId { get; set; }
    [Display(Name = "Country name")]
    public Country Country { get; set; }
}
