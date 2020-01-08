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
        Id = "507f1f77bcf86cd799439011",
        Animals = new List<Animal> {
          new Dog() { Name = "Brutus", IsChecked = true, KidFriendly = true, Id = "507f1f77bcf86cd799439012" , ShelterId = "507f1f77bcf86cd799439011"},
          new Cat() { Name = "Minoes", IsChecked = true, KidFriendly = true, Id = "507f1f77bcf86cd799439013" , ShelterId = "507f1f77bcf86cd799439011"},
          new Cat() { Name = "pspspsps", IsChecked = true, KidFriendly = false, Id = "507f1f77bcf86cd799439014" , ShelterId = "507f1f77bcf86cd799439011"}
        }
      };
      _context.Shelters.InsertOne(shelter);
    }
  }
} 