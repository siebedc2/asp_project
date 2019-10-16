﻿using System;
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
        public List<Animal> Animals {get; set;}
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
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsChecked { get; set; }
        public bool KidFriendly { get; set; }
    }

    public class Dog:Animal {
        public string Race { get; set; }
        public bool Declawed { get; set; }
    }

    public class Cat:Animal {
        public string Race { get; set; }
        public bool Barker { get; set; }
    }

    public class Other:Animal {
        public string Description { get; set; }
        public string Kind { get; set; }
    }
    // End Animal



}
