using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Daniel Bäckström, 2014-09-08, Assignment 1
namespace Assignment1
{
    /// <summary>
    /// A factory for objects inheriting mammal.
    /// </summary>
    static class MammalFactory
    {
        /// <summary>
        /// Creates a new instance of the specified mammal type.
        /// </summary>
        /// <param name="mammalType">The type of the mammal.</param>
        /// <returns>The new instance.</returns>
        internal static Animal CreateMammal(MammalType mammalType)
        {
            switch (mammalType)
            {
                case MammalType.Goose:
                    return new Goose();
                case MammalType.Zebra:
                    return new Zebra();
                default:
                    return null;
            }
        }
    }
}
