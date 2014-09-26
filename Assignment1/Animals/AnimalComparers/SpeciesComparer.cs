﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    sealed class SpeciesComparer : IComparer<IAnimal>
    {
        public int Compare(IAnimal x, IAnimal y)
        {
            return x.Species.CompareTo(y.Species);
        }
    }
}
