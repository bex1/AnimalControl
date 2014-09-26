using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Daniel Bäckström, 2014-09-25, Assignment 2
namespace Assignment2
{
    /// <summary>
    /// A reptile class.
    /// </summary>
    abstract class Reptile : Animal
    {
        private uint nbrEggsLaid;

        /// <summary>
        /// Initializes a reptile with specified values.
        /// The rest of the fields are default set and should be set manually before use.
        /// </summary>
        /// <param name="gender">The gender of the reptile. <see cref="GenderType"/></param>
        /// <param name="eaterType">The type of eater the reptile is. <see cref="EaterType"/></param>
        /// <param name="foodSchedule">The food schedule of the reptile.  <see cref="FoodSchedule"/></param>
        internal Reptile(EaterType eaterType, FoodSchedule foodSchedule) : base(eaterType, foodSchedule)
        {
        }

        /// <summary>
        /// Initializes a reptile with specified values.
        /// </summary>
        /// <param name="id">The ID of the reptile.</param>
        /// <param name="name">The name of the reptile.</param>
        /// <param name="age">The age of the reptile.</param>
        /// <param name="gender">The gender of the reptile. <see cref="GenderType"/></param>
        /// <param name="eaterType">The type of eater the reptile is. <see cref="EaterType"/></param>
        /// <param name="foodSchedule">The food schedule of the reptile.  <see cref="FoodSchedule"/></param>
        /// <param name="nbrEggsLaid">The number of eggs laid by the reptile.</param>
        internal Reptile(string id, string name, uint age, GenderType gender, EaterType eaterType, FoodSchedule foodSchedule, uint nbrEggsLaid) 
            : base(id, name, age, gender, eaterType, foodSchedule)
        {
            this.nbrEggsLaid = nbrEggsLaid;   
        }

        internal Reptile(Reptile reptile) : base(reptile)
        {
            this.nbrEggsLaid = reptile.nbrEggsLaid; 
        }

        /// <summary>
        /// The number of eggs laid by the reptile.
        /// </summary>
        internal uint NumberOfEggsLaid
        {
            get
            {
                return nbrEggsLaid;
            }
            set
            {
                nbrEggsLaid = value;
            }
        }

        /// <summary>
        /// Returns a string representation of the various special characteristics of a reptile.
        /// </summary>
        public override string SpecialCharacteristics
        {
            get
            {
                return "Number of eggs laid: " + nbrEggsLaid;
            }
        }
    }
}
