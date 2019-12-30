    
using System;
using System.Collections.Generic;


namespace Shelter.shared{
    public class Employee : BaseDbClass
    
    {// Begin Employee
        public string Name { get; set; }

        public int ShelterId { get; set; }
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