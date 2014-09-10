using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Daniel Bäckström, 2014-09-08, Assignment 1
namespace Assignment1
{

    // Note that the use of public on some of the different animal classes properties
    // is done because it is required for the listView to be able to read them.

    /// <summary>
    /// Base animal class.
    /// </summary>
    abstract class Animal
    {
        private string id;
        private string name;
        private uint age;
        private GenderType gender;

        /// <summary>
        /// Initializes an animal.
        /// Default values will be generated for other fields.
        /// </summary>
        /// <param name="id">The ID of the animal. Should be non null and non whitespace.</param>
        internal Animal() : this("Default", "Default", 0, GenderType.Unknown)
        {
        }

        /// <summary>
        /// Initializes an animal with specified parameters.
        /// </summary>
        /// <param name="id">The ID of the animal. Should be non null and non whitespace.</param>
        /// <param name="name">The name of the animal. Should be non null and non whitespace.</param>
        /// <param name="age">The age of the animal. Should be non-negative.</param>
        /// <param name="gender">The gender of the animal. <see cref="GenderType"/></param>
        internal Animal(string id, string name, uint age, GenderType gender)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("The id is not allowed to be null or consist only of whitespace.");
            }
            else if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The name is not allowed to be null or consist only of whitespace.");
            }
            this.id = id;
            this.name = name;
            this.age = age;
            this.gender = gender;
        }

        /// <summary>
        /// The gender of the animal.
        /// <see cref="GenderType"/>
        /// </summary>
        public GenderType Gender 
        {
            get
            {
                return gender;
            }
            set
            {
                gender = value;
            }
        }

        /// <summary>
        /// The age of the animal.
        /// <exception cref="ArgumentException">Thrown if one assigns a negative value to the Age property.</exception>
        /// </summary>
        public uint Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }
        }

        /// <summary>
        /// The name of the animal.
        /// <exception cref="ArgumentException">Thrown if one assigns a null or an only whitespace string to the Name property.</exception>
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
        /// The ID of the Animal.
        /// <exception cref="ArgumentException">Thrown if one assigns a null or an only whitespace string to the ID property.</exception>
        /// </summary>
        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("The name is not allowed to be null or consist only of whitespace.");
                }
                id = value;
            }
        }

        public abstract string SpecialCharacteristics
        {
            get;
        }
    }
}
