using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Daniel Bäckström, 2014-09-25, Assignment 2
namespace Assignment2
{
    /// <summary>
    /// A elk class.
    /// </summary>
    class Elk : Mammal
    {
        private uint nbrHorns;

        /// <summary>
        /// Initializes a default elk with default values.
        /// </summary>
        internal Elk() : base(EaterType.Herbivore, FoodScheduleConstants.GooseSchedule)
        {
        }

        /// <summary>
        /// Inititalizes a elk with the specified parameters.
        /// </summary>
        /// <param name="id">The ID of the elk.</param>
        /// <param name="name">The name of the elk.</param>
        /// <param name="age">The age of the elk.</param>
        /// <param name="gender">The gender of the elk. <see cref="GenderType"/></param>
        /// <param name="nbrTeeth">The number of teeths of the elk.</param>
        /// <param name="nbrHorns">The number of horns of the elk.</param>
        internal Elk(string id, string name, uint age, GenderType gender, uint nbrTeeth, uint nbrHorns) 
            : base(id, name, age, gender, EaterType.Herbivore, FoodScheduleConstants.GooseSchedule, nbrTeeth)
        {
            this.nbrHorns = nbrHorns;   
        }

        internal Elk(Elk elk) : base(elk)
        {
            this.nbrHorns = elk.nbrHorns;
        }

        /// <summary>
        /// The number of horns of the elk.
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
        /// Returns a string representation of the various special characteristics of a elk.
        /// </summary>
        public override string SpecialCharacteristics
        {
            get
            {
                return base.SpecialCharacteristics + ", Number of horns: " + nbrHorns;
            }
        }

        public override object Clone()
        {
            return new Elk(this);
        }
    }
}
