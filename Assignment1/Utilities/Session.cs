using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Daniel Bäckström, 2014-11-05, Assignment 4
namespace Assignment4
{
    /// <summary>
    /// A session class maintaining all the managers for easy serializing of the whole session.
    /// </summary>
    [Serializable]
    public class Session
    {
        private AnimalManager animalManager;
        private RecipeManager recipeManager;
        private StaffManager staffManager;
        private AnimalRecipesDictionary animalRecipes;

        private bool unsavedChanges;
        private string workingFilePath;

        /// <summary>
        /// Creates a new session with empty state.
        /// </summary>
        internal Session()
        {
            NewSession();
        }

        /// <summary>
        /// Overwrites the current session with a new session with empty state.
        /// 
        /// All unsaved session changes will be lost in the process.
        /// </summary>
        internal void NewSession() {
            animalManager = new AnimalManager();
            recipeManager = new RecipeManager();
            staffManager = new StaffManager();
            animalRecipes = new AnimalRecipesDictionary();
            unsavedChanges = false;
            workingFilePath = String.Empty;
        }

        /// <summary>
        /// Manager of animals.
        /// </summary>
        internal AnimalManager AnimalsManager{
            get {
                return animalManager;
            }
        }

        /// <summary>
        /// Manager of recipes
        /// </summary>
        internal RecipeManager RecipesManager
        {
            get
            {
                return recipeManager;
            }
            set
            {
                recipeManager = value;
            }
        }

        /// <summary>
        /// Manager of staff
        /// </summary>
        internal StaffManager StaffsManager
        {
            get
            {
                return staffManager;
            }
            set
            {
                staffManager = value;
            }
        }

        /// <summary>
        /// Associated animal recipes manager
        /// </summary>
        internal AnimalRecipesDictionary AnimalRecipeManager
        {
            get
            {
                return animalRecipes;
            }
        }

        /// <summary>
        /// Indicates if there are unsaved changes.
        /// </summary>
        internal bool UnsavedChanges
        {
            get
            {
                return unsavedChanges;
            }
            set
            {
                unsavedChanges = value;
            }
        }

        /// <summary>
        /// The current file path of the current working session file.
        /// </summary>
        internal string WorkingFilePath {
            get 
            {
                return workingFilePath;
            }
            set
            {
                workingFilePath = value;
            }
        }
    }
}
