using System.Collections.Generic;
using System.Linq;
using Shelter.shared;
using MongoDB.Driver;
using ShelterMvc.Models;

namespace Shelter.MVC
{
  public interface IShelterDataAccess
  {
    IMongoCollection<Shelter.shared.Shelter> GetAllShelters();
    IMongoCollection<Shelter.shared.Shelter> GetAllSheltersFull();
    Shelter.shared.Shelter GetShelterById(string id);

    IMongoCollection<Animal> GetAnimals(string shelterId);
    Animal GetAnimalByShelterAndId(string shelterId, string animalId);

    Animal UpdateAnimal(string shelterId, string animalId, Shelter.shared.Animal animal);
    Dog AddDog(string shelterId, Shelter.shared.Dog dog);
    Cat AddCat(string shelterId, Shelter.shared.Cat cat);
    Other AddOther(string shelterId, Shelter.shared.Other other);
    Animal DeleteAnimal(string shelterId, string animalId);

    Shelter.shared.Shelter UpdateShelter(string shelterId, Shelter.shared.Shelter shelter);
    Shelter.shared.Shelter AddShelter(Shelter.shared.Shelter shelter);
    Shelter.shared.Shelter DeleteShelter(string shelterId);

    IMongoCollection<Employee> GetShelterEmployees(string shelterId);
    Manager AddManager(string shelterId, Shelter.shared.Manager manager);
    Caretaker AddCaretaker(string shelterId, Shelter.shared.Caretaker caretaker);
    Administrator AddAdministrator(string shelterId, Shelter.shared.Administrator administrator);
    Employee UpdateEmployee(string shelterId, string employeeId, Shelter.shared.Employee employee);
    Employee DeleteEmployee(string shelterId, string employeeId);
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
      return _context.Shelters
        .Include(shelter => shelter.Animals)
        .Include(shelter => shelter.Employees);
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

    public Employee UpdateEmployee(string shelterId, string employeeId, Shelter.shared.Employee employee) {
      Employee updateEmployee =  _context.Employees.FirstOrDefault(x => x.ShelterId == shelterId && x.Id == employeeId);

      updateEmployee.Name = employee.Name;
      updateEmployee.ShelterId = shelterId;

      _context.Update(updateEmployee);
      _context.SaveChanges();
      return updateEmployee;
    }

    public void DeleteEmployee(string shelterId, string employeeId) {
      _context.Employees.DeleteOne(x => x.ShelterId == shelterId && x.Id == employeeId);
    }

  }
} 