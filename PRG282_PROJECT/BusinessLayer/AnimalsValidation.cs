using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG282_PROJECT.BusinessLayer
{
    class AnimalsValidation
    { 
            public static void ValidateAnimal(Animal animal)
            {
                if (animal == null)
                {
                    throw new ArgumentException("Animal details are required.");
                }

                if (string.IsNullOrEmpty(animal.AnimalID))
                {
                    throw new ArgumentException("Animal ID is required.");
                }

                if (!animal.AnimalID.StartsWith("WR-"))
                {
                    throw new ArgumentException("Animal ID must start with 'WR-'.");
                }

                if (animal.AnimalID.Contains("|"))
                {
                    throw new ArgumentException("Animal ID must not contain '|'.");
                }

                if (string.IsNullOrEmpty(animal.Name))
                {
                    throw new ArgumentException("Animal name is required.");
                }

                if (string.IsNullOrEmpty(animal.Species))
                {
                    throw new ArgumentException("Animal species is required.");
                }

                if (animal.Age < 0 || animal.Age > 100)
                {
                    throw new ArgumentException("Age must be between 0 and 100.");
                }

                if (animal.RecoveryScore < 0 || animal.RecoveryScore > 100)
                {
                    throw new ArgumentException("Recovery score must be between 0 and 100.");
                }
            }
        
    }
}

