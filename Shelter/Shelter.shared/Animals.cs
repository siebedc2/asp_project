using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace Shelter.shared {

    
    public class Animal : BaseDbClass {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsChecked { get; set; }
        public bool KidFriendly { get; set; }
        public int ShelterId { get; set; }
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