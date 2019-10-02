using System;
using System.Collections.Generic;

namespace Shelter.shared
{
    class Program
    {
        static void Main(string[] args)
        {
            var myShelter = new Shelter()
            {
                Name = "World's best virtual animal shelter"
            };

            myShelter.Animals = new List<>(Animal);
        }
    }
    
    public class Shelter {
        public string Name {get; set;}
        public ICollection<Employee> Employees {get; set;}
        public ICollection<Animal> Animals {get; set;}
    }

    
}
