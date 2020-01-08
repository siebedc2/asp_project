using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Shelter.shared
{
  public interface IDatabaseInitializer
  {
    void Initialize();
  }

  public class DatabaseInitializer : IDatabaseInitializer
  {
    private ShelterContext _context;
    private ILogger<DatabaseInitializer> _logger;
    public DatabaseInitializer(ShelterContext context, ILogger<DatabaseInitializer> logger)
    {
      _context = context;
      _logger = logger;
    }
    public void Initialize()
    {
      try
      {
          AddData();
      }
      catch (Exception ex)
      {
        _logger.LogCritical(ex, "Error occurred while creating database");

      }
    }

    private void AddData()
    {
      var shelter = new Shelter()
      {
        Name = "MijnEersteShelter",
        Id = "aa1",
        Animals = new List<Animal> {
          new Dog() { Name = "Brutus", IsChecked = true, KidFriendly = true, Id = "a1" , ShelterId = "aa1"},
          new Cat() { Name = "Minoes", IsChecked = true, KidFriendly = true, Id = "a2" , ShelterId = "aa1"},
          new Cat() { Name = "pspspsps", IsChecked = true, KidFriendly = false, Id = "a3" , ShelterId = "aa1"}
        }
      };
      _context.Shelters.InsertOne(shelter);
    }
  }
} 