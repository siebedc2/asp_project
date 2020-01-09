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
using MongoDB.Bson;
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
        
        /// <summary>
        /// Get a basic list of the shelters (NOT including animals or employees).
        /// </summary>
        [HttpGet("")]
        public IActionResult GetAllShelters(){
            
            return Json(_dataAccess.GetAllShelters());
            
        }

        /// <summary>
        /// Get a full list of the shelters including their respective animals and employees.
        /// </summary>
        [HttpGet("full")]
        public List<Shelter.shared.Shelter> GetAllSheltersFull(){
            var data = _dataAccess.GetAllSheltersFull();

            return data;
        }

        /// <summary>
        /// Get a single shelter by it's ID including it's animals and employees.
        /// </summary>
        /// <param name="id">The ID of the shelter you're looking for</param>
        [HttpGet("{id}")]
        public List<Shelter.shared.Shelter> GetShelter(string id){
            var data = _dataAccess.GetShelterById(id);
            return data;
        }

        /// <summary>
        /// Get all animals from 1 shelter.
        /// </summary>
        /// <param name="id">The ID of the shelter's animals you're looking' for</param>
        [HttpGet("{id}/animals")]
        public IEnumerable<Animal> GetShelterAnimals(string id)
        {
          var animals = _dataAccess.GetAnimals(id);
          return animals;
        }

        /// <summary>
        /// Get a single animal.
        /// </summary>
        /// <param name="shelterId">The ID of the shelter that the animal belongs to</param>
        /// <param name="animalId">The ID of the animal you're looking for</param>
        [HttpGet("{shelterId}/animals/{animalId}")]
        public IActionResult GetAnimalDetails(string shelterId, string animalId)
        {
          var animal = _dataAccess.GetAnimalByShelterAndId(shelterId, animalId);
          return animal == default(Shelter.shared.Animal) ? (IActionResult)NotFound() : Ok(animal);
        }

        /// <summary>
        /// Update a single animal.
        /// </summary>
        /// <param name="shelterId">The ID of the shelter that the animal belongs to</param>
        /// <param name="animalId">The ID of the animal you will update</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "name": "Jacky,
        ///        "dateOfBirth": "0001-01-01T00:00:00",
        ///        "isChecked": true,
        ///        "kidFriendly": true
        ///     }
        ///
        /// </remarks>
        [HttpPut("{shelterId}/animals/{animalId}")]
        public IActionResult UpdateAnimal(string shelterId, string animalId, [FromBody]Shelter.shared.Animal animal)
        {
          animal = _dataAccess.UpdateAnimal(shelterId, animalId, animal);
          return Ok(animal);
        }

        /// <summary>
        /// Add a Dog as animal to a shelter.
        /// </summary>
        /// <param name="shelterId">The ID of the shelter that the animal belongs to</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "name": "Loena",
        ///        "dateOfBirth": "2017-12-05T00:00:00",
        ///        "isChecked": true,
        ///        "kidFriendly": true,
        ///        "race": "Corgi",
        ///        "barker": false
        ///     }
        ///
        /// </remarks>
        [HttpPost("{shelterId}/animals/dog")]
        public IActionResult AddDog(string shelterId, [FromBody]Shelter.shared.Dog dog)
        {
          dog.ShelterId = shelterId;
          dog = _dataAccess.AddDog(shelterId, dog);
          return Ok(dog);
        }

        /// <summary>
        /// Add a Cat as animal to a shelter.
        /// </summary>
        /// <param name="shelterId">The ID of the shelter that the animal belongs to</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "name": "Mimi",
        ///        "dateOfBirth": "2017-12-05T00:00:00",
        ///        "isChecked": true,
        ///        "kidFriendly": true,
        ///        "race": "House cat",
        ///        "declawed": true
        ///     }
        ///
        /// </remarks>
        [HttpPost("{shelterId}/animals/cat")]
        public IActionResult AddCat(string shelterId, [FromBody]Shelter.shared.Cat cat)
        {
          cat.ShelterId = shelterId;
          cat = _dataAccess.AddCat(shelterId, cat);
          return Ok(cat);
        }

        /// <summary>
        /// Add any other species as animal to a shelter.
        /// </summary>
        /// <param name="shelterId">The ID of the shelter that the animal belongs to</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "name": "Bugs Bunny",
        ///        "dateOfBirth": "2017-12-05T00:00:00",
        ///        "isChecked": true,
        ///        "kidFriendly": true,
        ///        "kind": "bunny",
        ///        "description": "lightbrown bunny with hanging ears, eats carrots all the time"
        ///     }
        ///
        /// </remarks>
        [HttpPost("{shelterId}/animals/other")]
        public IActionResult AddOther(string shelterId, [FromBody]Shelter.shared.Other other)
        {
          other.ShelterId = shelterId;
          other = _dataAccess.AddOther(shelterId, other);
          return Ok(other);
        }

        /// <summary>
        /// Delete a single animal.
        /// </summary>
        /// <param name="shelterId">The ID of the shelter that the animal belongs to</param>
        /// <param name="animalId">The ID of the animal you want to delete</param>
        [HttpDelete("{shelterId}/animals/{animalId}")]
        public IActionResult DeleteAnimal(string shelterId, string animalId)
        {
            _dataAccess.DeleteAnimal(shelterId, animalId);
          return Ok("Deleted Animal");
        }

        /// <summary>
        /// Update a single shelter.
        /// </summary>
        /// <param name="id">The ID of the shelter you want to update</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "name": "Mechels Asiel"
        ///     }
        ///
        /// </remarks>
        [HttpPut("{id}")]
        public IActionResult UpdateShelter(string id, [FromBody]Shelter.shared.Shelter shelter)
        {
          shelter.Id = id;
          _dataAccess.UpdateShelter(id, shelter);
          return Ok(shelter);
        }

        /// <summary>
        /// Add a shelter.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "name": "Just Another Shelter"
        ///     }
        ///
        /// </remarks>
        [HttpPost("add")]
        public IActionResult AddShelter([FromBody]Shelter.shared.Shelter shelter)
        {
          shelter = _dataAccess.AddShelter(shelter);
          return Ok(shelter);
        }

        /// <summary>
        /// Delete a single shelter.
        /// </summary>
        /// <param name="id">The ID of the shelter</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteShelter(string id)
        {
          _dataAccess.DeleteShelter(id);
          return Ok("Deleted shelter");
        }

        /// <summary>
        /// Get all employees of a specific shelter.
        /// </summary>
        /// <param name="id">The ID of the shelter's employees you're searching for</param>
        [HttpGet("{id}/employees")]
        public IActionResult GetShelterEmployees(string id)
        {
          var employees = _dataAccess.GetShelterEmployees(id);
          return employees == default(IEnumerable<Employee>) ? (IActionResult)NotFound() : Ok(employees);
        }

        /// <summary>
        /// Add a MANAGER (employee) to a shelter.
        /// </summary>
        /// <param name="shelterId">The ID of the shelter that the manager belongs to</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "name": "John Doe",
        ///        "licensedManager": true
        ///     }
        ///
        /// </remarks>
        [HttpPost("{shelterId}/employees/manager")]
        public IActionResult AddManager(string shelterId, [FromBody]Shelter.shared.Manager manager)
        {
          manager.ShelterId = shelterId;
          manager = _dataAccess.AddManager(shelterId, manager);
          return Ok(manager);
        }

        /// <summary>
        /// Add a CARETAKER (employee) to a shelter.
        /// </summary>
        /// <param name="shelterId">The ID of the shelter that the caretaker belongs to</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "name": "Jane Doe",
        ///        "fixedContract": false
        ///     }
        ///
        /// </remarks>
        [HttpPost("{shelterId}/employees/caretaker")]
        public IActionResult AddCaretaker(string shelterId, [FromBody]Shelter.shared.Caretaker caretaker)
        {
          caretaker.ShelterId = shelterId;
          caretaker = _dataAccess.AddCaretaker(shelterId, caretaker);
          return Ok(caretaker);
        }

        /// <summary>
        /// Add an ADMINISTRATOR (employee) to a shelter.
        /// </summary>
        /// <param name="shelterId">The ID of the shelter that the administrator belongs to</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "name": "Marcel Cyfer",
        ///        "digitalAdministration": true
        ///     }
        ///
        /// </remarks>
        [HttpPost("{shelterId}/employees/administrator")]
        public IActionResult AddAdministrator(string shelterId, [FromBody]Shelter.shared.Administrator administrator)
        {
          administrator.ShelterId = shelterId;
          administrator = _dataAccess.AddAdministrator(shelterId, administrator);
          return Ok(administrator);
        }


        /// <summary>
        /// Update an employee of a specific shelter.
        /// </summary>
        /// <param name="shelterId">The ID of the shelter that the animal belongs to</param>
        /// <param name="employeeId">The ID of the employee that needs to be updated</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "name": "Jane Doe"
        ///     }
        ///
        /// </remarks>
        [HttpPut("{shelterId}/employees/{employeeId}")]
        public IActionResult UpdateEmployee(string shelterId, string employeeId, [FromBody]Shelter.shared.Employee employee)
        {
          employee.Id = employeeId;
          employee.ShelterId = shelterId;
          _dataAccess.UpdateEmployee(shelterId, employeeId, employee);
          return Ok(employee);
        }

        /// <summary>
        /// Delete an employee of a specific shelter.
        /// </summary>
        /// <param name="shelterId">The ID of the shelter that the animal belongs to</param>
        /// <param name="employeeId">The ID of the employee that needs to be deleted</param>
        [HttpDelete("{shelterId}/employees/{employeeId}")]
        public IActionResult DeleteEmployee(string shelterId, string employeeId)
        {
          _dataAccess.DeleteEmployee(shelterId, employeeId);
          return Ok("Deleted Employee");
        }

    }
}