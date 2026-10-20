using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG282_PROJECT
{
    class Animal
    {
        public string AnimalID { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }
        public int RecoveryScore { get; set; }
        public string HealthStatus { get; private set; }
        public string HousingUnit { get; private set; }

        public Animal() { }

       
        public Animal(string animalID, string name, string species, int age, int recoveryScore)
        {
            this.AnimalID = animalID;
            this.Name = name;
            this.Species = species;
            this.Age = age;
            CalculateStatusAndHousing(recoveryScore);
        }

      
        public Animal(string animalID, string name, string species, int age, int recoveryScore, string healthStatus, string housingUnit)
        {
            this.AnimalID = animalID;
            this.Name = name;
            this.Species = species;
            this.Age = age;
            this.RecoveryScore = recoveryScore;
            this.HealthStatus = healthStatus;
            this.HousingUnit = housingUnit;

          
        }

        public void CalculateStatusAndHousing(int score)
        {
            RecoveryScore = score;

            if (score >= 80 && score <= 100)
            {
                HealthStatus = "Release-Ready";
                HousingUnit = "Pre-Release Camp";
            }
            else if (score >= 60 && score <= 79)
            {
                HealthStatus = "Recovering";
                HousingUnit = "Outdoor Enclosure";
            }
            else if (score >= 40 && score <= 59)
            {
                HealthStatus = "Stable";
                HousingUnit = "Recovery Ward";
            }
            else if (score >= 20 && score <= 39)
            {
                HealthStatus = "Serious";
                HousingUnit = "High-Dependency Ward";
            }
            else if (score >= 0 && score <= 19)
            {
                HealthStatus = "Critical";
                HousingUnit = "Intensive Care Unit";
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(score), "Recovery score has to be between 0 and 100.");
            }
        }

        public string Display()
        {
            return $"{AnimalID}|{Name}|{Species}|{Age}|{RecoveryScore}|{HealthStatus}|{HousingUnit}";
        }
    }
}
