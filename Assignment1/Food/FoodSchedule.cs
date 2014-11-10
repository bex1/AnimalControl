using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Daniel Bäckström, 2014-11-05, Assignment 4
namespace Assignment4
{
    /// <summary>
    /// A container class holding a list of food schedule descriptions.
    /// </summary>
    [Serializable]
    public sealed class FoodSchedule
    {
        private List<string> foodDescriptionList;

        /// <summary>
        /// Default initializes a FoodSchedule.
        /// The list is empty by default.
        /// </summary>
        internal FoodSchedule()
        {
            foodDescriptionList = new List<string>();
        }

        /// <summary>
        /// Initializes with contents of specified list.
        /// Copies the list of food entries into the container.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if any entry consist solely of whitespace or is null.</exception>
        /// <param name="foodList">The list of schedule food entries.</param>
        internal FoodSchedule(List<string> foodList)
        {
            foodDescriptionList = new List<string>();
            // Internal protection copy
            foreach (string food in foodList) {
                if (string.IsNullOrWhiteSpace(food))
                    throw new ArgumentException("The food schedule item is not allowed to be null or consist solely of whitespace");
                foodDescriptionList.Add(food); // No need to clone since string is immutable
            }
        }

        /// <summary>
        /// Returns the number of entries in the food schedule.
        /// </summary>
        int Count
        {
            get
            {
                return foodDescriptionList.Count;
            }
        }
        
        /// <summary>
        /// Returns the food schedule entry at the specified index.
        /// </summary>
        /// <param name="index">The index of the entry.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if index is invalid.</exception>
        /// <returns>The food entry at the specified index.</returns>
        internal string GetFoodSchedule(int index)
        {
            if (!ValidateIndex(index))
                throw new IndexOutOfRangeException("There is no food schedule item at the specified index: " + index);
            return foodDescriptionList[index];
        }

        /// <summary>
        /// Adds a food schedule entry to the schedule.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the entry consist solely of whitespace or is null.</exception>
        /// <param name="item">The food entry.</param>
        internal void AddFoodScheduleItem(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentException("The food schedule item is not allowed to be null or consist solely of whitespace");
            foodDescriptionList.Add(item);
        }

        /// <summary>
        /// Changes the food entry at the specified index to the specified value entry.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">Thrown if index is invalid.</exception>
        /// <param name="item">The new entry.</param>
        /// <param name="index">The index of the entry.</param>
        internal void ChangeFoodScheduleItem(string item, int index)
        {
            if (!ValidateIndex(index))
                throw new IndexOutOfRangeException("There is no food schedule item at the specified index: " + index);
            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentException("The food schedule item is not allowed to be null or consist solely of whitespace");
            foodDescriptionList[index] = item;
        }

        /// <summary>
        /// Deletes the entry at the specified index.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">Thrown if index is invalid.</exception>
        /// <param name="index">The index of the entry.</param>
        internal void DeleteFoodScheduleItem(int index)
        {
            if (!ValidateIndex(index))
                throw new IndexOutOfRangeException("There is no food schedule item at the specified index: " + index);
            foodDescriptionList.RemoveAt(index);
        }

        /// <summary>
        /// Used to validate an index.
        /// </summary>
        /// <param name="index">The index to validate.</param>
        /// <returns>True if there is an entry at the specified index.</returns>
        internal bool ValidateIndex(int index) {
            return index < foodDescriptionList.Count; 
        }

        /// <summary>
        /// Returns a string representation of the whole schedule.
        /// </summary>
        /// <returns>A string representation of the whole schedule.</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Food details and the feeding schedule are as follows.\n");
            for (int i = 0; i < foodDescriptionList.Count; ++i)
            {
                builder.Append("(" + i + ") " + foodDescriptionList[i] + "\n");
            }
            return builder.ToString();
        }
    }
}
