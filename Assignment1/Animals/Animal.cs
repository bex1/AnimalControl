using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Daniel Bäckström, 2014-09-25, Assignment 2
namespace Assignment2
{

    // Note that the use of public on some of the different animal classes properties
    // is done because it is required for the listView to be able to read them.

    /// <summary>
    /// Base animal class.
    /// </summary>
    abstract class Animal : IAnimal
    {
        private string id;
        private string name;
        private uint age;
        private GenderType gender;
        private EaterType eaterType;
        private FoodSchedule foodSchedule;

        /// <summary>
        /// Initializes a animal with specified values.
        /// The rest of the fields are default set and should be set manually before use.
        /// </summary>
        /// <param name="eaterType">The type of eater the animal is. <see cref="EaterType"/></param>
        /// <param name="foodSchedule">The food schedule of the animal.  <see cref="FoodSchedule"/></param>
        internal Animal(EaterType eaterType, FoodSchedule foodSchedule) : this("Default", "Default", 0, GenderType.Unknown, eaterType, foodSchedule)
        {
        }

        /// <summary>
        /// Initializes an animal with specified parameters.
        /// </summary>
        /// <param name="id">The ID of the animal. Should be non null and non whitespace.</param>
        /// <param name="name">The name of the animal. Should be non null and non whitespace.</param>
        /// <param name="age">The age of the animal. Should be non-negative.</param>
        /// <param name="gender">The gender of the animal. <see cref="GenderType"/></param>
        /// <param name="eaterType">The type of eater the animal is. <see cref="EaterType"/></param>
        /// <param name="foodSchedule">The food schedule of the animal.  <see cref="FoodSchedule"/></param>
        internal Animal(string id, string name, uint age, GenderType gender, EaterType eaterType, FoodSchedule foodSchedule)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("The id is not allowed to be null or consist only of whitespace.");
            }
            else if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The name is not allowed to be null or consist only of whitespace.");
            }
            else if (foodSchedule == null)
            {
                throw new ArgumentNullException("The food schedule is not allowed to be null.");
            }
            this.id = id;
            this.name = name;
            this.age = age;
            this.gender = gender;
            this.eaterType = eaterType;
            this.foodSchedule = foodSchedule;
        }

        /// <summary>
        /// Animal copy constructor.
        /// </summary>
        /// <param name="animal">The animal to copy.</param>
        internal Animal(Animal animal)
        {
            this.id = animal.id;
            this.name = animal.name;
            this.age = animal.age;
            this.gender = animal.gender;
            this.eaterType = animal.eaterType;
            this.foodSchedule = (FoodSchedule)animal.foodSchedule.Clone();
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

        /// <summary>
        /// Returns the name of the animal specifies.
        /// </summary>
        public string Species
        {
            get
            {
                return this.GetType().Name;
            }
        }      
        
        /// <summary>
        /// Used to get special characteristics of an animal.
        /// </summary>
        public abstract string SpecialCharacteristics
        {
            get;
        }

        /// <summary>
        /// Returns the type of eater the animal is.
        /// </summary>
        /// <returns>The type of eater the animal is.</returns>
        public EaterType GetEaterType()
        {
            return eaterType;
        }

        /// <summary>
        /// Returns the food schedule for this animal.
        /// </summary>
        /// <returns>the food schedule for this animal</returns>
        public FoodSchedule GetFoodSchedule()
        {
            return foodSchedule;
        }

        /// <summary>
        /// Compares this animal to another animal.
        /// Default comparison is done on id only.
        /// </summary>
        /// <param name="other">The other animal.</param>
        /// <returns>0 if equal. > 0 if this is greater. < 0 if the other is greater.</returns>
        public int CompareTo(IAnimal other)
        {
            return this.id.CompareTo(other.ID);
        }

        public abstract object Clone();
    }
}
