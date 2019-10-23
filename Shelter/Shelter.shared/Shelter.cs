using System;
using System.Collections.Generic;


namespace Shelter.shared{
public class Shelter {
        public int Id { get; set; }
        public string Name {get; set;}
        public ICollection<Employee> Employees {get; set;}
        public ICollection<Animal> Animals {get; set;}
    }
}