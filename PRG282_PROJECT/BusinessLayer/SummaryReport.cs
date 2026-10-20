using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PRG282_PROJECT
{
    // Calculates the summary statistics and formats the report text.
    // It does no file I/O: AnimalDAL/Datahandler write the text to summary.txt.
    class SummaryReport
    {
        private const string Line = "----------------------------------------";

        public int TotalAnimals { get; private set; }
        public double AverageAge { get; private set; }
        public double AverageScore { get; private set; }
        public int Critical { get; private set; }
        public int Serious { get; private set; }
        public int Stable { get; private set; }
        public int Recovering { get; private set; }
        public int ReleaseReady { get; private set; }

        public SummaryReport(IEnumerable<Animal> animals)
        {
            List<Animal> list = animals == null ? new List<Animal>() : animals.ToList();

            TotalAnimals = list.Count;
            AverageAge = TotalAnimals > 0 ? list.Average(a => (double)a.Age) : 0.0;
            AverageScore = TotalAnimals > 0 ? list.Average(a => (double)a.RecoveryScore) : 0.0;

            Critical = CountStatus(list, "Critical");
            Serious = CountStatus(list, "Serious");
            Stable = CountStatus(list, "Stable");
            Recovering = CountStatus(list, "Recovering");
            ReleaseReady = CountStatus(list, "Release-Ready");
        }

        private static int CountStatus(List<Animal> list, string status)
        {
            return list.Count(a => status.Equals(a.HealthStatus, StringComparison.OrdinalIgnoreCase));
        }

        // "N/A" when there are no animals, otherwise 2 decimals with a full stop.
        public string AverageAgeText
        {
            get { return Format(AverageAge); }
        }

        public string AverageScoreText
        {
            get { return Format(AverageScore); }
        }

        private string Format(double value)
        {
            return TotalAnimals > 0 ? value.ToString("0.00", CultureInfo.InvariantCulture) : "N/A";
        }

        public string ToText(DateTime generated)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("BAOBAB RIDGE WILDLIFE REHABILITATION CENTRE");
            sb.AppendLine("ANIMAL SUMMARY REPORT");
            sb.AppendLine("Generated: " + generated.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture));
            sb.AppendLine(Line);
            sb.AppendLine("Total animals: " + TotalAnimals);
            sb.AppendLine("Average age (years): " + AverageAgeText);
            sb.AppendLine("Average recovery score: " + AverageScoreText);
            sb.AppendLine(Line);
            sb.AppendLine("Animals per status:");
            sb.AppendLine(" Critical: " + Critical);
            sb.AppendLine(" Serious: " + Serious);
            sb.AppendLine(" Stable: " + Stable);
            sb.AppendLine(" Recovering: " + Recovering);
            sb.AppendLine(" Release-Ready: " + ReleaseReady);
            return sb.ToString();
        }
    }
}
