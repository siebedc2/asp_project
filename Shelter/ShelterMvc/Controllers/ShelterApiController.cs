using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShelterMvc.Models;
using Shelter.shared;
using Microsoft.EntityFrameworkCore;
using Shelter.MVC;

namespace ShelterMvc.Controllers
{
    [Route("/api/shelters")]
    public class ShelterApiController : Controller
    {
        private readonly IShelterDataAccess _dataAccess;
        private readonly ILogger<ShelterApiController> _logger;
        public ShelterApiController(ILogger<ShelterApiController> logger, IShelterDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            _logger = logger;
        }
        
        [HttpGet("")]
        public IActionResult GetAllShelters(){
            
            return Json(_dataAccess.GetAllShelters());
            
        }

        [HttpGet("full")]
        public IActionResult GetAllSheltersFull(){
            return Json(_dataAccess.GetAllSheltersFull());
        }

        [HttpGet("{id}")]
        public IActionResult GetShelter(int id){
            var shelter = _dataAccess.GetShelterById(id);
            return shelter == default(Shelter.shared.Shelter) ? (IActionResult)NotFound() : Ok(shelter);
        }

        [HttpGet("{id}/animals")]
        public IActionResult GetShelterAnimals(int id)
        {
          var animals = _dataAccess.GetAnimals(id);
          return animals == default(IEnumerable<Animal>) ? (IActionResult)NotFound() : Ok(animals);
        }

        [HttpGet("{shelterId}/animals/{animalId}")]
        public IActionResult GetAnimalDetails(int shelterId, int animalId)
        {
          var animal = _dataAccess.GetAnimalByShelterAndId(shelterId, animalId);
          return animal == default(Shelter.shared.Animal) ? (IActionResult)NotFound() : Ok(animal);
        }

        [HttpPut("{shelterId}/animals/{animalId}")]
        public IActionResult UpdateAnimal(int shelterId, int animalId, [FromBody]Shelter.shared.Animal animal)
        {
          animal = _dataAccess.UpdateAnimal(shelterId, animalId, animal);
          return Ok(animal);
        }

        [HttpPost("{shelterId}/animals/dog")]
        public IActionResult AddDog(int shelterId, [FromBody]Shelter.shared.Dog dog)
        {
          dog = _dataAccess.AddDog(shelterId, dog);
          return Ok(dog);
        }

        [HttpPost("{shelterId}/animals/cat")]
        public IActionResult AddCat(int shelterId, [FromBody]Shelter.shared.Cat cat)
        {
          cat = _dataAccess.AddCat(shelterId, cat);
          return Ok(cat);
        }

        [HttpPost("{shelterId}/animals/other")]
        public IActionResult AddOther(int shelterId, [FromBody]Shelter.shared.Other other)
        {
          other = _dataAccess.AddOther(shelterId, other);
          return Ok(other);
        }

        [HttpDelete("{shelterId}/animals/{animalId}")]
        public IActionResult DeleteAnimal(int shelterId, int animalId)
        {
          Shelter.shared.Animal animal = _dataAccess.DeleteAnimal(shelterId, animalId);
          return Ok("Deleted Animal");
        }


        [HttpPut("{id}")]
        public IActionResult UpdateShelter(int id, [FromBody]Shelter.shared.Shelter shelter)
        {
          shelter = _dataAccess.UpdateShelter(id, shelter);
          return Ok(shelter);
        }

        [HttpPost("add")]
        public IActionResult AddShelter([FromBody]Shelter.shared.Shelter shelter)
        {
          shelter = _dataAccess.AddShelter(shelter);
          return Ok(shelter);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteShelter(int id)
        {
          Shelter.shared.Shelter shelter = _dataAccess.DeleteShelter(id);
          return Ok("Deleted shelter");
        }

        [HttpGet("{id}/employees")]
        public IActionResult GetShelterEmployees(int id)
        {
          var employees = _dataAccess.GetShelterEmployees(id);
          return employees == default(IEnumerable<Employee>) ? (IActionResult)NotFound() : Ok(employees);
        }

        [HttpPost("{shelterId}/employees/manager")]
        public IActionResult AddManager(int shelterId, [FromBody]Shelter.shared.Manager manager)
        {
          manager = _dataAccess.AddManager(shelterId, manager);
          return Ok(manager);
        }

        [HttpPost("{shelterId}/employees/caretaker")]
        public IActionResult AddCaretaker(int shelterId, [FromBody]Shelter.shared.Caretaker caretaker)
        {
          caretaker = _dataAccess.AddCaretaker(shelterId, caretaker);
          return Ok(caretaker);
        }

        [HttpPost("{shelterId}/employees/administrator")]
        public IActionResult AddAdministrator(int shelterId, [FromBody]Shelter.shared.Administrator administrator)
        {
          administrator = _dataAccess.AddAdministrator(shelterId, administrator);
          return Ok(administrator);
        }

        [HttpPut("{shelterId}/employees/{employeeId}")]
        public IActionResult UpdateEmployee(int shelterId, int employeeId, [FromBody]Shelter.shared.Employee employee)
        {
          employee = _dataAccess.UpdateEmployee(shelterId, employeeId, employee);
          return Ok(employee);
        }

        [HttpDelete("{shelterId}/employees/{employeeId}")]
        public IActionResult DeleteEmployee(int shelterId, int employeeId)
        {
          Shelter.shared.Employee employee = _dataAccess.DeleteEmployee(shelterId, employeeId);
          return Ok("Deleted Employee");
        }

    }
}