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
    List<Shelter.shared.Shelter> GetAllSheltersFull();
    List<Shelter.shared.Shelter> GetShelterById(string id);

    IEnumerable<Animal> GetAnimals(string shelterId);
    Animal GetAnimalByShelterAndId(string shelterId, string animalId);

    Animal UpdateAnimal(string shelterId, string animalId, Shelter.shared.Animal animal);
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

    public List<Shelter.shared.Shelter> GetAllSheltersFull()
    {
      var collection = _context.Shelters;
      var data = collection.Aggregate()
      .Lookup<Shelter.shared.Shelter, Shelter.shared.Shelter>("animals","id", "shelterId", "Animals")
      .Lookup<Shelter.shared.Shelter, Shelter.shared.Shelter>("employees","id", "shelterId", "Employees").ToList();
      foreach (var shelter in data)
      {
          foreach (var animal in shelter.Animals.ToList())
          {
              if(animal.ShelterId != shelter.Id){
                shelter.Animals.Remove(animal);
              }
          }
          foreach (var emp in shelter.Employees.ToList())
          {
              if(emp.ShelterId != shelter.Id){
                shelter.Employees.Remove(emp);
              }
          }
      }
      return data;
    }

    public Animal GetAnimalByShelterAndId(string shelterId, string animalId)
    {
      return _context.Animals.Find<Animal>(x => x.ShelterId == shelterId && x.Id == animalId).FirstOrDefault();
    }

    public IEnumerable<Animal> GetAnimals(string shelterId)
    {
      var collection = _context.Animals;
      var data = collection.Find<Animal>(x => x.ShelterId == shelterId).ToList();
      return data;
    }

    public List<Shelter.shared.Shelter> GetShelterById(string id)
    {
      var collection = _context.Shelters;
      var data = collection.Aggregate()
      .Match(x => x.Id == id)
      .Lookup<Shelter.shared.Shelter, Shelter.shared.Shelter>("animals","id", "shelterId", "Animals")
      .Lookup<Shelter.shared.Shelter, Shelter.shared.Shelter>("employees","id", "shelterId", "Employees")
      .ToList();
      foreach (var shelter in data)
      {
          foreach (var animal in shelter.Animals.ToList())
          {
              if(animal.ShelterId != id){
                shelter.Animals.Remove(animal);
              }
          }
          foreach (var emp in shelter.Employees.ToList())
          {
              if(emp.ShelterId != id){
                shelter.Employees.Remove(emp);
              }
          }
      }
      return data;
    }

    public Animal UpdateAnimal(string shelterId, string animalId, Shelter.shared.Animal animal) {
      Animal reference =_context.Animals.Find<Animal>(x => x.ShelterId == shelterId && x.Id == animalId).FirstOrDefault();
      if(animal.Name != reference.Name){
        reference.Name = animal.Name;
      }
      if(animal.DateOfBirth != reference.DateOfBirth){
        reference.DateOfBirth = animal.DateOfBirth;
      }
      if(animal.IsChecked != reference.IsChecked){
        reference.IsChecked = animal.IsChecked;
      }
      if(animal.KidFriendly != reference.KidFriendly){
        reference.KidFriendly = animal.KidFriendly;
      }
      _context.Animals.ReplaceOne(x => x.ShelterId == shelterId && x.Id == animalId, reference); 
      return reference;
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
      var data = collection.Find<Employee>(x => x.ShelterId == shelterId).ToList();
      return data;
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