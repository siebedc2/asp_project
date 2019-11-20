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

namespace ShelterMvc.Controllers
{
    [Route("/api/shelters")]
    public class ShelterApiController : Controller
    {
        private readonly ShelterContext _shelterContext;
        private readonly ILogger<ApiController> _logger;
        public ShelterApiController(ILogger<ShelterApiController> logger, ShelterContext shelterContext)
        {
            _shelterContext = shelterContext;
            _logger = logger;
        }
        
        [Route("")]
        public IActionResult GetAllShelters(){
            
            return Json(_shelterContext.Shelters);
            
        }

        [Route("full")]
        public IActionResult GetAllSheltersFull(){
            return Json(_shelterContext.Shelters
                .include(shelter => shelter.Animal)
                .include(shelter => shelter.Employee)
            );
        }

        [Route("{id}")]
        public IActionResult GetShelter(){
            var shelter = _shelterContext.Shelters.FirstOrDefault(x => x.Id == id);
            return shelter == default(Shelter.shared.Shelter) ? (IActionResult)NotFound() : Ok(shelter);
        }

        [Route("{id}/animals")]
    public IActionResult GetShelterAnimals(int id)
    {
      var shelter = _shelterContext.Shelters
        .Include(shelter => shelter.Animals)
        .FirstOrDefault(x => x.Id == id);
      return shelter == default(Shelter.shared.Shelter) ? (IActionResult)NotFound() : Ok(shelter.Animals);
    }


    [Route("{shelterId}/animals/{animalId}")]
    public IActionResult GetAnimalDetails(int shelterId, int animalId)
    {
      var animal = _shelterContext.Animals
        .FirstOrDefault(x => x.ShelterId == shelterId && x.Id == animalId);
      return animal == default(Shelter.shared.Animal) ? (IActionResult)NotFound() : Ok(animal);
    }


    }
}