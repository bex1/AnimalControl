using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Daniel Bäckström, 2014-09-25, Assignment 2
namespace Assignment4
{
    /// <summary>
    /// A mammal class.
    /// </summary>
    [Serializable]
    public abstract class Mammal : Animal
    {
        private uint nbrTeeth;

        /// <summary>
        /// Initializes a mammal with specified values.
        /// The rest of the fields are default set and should be set manually before use.
        /// </summary>
        /// <param name="eaterType">The type of eater the mammal is. <see cref="EaterType"/></param>
        /// <param name="foodSchedule">The food schedule of the mammal.  <see cref="FoodSchedule"/></param>
        internal Mammal(EaterType eaterType, FoodSchedule foodSchedule) : base(eaterType, foodSchedule)
        {
        }

        /// <summary>
        /// Initializes a mammal with specified values.
        /// </summary>
        /// <param name="id">The ID of the mammal.</param>
        /// <param name="name">The name of the mammal.</param>
        /// <param name="age">The age of the mammal.</param>
        /// <param name="gender">The gender of the mammal. <see cref="GenderType"/></param>
        /// <param name="eaterType">The type of eater the mammal is. <see cref="EaterType"/></param>
        /// <param name="foodSchedule">The food schedule of the mammal.  <see cref="FoodSchedule"/></param>
        /// <param name="nbrTeeth">The number of teeths of the mammal.</param>
        internal Mammal(string id, string name, uint age, GenderType gender, EaterType eaterType, FoodSchedule foodSchedule, uint nbrTeeth) 
            : base(id, name, age, gender, eaterType, foodSchedule)
        {
            this.nbrTeeth = nbrTeeth;   
        }

        /// <summary>
        /// The number of teeths of the mammal.
        /// </summary>
        internal uint NumberOfTeeth
        {
            get
            {
                return nbrTeeth;
            }
            set 
            {
                nbrTeeth = value;
            }
        }

        /// <summary>
        /// Returns a string representation of the various special characteristics of a mammal.
        /// </summary>
        public override string SpecialCharacteristics
        {
            get
            {
                return "Number of teeth: " + nbrTeeth;
            }
        }
    }
}
