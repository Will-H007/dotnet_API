namespace Worthy_API.Dtos.Metrics;

public class MetricsDto
{
    public int Id { get; set; } 
    

    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty; 
 
    public double Score { get; set; } = 0.0; 
}