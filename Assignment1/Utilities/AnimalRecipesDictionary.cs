using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    /// <summary>
    /// A dictionary for use with associating an animal ID with a list of recipes.
    /// </summary>
    public class AnimalRecipesDictionary
    {
        Dictionary<string, List<Recipe>> associatedRecipes;

        /// <summary>
        /// Initializes the dictionary.
        /// </summary>
        internal AnimalRecipesDictionary()
        {
            associatedRecipes = new Dictionary<string, List<Recipe>>();
        }

        /// <summary>
        /// Checks if the key is present in the dictionary.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True if the key is present, false otherwise.</returns>
        internal bool IsKeyPresent(string key)
        {
            return associatedRecipes.ContainsKey(key);
        }

        /// <summary>
        /// Adds the specified key value parameters to the dictionary.
        /// </summary>
        /// <param name="key">The key in the new entry.</param>
        /// <param name="recipes">The value of the new entry.</param>
        /// <exception cref="ArgumentException">Thrown if the key is not present in the dictionary.</exception>
        internal void Add(string key, List<Recipe> recipes)
        {
            if (IsKeyPresent(key))
            {
                throw new ArgumentException("The key is already present in the dictionary.");
            }
            associatedRecipes.Add(key, recipes);
        }

        /// <summary>
        /// Reassigns the key association with the specified recipes.
        /// </summary>
        /// <param name="key">The key to reassociate.</param>
        /// <param name="recipes">The new association.</param>
        /// <exception cref="ArgumentException">Thrown if the key is not present in the dictionary.</exception>
        internal void Change(string key, List<Recipe> recipes)
        {
            if (!IsKeyPresent(key))
            {
                throw new ArgumentException("The key is not present in the dictionary. Please add instead.");
            }
            associatedRecipes[key] = recipes;
        }

        /// <summary>
        /// Deletes the key and its associated recipes from the dictionary.
        /// </summary>
        /// <param name="key">The key to delete.</param>
        /// <exception cref="ArgumentException">Thrown if the key is not present in the dictionary.</exception>
        internal void Delete(string key)
        {
            if (!IsKeyPresent(key))
            {
                throw new ArgumentException("The key is not present in the dictionary.");
            }
            associatedRecipes.Remove(key);
        }

        /// <summary>
        /// Fetches the recipes associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose recipe associations are to be fetched.</param>
        /// <returns>The associated list of recipes.</returns>
        /// <exception cref="ArgumentException">Thrown if the key is not present in the dictionary.</exception>
        internal List<Recipe> GetRecipes(string key)
        {
            if (!IsKeyPresent(key))
            {
                throw new ArgumentException("The key is not present in the dictionary.");
            }
            return associatedRecipes[key];
        }
    }
}
