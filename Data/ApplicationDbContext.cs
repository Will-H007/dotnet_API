using Microsoft.EntityFrameworkCore;
using Worthy_API.Models;

namespace Worthy_API.Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    } 
    
    public DbSet<Metrics> Metrics { get; set; }
    
    
}