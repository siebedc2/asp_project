using MongoDB.Driver;

namespace Shelter.shared
{
  public class ShelterContext
  {
    private readonly IMongoDatabase _db;
    public ShelterContext(IMongoClient client, string dbName) 
    {
      _db = client.GetDatabase(dbName);
    }
      public IMongoCollection<Animal> Animals => _db.GetCollection<Animal>("animals");
      public IMongoCollection<Employee> Employees => _db.GetCollection<Employee>("employees");
      public IMongoCollection<Shelter> Shelters => _db.GetCollection<Shelter>("shelters");

    // public IMongoCollection<Animal> _Animals { get; set; }
    // public IMongoCollection<Employee> Employees { get; set; }
    // public IMongoCollection<Shelter> Shelters { get; set; }
    
  }
} 