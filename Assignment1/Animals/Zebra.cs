using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Daniel Bäckström, 2014-09-25, Assignment 2
namespace Assignment3
{
    /// <summary>
    /// A zebra class.
    /// </summary>
    public class Zebra : Mammal
    {
        private uint nbrStripes;

        /// <summary>
        /// Initializes a default zebra with default values.
        /// </summary>
        internal Zebra() : base(EaterType.Herbivore, FoodScheduleConstants.ZebraSchedule)
        {
        }

        /// <summary>
        /// Initializes a zebra with specified values.
        /// </summary>
        /// <param name="id">The ID of the zebra.</param>
        /// <param name="name">The name of the zebra.</param>
        /// <param name="age">The age of the zebra.</param>
        /// <param name="gender">The gender of the zebra. <see cref="GenderType"/></param>
        /// <param name="nbrTeeth">The number of teeths of the zebra.</param>
        /// <param name="nbrStripes">The number of stripes of the zebra.</param>
        internal Zebra(string id, string name, uint age, GenderType gender, uint nbrTeeth, uint nbrStripes)
            : base(id, name, age, gender, EaterType.Herbivore, FoodScheduleConstants.ZebraSchedule, nbrTeeth)
        {
            this.nbrStripes = nbrStripes;   
        }

        /// <summary>
        /// Number of stripes of this zebra.
        /// </summary>
        internal uint NumberStripes
        {
            get
            {
                return nbrStripes;
            }
            set
            {
                nbrStripes = value;
            }
        }

        /// <summary>
        /// Returns a string representation of the various special characteristics of a zebra.
        /// </summary>
        public override string SpecialCharacteristics
        {
            get
            {
                return base.SpecialCharacteristics + ", Number of stripes: " + nbrStripes;
            }
        }
    }
}
