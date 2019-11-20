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
        if (_context.Database.EnsureCreated())
        {
          AddData();
        }
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
        Id = 1,
        Animals = new List<Animal> {
          new Dog() { Name = "Brutus", IsChecked = true, KidFriendly = true, Id = 1 , ShelterId = 1},
          new Cat() { Name = "Minoes", IsChecked = true, KidFriendly = true, Id = 2 , ShelterId = 1}
        }
      };
      _context.Shelters.Add(shelter);

      _context.SaveChanges();
    }
  }
} 