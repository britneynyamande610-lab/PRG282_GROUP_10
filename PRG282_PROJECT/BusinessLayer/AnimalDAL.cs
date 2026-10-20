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
            int totalAnimals = animals.Count;
            double averageAge = totalAnimals > 0 ? animals.Average(a => a.Age) : 0.00;
            double averageScore = totalAnimals > 0 ? animals.Average(a => (double)a.RecoveryScore) : 0.00;

            string averageAgeText = totalAnimals > 0
                ? averageAge.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)
                : "N/A";
            string averageScoreText = totalAnimals > 0
                ? averageScore.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)
                : "N/A";

            StringBuilder report = new StringBuilder();
            report.AppendLine("BAOBAB RIDGE WILDLIFE REHABILITATION CENTRE");
            report.AppendLine("ANIMAL SUMMARY REPORT");
            report.AppendLine("Generated: " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            report.AppendLine("----------------------------------------");
            report.AppendLine("Total animals: " + totalAnimals);
            report.AppendLine("Average age (years): " + averageAgeText);
            report.AppendLine("Average recovery score: " + averageScoreText);
            report.AppendLine("----------------------------------------");
            report.AppendLine("Animals per status:");
            report.AppendLine(" Critical: " + animals.Count(a => a.HealthStatus.Equals("Critical", StringComparison.OrdinalIgnoreCase)));
            report.AppendLine(" Serious: " + animals.Count(a => a.HealthStatus.Equals("Serious", StringComparison.OrdinalIgnoreCase)));
            report.AppendLine(" Stable: " + animals.Count(a => a.HealthStatus.Equals("Stable", StringComparison.OrdinalIgnoreCase)));
            report.AppendLine(" Recovering: " + animals.Count(a => a.HealthStatus.Equals("Recovering", StringComparison.OrdinalIgnoreCase)));
            report.AppendLine(" Release-Ready: " + animals.Count(a => a.HealthStatus.Equals("Release-Ready", StringComparison.OrdinalIgnoreCase)));

            string entry = report.ToString() + Environment.NewLine + "----------------------------------------" + Environment.NewLine;
            if (append)
            {
                handler.AppendAllText("summary.txt", entry);
            }
            else
            {
                handler.WriteAllText("summary.txt", entry);
            }
        }
    }
}
