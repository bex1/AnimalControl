using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Daniel Bäckström, 2014-09-25, Assignment 2
namespace Assignment3
{
    public class Lizard : Reptile
    {
        private bool canDropTail;
       

        /// <summary>
        /// Initializes a default Lizard with default values.
        /// </summary>
        internal Lizard() : base(EaterType.Omnivorous, FoodScheduleConstants.LizardSchedule)
        {
        }

        /// <summary>
        /// Initializes a default Lizard with specified values.
        /// </summary>
        /// <param name="id">The ID of the lizard.</param>
        /// <param name="name">The name of the lizard.</param>
        /// <param name="age">The age of the lizard.</param>
        /// <param name="gender">The gender of the lizard. <see cref="GenderType"/></param>
        /// <param name="nbrEggsLaid">The number of eggs laid by the lizard.</param>
        /// <param name="canDropTail">Indicates id the lizard can drop its tail.</param>
        internal Lizard(string id, string name, uint age, GenderType gender, uint nbrEggsLaid, bool canDropTail) 
            : base(id, name, age, gender, EaterType.Omnivorous, FoodScheduleConstants.LizardSchedule, nbrEggsLaid)
        {
            this.canDropTail = canDropTail;   
        }

        /// <summary>
        /// Property indicating id the lizard can drop it's tail.
        /// </summary>
        internal bool CanDropTail
        {
            get 
            {
                return canDropTail;
            }
            set
            {
                canDropTail = value;
            }
            
        }

        /// <summary>
        /// Returns a string representation of the various special characteristics of a lizard.
        /// </summary>
        public override string SpecialCharacteristics
        {
            get
            {
                return base.SpecialCharacteristics + ", Can drop it's tail: " + (canDropTail ? "yes" : "no");
            }
        }
    }
}
