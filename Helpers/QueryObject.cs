namespace Worthy_API.Helpers;

public class QueryObject
{

    public string? Name { get; set; } = null;

    public string? Type { get; set; } = null;
    
    public double Score { get; set; } = 0.0;
        
    public string? SortBy { get; set; } = null;
        
    public bool IsDescending { get; set; } = false;
        
    public int PageNumber { get; set; } = 1;
        
    public int PageSize { get; set; } = 20;
}