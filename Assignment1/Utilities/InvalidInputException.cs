using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Daniel Bäckström, 2014-09-08, Assignment 1
namespace Assignment1
{
    /// <summary>
    /// Exception used for signaling invalid input.
    /// </summary>
    internal class InvalidInputException : Exception
    {
        /// <summary>
        /// Initializes a default Exception.
        /// </summary>
        internal InvalidInputException()
        {
        }

        /// <summary>
        /// Initializes a exception with the specified message.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        internal InvalidInputException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a exception with the specified message and inner exception.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        /// <param name="inner">Inner exception.</param>
        internal InvalidInputException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
