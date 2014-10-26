using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Daniel Bäckström, 2014-09-25, Assignment 2
namespace Assignment3
{
    /// <summary>
    /// Registry class for the animals.
    /// </summary>
    public class AnimalManager : ListManager<IAnimal>
    {
        private static readonly string mammalIdBase = "mml";
        private static readonly string reptileIdBase = "rpt";
        private int lastMammalIdNumber;
        private int lastReptileIdNumber;

        /// <summary>
        /// Adds the specified animal to registry.
        /// 
        /// Also assigns a registry id to the animal.
        /// </summary>
        /// <param name="animal">The animal to add.</param>
        public override void Add(IAnimal animal)
        {
            // Give the animal an ID
            GenerateID(animal);
            // Add the animal to the list
            base.Add(animal);
        }

        /// <summary>
        /// Changes the animal at the specified position.
        /// </summary>
        /// <param name="animal">The new animal.</param>
        /// <param name="index">The position of the animal to change.</param>
        public override void ChangeAt(IAnimal animal, int index)
        {
            // Copy id
            animal.ID = GetAt(index).ID;
            // Add the animal to the list
            base.ChangeAt(animal, index);
        }

        /// <summary>
        /// Sorts the animals on their ID.
        /// </summary>
        internal void SortOnID()
        {
            Sort();
        }

        /// <summary>
        /// Sorts the animals on their Name.
        /// </summary>
        internal void SortOnName()
        {
            Sort(new NameComparer());
        }

        /// <summary>
        /// Sorts the animals on their Species.
        /// </summary>
        internal void SortOnSpecies()
        {
            Sort(new SpeciesComparer());
        }

        /// <summary>
        /// Sorts the animals on their Age.
        /// </summary>
        internal void SortOnAge()
        {
            Sort(new AgeComparer());
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
