using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Daniel Bäckström, 2014-09-08, Assignment 1
namespace Assignment1
{
    /// <summary>
    /// A mammal class.
    /// </summary>
    abstract class Mammal : Animal
    {
        private uint nbrTeeth;

        /// <summary>
        /// Initializes a mammal with default values.
        /// </summary>
        internal Mammal() 
        {
        }

        /// <summary>
        /// Initializes a mammal with specified values.
        /// </summary>
        /// <param name="id">The ID of the mammal.</param>
        /// <param name="name">The name of the mammal.</param>
        /// <param name="age">The age of the mammal.</param>
        /// <param name="gender">The gender of the mammal.</param>
        /// <param name="nbrTeeth">The number of teeths of the mammal.</param>
        internal Mammal(string id, string name, uint age, GenderType gender, uint nbrTeeth) : base(id, name, age, gender)
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
