using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PRG282_PROJECT.BusinessLayer
{
    class AnimalsValidation
    {
        public const string IdPrefix = "WR-";
        public const int MaxTextLength = 30;
        public static string ValidateInput(string id, string name, string species, string ageText, string scoreText,
            bool isNew, IEnumerable<Animal> existingAnimals,
            out int age, out int score)
        {
            age = 0;
            score = 0;

            if (string.IsNullOrWhiteSpace(id))
                return "Animal ID is required.";

            if (!id.StartsWith(IdPrefix, StringComparison.Ordinal))
                return "Animal ID must start with '" + IdPrefix + "' (capital letters).";

            if (id.Length == IdPrefix.Length)
                return "Animal ID needs something after '" + IdPrefix + "', e.g. WR-1030.";

            if (id.Contains("|"))
                return "Animal ID must not contain '|'.";

            if (isNew && existingAnimals != null &&
                existingAnimals.Any(a => a.AnimalID.Trim().Equals(id, StringComparison.OrdinalIgnoreCase)))
                return "An animal with ID '" + id + "' already exists.";

            string textError = ValidateText("Name", name);
            if (textError != null) return textError;

            textError = ValidateText("Species", species);
            if (textError != null) return textError;

            // NumberStyles.None rejects decimals, signs, spaces and letters (whole numbers only).
            if (!int.TryParse(ageText, NumberStyles.None, CultureInfo.InvariantCulture, out age) || age < 0 || age > 100)
            {
                age = 0;
                return "Age must be a whole number between 0 and 100.";
            }

            if (!int.TryParse(scoreText, NumberStyles.None, CultureInfo.InvariantCulture, out score) || score < 0 || score > 100)
            {
                score = 0;
                return "Recovery score must be a whole number between 0 and 100.";
            }

            return null;
        }

        private static string ValidateText(string label, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return label + " is required.";

            if (value.Length > MaxTextLength)
                return label + " must be " + MaxTextLength + " characters or fewer.";

            // '|' is the field separator in animals.txt, so it would corrupt the line.
            if (value.Contains("|"))
                return label + " must not contain '|'.";

            return null;
        }

        // Final safety net on a finished Animal object, just before it is written to the file.
        public static void ValidateAnimal(Animal animal)
        {
            if (animal == null)
                throw new ArgumentException("Animal details are required.");

            if (string.IsNullOrWhiteSpace(animal.AnimalID))
                throw new ArgumentException("Animal ID is required.");

            if (!animal.AnimalID.StartsWith(IdPrefix, StringComparison.Ordinal))
                throw new ArgumentException("Animal ID must start with '" + IdPrefix + "'.");

            if (animal.AnimalID.Contains("|"))
                throw new ArgumentException("Animal ID must not contain '|'.");

            string textError = ValidateText("Name", animal.Name);
            if (textError != null) throw new ArgumentException(textError);

            textError = ValidateText("Species", animal.Species);
            if (textError != null) throw new ArgumentException(textError);

            if (animal.Age < 0 || animal.Age > 100)
                throw new ArgumentException("Age must be between 0 and 100.");

            if (animal.RecoveryScore < 0 || animal.RecoveryScore > 100)
                throw new ArgumentException("Recovery score must be between 0 and 100.");
        }
    }
}
