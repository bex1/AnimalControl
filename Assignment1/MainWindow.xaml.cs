using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Windows.Media.Animation;
using System.IO;

// Daniel Bäckström, 2014-11-05, Assignment 4
namespace Assignment4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static readonly string mammalContent = "Number of teeth:";
        private static readonly string reptileContent = "Number eggs laid:";
        private static readonly string gooseContent = "Number of horns:";
        private static readonly string zebraContent = "Number of stripes:";
        private static readonly string lizardContent = "Can loose tail(y/n):";
        private static readonly string snakeContent = "Is poisonous(y/n):";

        private Session session;

        // ***** Initialization section *****

        /// <summary>
        /// Initializes the mainwindow.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            InitializeGUI();

            AddKeyBoardHandlers();

            session = new Session();
        }

        /// <summary>
        /// Initializes the GUI.
        /// </summary>
        private void InitializeGUI()
        {
            // Show the window in the center of the screen.
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            // Add category items
            AddItemsToListBox(lboxCategory, Enum.GetValues(typeof(CategoryType)));
            // Add gender items
            AddItemsToListBox(lboxGender, Enum.GetValues(typeof(GenderType)));
        }

        /// <summary>
        /// Adds the specified items to the specified list box.
        /// </summary>
        /// <param name="listBox">The list box where the items should be added.</param>
        /// <param name="items">The items to add.</param>
        private void AddItemsToListBox(ListBox listBox, Array items)
        {
            foreach (var item in items)
            {
                listBox.Items.Add(item);
            }
        }

        private void AddKeyBoardHandlers()
        {
            AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)HandleKeyDownEvent);
        }

        // ***** Input section *****

        // ** File handling input

        /// <summary>
        /// Starts a new session.
        /// 
        /// If the user has made unsaved changes a dialog will be shown asking for confirmation to start a new session, 
        /// loosing the unsaved changes in the process, or to abort.
        /// </summary>
        private void NewSession()
        {
            if (session.UnsavedChanges)
            {
                // Create dialog
                UnsavedChangesDialog dialog = new UnsavedChangesDialog("There are unsaved changes.\nThe unsaved changes will be lost if a new session is started.");
                // Set to owner to center over mainwindow on show
                dialog.Owner = this;

                // Show the dialog
                if (dialog.ShowDialog() == true)
                {
                    // If user wants to abort just return
                    return;
                }
            }

            // Ok to start a new session...

            // Just loose the current session by creating new empty managers and clear all input controls by tree traversing
            session.NewSession();
            ClearChildrenControls(this);
        }


        /// <summary>
        /// Tries to shutdown the application.
        /// 
        /// If there are unsaved changes, a dialog will prompt before continuing, since unsaved changes will be lost.
        /// </summary>
        private void ShutDown()
        {
            if (session.UnsavedChanges)
            {
                // Create dialog
                UnsavedChangesDialog dialog = new UnsavedChangesDialog("There are unsaved changes.\nThe unsaved changes will be lost if you exit before savingS.");
                // Set to owner to center over mainwindow on show
                dialog.Owner = this;

                // Show the dialog
                if (dialog.ShowDialog() == true)
                {
                    // If user wants to abort just return
                    return;
                }
            }

            // Continue with shutdown
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Saves the current session to the active working file.
        /// </summary>
        private void SaveToFile()
        {
            try
            {
                BinSerializerUtility.BinaryFileSerialize(session, session.WorkingFilePath);
                session.UnsavedChanges = false;
            }
            catch (Exception ex)
            {
                OkDialog dialog = new OkDialog(ex.Message);
                dialog.Owner = this;
                dialog.ShowDialog();
            }
        }

        /// <summary>
        /// Opens a session from the specified filepath.
        /// All current unsaved changes will be lost.
        /// </summary>
        /// <param name="filePath">The file to read from.</param>
        private void OpenFromFile(string filePath)
        {
            try
            {
                session = BinSerializerUtility.BinaryFileDeSerialize<Session>(filePath);
                session.UnsavedChanges = false;
                ClearChildrenControls(this);
                UpdateListView(lviewAnimals, session.AnimalsManager);
                UpdateListView(lviewRecipeAnimals, session.AnimalsManager);
                UpdateListView(lviewRecipes, session.RecipesManager);
                UpdateListView(lviewAvailableRecipes, session.RecipesManager);
                UpdateListView(lviewStaff, session.StaffsManager);
            }
            catch (Exception ex)
            {
                OkDialog dialog = new OkDialog(ex.Message);
                dialog.Owner = this;
                dialog.ShowDialog();
            }
        }

        /// <summary>
        /// Imports recipes from the specifies xml file.
        /// This adds the recipes from the file to the current session and does not overwrite the recipes in the session.
        /// </summary>
        /// <param name="filePath">The path to recipe xml file.</param>
        private void ImportXMLRecipes(string filePath)
        {
            try
            {
                RecipeManager fileRecipes = XMLSerializerUtility.XMLFileDeSerialize<RecipeManager>(filePath); 
                for (int i = 0; i < fileRecipes.Count; ++i) {
                    session.RecipesManager.Add(fileRecipes.GetAt(i));
                }
                session.UnsavedChanges = true;
                ClearChildrenControls(tabAnimalRecipes);
                ClearChildrenControls(tabRecipes);
                UpdateListView(lviewRecipeAnimals, session.AnimalsManager);
                UpdateListView(lviewRecipes, session.RecipesManager);
                UpdateListView(lviewAvailableRecipes, session.RecipesManager);
            }
            catch (Exception ex)
            {
                OkDialog dialog = new OkDialog(ex.Message);
                dialog.Owner = this;
                dialog.ShowDialog();
            }
        }

        /// <summary>
        /// Exports recipes to the specified xml file.
        /// </summary>
        /// <param name="filePath">The path to write to.</param>
        private void ExportXMLRecipes(string filePath)
        {
            try
            {
                XMLSerializerUtility.XMLFileSerialize(session.RecipesManager, filePath);
            }
            catch (Exception ex)
            {
                OkDialog dialog = new OkDialog(ex.Message);
                dialog.Owner = this;
                dialog.ShowDialog();
            }
        }

        /// <summary>
        /// Imports staff from the specifies xml file.
        /// This adds the staff from the file to the current session and does not overwrite the staff in the session.
        /// </summary>
        /// <param name="filePath">The path to staff xml file.</param>
        private void ImportXMLStaff(string filePath)
        {
            try
            {
                StaffManager fileStaff = XMLSerializerUtility.XMLFileDeSerialize<StaffManager>(filePath);
                for (int i = 0; i < fileStaff.Count; ++i)
                {
                    session.StaffsManager.Add(fileStaff.GetAt(i));
                }
                session.UnsavedChanges = true;
                ClearChildrenControls(tabStaff);
                UpdateListView(lviewStaff, session.StaffsManager);
            }
            catch (Exception ex)
            {
                OkDialog dialog = new OkDialog(ex.Message);
                dialog.Owner = this;
                dialog.ShowDialog();
            }
        }

        /// <summary>
        /// Exports staff to the specified xml file.
        /// </summary>
        /// <param name="filePath">The path to write to.</param>
        private void ExportStaffRecipes(string filePath)
        {
            try
            {
                XMLSerializerUtility.XMLFileSerialize(session.StaffsManager, filePath);
            }
            catch (Exception ex)
            {
                OkDialog dialog = new OkDialog(ex.Message);
                dialog.Owner = this;
                dialog.ShowDialog();
            }
        }

        // ** Animal input

        /// <summary>
        /// Creates and adds the animal which is specified in the UI. 
        /// Then hands it over to the AnimalManager.
        /// </summary>
        private void AddAnimal()
        {
            try
            {
                session.AnimalsManager.Add(ReadAndValidateAnimalInput());
                UpdateListView(lviewAnimals, session.AnimalsManager);
                lviewAnimals.SelectedIndex = lviewAnimals.Items.Count - 1;
                session.UnsavedChanges = true;
            }
            catch (InvalidInputException ex)
            {
                HandleInvalidInputFrom(ex.getElement(), ex.Message);
            }
        }

        /// <summary>
        /// Reads and validates the input into the animal and if all input is validated returns the factorycreated animal.
        /// </summary>
        /// <returns>The animal created.</returns>
        /// <exception cref="InvalidInputException">Thrown if any input validation fails.</exception>
        private IAnimal ReadAndValidateAnimalInput()
        {
            IAnimal animal = null;

            CategoryType category = GetSelectedAnimalCategory();

            switch (category)
            {
                case CategoryType.Mammal:
                    MammalType mammalType = GetSelectedMammal();
                    animal = MammalFactory.CreateMammal(mammalType);
                    break;
                case CategoryType.Reptile:
                    ReptileType reptileType = GetSelectedReptile();
                    animal = ReptileFactory.CreateReptile(reptileType);
                    break;
            }

            animal.Name = ReadStringFromTextBox(txtName, "Name is not valid!");

            animal.Age = ReadUIntFromTextBox(txtAge, "Age is not valid!");

            animal.Gender = GetSelectedAnimalGender();

            if (animal is Mammal)
            {
                ((Mammal)animal).NumberOfTeeth = ReadUIntFromTextBox(txtCategorySpecific, "Number of teeth is not valid!");
                if (animal is Elk)
                {
                    ((Elk)animal).NumberHorns = ReadUIntFromTextBox(txtAnimalSpecific, "Number of horns is not valid!");
                }
                else if (animal is Zebra)
                {
                    ((Zebra)animal).NumberStripes = ReadUIntFromTextBox(txtAnimalSpecific, "Number of stripes is not valid!");
                }
            }
            else if (animal is Reptile)
            {
                ((Reptile)animal).NumberOfEggsLaid = ReadUIntFromTextBox(txtCategorySpecific, "Number of eggs laid is not valid!");
                if (animal is Lizard)
                {
                    ((Lizard)animal).CanDropTail = ReadBoolFromTextBox(txtAnimalSpecific, "Can the lizard drop the tail? Answer with (y) for yes or (n) for no.");
                }
                else if (animal is Snake)
                {
                    ((Snake)animal).IsPoisonous = ReadBoolFromTextBox(txtAnimalSpecific, "Is the snake poisonous? Answer with (y) for yes or (n) for no.");
                }
            }

            return animal;
        }

        /// <summary>
        /// Returns the selected category.
        /// 
        /// In the situation when one is listing all animals, the returned 
        /// <see cref="CategoryType"/> will be biased on the current selected animal instead.
        /// </summary>
        /// <returns>The selected <see cref="CategoryType"/>.</returns>
        /// <exception cref="InvalidInputException">Thrown if no category is selected.</exception>
        private CategoryType GetSelectedAnimalCategory()
        {
            if (cbxListAll.IsChecked.Value)
            {
                if (lboxAnimal.SelectedItem == null)
                {
                    throw new InvalidInputException("Select an animal!", lboxAnimal);
                }
                if (lboxAnimal.SelectedItem is MammalType)
                {
                    return CategoryType.Mammal;
                }
                else
                {
                    return CategoryType.Reptile;
                }
            }
            else
            {
                if (lboxCategory.SelectedItem == null)
                {
                    throw new InvalidInputException("Select a category!", lboxCategory);
                }
                return (CategoryType)lboxCategory.SelectedItem;
            }
        }

        /// <summary>
        /// Returns the selected <see cref="ReptileType"/>.
        /// </summary>
        /// <returns>The selected <see cref="ReptileType"/>.</returns>
        /// <exception cref="InvalidInputException">Thrown if no animal is selected.</exception>
        private ReptileType GetSelectedReptile()
        {
            if (lboxAnimal.SelectedItem == null)
            {
                throw new InvalidInputException("Select an animal!", lboxAnimal);
            }
            return (ReptileType)lboxAnimal.SelectedItem;
        }

        /// <summary>
        /// Returns the selected <see cref="MammalType"/>.
        /// </summary>
        /// <returns>The selected <see cref="MammalType"/>.</returns>
        /// <exception cref="InvalidInputException">Thrown if no animal is selected.</exception>
        private MammalType GetSelectedMammal()
        {
            if (lboxAnimal.SelectedItem == null)
            {
                throw new InvalidInputException("Select an animal!", lboxAnimal);
            }
            return (MammalType)lboxAnimal.SelectedItem;
        }

        /// <summary>
        /// Returns the selected <see cref="GenderType"/>.
        /// </summary>
        /// <returns>The selected <see cref="GenderType"/>.</returns>
        /// <exception cref="InvalidInputException">Thrown if no gender is selected.</exception>
        private GenderType GetSelectedAnimalGender()
        {
            if (lboxGender.SelectedItem == null)
            {
                throw new InvalidInputException("Select a gender!", lboxGender);
            }
            return (GenderType)lboxGender.SelectedItem;
        }

        /// <summary>
        /// Reads a string from the specified textbox and returns it if valid.
        /// </summary>
        /// <param name="txtBox">The textbox to read from.</param>
        /// <param name="failMessage">The message to show if the read string is invalid.</param>
        /// <returns>The valid string in the textbox.</returns>
        /// <exception cref="InvalidInputException">Thrown if the string is empty or consist solely of whitespace.</exception>
        private string ReadStringFromTextBox(TextBox txtBox, string failMessage)
        {
            string str = txtBox.Text.Trim();

            if (String.IsNullOrWhiteSpace(str)) // Do not allow whitespace names
            {
                throw new InvalidInputException(failMessage, txtBox);
            }

            return str;
        }

        /// <summary>
        /// Converts the content of the specified text box to an unsigned int and returns it.
        /// </summary>
        /// <param name="txtBox">The text box which the unsigned int is read from.</param>
        /// <param name="failMessage">The message passed with the possible throw of <see cref="InvalidInputException"/></param>
        /// <returns>Returns the content of the specified text box as an unsigned int.</returns>
        /// <exception cref="InvalidInputException">Thrown if the content in the specified text box cannot på converted to unsigned int.</exception>
        private uint ReadUIntFromTextBox(TextBox txtBox, string failMessage)
        {
            uint value = 0;

            bool success = uint.TryParse(txtBox.Text, out value); //true if ok

            if (!success)
            {
                throw new InvalidInputException(failMessage, txtBox);
            }

            return value;
        }

        /// <summary>
        /// Converts the content of the specified text box to a boolean and returns it.
        /// The character y corresponds to true and the character n corresponds to false. 
        /// </summary>
        /// <param name="txtBox">The text box which the boolean is read from.</param>
        /// <param name="failMessage">The message passed with the possible throw of <see cref="InvalidInputException"/></param>
        /// <returns>Returns the content of the specified text box as a boolean.</returns>
        /// <exception cref="InvalidInputException">Thrown if the content in the specified text box cannot på converted to boolean.</exception>
        private bool ReadBoolFromTextBox(TextBox txtBox, string failMessage)
        {

            if (txtBox.Text.Trim().Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if (txtBox.Text.Trim().Equals("n", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            else
            {
                throw new InvalidInputException(failMessage, txtBox);
            }
        }

        /// <summary>
        /// Handles invalid input from the specified input element.
        /// Shows a dialog and indicates the the element which contained the invalid input by playing an animation.
        /// 
        /// The user is also provided with the option to move the focus to the input control for easy editing. 
        /// </summary>
        /// <param name="element">The input control element.</param>
        /// <param name="errorMessage">The errormessage to show in the dialog.</param>
        private void HandleInvalidInputFrom(FrameworkElement element, string errorMessage)
        {
            // Create dialog
            InputErrorDialog dialog = new InputErrorDialog(errorMessage);
            Point mousePos = PointToScreen(Mouse.GetPosition(Application.Current.MainWindow));
            dialog.Left = mousePos.X - dialog.Width / 2;
            dialog.Top = mousePos.Y - dialog.Height / 2 - 40;

            // Play animation that indicates failed input control
            Storyboard s = (Storyboard)TryFindResource("blink");
            Storyboard.SetTarget(s, element);
            s.Begin();

            // Show the dialog
            if (dialog.ShowDialog() == true)
            {
                // If User pressed ok, we acquire focus for editing to the input control
                element.Focus();
            }

            // Stop the animation when user has decided
            s.Stop();
        }

        /// <summary>
        /// Deletes the selected animal.
        /// 
        /// If no animal is selected an error dialog will be shown.
        /// </summary>
        private void DeleteSelectedAnimal()
        {
            if (lviewAnimals.SelectedItem is IAnimal)
            {
                session.AnimalsManager.DeleteAt(lviewAnimals.SelectedIndex);
                UpdateListView(lviewAnimals, session.AnimalsManager);
                if (lviewAnimals.Items.Count > 0)
                {
                    lviewAnimals.SelectedIndex = 0;
                }
                session.UnsavedChanges = true;
            }
            else
            {
                HandleInvalidInputFrom(lviewAnimals, "No animal selected!");
            }
        }

        /// <summary>
        /// Applies the specified changes to the selected animal.
        /// 
        /// If the input is invalid or no animal is selected a error dialog is shown.
        /// </summary>
        private void ChangeSelectedAnimal()
        {
            if (lviewAnimals.SelectedItem is IAnimal)
            {
                int selectedIndex = lviewAnimals.SelectedIndex;
                try
                {
                    session.AnimalsManager.ChangeAt(ReadAndValidateAnimalInput(), selectedIndex);
                }
                catch (InvalidInputException ex)
                {
                    HandleInvalidInputFrom(ex.getElement(), ex.Message);
                }
                UpdateListView(lviewAnimals, session.AnimalsManager);
                lviewAnimals.SelectedIndex = selectedIndex;
                session.UnsavedChanges = true;
            }
            else
            {
                HandleInvalidInputFrom(lviewAnimals, "No animal selected!");
            }
        }

        // ** Recipe input

        /// <summary>
        /// Clears all recipe related input controls.
        /// </summary>
        private void ClearAllRecipeControls()
        {
            lviewRecipes.UnselectAll();
            lviewIngredients.Items.Clear();
            txtRecipeName.Clear();
            txtIngredient.Clear();
            txtRecipeName.Focus();
        }

        /// <summary>
        /// Adds a recipe to the list of recipe if the input is valid.
        /// 
        /// If not valid input, a error dialog is shown.
        /// </summary>
        private void AddRecipe()
        {
            try
            {
                session.RecipesManager.Add(ReadAndValidateRecipeInput());
                UpdateListView(lviewRecipes, session.RecipesManager);
                int selectedIngredientIndex = lviewIngredients.SelectedIndex;
                lviewRecipes.SelectedIndex = lviewRecipes.Items.Count - 1;
                if (selectedIngredientIndex > -1)
                {
                    lviewIngredients.SelectedIndex = selectedIngredientIndex;
                }
                session.UnsavedChanges = true;
            }
            catch (InvalidInputException ex)
            {
                HandleInvalidInputFrom(ex.getElement(), ex.Message);
            }
        }

        /// <summary>
        /// Deletes the selected recipe.
        /// 
        /// Shows error dialog if no recipe is selected.
        /// </summary>
        private void DeleteSelectedRecipe()
        {
            if (lviewRecipes.SelectedItem is Recipe)
            {
                session.RecipesManager.DeleteAt(lviewRecipes.SelectedIndex);
                UpdateListView(lviewRecipes, session.RecipesManager);
                if (lviewRecipes.Items.Count > 0)
                {
                    lviewRecipes.SelectedIndex = 0;
                }
                else
                {
                    txtRecipeName.Clear();
                }
                session.UnsavedChanges = true;
            }
            else
            {
                HandleInvalidInputFrom(lviewRecipes, "No recipe selected!");
            }
        }

        /// <summary>
        /// Applies the specified changes to the selected recipe.
        /// 
        /// Shows a error dialog if no recipe is selected or the input is invalid.
        /// </summary>
        private void ApplyRecipeChanges()
        {
            if (lviewRecipes.SelectedItem is Recipe)
            {
                try
                {
                    int selectedIndex = lviewRecipes.SelectedIndex;
                    session.RecipesManager.ChangeAt(ReadAndValidateRecipeInput(), selectedIndex);
                    UpdateListView(lviewRecipes, session.RecipesManager);
                    lviewRecipes.SelectedIndex = selectedIndex;
                    session.UnsavedChanges = true;
                }
                catch (InvalidInputException ex)
                {
                    HandleInvalidInputFrom(ex.getElement(), ex.Message);
                }
            }
        }

        /// <summary>
        /// Reads the input controls and returns a new recipe according to the input.
        /// </summary>
        /// <returns>Returns the new recipe.</returns>
        /// <exception cref="InvalidInputException">Thrown if the the recipe input is invalid.</exception>
        private Recipe ReadAndValidateRecipeInput()
        {
            Recipe recipe = new Recipe();

            recipe.Name = ReadStringFromTextBox(txtRecipeName, "No recipe name specified.\nPlease specify a name.");

            GetIngredients(recipe);

            return recipe;
        }

        /// <summary>
        /// Assigns the ingredients specified in the UI to the specified recipe.
        /// </summary>
        /// <param name="recipe">The recipe which the ingrdients should be addded to.</param>
        /// <exception cref="InvalidInputException">Thrown if no ingredients are added.</exception>
        private void GetIngredients(Recipe recipe)
        {
            if (lviewIngredients.Items.IsEmpty)
            {
                throw new InvalidInputException("No ingredients added.\nPlease add ingredients first.", txtIngredient);
            }

            for (int i = 0; i < lviewIngredients.Items.Count; ++i) {
                recipe.Ingredients.Add((string)lviewIngredients.Items[i]);
            }
        }

        // ** Ingredient input

        /// <summary>
        /// Adds the specified ingredient to the ingredient list.
        /// 
        /// An error dialog is shown if the validation of the ingredient input fails.
        /// </summary>
        private void AddIngredient()
        {
            try
            {
                lviewIngredients.Items.Add(ReadAndValidateIngredientInput());
                lviewIngredients.SelectedIndex = lviewIngredients.Items.Count - 1;
            }
            catch (InvalidInputException ex)
            {
                HandleInvalidInputFrom(ex.getElement(), ex.Message);
            }

        }

        /// <summary>
        /// Changes the selected ingredient to the specified one in the ingredient textbox.
        /// 
        /// An error dialog is shown if the validation of the ingredient input fails or if no ingredient is selected.
        /// </summary>
        private void ChangeSelectedIngredient()
        {
            if (lviewIngredients.SelectedItem is string)
            {
                try
                {
                    lviewIngredients.Items[lviewIngredients.SelectedIndex] = ReadAndValidateIngredientInput();
                }
                catch (InvalidInputException ex)
                {
                    HandleInvalidInputFrom(ex.getElement(), ex.Message);
                }
            }
            else
            {
                HandleInvalidInputFrom(lviewIngredients, "No ingredient selected!");
            }
        }

        /// <summary>
        /// Deletes the selected ingredient.
        /// 
        /// An error dialog is shown if no ingredient is selected.
        /// </summary>
        private void DeleteSelectedIngredient()
        {
            if (lviewIngredients.SelectedItem is string)
            {
                lviewIngredients.Items.RemoveAt(lviewIngredients.SelectedIndex);
                if (lviewIngredients.Items.Count > 0)
                {
                    lviewIngredients.SelectedIndex = 0;
                }
            }
            else
            {
                HandleInvalidInputFrom(lviewIngredients, "No ingredient selected!");
            }
        }

        /// <summary>
        /// Reads and validates the ingredient input and returns the input.
        /// </summary>
        /// <returns>The ingredient presented as a string.</returns>
        /// <exception cref="InvalidInputException">Thrown if the input is invalid.</exception>
        private string ReadAndValidateIngredientInput()
        {
            string name = ReadStringFromTextBox(txtIngredient, "Please specify the ingredient.");
            return name;
        }

        // ** Animal Recipes input

        /// <summary>
        /// Associates the selected avaible recipe with the selected animal.
        /// 
        /// An error dialog is shown if there is no animal or recipe selected, or if the recipe is already associated with the animal.
        /// </summary>
        private void AssociateRecipeWithSelectedAnimal()
        {
            if (lviewRecipeAnimals.SelectedItem is IAnimal)
            {
                if (lviewAvailableRecipes.SelectedItem is Recipe)
                {
                    string animalID = ((IAnimal)lviewRecipeAnimals.SelectedItem).ID;
                    Recipe recipe = (Recipe)lviewAvailableRecipes.SelectedItem;
                    if (session.AnimalRecipeManager.IsKeyPresent(animalID))
                    {
                        List<Recipe> recipes = session.AnimalRecipeManager.GetRecipes(animalID);
                            recipes.Add(recipe);
                            lviewAnimalRecipes.Items.Add(recipe);
                    }
                    else
                    {
                        session.AnimalRecipeManager.Add(animalID, new List<Recipe> { recipe });
                        lviewAnimalRecipes.Items.Add(recipe);
                    }
                    session.UnsavedChanges = true;
                }
                else
                {
                    HandleInvalidInputFrom(lviewAvailableRecipes, "No recipe selected to add to the selected animal! Please select a recipe.");
                }
            }
            else
            {
                HandleInvalidInputFrom(lviewRecipeAnimals, "No animal selected! Please select an animal to add the recipe to!");
            }
        }

        /// <summary>
        /// Removes the association of the selected recipe with the selected animal.
        /// 
        /// An error dialog is shown if no recipe is selected for removal.
        /// </summary>
        private void UnAssociateRecipeWithSelectedAnimal()
        {
            if (lviewAnimalRecipes.SelectedItem is Recipe)
            {
                string animalID = ((IAnimal)lviewRecipeAnimals.SelectedItem).ID;
                int recipeIndex = lviewAnimalRecipes.SelectedIndex;
                if (session.AnimalRecipeManager.IsKeyPresent(animalID))
                {
                    session.AnimalRecipeManager.GetRecipes(animalID).RemoveAt(recipeIndex);
                }
                lviewAnimalRecipes.Items.RemoveAt(recipeIndex);
                if (lviewAnimalRecipes.Items.Count > 0) {
                    lviewAnimalRecipes.SelectedIndex = 0;
                }
                session.UnsavedChanges = true;
            }
            else
            {
                HandleInvalidInputFrom(lviewAnimalRecipes, "No recipe selected to delete from the animal. Select one and then delete.");
            }
        }


        // ** Staff input

        /// <summary>
        /// Clears all staff input controls.
        /// </summary>
        private void ClearStaffControls()
        {
            lviewStaff.UnselectAll();
            lviewQualifications.Items.Clear();
            txtStaffName.Clear();
            txtQualification.Clear();
            txtStaffName.Focus();
        }

        /// <summary>
        /// Adds a staff member to the list of staffmembers by reading and validating the UI input.
        /// 
        /// An error dialog is shown if the validation of the UI input fails.
        /// </summary>
        private void AddStaff()
        {
            try
            {
                session.StaffsManager.Add(ReadAndValidateStaffInput());
                UpdateListView(lviewStaff, session.StaffsManager);
                int selectedQualificationIndex = lviewQualifications.SelectedIndex;
                lviewStaff.SelectedIndex = lviewStaff.Items.Count - 1;
                if (selectedQualificationIndex > -1)
                {
                    lviewQualifications.SelectedIndex = selectedQualificationIndex;
                }
                session.UnsavedChanges = true;
            }
            catch (InvalidInputException ex)
            {
                HandleInvalidInputFrom(ex.getElement(), ex.Message);
            }
        }

        /// <summary>
        /// Deletes the selected staff member.
        /// 
        /// An error dialog is shown if no staff member is selected.
        /// </summary>
        private void DeleteSelectedStaff()
        {
            if (lviewStaff.SelectedItem is Staff)
            {
                session.StaffsManager.DeleteAt(lviewStaff.SelectedIndex);
                UpdateListView(lviewStaff, session.StaffsManager);
                if (lviewStaff.Items.Count > 0)
                {
                    lviewStaff.SelectedIndex = 0;
                }
                else
                {
                    txtStaffName.Clear();
                }
                session.UnsavedChanges = true;
            }
            else
            {
                HandleInvalidInputFrom(lviewStaff, "No staff selected!");
            }
        }

        /// <summary>
        /// Reads and validates the UI staff input into a new staff member and returns it.
        /// </summary>
        /// <returns>The ui initialized staff member.</returns>
        /// <exception cref="InvalidInputException">Thrown if the staff input is invalid.</exception>
        private Staff ReadAndValidateStaffInput()
        {
            Staff staff = new Staff();

            staff.Name = ReadStringFromTextBox(txtStaffName, "No staff name specified.\nPlease specify a name.");

            GetQualifications(staff);

            return staff;
        }

        /// <summary>
        /// Reads the listed qualifications from the ui and saves the data into the specified staff member.
        /// </summary>
        /// <param name="staff">The staff member to assign the qualifications to.</param>
        /// <exception cref="InvalidInputException">Thrown if no qualifications are added.</exception>
        private void GetQualifications(Staff staff)
        {
            if (lviewQualifications.Items.IsEmpty)
            {
                throw new InvalidInputException("No qualifications added.\nPlease add ingredients first.", txtQualification);
            }

            for (int i = 0; i < lviewQualifications.Items.Count; ++i)
            {
                staff.Qualifications.Add((string)lviewQualifications.Items[i]);
            }
        }

        /// <summary>
        /// Applies the specified staff changes in the ui to the selected staff member.
        /// 
        /// An error dialog is shown if the input validation fails.
        /// </summary>
        private void ApplyStaffChanges()
        {
            if (lviewStaff.SelectedItem is Staff)
            {
                try
                {
                    int selectedIndex = lviewStaff.SelectedIndex;
                    session.StaffsManager.ChangeAt(ReadAndValidateStaffInput(), selectedIndex);
                    UpdateListView(lviewStaff, session.StaffsManager);
                    lviewStaff.SelectedIndex = selectedIndex;
                    session.UnsavedChanges = true;
                }
                catch (InvalidInputException ex)
                {
                    HandleInvalidInputFrom(ex.getElement(), ex.Message);
                }
            }
        }

        // ** Qualification input

        /// <summary>
        /// Adds a qualification to the list of qualifications.
        /// 
        /// An error dialog is shown if the input validation of the qualification fails.
        /// </summary>
        private void AddQualification()
        {
            try
            {
                lviewQualifications.Items.Add(ReadAndValidateQualificationInput());
                lviewQualifications.SelectedIndex = lviewQualifications.Items.Count - 1;
            }
            catch (InvalidInputException ex)
            {
                HandleInvalidInputFrom(ex.getElement(), ex.Message);
            }
        }

        /// <summary>
        /// Applies the specified changes to the selected qualification
        /// 
        /// An error dialog is shown if the input validation of the qualification fails or if no qualification is selected.
        /// </summary>
        private void ChangeSelectedQualification()
        {
            if (lviewQualifications.SelectedItem is string)
            {
                try
                {
                    lviewQualifications.Items[lviewQualifications.SelectedIndex] = ReadAndValidateQualificationInput();
                }
                catch (InvalidInputException ex)
                {
                    HandleInvalidInputFrom(ex.getElement(), ex.Message);
                }
            }
            else
            {
                HandleInvalidInputFrom(lviewQualifications, "No qualification selected!");
            }
        }

        /// <summary>
        /// Deletes the selected qualification.
        /// 
        /// An error dialog is shown if no qualification is selected.
        /// </summary>
        private void DeleteSelectedQualification()
        {
            if (lviewQualifications.SelectedItem is string)
            {
                lviewQualifications.Items.RemoveAt(lviewQualifications.SelectedIndex);
                if (lviewQualifications.Items.Count > 0)
                {
                    lviewQualifications.SelectedIndex = 0;
                }
            }
            else
            {
                HandleInvalidInputFrom(lviewQualifications, "No qualification selected!");
            }
        }

        /// <summary>
        /// Reads and validates the qualification input and returns the read qualifation as a string.
        /// </summary>
        /// <returns>The qualification.</returns>
        /// <exception cref="InvalidInputException">Thrown if the input for the qualification is invalid.</exception>
        private string ReadAndValidateQualificationInput()
        {
            string qualification = ReadStringFromTextBox(txtQualification, "Please specify the qualification.");
            return qualification;
        }



        // ***** UI handling section *****

        // ** General UI

        /// <summary>
        /// Method that recursively traverses the logical wpf tree and clears all relevant controls.
        /// 
        /// The relevant controls are Textboxes, Listviews, Listboxes, Checkboxes and Textblocks.
        /// </summary>
        /// <param name="obj">The dependencyobject which children should be cleared.</param>
        void ClearChildrenControls(DependencyObject obj)
        {
            foreach (Object child in LogicalTreeHelper.GetChildren(obj))
            {
                if (child is TextBox)
                    ((TextBox)child).Clear();
                else if (child is ListView)
                    ((ListView)child).Items.Clear();
                else if (child is ListBox)
                    ((ListBox)child).UnselectAll();
                else if (child is CheckBox)
                    ((CheckBox)child).IsChecked = false;
                else if (child is TextBlock)
                    ((TextBlock)child).Text = "";
                else if (child is DependencyObject)
                    ClearChildrenControls((DependencyObject)child);
            }
        }

        // ** Animal tab UI

        /// <summary>
        /// Fills the animal list box with regards to the selected category.
        /// </summary>
        private void ShowAnimalsForSelectedCategory()
        {
            lboxAnimal.Items.Clear();
            if (lboxCategory.SelectedItem != null)
            {
                CategoryType selectedCategory = (CategoryType)lboxCategory.SelectedItem;
                switch (selectedCategory)
                {
                    case CategoryType.Mammal:
                        AddItemsToListBox(lboxAnimal, Enum.GetValues(typeof(MammalType)));
                        break;
                    case CategoryType.Reptile:
                        AddItemsToListBox(lboxAnimal, Enum.GetValues(typeof(ReptileType)));
                        break;
                }
            }
        }

        /// <summary>
        /// Updates the content of the labels in the specifications section.
        /// If all animals are listed the section is updated in regards of the selected animal,
        /// if not it is updated based on selected category and selected animal.
        /// </summary>
        private void UpdateAnimalSpecificContent()
        {
            if (!cbxListAll.IsChecked.Value)
            {
                UpdateAnimalSpecificContentOnSelectedCategoryAndAnimal();
            }
            else
            {
                UpdateAnimalSpecificOnSelectedAnimal();
            }
        }

        /// <summary>
        /// Updates the content of the labels in the specifications section with regards to 
        /// the selected category and animal.
        /// </summary>
        private void UpdateAnimalSpecificContentOnSelectedCategoryAndAnimal()
        {
            if (lboxCategory.SelectedItem != null)
            {
                CategoryType selectedCategory = (CategoryType)lboxCategory.SelectedItem;
                switch (selectedCategory)
                {
                    case CategoryType.Mammal:
                        lblCategorySpecific.Content = mammalContent;
                        if (lboxAnimal.SelectedItem != null)
                        {
                            UpdateMammalSpecificContent();
                        }
                        break;
                    case CategoryType.Reptile:
                        lblCategorySpecific.Content = reptileContent;
                        if (lboxAnimal.SelectedItem != null)
                        {
                            UpdateReptileSpecificContent();
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Updates the content of the labels in the specifications section with regards to 
        /// the selected animal only.
        /// </summary>
        private void UpdateAnimalSpecificOnSelectedAnimal()
        {
            if (lboxAnimal.SelectedItem is MammalType)
            {
                lblCategorySpecific.Content = mammalContent;
                UpdateMammalSpecificContent();
            }
            else if (lboxAnimal.SelectedItem is ReptileType)
            {
                lblCategorySpecific.Content = reptileContent;
                UpdateReptileSpecificContent();

            }
        }

        /// <summary>
        /// Updates the content of the animal label in the specifications section with selected mammal content.
        /// </summary>
        private void UpdateMammalSpecificContent()
        {
            MammalType selectedMammal = (MammalType)lboxAnimal.SelectedItem;
            switch (selectedMammal)
            {
                case MammalType.Elk:
                    lblAnimalSpecific.Content = gooseContent;
                    txtblckSchedule.Text = FoodScheduleConstants.GooseSchedule.ToString();
                    lblEaterType.Content = EaterType.Herbivore.ToString();
                    break;
                case MammalType.Zebra:
                    lblAnimalSpecific.Content = zebraContent;
                    txtblckSchedule.Text = FoodScheduleConstants.ZebraSchedule.ToString();
                    lblEaterType.Content = EaterType.Herbivore.ToString();
                    break;
            }
        }

        /// <summary>
        /// Updates the content of the animal label in the specifications section with selected reptile content.
        /// </summary>
        private void UpdateReptileSpecificContent()
        {
            ReptileType selectedReptile = (ReptileType)lboxAnimal.SelectedItem;
            switch (selectedReptile)
            {
                case ReptileType.Lizard:
                    lblAnimalSpecific.Content = lizardContent;
                    txtblckSchedule.Text = FoodScheduleConstants.LizardSchedule.ToString();
                    lblEaterType.Content = EaterType.Omnivorous.ToString();
                    break;
                case ReptileType.Snake:
                    lblAnimalSpecific.Content = snakeContent;
                    txtblckSchedule.Text = FoodScheduleConstants.SnakeSchedule.ToString();
                    lblEaterType.Content = EaterType.Carnivore.ToString();
                    break;
            }
        }

        /// <summary>
        /// Updates the animal view so it shows the current list of animals created.
        /// </summary>
        private void UpdateListView<T>(ListView listView, IListManager<T> manager)
        {
            listView.Items.Clear();
            for (int i = 0; i < manager.Count; ++i)
            {
                listView.Items.Add(manager.GetAt(i));
            }
        }

        /// <summary>
        /// Allows the user to browse for an image and display it.
        /// </summary>
        private void UploadImage()
        {
            // Create OpenFileDialog 
            OpenFileDialog dlg = new OpenFileDialog();

            // Set filter for image file extensions 
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.gif) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.gif";

            // Display the dialog and fetch the result
            Nullable<bool> result = dlg.ShowDialog();

            // Display the image if a file was selected
            if (result.HasValue && result.Value)
            {
                // Show image
                ellipseImage.Fill = new ImageBrush(new BitmapImage(new Uri(@dlg.FileName)));
            }
        }

        /// <summary>
        /// Updates all the ui fields with contents of the specified animal.
        /// </summary>
        /// <param name="animal">The animal which contents should be shown.</param>
        private void UpdateFromCreatedAnimal(IAnimal animal)
        {
            txtAge.Text = "" + animal.Age;
            txtName.Text = animal.Name;
            lboxGender.SelectedItem = animal.Gender;
            if (animal is Mammal)
            {
                lboxCategory.SelectedItem = CategoryType.Mammal;
                txtCategorySpecific.Text = "" + ((Mammal)animal).NumberOfTeeth;
                if (animal is Elk)
                {
                    lboxAnimal.SelectedItem = MammalType.Elk;
                    txtAnimalSpecific.Text = "" + ((Elk)animal).NumberHorns;
                }
                else if (animal is Zebra)
                {
                    lboxAnimal.SelectedItem = MammalType.Zebra;
                    txtAnimalSpecific.Text = "" + ((Zebra)animal).NumberStripes;
                }
            }
            else if (animal is Reptile)
            {
                lboxCategory.SelectedItem = CategoryType.Reptile;
                txtCategorySpecific.Text = "" + ((Reptile)animal).NumberOfEggsLaid;
                if (animal is Snake)
                {
                    lboxAnimal.SelectedItem = ReptileType.Snake;
                    txtAnimalSpecific.Text = ((Snake)animal).IsPoisonous ? "y" : "n";
                }
                else if (animal is Lizard)
                {
                    lboxAnimal.SelectedItem = ReptileType.Lizard;
                    txtAnimalSpecific.Text = ((Lizard)animal).CanDropTail ? "y" : "n";
                }
            }
        }

        /// <summary>
        /// Sorts the animal list view based on the specified property.
        /// </summary>
        /// <param name="property">A string representation of the property. One of {"ID", "Species", "Name", "Age"}</param>
        private void SortAnimalListViewOn(string property)
        {
            switch (property)
            {
                case "ID":
                    session.AnimalsManager.SortOnID();
                    break;
                case "Species":
                    session.AnimalsManager.SortOnSpecies();
                    break;
                case "Name":
                    session.AnimalsManager.SortOnName();
                    break;
                case "Age":
                    session.AnimalsManager.SortOnAge();
                    break;
                default:
                    return;
            }
            UpdateListView(lviewAnimals, session.AnimalsManager);
        }

        // ** Recipe tab UI

        /// <summary>
        /// Fills the ui with the data in the specified recipe.
        /// </summary>
        /// <param name="recipe">The recipe to display in the ui.</param>
        private void UpdateFromRecipe(Recipe recipe)
        {
            txtRecipeName.Text = recipe.Name;
            txtIngredient.Clear();
            UpdateListView(lviewIngredients, recipe.Ingredients);
        }

        /// <summary>
        /// Fills the ui with the data in the specified ingredient.
        /// </summary>
        /// <param name="ingredient">The ingredient to display in the ui.</param>
        private void UpdateFromIngredient(string ingredient)
        {
            txtIngredient.Text = ingredient;
        }

        // ** Staff tab UI

        /// <summary>
        /// Fills the ui with the data in the specified staff member.
        /// </summary>
        /// <param name="staff">The staff member to display in the ui.</param>
        private void UpdateFromStaff(Staff staff)
        {
            txtStaffName.Text = staff.Name;
            txtQualification.Clear();
            UpdateListView(lviewQualifications, staff.Qualifications);
        }

        /// <summary>
        /// Fills the ui with the data in the specified qualification.
        /// </summary>
        /// <param name="qualification">The qualification to display in the ui.</param>
        private void UpdateFromQualification(string qualification)
        {
            txtQualification.Text = qualification;
        }


        // ***** UI event section *****
        
        // ** Animals tab events

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            UploadImage();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddAnimal();
        }

        private void txtAge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Only digits allowed for input
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void cbxListAll_Checked(object sender, RoutedEventArgs e)
        {
            lboxCategory.IsEnabled = false;
            lboxAnimal.Items.Clear();
            AddItemsToListBox(lboxAnimal, Enum.GetValues(typeof(MammalType)));
            AddItemsToListBox(lboxAnimal, Enum.GetValues(typeof(ReptileType)));
            txtAnimalSpecific.Clear();
            txtCategorySpecific.Clear();
            expSpecifications.IsEnabled = false;
            expSpecifications.IsExpanded = false;
            lblEaterType.Content = "Unknown";
            txtblckSchedule.Text = "";
        }

        private void cbxListAll_Unchecked(object sender, RoutedEventArgs e)
        {
            lboxAnimal.Items.Clear();
            ShowAnimalsForSelectedCategory();
            lboxCategory.IsEnabled = true;
            txtAnimalSpecific.Clear();
            txtCategorySpecific.Clear();
            expSpecifications.IsEnabled = false;
            expSpecifications.IsExpanded = false;
            lblEaterType.Content = "Unknown";
            txtblckSchedule.Text = "";
        }

        private void lboxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowAnimalsForSelectedCategory();
            UpdateAnimalSpecificContent();
            txtAnimalSpecific.Clear();
            txtCategorySpecific.Clear();
            expSpecifications.IsEnabled = false;
            expSpecifications.IsExpanded = false;
            lblEaterType.Content = "Unknown";
            txtblckSchedule.Text = "";
        }

        private void lboxAnimal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAnimalSpecificContent();
            txtAnimalSpecific.Clear();
            expSpecifications.IsEnabled = true;
            expSpecifications.IsExpanded = true;
        }

        private void lviewAnimals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lviewAnimals.SelectedItem is IAnimal)
            {
                UpdateFromCreatedAnimal((IAnimal)lviewAnimals.SelectedItem);
                btnDelete.IsEnabled = true;
                btnChange.IsEnabled = true;
            }
            else
            {
                btnDelete.IsEnabled = false;
                btnChange.IsEnabled = false;
            }
        }

        private void lviewAnimalsHeader_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is GridViewColumnHeader)
            {
                GridViewColumnHeader headerClicked = (GridViewColumnHeader)e.OriginalSource;
                string header = (string)headerClicked.Content;
                SortAnimalListViewOn(header);
            }
        }

        private void lbox_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is ListBox)
            {
                ListBox lBox = (ListBox)sender;
                if (lBox.SelectedItem == null)
                {
                    lBox.SelectedItem = lBox.Items[0];
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteSelectedAnimal();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedAnimal();
        }

        // ** Recipes tab events

        private void lviewRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lviewRecipes.SelectedItem is Recipe)
            {
                btnDeleteRecipe.IsEnabled = true;
                btnChangeRecipe.IsEnabled = true;
                UpdateFromRecipe((Recipe)lviewRecipes.SelectedItem);
            }
            else
            {
                btnDeleteRecipe.IsEnabled = false;
                btnChangeRecipe.IsEnabled = false;
            }
        }

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            AddIngredient();
        }

        
        private void btnChangeIngredient_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedIngredient();
        }

        

        private void btnDeleteIngredient_Click(object sender, RoutedEventArgs e)
        {
            DeleteSelectedIngredient();
        }

        

        private void btnDeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            DeleteSelectedRecipe();
        }

        private void btnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            AddRecipe();
        }
        
        private void btnChangeRecipe_Click(object sender, RoutedEventArgs e)
        {
            ApplyRecipeChanges();
        }

        private void lviewIngredients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lviewIngredients.SelectedItem is string)
            {
                btnChangeIngredient.IsEnabled = true;
                btnDeleteIngredient.IsEnabled = true;
                UpdateFromIngredient((string)lviewIngredients.SelectedItem);
            }
            else
            {
                btnChangeIngredient.IsEnabled = false;
                btnDeleteIngredient.IsEnabled = false;
            }
        }

        private void btnNewRecipe_Click(object sender, RoutedEventArgs e)
        {
            ClearAllRecipeControls();
        }

 

        // ** Animal Recipes tab events

        private void lviewRecipeAnimals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSelectedAnimalRecipes();
        }

        private void UpdateSelectedAnimalRecipes()
        {
            if (lviewRecipeAnimals.SelectedItem is IAnimal)
            {
                txtblckAnimalIngredients.Text = "";
                lviewAnimalRecipes.Items.Clear();

                IAnimal animal = (IAnimal)lviewRecipeAnimals.SelectedItem;
                if (session.AnimalRecipeManager.IsKeyPresent(animal.ID))
                {
                    foreach (Recipe recipe in session.AnimalRecipeManager.GetRecipes(animal.ID))
                    {
                        lviewAnimalRecipes.Items.Add(recipe);
                    }
                }
            }
        }

        private void UpdateIngredientsFromRecipe(TextBlock txtblck, Recipe recipe)
        {
            txtblck.Text = recipe.ToString();
        }

        private void lviewAvailableRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lviewAvailableRecipes.SelectedItem is Recipe)
            {
                UpdateIngredientsFromRecipe(txtblckAvailableIngredients, (Recipe)lviewAvailableRecipes.SelectedItem);
                btnAddRecipeToAnimal.IsEnabled = true;
            }
            else
            {
                txtblckAvailableIngredients.Text = "";
                btnAddRecipeToAnimal.IsEnabled = false;
            }
        }

        private void lviewAnimalRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lviewAnimalRecipes.SelectedItem is Recipe)
            {
                UpdateIngredientsFromRecipe(txtblckAnimalIngredients, (Recipe)lviewAnimalRecipes.SelectedItem);
                btnDeleteAnimalRecipe.IsEnabled = true;
            }
            else
            {
                txtblckAnimalIngredients.Text = "";
                btnDeleteAnimalRecipe.IsEnabled = false;
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (tabAnimalRecipes.IsSelected)
                {
                    UpdateListView(lviewRecipeAnimals, session.AnimalsManager);
                    UpdateListView(lviewAvailableRecipes, session.RecipesManager);
                    lviewAnimalRecipes.Items.Clear();
                }
            }
        }


        private void btnAddRecipeToAnimal_Click(object sender, RoutedEventArgs e)
        {
            AssociateRecipeWithSelectedAnimal();
        }

        private void btnDeleteAnimalRecipe_Click(object sender, RoutedEventArgs e)
        {
            UnAssociateRecipeWithSelectedAnimal();
            
        }

        // ** Staff tab events

        private void lviewStaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lviewStaff.SelectedItem is Staff)
            {
                btnChangeStaff.IsEnabled = true;
                btnDeleteStaff.IsEnabled = true;
                UpdateFromStaff((Staff)lviewStaff.SelectedItem);
            }
            else
            {
                btnChangeStaff.IsEnabled = false;
                btnDeleteStaff.IsEnabled = false;
            }
        }

        private void btnNewStaff_Click(object sender, RoutedEventArgs e)
        {
            ClearStaffControls();
        }

        private void btnDeleteQualification_Click(object sender, RoutedEventArgs e)
        {
            DeleteSelectedQualification();
        }

        private void btnChangeQualification_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedQualification();
        }

        private void btnAddQualification_Click(object sender, RoutedEventArgs e)
        {
            AddQualification();
        }

        private void lviewQualifications_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lviewQualifications.SelectedItem is string)
            {
                btnChangeQualification.IsEnabled = true;
                btnDeleteQualification.IsEnabled = true;
                UpdateFromQualification((string)lviewQualifications.SelectedItem);
            }
            else
            {
                btnChangeQualification.IsEnabled = false;
                btnDeleteQualification.IsEnabled = false;
            }
        }

        private void btnDeleteStaff_Click(object sender, RoutedEventArgs e)
        {
            DeleteSelectedStaff();
        }

        private void btnAddStaff_Click(object sender, RoutedEventArgs e)
        {
           AddStaff();
        }

        private void btnChangeStaff_Click(object sender, RoutedEventArgs e)
        {
            ApplyStaffChanges();
        }

        // ** File menu events

        private void mnuFileNew_Click(object sender, RoutedEventArgs e)
        {
            NewSession();
        }

        private void mnuFileSave_Click(object sender, RoutedEventArgs e)
        {
            if (session.WorkingFilePath == String.Empty)
            {
                mnuFileSaveAs_Click(sender, e);
            }
            else
            {
                SaveToFile();
            }
        }
       
        private void mnuFileSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Binary file (*.bin)|*.bin";

            saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            if (saveFileDialog.ShowDialog() == true)
            {
                session.WorkingFilePath = saveFileDialog.FileName;
                SaveToFile();
            }
        }

        private void mnuFileOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Binary file (*.bin)|*.bin";

            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            if (openFileDialog.ShowDialog() == true)
            {
                OpenFromFile(openFileDialog.FileName);
            }
        }

        private void mnuFileExit_Click(object sender, RoutedEventArgs e)
        {
            ShutDown();
        }
        
        private void mnuFileXMLImportRecipes_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "XML file (*.xml)|*.xml";

            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            if (openFileDialog.ShowDialog() == true)
            {
                ImportXMLRecipes(openFileDialog.FileName);
            }
        }

        private void mnuFileXMLExportRecipes_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "XML file (*.xml)|*.xml";

            saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            if (saveFileDialog.ShowDialog() == true)
            {
                ExportXMLRecipes(saveFileDialog.FileName);
            }
        }
        
        private void mnuFileXMLImportStaff_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "XML file (*.xml)|*.xml";

            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            if (openFileDialog.ShowDialog() == true)
            {
                ImportXMLStaff(openFileDialog.FileName);
            }
        }

        private void mnuFileXMLExportStaff_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "XML file (*.xml)|*.xml";

            saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            if (saveFileDialog.ShowDialog() == true)
            {
                ExportStaffRecipes(saveFileDialog.FileName);
            }
        }

        // ** Keyboard shortcut events

        private void HandleKeyDownEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.N && Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                NewSession();
            }
            else if (e.Key == Key.O && Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                mnuFileOpen_Click(null, null);
            }
            else if (e.Key == Key.S && Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                mnuFileSave_Click(null, null);
            }
            else if (e.Key == Key.X && Keyboard.Modifiers.HasFlag(ModifierKeys.Alt))
            {
                ShutDown();
            }
        }

        // ** General events

        private void AnimalWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (session.UnsavedChanges)
            {
                // Create dialog
                UnsavedChangesDialog dialog = new UnsavedChangesDialog("There are unsaved changes.\nThe unsaved changes will be lost if you exit before saving.");
                // Set to owner to center over mainwindow on show
                dialog.Owner = this;

                // Show the dialog
                if (dialog.ShowDialog() == true)
                {
                    // If user wants to abort cancel closing of window
                    e.Cancel = true;
                }
            }
        }
    }
}
