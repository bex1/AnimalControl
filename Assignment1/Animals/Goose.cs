using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Daniel Bäckström, 2014-09-08, Assignment 1
namespace Assignment1
{
    /// <summary>
    /// A goose class.
    /// </summary>
    class Goose : Mammal
    {
        private uint nbrHorns;

        /// <summary>
        /// Initializes a default goose with default values.
        /// </summary>
        internal Goose()
        {
        }

        /// <summary>
        /// Inititalizes a goose with the specified parameters.
        /// </summary>
        /// <param name="id">The ID of the goose.</param>
        /// <param name="name">The name of the goose.</param>
        /// <param name="age">The age of the goose.</param>
        /// <param name="gender">The gender of the goose.</param>
        /// <param name="nbrTeeth">The number of teeths of the goose.</param>
        /// <param name="nbrHorns">The number of horns of the goose.</param>
        internal Goose(string id, string name, uint age, GenderType gender, uint nbrTeeth, uint nbrHorns) : base(id, name, age, gender, nbrTeeth)
        {
            this.nbrHorns = nbrHorns;   
        }

        /// <summary>
        /// The number of horns of the goose.
        /// </summary>
        internal uint NumberHorns
        {
            get
            {
                return nbrHorns;
            }
            set
            {
                nbrHorns = value;
            }
        }

        /// <summary>
        /// Returns a string representation of the various special characteristics of a goose.
        /// </summary>
        public override string SpecialCharacteristics
        {
            get
            {
                return base.SpecialCharacteristics + ", Number of horns: " + nbrHorns;
            }
        }
    }
}
