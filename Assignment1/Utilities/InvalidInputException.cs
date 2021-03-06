﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

// Daniel Bäckström, 2014-11-05, Assignment 4
namespace Assignment4
{
    /// <summary>
    /// Exception used for signaling invalid input.
    /// </summary>
    public class InvalidInputException : Exception
    {
        private FrameworkElement element;
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

        /// <summary>
        /// Initializes a exception with the specified message.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        /// <param name="element">The UI element where the error accured.</param>
        internal InvalidInputException(string message, FrameworkElement element)
            : base(message)
        {
            this.element = element;
        }

        /// <summary>
        /// Returns the UI element where the error accured.
        /// </summary>
        /// <returns>The UI element where the error accured.</returns>
        internal FrameworkElement getElement()
        {
            return element;
        }
    }
}
