using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Daniel Bäckström, 2014-09-25, Assignment 2
namespace Assignment3
{
    public interface IAnimal : IComparable<IAnimal>
    {
        /// <summary>
        /// The gender of the animal.
        /// <see cref="GenderType"/>
        /// </summary>
        GenderType Gender { get; set; }

        /// <summary>
        /// The ID of the Animal.
        /// <exception cref="ArgumentException">Thrown if one assigns a null or an only whitespace string to the ID property.</exception>
        /// </summary>
        string ID { get; set; }

        /// <summary>
        /// The name of the animal.
        /// <exception cref="ArgumentException">Thrown if one assigns a null or an only whitespace string to the Name property.</exception>
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The age of the animal.
        /// <exception cref="ArgumentException">Thrown if one assigns a negative value to the Age property.</exception>
        /// </summary>
        uint Age { get; set; }

        /// <summary>
        /// Returns the name of the animal specifies.
        /// </summary>
        string Species { get; }

        /// <summary>
        /// Returns the type of eater the animal is.
        /// </summary>
        /// <returns>The type of eater the animal is.</returns>
        EaterType GetEaterType();

        /// <summary>
        /// Returns the food schedule for this animal.
        /// </summary>
        /// <returns>the food schedule for this animal</returns>
        FoodSchedule GetFoodSchedule();
    }
}
