﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Daniel Bäckström, 2014-09-25, Assignment 2
namespace Assignment2
{
    /// <summary>
    /// Statically internal hard coded schedules for different animals.
    /// </summary>
    static class FoodScheduleConstants
    {
        internal static readonly FoodSchedule LizardSchedule = new FoodSchedule(new List<string>(new string[] { "Morning: A handful of leafs.", "Lunch: A handful of flies.", "Evening: More leafs!" }));
        internal static readonly FoodSchedule ZebraSchedule  = new FoodSchedule(new List<string>(new string[] { "Morning: A crate of grass.", "Lunch: Another crate of grass.", "Evening: Grass grass grass grass...." }));
        internal static readonly FoodSchedule SnakeSchedule  = new FoodSchedule(new List<string>(new string[] { "Morning: 1 mice.", "Lunch: 2 mice.", "Evening: 3 mice" }));
        internal static readonly FoodSchedule GooseSchedule  = new FoodSchedule(new List<string>(new string[] { "Morning: A crate of grass.", "Lunch: Some bark.", "Evening: Apples! Lets party!" }));
    }
}