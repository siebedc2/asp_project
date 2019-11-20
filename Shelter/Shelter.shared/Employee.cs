    
using System;
using System.Collections.Generic;


namespace Shelter.shared{
    public abstract class Employee : BaseDbClass
    
    {// Begin Employee
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
}