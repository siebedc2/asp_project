using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Shelter.shared
{
  public class ShelterContext : DbContext
  {
    public ShelterContext(IMongoCollection<ShelterContext> options) 
    {

    }

    public DbSet<Animal> Animals { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Shelter> Shelters { get; set; }
    
  }
} 