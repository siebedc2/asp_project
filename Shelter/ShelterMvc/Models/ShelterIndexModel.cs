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
                var animal = new Shelter.shared.Shelter() {
                    Animals = new List<Animal> {
                        new Dog() { name = "Brutus", IsChecked = true, KidFriendly = true },
                        new Cat() { name = "Minoes", IsChecked = true, KidFriendly = true }
                    }
                };
            
            _animal = animal;
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