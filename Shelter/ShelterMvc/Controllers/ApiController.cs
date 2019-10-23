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
    public class ApiController : Controller
    {
        private readonly ILogger<ApiController> _logger;
        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Brewery(){
            
            return Json(new {foo="bar", baz="Blech"});
        }
        [HttpPost]
        public IActionResult Post(){
            
            return Json(new {foo="post", baz="Blech"});
        }
    }
}