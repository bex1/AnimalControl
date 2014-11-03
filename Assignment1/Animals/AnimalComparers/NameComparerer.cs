using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
