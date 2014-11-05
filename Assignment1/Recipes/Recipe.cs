using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Daniel Bäckström, 2014-11-05, Assignment 4
namespace Assignment4
{
    /// <summary>
    /// A recipe with a name and a couple of ingredients.
    /// </summary>
    [Serializable]
    public class Recipe
    {
        private ListManager<string> ingredients;
        private string name;

        /// <summary>
        /// Initializes the recipe.
        /// </summary>
        internal Recipe()
        {
            ingredients = new ListManager<string>();
            name = "Default";
        }

        /// <summary>
        /// Property used to get access to the ingredients.
        /// </summary>
        public ListManager<string> Ingredients
        {
            get
            {
                return ingredients;
            }
            set
            {
                ingredients = value;
            }
        }

        /// <summary>
        /// Gets and sets the name of the recipe.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("The name is not allowed to be null or consist only of whitespace.");
                }
                name = value;
            }
        }

        /// <summary>
        /// Returns a string representation of the recipe.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(name + ":");
            for (int i = 0; i < ingredients.Count; ++i)
            {
                builder.Append("\n" + (i + 1) + ": " + ingredients.GetAt(i));
            }
            return builder.ToString();
        } 
    }
}
