using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Daniel Bäckström, 2014-11-05, Assignment 4
namespace Assignment4
{
    /// <summary>
    /// A comparer of animals.
    /// 
    /// The animals are compared according to species.
    /// </summary>
    public sealed class SpeciesComparer : IComparer<IAnimal>
    {
        /// <summary>
        /// Compares tha species of two animals. 
        /// </summary>
        /// <param name="x">First animal to compare</param>
        /// <param name="y">Second animal to compare</param>
        /// <returns>the result of string compare of the species</returns>
        public int Compare(IAnimal x, IAnimal y)
        {
            return x.Species.CompareTo(y.Species);
        }
    }
}
