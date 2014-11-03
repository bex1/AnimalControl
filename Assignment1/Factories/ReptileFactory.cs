using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Daniel Bäckström, 2014-09-25, Assignment 2
namespace Assignment4
{
    /// <summary>
    /// A factory for objects inheriting reptile.
    /// </summary>
    public static class ReptileFactory
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
