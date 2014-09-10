using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Daniel Bäckström, 2014-09-08, Assignment 1
namespace Assignment1
{
    /// <summary>
    /// A reptile class.
    /// </summary>
    abstract class Reptile : Animal
    {
        private uint nbrEggsLaid;

        /// <summary>
        /// Initializes a reptile with default values.
        /// </summary>
        internal Reptile()
        {
        }

        /// <summary>
        /// Initializes a reptile with specified values.
        /// </summary>
        /// <param name="id">The ID of the reptile.</param>
        /// <param name="name">The name of the reptile.</param>
        /// <param name="age">The age of the reptile.</param>
        /// <param name="gender">The gender of the reptile.</param>
        /// <param name="nbrEggsLaid">The number of eggs laid by the reptile.</param>
        internal Reptile(string id, string name, uint age, GenderType gender, uint nbrEggsLaid) : base(id, name, age, gender)
        {
            this.nbrEggsLaid = nbrEggsLaid;   
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
