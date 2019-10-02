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


    public class Employee {
        public string Name { get; set; }
    }

    public class Animal {
        public string name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsChecked { get; set; }
        public bool KidFriendly { get; set; }
    }



}
