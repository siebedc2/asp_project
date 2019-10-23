using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShelterMvc.Models;
using Shelter.shared;

namespace ShelterMvc.Controllers
{
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;

        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
        }

        // GET api/shelter
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Id is X", "Sheltername is Y" };
        }

        // GET: api/shelter/{id}/animals
        [HttpGet("{id}")]
        public ActionResult<Animal> GetAnimals(int id)
        {
            var animal = ShelterIndexModel.Shelter.Animals.FirstOrDefault(x => x.Id == id);

            if (animal == null)
            {
                return null;
            }

            return animal;
        }


    }
}
