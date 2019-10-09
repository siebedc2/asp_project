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
            var Animals = new List<Animal>();
            Animals.Add(new Dog() { name = "Brutus", IsChecked = true, KidFriendly = true });
            Animals.Add(new Cat() { name = "Minoes", IsChecked = true, KidFriendly = true });
            return View(Animals);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
