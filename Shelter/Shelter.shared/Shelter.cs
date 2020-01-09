using System;
using System.Collections.Generic;
using MongoDB.Driver;
namespace Shelter.shared{
public class Shelter : BaseDbClass {        
        public string Name {get; set;}
        public IMongoCollection<Employee> Employees {get; set;}
        public IMongoCollection<Animal> Animals {get; set;}
    }
}