﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Daniel Bäckström, 2014-09-08, Assignment 1
namespace Assignment1
{
    /// <summary>
    /// A snake class.
    /// </summary>
    class Snake : Reptile
    {
        bool poisonous;

        /// <summary>
        /// Initializes a default snake with default values.
        /// </summary>
        internal Snake() {
        }

        /// <summary>
        /// Initializes a snake with specified values.
        /// </summary>
        /// <param name="id">The ID of the snake.</param>
        /// <param name="name">The name of the snake.</param>
        /// <param name="age">The age of the snake.</param>
        /// <param name="gender">The gender of the snake.</param>
        /// <param name="nbrEggsLaid">The number of eggs laid by the snake.</param>
        /// <param name="poisonous">Indicates if the snake is poisonous.</param>
        internal Snake(string id, string name, uint age, GenderType gender, uint nbrEggsLaid, bool poisonous)
            : base(id, name, age, gender, nbrEggsLaid)
        {
            this.poisonous = poisonous;   
        }

        /// <summary>
        /// Indicates if the snake is poisonous.
        /// </summary>
        internal bool IsPoisonous
        {
            get
            {
                return poisonous;
            }
            set
            {
                poisonous = value;
            }
        }

        /// <summary>
        /// Returns a string representation of the various special characteristics of a snake.
        /// </summary>
        public override string SpecialCharacteristics
        {
            get
            {
                return base.SpecialCharacteristics + ", Is poisonous: " + (poisonous ? "yes" : "no");
            }
        }
    }
}
