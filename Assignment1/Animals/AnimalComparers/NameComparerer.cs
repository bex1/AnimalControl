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
    /// The animals are compared according to their name.
    /// </summary>
    public sealed class NameComparer : IComparer<IAnimal>
    {
        /// <summary>
        /// Compares tha name of two animals. 
        /// </summary>
        /// <param name="x">First animal to compare</param>
        /// <param name="y">Second animal to compare</param>
        /// <returns>the result of string compare of the names</returns>
        public int Compare(IAnimal x, IAnimal y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
