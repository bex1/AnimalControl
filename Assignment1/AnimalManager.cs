using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Daniel Bäckström, 2014-09-25, Assignment 2
namespace Assignment2
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

        private List<IAnimal> animals;

        /// <summary>
        /// Initialize a a new animalmanager.
        /// </summary>
        internal AnimalManager()
        {
            animals = new List<IAnimal>();
        }

        /// <summary>
        /// Returns the number of animals in the registry.
        /// </summary>
        internal int Count
        {
            get
            {
                return animals.Count;
            }
        }

        /// <summary>
        /// Returns a copy of the animal entry at the specified index.
        /// </summary>
        /// <param name="index">The index of the animal entry.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if index is invalid.</exception>
        /// <returns>The animal entry at the specified index.</returns>
        internal IAnimal GetAnimal(int index)
        {
            if (!ValidateIndex(index))
                throw new IndexOutOfRangeException("There is no animal entry at the specified index: " + index);
            return (IAnimal)animals[index].Clone();
        }

        /// <summary>
        /// Used to validate an index.
        /// </summary>
        /// <param name="index">The index to validate.</param>
        /// <returns>True if there is an entry at the specified index.</returns>
        internal bool ValidateIndex(int index)
        {
            return index < animals.Count;
        }

        /// <summary>
        /// Adds a copy of an animal to the registry.
        /// </summary>
        /// <param name="animal">The animal to add.</param>
        internal void AddAnimal(IAnimal animal)
        {
            // Internal copy protection
            IAnimal copy = (IAnimal)animal.Clone();
            // Give the animal an ID
            GenerateID(copy);
            // Add the animal to the list
            animals.Add(copy);
        }

        /// <summary>
        /// Sorts the animals on their ID.
        /// </summary>
        internal void SortOnID()
        {
            animals.Sort();
        }

        /// <summary>
        /// Sorts the animals on their Name.
        /// </summary>
        internal void SortOnName()
        {
            animals.Sort(new NameComparer());
        }

        /// <summary>
        /// Sorts the animals on their Species.
        /// </summary>
        internal void SortOnSpecies()
        {
            animals.Sort(new SpeciesComparer());
        }

        /// <summary>
        /// Sorts the animals on their Age.
        /// </summary>
        internal void SortOnAge()
        {
            animals.Sort(new AgeComparer());
        }


        /// <summary>
        /// Generates a new ID for a specified category.
        /// </summary>
        /// <param name="category">The category which the generation is based upon.</param>
        /// <returns>Returns the generated ID.</returns>
        private void GenerateID(IAnimal animal)
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
