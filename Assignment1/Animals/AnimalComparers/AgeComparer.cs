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
    /// The animals are compared according to their age.
    /// </summary>
    public sealed class AgeComparer : Comparer<IAnimal>
    {
        /// <summary>
        /// Compares tha age of two animals. 
        /// </summary>
        /// <param name="x">First animal to compare</param>
        /// <param name="y">Second animal to compare</param>
        /// <returns>0 if x is younger than y. 0 if x and y are of the same age. > 0 if x is older than y.</returns>
        public override int Compare(IAnimal x, IAnimal y)
        {
            return x.Age.CompareTo(y.Age);
        }
    }
}
