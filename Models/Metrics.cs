using System.ComponentModel.DataAnnotations;

namespace Worthy_API.Models;

public class Metrics
{
  
    public int Id { get; set; } 
    
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Type { get; set; } = string.Empty; 
    [Required] 
    public double Score { get; set; } = 0.0;
}