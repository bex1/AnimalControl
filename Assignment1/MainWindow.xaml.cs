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

// Daniel Bäckström, 2014-09-25, Assignment 2
namespace Assignment2
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

        private AnimalManager animalManager;


        // ***** Initialization section *****

        /// <summary>
        /// Initializes the mainwindow.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            InitializeGUI();

            animalManager = new AnimalManager();
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


        // ***** Input section *****

        /// <summary>
        /// Creates and adds the animal which is specified in the UI. 
        /// Then hands it over to the AnimalManager.
        /// </summary>
        private void AddAnimal()
        {
            try
            {
                animalManager.AddAnimal(ReadAndValidateInput());
            }
            catch (InvalidInputException ex)
            {
                HandleInvalidInputFrom(ex.getElement(), ex.Message);
            }
            UpdateAnimalView();
        }

        /// <summary>
        /// Reads and validates the input into the animal and if all input is validated returns the factorycreated animal.
        /// </summary>
        /// <returns>The animal created.</returns>
        /// <exception cref="InvalidInputException">Thrown if any input validation fails.</exception>
        private IAnimal ReadAndValidateInput()
        {
            IAnimal animal = null;

            CategoryType category = GetSelectedCategory();

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

            animal.Name = ReadName();

            animal.Age = ReadUIntFromTextBox(txtAge, "Age is not valid!");

            animal.Gender = GetSelectedGender();

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
        private CategoryType GetSelectedCategory()
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
        private GenderType GetSelectedGender()
        {
            if (lboxGender.SelectedItem == null)
            {
                throw new InvalidInputException("Select a gender!", lboxGender);
            }
            return (GenderType)lboxGender.SelectedItem;
        }

        /// <summary>
        /// Returns the name of the animal.
        /// </summary>
        /// <returns>The name of the animal.</returns>
        /// <exception cref="InvalidInputException">Thrown if the name field is empty or only whitespace.</exception>
        private string ReadName()
        {
            string name = txtName.Text.Trim();

            if (String.IsNullOrWhiteSpace(name)) // Do not allow whitespace names
            {
                throw new InvalidInputException("Name is not valid!", txtName);
            }

            return name;
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


        // ***** UI handling section *****

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
        private void UpdateAnimalView()
        {
            lviewAnimals.Items.Clear();
            for (int i = 0; i < animalManager.Count; ++i)
            {
                lviewAnimals.Items.Add(animalManager.GetAnimal(i));
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
                    animalManager.SortOnID();
                    break;
                case "Species":
                    animalManager.SortOnSpecies();
                    break;
                case "Name":
                    animalManager.SortOnName();
                    break;
                case "Age":
                    animalManager.SortOnAge();
                    break;
                default:
                    return;
            }
            UpdateAnimalView();
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


        // ***** UI event section *****

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
                UpdateFromCreatedAnimal((IAnimal)lviewAnimals.SelectedItem);
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
    }
}
