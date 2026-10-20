using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PRG282_PROJECT
{
    class AnimalDAL
    {
        private readonly string filePath;
        private readonly Datahandler handler = new Datahandler();

        public AnimalDAL(string filePath)
        {
            this.filePath = filePath;
        }

        public List<Animal> GetAllAnimals()
        {
            List<Animal> animals = new List<Animal>();

            if (!File.Exists(filePath))
            {
                return animals;
            }

            string[] lines = handler.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = line.Split('|');
                int age;
                int score;

                if (parts.Length >= 5 &&
                    int.TryParse(parts[3], out age) &&
                    int.TryParse(parts[4], out score) &&
                    score >= 0 && score <= 100)
                {
                    animals.Add(new Animal(parts[0], parts[1], parts[2], age, score));
                }
            }

            return animals;
        }

        public void InsertAnimal(Animal animal)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(animal.Display());
            }
        }

        public void DeleteAnimal(string animalID)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("animals.txt could not be found.");
            }

            string[] lines = handler.ReadAllLines(filePath);
            List<string> remainingLines = new List<string>();
            bool found = false;

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    remainingLines.Add(line);
                    continue;
                }

                string lineID = line.Split('|')[0].Trim();

                if (lineID.Equals(animalID, StringComparison.OrdinalIgnoreCase))
                {
                    found = true;
                }
                else
                {
                    remainingLines.Add(line);
                }
            }

            if (!found)
            {
                throw new Exception("Animal could not be found.");
            }

            handler.WriteAllLines(filePath, remainingLines);
        }

        public bool UpdateAnimal(Animal updated)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("animals.txt could not be found.");
            }

            string[] lines = handler.ReadAllLines(filePath);
            bool replaced = false;

            for (int i = 0; i < lines.Length; i++)
            {
                string lineID = lines[i].Split('|')[0].Trim();

                if (!replaced && lineID.Equals(updated.AnimalID, StringComparison.OrdinalIgnoreCase))
                {
                    lines[i] = updated.Display();
                    replaced = true;
                }
            }

            if (!replaced)
            {
                return false;
            }

            handler.WriteAllLines(filePath, lines);
            return true;
        }

        public void SaveSummary(List<Animal> animals, bool append)
        {
            string report = new SummaryReport(animals).ToText(DateTime.Now);
        
            if (append)
                handler.AppendAllText("summary.txt", report + "----------------------------------------" + Environment.NewLine);
            else
                handler.WriteAllText("summary.txt", report);
        }    
    }
}
