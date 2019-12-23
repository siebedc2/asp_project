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

    Animal UpdateAnimal(int shelterId, int animalId, Shelter.shared.Animal animal);
    Animal AddAnimal(int shelterId, Shelter.shared.Animal animal);
    Animal DeleteAnimal(int shelterId, int animalId);

    Shelter.shared.Shelter UpdateShelter(int shelterId, Shelter.shared.Shelter shelter);
    Shelter.shared.Shelter AddShelter(Shelter.shared.Shelter shelter);
    Shelter.shared.Shelter DeleteShelter(int shelterId);
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
    public Animal UpdateAnimal(int shelterId, int animalId, Shelter.shared.Animal animal) {
      Animal updateAnimal =  _context.Animals.FirstOrDefault(x => x.ShelterId == shelterId && x.Id == animalId);

      updateAnimal.Name = animal.Name;
      updateAnimal.DateOfBirth = animal.DateOfBirth;
      updateAnimal.IsChecked = animal.IsChecked;
      updateAnimal.KidFriendly = animal.KidFriendly;
      updateAnimal.ShelterId = animal.ShelterId;

      _context.Update(updateAnimal);
      _context.SaveChanges();
      return updateAnimal;
    }

    public Animal AddAnimal(int shelterId, Shelter.shared.Animal animal) {
      Animal newAnimal = new Animal{ 
        Name = animal.Name,
        DateOfBirth = animal.DateOfBirth,
        IsChecked = animal.IsChecked,
        KidFriendly = animal.KidFriendly,
        ShelterId = shelterId
        };
      _context.Add(newAnimal);
      _context.SaveChanges();
      return newAnimal;
    }

    public Animal DeleteAnimal(int shelterId, int animalId) {
      Animal deleteAnimal =  _context.Animals.FirstOrDefault(x => x.ShelterId == shelterId && x.Id == animalId);
      _context.Remove(deleteAnimal);
      _context.SaveChanges();
      return deleteAnimal;
    }

    public Shelter.shared.Shelter UpdateShelter(int shelterId, Shelter.shared.Shelter shelter) {
      Shelter.shared.Shelter updateShelter =  _context.Shelters.FirstOrDefault(x => x.Id == shelterId);

      updateShelter.Name = shelter.Name;

      _context.Update(updateShelter);
      _context.SaveChanges();
      return updateShelter;
    }

    public Shelter.shared.Shelter AddShelter(Shelter.shared.Shelter shelter) {
      Shelter.shared.Shelter newShelter = new Shelter.shared.Shelter{ Name = shelter.Name };
      _context.Add(newShelter);
      _context.SaveChanges();
      return newShelter;
    }

    public Shelter.shared.Shelter DeleteShelter(int shelterId) {
      Shelter.shared.Shelter deleteShelter =  _context.Shelters.FirstOrDefault(x => x.Id == shelterId);
      _context.Remove(deleteShelter);
      _context.SaveChanges();
      return deleteShelter;
    }

  }
} 