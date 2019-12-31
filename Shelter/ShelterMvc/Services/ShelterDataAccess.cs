using System.Collections.Generic;
using System.Linq;
using Shelter.shared;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ShelterMvc.Models;

namespace Shelter.MVC
{
  public interface IShelterDataAccess
  {
    IEnumerable<Shelter.shared.Shelter> GetAllShelters();
    IEnumerable<Shelter.shared.Shelter> GetAllSheltersFull();
    Shelter.shared.Shelter GetShelterById(string id);

    IEnumerable<Animal> GetAnimals(string shelterId);
    Animal GetAnimalByShelterAndId(string shelterId, string animalId);

    Animal UpdateAnimal(string shelterId, string animalId, Shelter.shared.Animal animal);
    Dog AddDog(string shelterId, Shelter.shared.Dog dog);
    Cat AddCat(string shelterId, Shelter.shared.Cat cat);
    Other AddOther(string shelterId, Shelter.shared.Other other);
    Animal DeleteAnimal(string shelterId, string animalId);

    Shelter.shared.Shelter UpdateShelter(string shelterId, Shelter.shared.Shelter shelter);
    Shelter.shared.Shelter AddShelter(Shelter.shared.Shelter shelter);
    Shelter.shared.Shelter DeleteShelter(string shelterId);

    IEnumerable<Employee> GetShelterEmployees(string shelterId);
    Manager AddManager(string shelterId, Shelter.shared.Manager manager);
    Caretaker AddCaretaker(string shelterId, Shelter.shared.Caretaker caretaker);
    Administrator AddAdministrator(string shelterId, Shelter.shared.Administrator administrator);
    Employee UpdateEmployee(string shelterId, string employeeId, Shelter.shared.Employee employee);
    Employee DeleteEmployee(string shelterId, string employeeId);
  }

  public class ShelterDataAccess : IShelterDataAccess
  {

    private readonly IMongoCollection<ShelterContext> _context;

    public ShelterDataAccess(IShelterDatabaseSettings settings)
    {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _context = database.GetCollection<ShelterContext>(settings.ShelterCollectionName);
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

    public Animal GetAnimalByShelterAndId(string shelterId, string animalId)
    {
      return _context.Animals
        .FirstOrDefault(x => x.ShelterId == shelterId && x.Id == animalId);
    }

    public IEnumerable<Animal> GetAnimals(string shelterId)
    {
      return _context.Shelters
        .Include(shelter => shelter.Animals)
        .FirstOrDefault(x => x.Id == shelterId)?.Animals;
    }

    public Shelter.shared.Shelter GetShelterById(string id)
    {
      return _context.Shelters.FirstOrDefault(x => x.Id == id);
    }

    public Animal UpdateAnimal(string shelterId, string animalId, Shelter.shared.Animal animal) {
      Animal updateAnimal =  _context.Animals.FirstOrDefault(x => x.ShelterId == shelterId && x.Id == animalId);

      updateAnimal.Name = animal.Name;
      updateAnimal.DateOfBirth = animal.DateOfBirth;
      updateAnimal.IsChecked = animal.IsChecked;
      updateAnimal.KidFriendly = animal.KidFriendly;
      updateAnimal.ShelterId = shelterId;

      _context.Update(updateAnimal);
      _context.SaveChanges();
      return updateAnimal;
    }

    public Dog AddDog(string shelterId, Shelter.shared.Dog dog) {
      Dog newDog = new Dog{ 
        Name = dog.Name,
        DateOfBirth = dog.DateOfBirth,
        IsChecked = dog.IsChecked,
        KidFriendly = dog.KidFriendly,
        ShelterId = shelterId,
        Race = dog.Race,
        Barker = dog.Barker
      };
        _context.Add(newDog);
        _context.SaveChanges();
        return newDog;
    }

    public Cat AddCat(string shelterId, Shelter.shared.Cat cat) {
      Cat newCat = new Cat{ 
        Name = cat.Name,
        DateOfBirth = cat.DateOfBirth,
        IsChecked = cat.IsChecked,
        KidFriendly = cat.KidFriendly,
        ShelterId = shelterId,
        Race = cat.Race,
        Declawed = cat.Declawed
      };
        _context.Add(newCat);
        _context.SaveChanges();
        return newCat;
    }

    public Other AddOther(string shelterId, Shelter.shared.Other other) {
      Other newOther = new Other{ 
        Name = other.Name,
        DateOfBirth = other.DateOfBirth,
        IsChecked = other.IsChecked,
        KidFriendly = other.KidFriendly,
        ShelterId = shelterId,
        Kind = other.Kind,
        Description = other.Description
      };
        _context.Add(newOther);
        _context.SaveChanges();
        return newOther;
    }

    public Animal DeleteAnimal(string shelterId, string animalId) {
      Animal deleteAnimal =  _context.Animals.FirstOrDefault(x => x.ShelterId == shelterId && x.Id == animalId);
      _context.Remove(deleteAnimal);
      _context.SaveChanges();
      return deleteAnimal;
    }

    public Shelter.shared.Shelter UpdateShelter(string shelterId, Shelter.shared.Shelter shelter) {
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

    public Shelter.shared.Shelter DeleteShelter(string shelterId) {
      Shelter.shared.Shelter deleteShelter =  _context.Shelters.FirstOrDefault(x => x.Id == shelterId);
      _context.Remove(deleteShelter);
      _context.SaveChanges();
      return deleteShelter;
    }

    public IEnumerable<Employee> GetShelterEmployees(string shelterId)
    {
      return _context.Shelters
        .Include(shelter => shelter.Employees)
        .FirstOrDefault(x => x.Id == shelterId)?.Employees;
    }

    public Manager AddManager(string shelterId, Shelter.shared.Manager manager) {
      Manager newManager = new Manager{ 
        Name = manager.Name,
        LicensedManager = manager.LicensedManager,
        ShelterId = shelterId
      };
        _context.Add(newManager);
        _context.SaveChanges();
        return newManager;
    }

    public Caretaker AddCaretaker(string shelterId, Shelter.shared.Caretaker caretaker) {
      Caretaker newCaretaker = new Caretaker{ 
        Name = caretaker.Name,
        FixedContract = caretaker.FixedContract,
        ShelterId = shelterId
      };
        _context.Add(newCaretaker);
        _context.SaveChanges();
        return newCaretaker;
    }

    public Administrator AddAdministrator(string shelterId, Shelter.shared.Administrator administrator) {
      Administrator newAdministrator = new Administrator{ 
        Name = administrator.Name,
        DigitalAdministration = administrator.DigitalAdministration,
        ShelterId = shelterId
      };
        _context.Add(newAdministrator);
        _context.SaveChanges();
        return newAdministrator;
    }

    public Employee UpdateEmployee(string shelterId, string employeeId, Shelter.shared.Employee employee) {
      Employee updateEmployee =  _context.Employees.FirstOrDefault(x => x.ShelterId == shelterId && x.Id == employeeId);

      updateEmployee.Name = employee.Name;
      updateEmployee.ShelterId = shelterId;

      _context.Update(updateEmployee);
      _context.SaveChanges();
      return updateEmployee;
    }

    public Employee DeleteEmployee(string shelterId, string employeeId) {
      Employee deleteEmployee =  _context.Employees.FirstOrDefault(x => x.ShelterId == shelterId && x.Id == employeeId);
      _context.Remove(deleteEmployee);
      _context.SaveChanges();
      return deleteEmployee;
    }

  }
} 