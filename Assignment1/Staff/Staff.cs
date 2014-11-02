using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    /// <summary>
    /// A staff class with a name and a list of qualifications.
    /// </summary>
    [Serializable]
    public class Staff
    {
        private string name;
        private IListManager<string> qualifications;

        /// <summary>
        /// Default initializes the staff member.
        /// </summary>
        internal Staff() {
            qualifications = new ListManager<string>();
            name = "Default";
        }

        /// <summary>
        /// Gives acccess to the list of qualifications.
        /// </summary>
        internal IListManager<string> Qualifications
        {
            get
            {
                return qualifications;
            }
        }

        /// <summary>
        /// Gets and sets the name of the staff member.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("The name is not allowed to be null or consist only of whitespace.");
                }
                name = value;
            }
        }
    }
}
