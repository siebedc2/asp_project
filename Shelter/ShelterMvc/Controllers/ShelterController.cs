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
   
    public class ShelterController : Controller
    {
        private readonly ILogger<ShelterController> _logger;

        public ShelterController(ILogger<ShelterController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            return View(ShelterIndexModel.Shelter);
        }

        public IActionResult Detail(int id)
        {
            var targetAnimal = ShelterIndexModel.Shelter.Animals.FirstOrDefault(x => x.Id == id);
            if (targetAnimal == default(Animal))
            {
                return NotFound();
            }
            return View(targetAnimal);
        }

        public IActionResult Edit(int id)
        {
            var targetAnimal = ShelterIndexModel.Shelter.Animals.FirstOrDefault(x => x.Id == id);
            if (targetAnimal == default(Animal))
            {
                return NotFound();
            }
            return View(targetAnimal);
        }

        [HttpPost]
        public IActionResult DoEdit(int id)
        {
            var targetAnimal = ShelterIndexModel.Shelter.Animals.FirstOrDefault(x => x.Id == id);
            if (targetAnimal == default(Animal))
            {
                return NotFound();
            }
            ShelterIndexModel.Shelter.Animals.Remove(targetAnimal);
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(int id)
        {
            var targetAnimal = ShelterIndexModel.Shelter.Animals.FirstOrDefault(x => x.Id == id);
            if (targetAnimal == default(Animal))
            {
                return NotFound();
            }
            return View(targetAnimal);
        }

        [HttpPost]
        public IActionResult DoDelete(int id)
        {
            var targetAnimal = ShelterIndexModel.Shelter.Animals.FirstOrDefault(x => x.Id == id);
            if (targetAnimal == default(Animal))
            {
                return NotFound();
            }
            ShelterIndexModel.Shelter.Animals.Remove(targetAnimal);
            return RedirectToAction(nameof(Index));

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
