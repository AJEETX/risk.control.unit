
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;
public class Country
{
    [Key]

    public int CountryId { get; set; }
    public string CountryName { get; set; }
}
public class State
{
    [Key]
    public int StateId { get; set; }
    public string StateName { get; set; }
    public  int CountryId { get; set; }
    [ForeignKey (nameof(CountryId))]
    public virtual Country Country { get; set; }
}
public class City
{
    [Key]
    public int CityId { get; set; }
    public string CityName { get; set; }
    public int CountryId { get; set; } 
    public virtual Country Country { get; set; }
    public int StateId { get; set; } 
    public virtual State State { get; set; }          
}
