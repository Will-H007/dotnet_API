using Worthy_API.Helpers;
using Worthy_API.Models;

namespace Worthy_API.Interfaces;

public interface IMetricsRepository
{
   Task<List<Metrics>> GetAllAsync(QueryObject query);
   Task<Metrics> GetByIdAsync(int id);

   Task<Metrics> CreateAsync(Metrics metrics);
   
   Task<Metrics?> UpdateAsync(Metrics metrics);
   Task<Metrics?> DeleteAsync(int id);

}