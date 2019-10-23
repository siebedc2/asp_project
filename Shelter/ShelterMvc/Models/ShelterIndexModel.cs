using System;
using System.Collections.Generic;
using Shelter.shared;

namespace ShelterMvc.Models
{
    public class ShelterIndexModel
    {
        private static bool _isInitialized = false;
        private static Shelter.shared.Shelter _animal = null;

        private static void Initialize() {
            if (!_isInitialized) {
                var shelter = new Shelter.shared.Shelter() {
                    Animals = new List<Animal> {
                        new Dog() { Name = "Brutus", IsChecked = true, KidFriendly = true, Id = 1 , ShelterId = 1},
                        new Cat() { Name = "Minoes", IsChecked = true, KidFriendly = true, Id = 2 , ShelterId = 1}
                    }
                };

            shelter.Id = 1;
            shelter.Name = "Shelter1";

            _animal = shelter;
            _isInitialized = true;
            
            }

        }

        public static Shelter.shared.Shelter Shelter {
            get {
                Initialize();
                return _animal;
            }
        }
      
        
    }
}