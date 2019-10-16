using System;
using System.Collections.Generic;


namespace Shelter.shared{
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