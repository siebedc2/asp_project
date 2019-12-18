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
        
        [Route("")]
        public IActionResult GetAllShelters(){
            
            return Json(_dataAccess.GetAllShelters());
            
        }

        [Route("full")]
        public IActionResult GetAllSheltersFull(){
            return Json(_dataAccess.GetAllSheltersFull());
        }

        [Route("{id}")]
        public IActionResult GetShelter(int id){
            var shelter = _dataAccess.GetShelterById(id);
            return shelter == default(Shelter.shared.Shelter) ? (IActionResult)NotFound() : Ok(shelter);
        }

        [Route("{id}/animals")]
    public IActionResult GetShelterAnimals(int id)
    {
      var animals = _dataAccess.GetAnimals(id);
      return animals == default(IEnumerable<Animal>) ? (IActionResult)NotFound() : Ok(animals);
    }


    [Route("{shelterId}/animals/{animalId}")]
    public IActionResult GetAnimalDetails(int shelterId, int animalId)
    {
      var animal = _dataAccess.GetAnimalByShelterAndId(shelterId, animalId);
      return animal == default(Shelter.shared.Animal) ? (IActionResult)NotFound() : Ok(animal);
    }

    [HttpPut("{shelterId}/animals/{animalId}")]
    public IActionResult UpdateAnimal(int shelterId, int animalId, [FromBody]Shelter.shared.Animal animal)
    {
      return Ok(animal);
    }


    }
}