using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using PRG282_PROJECT.BusinessLayer;


namespace PRG282_PROJECT
{
    public partial class Form1 : Form

    {
        private string filePath = "animals.txt";
        private string editingID = null;

        private AnimalDAL animalDAL;

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
            {
                List<Animal> animalList = animalDAL.GetAllAnimals();

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
                string id = txtID.Text.Trim();
                string name = txtName.Text.Trim();
                string species = txtSpecies.Text.Trim();

                int age, score;
                string error = AnimalsValidation.ValidateInput(id, name, species,
                    txtAge.Text.Trim(), txtScore.Text.Trim(),
                    true, animalDAL.GetAllAnimals(), out age, out score);

                if (error != null)
                {
                    MessageBox.Show(error, "Validation Error");
                    return;
                }

                Animal newAnimal = new Animal(id, name, species, age, score);
                AnimalsValidation.ValidateAnimal(newAnimal);
                animalDAL.InsertAnimal(newAnimal);

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
            txtID.Clear();
            txtName.Clear();
            txtSpecies.Clear();
            txtAge.Clear();
            txtScore.Clear();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (editingID == null)
            {
                MessageBox.Show("Please load an animal for editing(click row).", "Error");
                return;
            }
            //trim before validation, txtID is read-only while editing so the stored editingID is used
            string name = txtName.Text.Trim();
            string species = txtSpecies.Text.Trim();

            int ages, scores;
            string error = AnimalsValidation.ValidateInput(editingID, name, species,
                txtAge.Text.Trim(), txtScore.Text.Trim(),
                false, null, out ages, out scores);

            if (error != null)
            {
                MessageBox.Show(error, "Validation Error");
                return;
            }
            //The constructor calls CalculateStatusAndHousing, which calculates the health status and housing based on the recovery score.
            Animal updated = new Animal(editingID, name, species, ages, scores);

            try
            {
                if (!animalDAL.UpdateAnimal(updated))
                {
                    MessageBox.Show("Could not find " + updated.AnimalID + " in animals.txt.", "Update Error");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving animal: " + ex.Message, "File Error");
                return;
            }

            MessageBox.Show("Animal updated successfully!\n\nStatus: " + updated.HealthStatus + "\nHousing Unit: " + updated.HousingUnit, "Success");
            ExitEditMode();
            LoadAnimalsData();
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

        private void btnSummary_Click_1(object sender, EventArgs e)
        {
            try
            {
                List<Animal> animals = dgvAnimals.DataSource as List<Animal> ?? new List<Animal>();
                animalDAL.SaveSummary(animals, false);
                MessageBox.Show("Summary report generated and saved to summary.txt successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating summary report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            try
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

                DialogResult result = MessageBox.Show(
                    "Delete " + selectedAnimal.AnimalID + " (" + selectedAnimal.Name + ")? This cannot be undone.",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    animalDAL.DeleteAnimal(selectedAnimal.AnimalID);
                    MessageBox.Show("Animal deleted successfully!", "Success");
                    ExitEditMode();
                    LoadAnimalsData();
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "File Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting animal: " + ex.Message, "Error");
            }
        }

    }
}
