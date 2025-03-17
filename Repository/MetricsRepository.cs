using Microsoft.EntityFrameworkCore;
using Worthy_API.Data;
using Worthy_API.Helpers;
using Worthy_API.Interfaces;
using Worthy_API.Models;

namespace Worthy_API.Repository;

public class MetricsRepository :IMetricsRepository
{
    private readonly ApplicationDbContext _context;
    
    public MetricsRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<Metrics>> GetAllAsync(QueryObject query)
    {
        var metrics = _context.Metrics.AsQueryable();
        // Filter based on query parameters, if present
        if (!string.IsNullOrWhiteSpace(query.Type))
        {
            metrics = metrics.Where(m => m.Type.ToLower().Contains(query.Type.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            if (query.SortBy.ToLower().Equals("score", StringComparison.OrdinalIgnoreCase))
            {
                metrics = query.IsDescending ? metrics.OrderByDescending(c => c.Score) : metrics.OrderBy(c => c.Score);
 
            }
       
        }
       
        return await metrics.ToListAsync(); 
    }

    public async Task<Metrics> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Metrics> CreateAsync(Metrics metrics)
    {
        throw new NotImplementedException();
    }

    public async Task<Metrics?> UpdateAsync(Metrics metrics)
    {
        throw new NotImplementedException();
    }

    public async Task<Metrics?> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}