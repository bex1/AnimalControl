using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Daniel Bäckström, 2014-09-25, Assignment 2
namespace Assignment4
{
    /// <summary>
    /// A factory for objects inheriting mammal.
    /// </summary>
    public static class MammalFactory
    {
        /// <summary>
        /// Creates a new instance of the specified mammal type.
        /// </summary>
        /// <param name="mammalType">The type of the mammal.</param>
        /// <returns>The new instance.</returns>
        internal static Mammal CreateMammal(MammalType mammalType)
        {
            switch (mammalType)
            {
                case MammalType.Elk:
                    return new Elk();
                case MammalType.Zebra:
                    return new Zebra();
                default:
                    return null;
            }
        }
    }
}
