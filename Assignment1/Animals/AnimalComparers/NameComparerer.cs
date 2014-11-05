using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Daniel Bäckström, 2014-11-05, Assignment 4
namespace Assignment4
{
    public sealed class NameComparer : IComparer<IAnimal>
    {
        public int Compare(IAnimal x, IAnimal y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
