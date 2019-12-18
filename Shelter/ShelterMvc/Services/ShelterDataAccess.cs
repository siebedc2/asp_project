using System.Collections.Generic;
using System.Linq;
using Shelter.shared;
using Microsoft.EntityFrameworkCore;

namespace Shelter.MVC
{
  public interface IShelterDataAccess
  {
    IEnumerable<Shelter.shared.Shelter> GetAllShelters();
    IEnumerable<Shelter.shared.Shelter> GetAllSheltersFull();
    Shelter.shared.Shelter GetShelterById(int id);

    IEnumerable<Animal> GetAnimals(int shelterId);
    Animal GetAnimalByShelterAndId(int shelterId, int animalId);
  }

  public class ShelterDataAccess : IShelterDataAccess
  {
    private ShelterContext _context;

    public ShelterDataAccess(ShelterContext context)
    {
      _context = context;
    }

    public IEnumerable<Shelter.shared.Shelter> GetAllShelters()
    {
      return _context.Shelters;
    }

    public IEnumerable<Shelter.shared.Shelter> GetAllSheltersFull()
    {
      return _context.Shelters
        .Include(shelter => shelter.Animals)
        .Include(shelter => shelter.Employees);
    }

    public Animal GetAnimalByShelterAndId(int shelterId, int animalId)
    {
      return _context.Animals
        .FirstOrDefault(x => x.ShelterId == shelterId && x.Id == animalId);
    }

    public IEnumerable<Animal> GetAnimals(int shelterId)
    {
      return _context.Shelters
        .Include(shelter => shelter.Animals)
        .FirstOrDefault(x => x.Id == shelterId)?.Animals;
    }

    public Shelter.shared.Shelter GetShelterById(int id)
    {
      return _context.Shelters.FirstOrDefault(x => x.Id == id);
    }
    public Animal UpdateAnimal(int shelterId, int animalId) {
      return _context.Animals
        .FirstOrDefault(x => x.ShelterId == shelterId && x.Id == animalId);
    }
  }
} 