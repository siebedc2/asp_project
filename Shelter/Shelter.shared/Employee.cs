    
using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;


namespace Shelter.shared{
    [BsonKnownTypes(typeof(Manager), typeof(Caretaker), typeof(Administrator))]
    public class Employee : BaseDbClass
    
    {// Begin Employee
        public string Name { get; set; }

        public string ShelterId { get; set; }
    }
    public class Manager:Employee {
        // Properties manager
        public bool LicensedManager { get; set; }
    }

    public class Caretaker:Employee {
        // Properties caretaker
        public bool FixedContract { get; set; }
    }

    public class Administrator:Employee {
        // Properties administrator
        public bool DigitalAdministration { get; set; }
    }
    // End Employee
}