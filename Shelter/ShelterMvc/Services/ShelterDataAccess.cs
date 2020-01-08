using System.Collections.Generic;
using System.Linq;
using Shelter.shared;
using MongoDB.Driver;
// using System.Threading.Tasks;
namespace Shelter.MVC
{
  public interface IShelterDataAccess
  {
    IMongoCollection<Shelter.shared.Shelter> GetAllShelters();
    IMongoCollection<Shelter.shared.Shelter> GetAllSheltersFull();
    Shelter.shared.Shelter GetShelterById(string id);

    IMongoCollection<Animal> GetAnimals(string shelterId);
    Animal GetAnimalByShelterAndId(string shelterId, string animalId);

    void UpdateAnimal(string shelterId, string animalId, Shelter.shared.Animal animal);
    Dog AddDog(string shelterId, Shelter.shared.Dog dog);
    Cat AddCat(string shelterId, Shelter.shared.Cat cat);
    Other AddOther(string shelterId, Shelter.shared.Other other);
    void DeleteAnimal(string shelterId, string animalId);

    void UpdateShelter(string shelterId, Shelter.shared.Shelter shelter);
    Shelter.shared.Shelter AddShelter(Shelter.shared.Shelter shelter);
    void DeleteShelter(string shelterId);

    IMongoCollection<Employee> GetShelterEmployees(string shelterId);
    Manager AddManager(string shelterId, Shelter.shared.Manager manager);
    Caretaker AddCaretaker(string shelterId, Shelter.shared.Caretaker caretaker);
    Administrator AddAdministrator(string shelterId, Shelter.shared.Administrator administrator);
    void UpdateEmployee(string shelterId, string employeeId, Shelter.shared.Employee employee);
    void DeleteEmployee(string shelterId, string employeeId);
  }

  public class ShelterDataAccess : IShelterDataAccess
  {

    private readonly ShelterContext _context;
    public ShelterDataAccess(ShelterContext db)
    {
            _context = db;
    }

    public IMongoCollection<Shelter.shared.Shelter> GetAllShelters()
    {
      return _context.Shelters;
    }

    public IMongoCollection<Shelter.shared.Shelter> GetAllSheltersFull()
    {
      var collection = _context.Shelters;
      collection.Aggregate().Lookup("Animals", "Id", "ShelterId", "Animals")
      .Lookup("Employees", "Id", "ShelterId", "Employees");
      return collection;
    }

    public Animal GetAnimalByShelterAndId(string shelterId, string animalId)
    {
      return _context.Animals.Find<Animal>(x => x.ShelterId == shelterId && x.Id == animalId).FirstOrDefault();
    }

    public IMongoCollection<Animal> GetAnimals(string shelterId)
    {
      return _context.Shelters
        .Include(shelter => shelter.Animals)
        .FirstOrDefault(x => x.Id == shelterId)?.Animals;
    }

    public Shelter.shared.Shelter GetShelterById(string id)
    {
      return _context.Shelters.Find<Shelter.shared.Shelter>(x => x.Id == id).FirstOrDefault();
    }

    public void UpdateAnimal(string shelterId, string animalId, Shelter.shared.Animal animal) {
      _context.Animals.ReplaceOne(x => x.ShelterId == shelterId && x.Id == animalId, animal); 
    }

    public Dog AddDog(string shelterId, Shelter.shared.Dog dog) {
        _context.Animals.InsertOne(dog);
        return dog;
    }

    public Cat AddCat(string shelterId, Shelter.shared.Cat cat) {
      _context.Animals.InsertOne(cat);
      return cat;
    }

    public Other AddOther(string shelterId, Shelter.shared.Other other) {
      _context.Animals.InsertOne(other);
      return other;
    }

    public void DeleteAnimal(string shelterId, string animalId) {
      _context.Animals.DeleteOne(x => x.ShelterId == shelterId && x.Id == animalId);
    }

    public void UpdateShelter(string shelterId, Shelter.shared.Shelter shelter) {
      _context.Shelters.ReplaceOne(x => x.Id == shelterId, shelter);
    }

    public Shelter.shared.Shelter AddShelter(Shelter.shared.Shelter shelter) {
      _context.Shelters.InsertOne(shelter);
      return shelter;
    }

    public void DeleteShelter(string shelterId) {
      _context.Shelters.DeleteOne(x => x.Id == shelterId);
    }

    public IMongoCollection<Employee> GetShelterEmployees(string shelterId)
    {
      return _context.Shelters
        .Include(shelter => shelter.Employees)
        .FirstOrDefault(x => x.Id == shelterId)?.Employees;
    }

    public Manager AddManager(string shelterId, Shelter.shared.Manager manager) {
      _context.Employees.InsertOne(manager);
      return manager;
    }

    public Caretaker AddCaretaker(string shelterId, Shelter.shared.Caretaker caretaker) {
      _context.Employees.InsertOne(caretaker);
      return caretaker;
    }

    public Administrator AddAdministrator(string shelterId, Shelter.shared.Administrator administrator) {
      _context.Employees.InsertOne(administrator);
      return administrator;
    }

    public void UpdateEmployee(string shelterId, string employeeId, Shelter.shared.Employee employee) {
      _context.Employees.ReplaceOne(x => x.ShelterId == shelterId && x.Id == employeeId, employee);
    }

    public void DeleteEmployee(string shelterId, string employeeId) {
      _context.Employees.DeleteOne(x => x.ShelterId == shelterId && x.Id == employeeId);
    }
  }
} 