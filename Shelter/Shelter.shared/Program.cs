using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace Shelter.shared{
public class Shelters {
        public IMongoCollection<Shelter> SheltersList {get; set;}
    }
}