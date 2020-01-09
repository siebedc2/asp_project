using System.Collections.Generic;
using System.Linq;
using Shelter.shared;
using MongoDB.Driver;
using MongoDB.Bson;
// using System.Threading.Tasks;
namespace Shelter.MVC
{
  public interface IShelterDataAccess
  {
    IEnumerable<Shelter.shared.Shelter> GetAllShelters();
    List<BsonDocument> GetAllSheltersFull();
    Shelter.shared.Shelter GetShelterById(string id);

    IEnumerable<Animal> GetAnimals(string shelterId);
    Animal GetAnimalByShelterAndId(string shelterId, string animalId);

    void UpdateAnimal(string shelterId, string animalId, Shelter.shared.Animal animal);
    Dog AddDog(string shelterId, Shelter.shared.Dog dog);
    Cat AddCat(string shelterId, Shelter.shared.Cat cat);
    Other AddOther(string shelterId, Shelter.shared.Other other);
    void DeleteAnimal(string shelterId, string animalId);

    void UpdateShelter(string shelterId, Shelter.shared.Shelter shelter);
    Shelter.shared.Shelter AddShelter(Shelter.shared.Shelter shelter);
    void DeleteShelter(string shelterId);

    IEnumerable<Employee> GetShelterEmployees(string shelterId);
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

    public IEnumerable<Shelter.shared.Shelter> GetAllShelters()
    {
      return _context.Shelters.AsQueryable();
    }

    public List<BsonDocument> GetAllSheltersFull()
    {
      var collection = _context.Shelters;
      var data = collection.Aggregate().Lookup("animals","id", "shelterId", "Animals").Lookup("employees","id", "shelterId", "Employees").ToList();
      return data;
    }

    public Animal GetAnimalByShelterAndId(string shelterId, string animalId)
    {
      return _context.Animals.Find<Animal>(x => x.ShelterId == shelterId && x.Id == animalId).FirstOrDefault();
    }

    public IEnumerable<Animal> GetAnimals(string shelterId)
    {
      var collection = _context.Animals;
      collection.Find<Animal>(x => x.Id == shelterId);
      return collection.AsQueryable();
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

    public IEnumerable<Employee> GetShelterEmployees(string shelterId)
    {
      var collection = _context.Employees;
      collection.Find<Employee>(x => x.Id == shelterId);
      return collection.AsQueryable();
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