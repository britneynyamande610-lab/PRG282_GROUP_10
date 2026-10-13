using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;


namespace PRG282_PROJECT
{
    public partial class Form1 : Form

    {
        private string filePath = "animals.txt";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
            LoadAnimalsData();
        }

        
        private void LoadAnimalsData()
        {
            List<Animal> animalList = new List<Animal>();
            int skippedCount = 0;

            try
            {
                if (!File.Exists(filePath))
                {
                    using (StreamWriter writer = new StreamWriter(filePath)) { }
                    MessageBox.Show("File missing. Created new animals.txt.", "Info");
                   
                }

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        string[] parts = line.Split('|');

                        if (parts.Length == 5 &&
                            int.TryParse(parts[3], out int age) &&
                            int.TryParse(parts[4], out int score) &&
                            score >= 0 && score <= 100)
                        {
                            Animal animal = new Animal(parts[0], parts[1], parts[2], age, score);
                            animalList.Add(animal);
                        }
                        else
                        {
                            skippedCount++;
                        }
                    }
                }

                if (skippedCount > 0)
                {
                    MessageBox.Show($"Skipped {skippedCount} invalid line(s).", "Warning");
                }

                dgvAnimals.DataSource = null;
                dgvAnimals.DataSource = animalList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading file: " + ex.Message, "Error");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            try
            {

                string id = textBox1.Text;
                string name = textBox2.Text;
                string species = textBox3.Text;
                string ageText = textBox4.Text;
                string scoreText = textBox5.Text;

                if (!id.StartsWith("WR-") || id.Contains("|"))
                {
                    MessageBox.Show("ID must start with 'WR-' and not contain '|'.", "Validation Error");
                    return;
                }

                if (!int.TryParse(scoreText, out int score) || score < 0 || score > 100)
                {
                    MessageBox.Show("Score must be between 0 and 100.", "Validation Error");
                    return;
                }

                if (!int.TryParse(ageText, out int age))
                {
                    MessageBox.Show("Age must be a valid integer.", "Validation Error");
                    return;
                }

                Animal newAnimal = new Animal(id, name, species, age, score);

                using (StreamWriter writer = new StreamWriter(filePath, append: true))
                {
                    writer.WriteLine($"{newAnimal.AnimalID}|{newAnimal.Name}|{newAnimal.Species}|{newAnimal.Age}|{newAnimal.RecoveryScore}");
                }

                MessageBox.Show("Animal added successfully!", "Success");

                ClearInputs();
                LoadAnimalsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error writing to file: " + ex.Message, "Error");
            }
        }

        private void ClearInputs()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }
    }
}
