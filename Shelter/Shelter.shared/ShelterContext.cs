using Microsoft.EntityFrameworkCore;

namespace Shelter.shared
{
  public class ShelterContext : DbContext
  {
    public ShelterContext(DbContextOptions<ShelterContext> options) : base(options)
    {

    }

    public DbSet<Animal> Animals { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Shelter> Shelters { get; set; }
    
  }
} 