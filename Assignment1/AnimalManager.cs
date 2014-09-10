using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Daniel Bäckström, 2014-09-08, Assignment 1
namespace Assignment1
{
    /// <summary>
    /// Registry class for the animals.
    /// </summary>
    class AnimalManager
    {
        private static readonly string mammalIdBase = "mml";
        private static readonly string reptileIdBase = "rpt";
        private int lastMammalIdNumber;
        private int lastReptileIdNumber;

        private List<Animal> animals;

        /// <summary>
        /// Initialize a a new animalmanager.
        /// </summary>
        internal AnimalManager()
        {
            animals = new List<Animal>();
        }

        /// <summary>
        /// Returns a read only collection of the animals managed.
        /// </summary>
        internal IReadOnlyCollection<Animal> Animals
        {
            get
            {
                return animals.AsReadOnly();
            }
        }

        /// <summary>
        /// Adds an animal to the registry.
        /// </summary>
        /// <param name="animal">The animal to add.</param>
        internal void AddAnimal(Animal animal)
        {
            // Give the animal an ID
            GenerateID(animal);
            // Add the animal to the list
            animals.Add(animal);
        }

        /// <summary>
        /// Generates a new ID for a specified category.
        /// </summary>
        /// <param name="category">The category which the generation is based upon.</param>
        /// <returns>Returns the generated ID.</returns>
        private void GenerateID(Animal animal)
        {
            if (animal is Mammal)
            {
                ++lastMammalIdNumber;
                animal.ID = mammalIdBase + lastMammalIdNumber.ToString("000");
            }
            else if (animal is Reptile)
            {
                ++lastReptileIdNumber;
                animal.ID = reptileIdBase + lastReptileIdNumber.ToString("000");
            }
        }
    }
}
