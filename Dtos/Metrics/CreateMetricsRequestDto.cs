using System.ComponentModel.DataAnnotations;

namespace Worthy_API.Dtos.Metrics;

public class CreateMetricsRequestDto
{
    [Required]
    [MaxLength(10, ErrorMessage = "Name cannot be longer than 10 characters.")]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(10, ErrorMessage = "Type cannot be longer than 10 characters.")]
    public string Type { get; set; } = string.Empty;
    
    
    [Range(1, int.MaxValue, ErrorMessage = "Score cannot be negative.")] 
    public double Score { get; set; } = 0.0; 
}