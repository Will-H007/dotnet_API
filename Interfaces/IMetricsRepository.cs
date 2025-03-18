using Worthy_API.Dtos.Metrics;
using Worthy_API.Helpers;
using Worthy_API.Models;

namespace Worthy_API.Interfaces;

public interface IMetricsRepository
{
   Task<List<Metrics>> GetAllAsync(QueryObject query);
   Task<Metrics?> GetByIdAsync(int id);

   Task<Metrics> CreateAsync(Metrics metrics);
   
   Task<Metrics?> UpdateAsync(int id,UpdateMetricsRequestDto MetricsDto);
   Task<Metrics?> DeleteAsync(int id);

}