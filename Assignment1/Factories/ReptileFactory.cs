using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Daniel Bäckström, 2014-09-08, Assignment 1
namespace Assignment1
{
    /// <summary>
    /// A factory for objects inheriting reptile.
    /// </summary>
    static class ReptileFactory
    {
        /// <summary>
        /// Creates a new instance of the specified reptile type.
        /// </summary>
        /// <param name="mammalType">The type of the reptile.</param>
        /// <returns>The new instance.</returns>
        internal static Reptile CreateReptile(ReptileType reptileType)
        {
            switch (reptileType)
            {
                case ReptileType.Lizard:
                    return new Lizard();
                case ReptileType.Snake:
                    return new Snake();
                default:
                    return null;
            }
        }
    }
}
