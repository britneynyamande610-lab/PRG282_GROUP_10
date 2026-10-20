using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Globalization;


namespace PRG282_PROJECT
{
    public partial class Form1 : Form

    {
        private string filePath = "animals.txt";
        private string editingID = null; // Holds the ID of the animal that is being edited
        public Form1()
        {
            InitializeComponent();
             animalDAL = new AnimalDAL(filePath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            LoadAnimalsData();
        }


        private void LoadAnimalsData()
        {
            try
            {  List<Animal> animalList = animalDAL.GetAllAnimals();

                dgvAnimals.DataSource = null;
                dgvAnimals.DataSource = animalList;
            
            }
            catch(Exception ex)
            {
            MessageBox.Show("Error reading file: " + ex.Message, "Error");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

    try
    {

                string id = txtID.Text;
                string name = txtName.Text;
                string species = txtSpecies.Text;
                string ageText = txtAge.Text;
                string scoreText = txtScore.Text;
     if (string.IsNullOrEmpty(id))
    {
        MessageBox.Show("ID is required.", "Validation Error");
        return;
    }

    if (!id.StartsWith("WR-"))
    {
        MessageBox.Show("ID must start with 'WR-'.", "Validation Error");
        return;
    }

    if (id.Contains("|"))
    {
        MessageBox.Show("ID must not contain '|'.", "Validation Error");
        return;
    }

            int score;
            if (int.TryParse(scoreText, out score))
        {
            if (score < 0 || score > 100)
            {
            MessageBox.Show("Score must be between 0 and 100.", "Validation Error");
            return;
            }
            }
            else
            {
            MessageBox.Show("Score must be a whole number.", "Validation Error");
            return;
            }

            int age;
            if (int.TryParse(ageText, out age))
            {
            if (age < 0 || age > 100)
            {
            MessageBox.Show("Age must a number between 0 and 100", "Validation Error");
            return;
            }
            }
            else
            {
            MessageBox.Show("Age must be a whole number.", "Validation Error");
            return;
            }

            Animal newAnimal = new Animal(id, name, species, age, score);
            animalDAL.InsertAnimal(newAnimal);

            MessageBox.Show("Animal added successfully!", "Success");
            ClearInputs();
            LoadAnimalsData();
            }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error writing to file: " + ex.Message, "Error");
            }
        }
        //clears all textboxes 
        private void ClearInputs()
        {
            txtID.Clear();
            txtName.Clear();
            txtSpecies.Clear();
            txtAge.Clear();
            txtScore.Clear();
        }

        //Values are validated, recover score is recalculated and updated info is only rewritten in the animal's line im animals.txt
        //btnSaveChanges
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (editingID == null)
            {
                MessageBox.Show("Please load an animal for editing(click row).", "Error");
                return;
            }
            //trim before validation, textID not included as it is read-only and not editable
            string name = txtName.Text.Trim();
            string species = txtSpecies.Text.Trim();
            string age = txtAge.Text.Trim();
            string score = txtScore.Text.Trim();

            if (name.Length < 1 || name.Length > 30 || name.Contains("|"))
            {
                MessageBox.Show("Name is required, must be between 1 and 30 characters and not contain '|'.", "Validation Error");

                txtName.Focus();
                return;
            }

            if (species.Length < 1 || species.Length > 30 || species.Contains("|"))
            {
                MessageBox.Show("Species is required, must be between 1 and 30 characters and not contain '|'.", "Validation Error");
                txtSpecies.Focus();
                return;
            }

            //NumbersStyles.None rejects decimals, signs and letters(whole numbers only)
            if(!int.TryParse(age, out int ages) || ages < 0 || ages > 100)
            {
                MessageBox.Show("Age must be a whole number between 0 and 100.", "Validation Error");
                txtAge.Focus();
                return;
            }

            if (!int.TryParse(score, out int scores) || scores < 0 || scores > 100)
            {
                MessageBox.Show("Recovery Score must be a whole number between 0 and 100.", "Validation Error");
                txtScore.Focus();
                return;
            }
            //The constructor calls CalculateStatusAndHousing, which calculates the health status and housing based on the recovery score.
            Animal updated = new Animal(editingID, name, species, ages, scores);

            if (!ReplaceAnimalInFile(updated))
            {
                return;//error message already shown in ReplaceAnimalInFile
            }

            MessageBox.Show("Animal updated successfully!\n\nStatus: " + updated.HealthStatus + "\nHousing Unit: " + updated.HousingUnit, "Success");
            ExitEditMode();
            LoadAnimalsData();
        }

        //Rewrites animals.txt and replaces only the animal that needs to be updated
        private bool ReplaceAnimalInFile(Animal updated)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
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
                    MessageBox.Show("Could not find " + updated.AnimalID + "in animals,txt.", "Update Error");
                    return false;
                }
                //Writes all lines to animals.txt without a BOM(byte order mark)                          
                File.WriteAllLines(filePath, lines, new UTF8Encoding(false));
                //BOM is a small amount of invisible bytes of data that indicates the encoding of a text file.
                //We make the encoding false to avoid adding a BOM to the start of the file
                return true;

            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("animals.txt could not be found, changes were not saved.", "File Error");
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("The folder containing animals.txt could not be found, changes were not saved.", "File Error");
            }
            catch (IOException)
            {
                MessageBox.Show("Could not save file because it is open in another application.", "File Error");
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("You do not have permission to change animals.txt, changes were not saved.", "File Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred while saving: " + ex.Message, "Error");
            }
            return false;
        }


        //Load the details of the selected animal into the text boxes
        private void dgvAnimals_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;  //ignore header row clicks
            }

            Animal clicked = dgvAnimals.Rows[e.RowIndex].DataBoundItem as Animal;
            if (clicked != null)
            {
                LoadAnimalIntoForm(clicked);
            }
        }

        //Puts an animal's details into the text boxes for editing
        private void LoadAnimalIntoForm(Animal animal)
        {
            txtID.Text = animal.AnimalID;
            txtName.Text = animal.Name;
            txtSpecies.Text = animal.Species;
            txtAge.Text = animal.Age.ToString();
            txtScore.Text = animal.RecoveryScore.ToString();

            editingID = animal.AnimalID; // Stores the ID of the animal being edited
            SetEditMode(true);
        }

        //switches between edit mode. ID textbox is locked, add button is disabled and save changes button is enabled
        private void SetEditMode(bool editing)
        {
            txtID.ReadOnly = editing; // makes txtID read-only when editing
            btnAdd.Enabled = !editing;
            btnSave.Enabled = editing;

            if (!editing)
            {
                editingID = null;
            }
        }
        //All textboxes are cleared and returns to add mode
        private void btnClear_Click(object sender, EventArgs e)
        {
            ExitEditMode();
        }

        //All textboxes are cleared and returns to add mode
        private void ExitEditMode()
        {
            ClearInputs();
            txtSearch.Clear();
            SetEditMode(false);
        }

        //Finds the animal withe the matching ID and loads it into the textboxes
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchID = txtSearch.Text.Trim();

            if (searchID == "")
            {
                MessageBox.Show("Please type an Animal ID to search for.", "Search");
                txtSearch.Focus();
                return;
            }

            List<Animal> animals = dgvAnimals.DataSource as List<Animal>;
            Animal found = null;

            if (animals != null)
            {
                found = animals.FirstOrDefault(a => a.AnimalID.Trim().Equals(searchID, StringComparison.OrdinalIgnoreCase));
            }

            if (found == null)
            {
                MessageBox.Show("No animal with ID '" + searchID + "' was found.", "Search");
                return;
            }

            LoadAnimalIntoForm(found);

            //Highlights the mathing row in the grid
            foreach (DataGridViewRow row in dgvAnimals.Rows)
            {
                if (row.DataBoundItem == found)
                {
                    dgvAnimals.ClearSelection();
                    row.Selected = true;
                    break;
                }
            }
        }
    
    private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAnimals.CurrentRow == null || dgvAnimals.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Please select an animal to delete.", "Validation Error");
                return;
            }

            Animal selectedAnimal = dgvAnimals.CurrentRow.DataBoundItem as Animal;
            if (selectedAnimal == null)
            {
                MessageBox.Show("Please select a valid animal to delete.", "Validation Error");
                return; 
            }
            DialogResult result = MessageBox.Show($"Delete {selectedAnimal.AnimalID} ({selectedAnimal.Name})? This cannot be undone.","Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                if (DeleteAnimalFromFile(selectedAnimal.AnimalID))
                {
                    MessageBox.Show("Animal deleted successfully!", "Success");
                    ExitEditMode();
                    LoadAnimalsData();
                }
            }
         }
        private bool DeleteAnimalFromFile(string animalID)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("animals.txt could not be found.", "File Error");
                    return false;
                }

                List<string> lines = File.ReadAllLines(filePath).ToList();

                //Removing lines matching animalID
                lines.RemoveAll(line =>
                {
                    if (string.IsNullOrWhiteSpace(line)) return false;
                    string lineID = line.Split('|')[0].Trim();
                    return lineID.Equals(animalID, StringComparison.OrdinalIgnoreCase);
                });

                File.WriteAllLines(filePath, lines, new UTF8Encoding(false));
                return true;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("animals.txt could not be found, animal was not deleted.", "File Error");
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("The folder containing animals.txt could not be found, animal was not deleted", "File Error");
            }
            catch (IOException)
            {
                MessageBox.Show("Could not save file because it is open in another application.", "File Error");
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("You do not have permissions to modify animals.txt, animal was not deleted.", "File Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occured while deleting: " + ex.Message, "Error");
            }
            return false;
        }
    }

}
