using System;
using System.Collections.Generic;
using Shelter.shared;

namespace ShelterMvc.Models
{
    public class ShelterIndexModel
    {
        public List<Animal> Animals { get; set; } //uses the Animal class from Shelter.shared namespace
    }
}