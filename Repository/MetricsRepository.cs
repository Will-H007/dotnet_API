using Microsoft.EntityFrameworkCore;
using Worthy_API.Data;
using Worthy_API.Dtos.Metrics;
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

    public async Task<Metrics?> GetByIdAsync(int id)
    {
       return await _context.Metrics.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Metrics> CreateAsync(Metrics metricsModel)
    {
        await _context.Metrics.AddAsync(metricsModel);
        await _context.SaveChangesAsync();
        return metricsModel;
    }



    public async Task<Metrics?> UpdateAsync(int id, UpdateMetricsRequestDto metricsDto)
    {
        var existingMetrics = await _context.Metrics.FirstOrDefaultAsync(m => m.Id == id);
        if (existingMetrics == null)
        {
            return null;
        }
        existingMetrics.Name = metricsDto.Name;
        existingMetrics.Type = metricsDto.Type;
        existingMetrics.Score = metricsDto.Score;
        await _context.SaveChangesAsync();
        return existingMetrics;
    }

    public async Task<Metrics?> DeleteAsync(int id)
    {
        var metricsModel = await _context.Metrics.FirstOrDefaultAsync(m => m.Id == id);
        if (metricsModel == null)
        {
            return null;
        } 
        _context.Metrics.Remove(metricsModel);
        await _context.SaveChangesAsync();
        return metricsModel;
    }
}