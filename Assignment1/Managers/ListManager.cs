using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    /// <summary>
    /// A manager implementation which can be used as a generic list based registry. 
    /// </summary>
    /// <typeparam name="T">The type of register data.</typeparam>
    public class ListManager<T> : IListManager<T>
    {
        private List<T> list;

        /// <summary>
        /// Initializes the list manager aka registry.
        /// </summary>
        internal ListManager()
        {
            list = new List<T>();
        }

        /// <summary>
        /// Returns the number of objects in the registry.
        /// </summary>
        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        /// <summary>
        /// Adds an object to the registry.
        /// </summary>
        /// <param name="obj">The object to add.</param>
        public virtual void Add(T obj)
        {
            list.Add(obj);
        }

        /// <summary>
        /// Changes the object at the specified index.
        /// </summary>
        /// <param name="obj">The new object.</param>
        /// <param name="index">The index where the object should be changed</param>
        public virtual void ChangeAt(T obj, int index)
        {
            if (!IsValidIndex(index))
                throw new IndexOutOfRangeException("The index: (" + index + ") is out of range. Valid indices is in the interval [0," + (Count - 1) + "].");
            list[index] = obj;
        }

        /// <summary>
        /// Deletes the object at the specified index.
        /// </summary>
        /// <param name="index">The index where the object is deleted.</param>
        public void DeleteAt(int index)
        {
            if (!IsValidIndex(index))
                throw new IndexOutOfRangeException("The index: (" + index + ") is out of range. Valid indices is in the interval [0," + (Count - 1) + "].");
            list.RemoveAt(index);
        }

        /// <summary>
        /// Clears the registry of data.
        /// </summary>
        public void DeleteAll()
        {
            list.Clear();
        }

        /// <summary>
        /// Returns the object at the specified index.
        /// </summary>
        /// <param name="index">The index that points out the returned object.</param>
        /// <returns>The object at the specified index.</returns>
        public T GetAt(int index)
        {
            if (!IsValidIndex(index))
                throw new IndexOutOfRangeException("The index: (" + index + ") is out of range. Valid indices is in the interval [0," + (Count - 1) + "].");
            return list[index];
        }

        /// <summary>
        /// Returns a string representation of all the objects in an array.
        /// </summary>
        /// <returns>The string array represention of the objects.</returns>
        public string[] ToStringArray()
        {
            string[] strings = new string[Count];
            for (int i = 0; i < Count; ++i)
            {
                strings[i] = list[i].ToString();
            }
            return strings;
        }

        /// <summary>
        /// Returns a string representation of all the objects in a list.
        /// </summary>
        /// <returns>The string list represention of the objects.</returns>
        public List<string> ToStringList()
        {
            List<string> strings = new List<string>();
            foreach (T obj in list) {
                strings.Add(obj.ToString());
            }
            return strings;
        }

        /// <summary>
        /// Checks if the specified index is valid.
        /// </summary>
        /// <param name="index">The index to check.</param>
        /// <returns>True if an object recides at the specified index, false otherwise.</returns>
        public bool IsValidIndex(int index)
        {
            return index >= 0 && index < Count;
        }

        /// <summary>
        /// Default sorts the registry utilizing IComparable.
        /// </summary>
        public void Sort()
        {
            list.Sort();
        }

        /// <summary>
        /// Sorts the registry according to the specified comparer.
        /// </summary>
        /// <param name="comparer">A comparer of the elements.</param>
        public void Sort(IComparer<T> comparer)
        {
            list.Sort(comparer);
        }
    }
}
