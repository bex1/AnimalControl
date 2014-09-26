﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    sealed class AgeComparer : Comparer<IAnimal>
    {
        public override int Compare(IAnimal x, IAnimal y)
        {
            return x.Age.CompareTo(y.Age);
        }
    }
}