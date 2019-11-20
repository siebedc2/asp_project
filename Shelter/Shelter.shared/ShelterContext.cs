using Microsoft.EntityFrameworkCore;

namespace Brewery.Shared
{
  public class BreweryContext : DbContext
  {
    public BreweryContext(DbContextOptions<ShelterContext> options) : base(options)
    {

    }

    public DbSet<Animals> Animals { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Shelter> Shelters { get; set; }
    
  }
} 