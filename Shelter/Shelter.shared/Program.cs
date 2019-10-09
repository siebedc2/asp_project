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

            myShelter.Animals = new List<Animal>();
        }
    }
    
    public class Shelter {
        public string Name {get; set;}
        public ICollection<Employee> Employees {get; set;}
        public ICollection<Animal> Animals {get; set;}
    }

    // Begin Employee
    public abstract class Employee {
        public string Name { get; set; }
    }

    public class Manager:Employee {
        // Properties manager
    }

    public class Caretaker:Employee {
        // Properties caretaker
    }

    public class Administrator:Employee {
        // Properties administrator
    }
    // End Employee

    // Begin Animal
    public abstract class Animal {
        public string name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsChecked { get; set; }
        public bool KidFriendly { get; set; }
    }

    public class Dog:Animal {
        public string race { get; set; }
        public bool declawed { get; set; }
    }

    public class Cat:Animal {
        public string race { get; set; }
        public bool barker { get; set; }
    }

    public class Other:Animal {
        public string description { get; set; }
        public string kind { get; set; }
    }
    // End Animal



}
