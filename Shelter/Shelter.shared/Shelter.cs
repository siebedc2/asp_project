using System;
using System.Collections.Generic;
using MongoDB.Driver;
namespace Shelter.shared{
public class Shelter : BaseDbClass {        
        public string Name {get; set;}
        public ICollection<Employee> Employees {get; set;}
        public ICollection<Animal> Animals {get; set;}
    }
}