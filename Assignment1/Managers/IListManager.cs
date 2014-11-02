using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    /// <summary>
    /// A manager interface which can be used as a generic list based registry. 
    /// </summary>
    /// <typeparam name="T">The type of register data.</typeparam>
    public interface IListManager<T>
    {
        /// <summary>
        /// Returns the number of objects in the registry.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Adds an object to the registry.
        /// </summary>
        /// <param name="obj">The object to add.</param>
        void Add(T obj);

        /// <summary>
        /// Changes the object at the specified index.
        /// </summary>
        /// <param name="obj">The new object.</param>
        /// <param name="index">The index where the object should be changed</param>
        void ChangeAt(T obj, int index);

        /// <summary>
        /// Deletes the object at the specified index.
        /// </summary>
        /// <param name="index">The index where the object is deleted.</param>
        void DeleteAt(int index);

        /// <summary>
        /// Clears the registry of data.
        /// </summary>
        void DeleteAll();

        /// <summary>
        /// Returns the object at the specified index.
        /// </summary>
        /// <param name="index">The index that points out the returned object.</param>
        /// <returns>The object at the specified index.</returns>
        T GetAt(int index);

        /// <summary>
        /// Returns a string representation of all the objects in an array.
        /// </summary>
        /// <returns>The string array represention of the objects.</returns>
        string[] ToStringArray();

        /// <summary>
        /// Returns a string representation of all the objects in a list.
        /// </summary>
        /// <returns>The string list represention of the objects.</returns>
        List<string> ToStringList();

        /// <summary>
        /// Checks if the specified index is valid.
        /// </summary>
        /// <param name="index">The index to check.</param>
        /// <returns>True if an object recides at the specified index, false otherwise.</returns>
        bool IsValidIndex(int index);

        /// <summary>
        /// Default sorts the registry utilizing IComparable.
        /// </summary>
        void Sort();

        /// <summary>
        /// Sorts the registry according to the specified comparer.
        /// </summary>
        /// <param name="comparer">A comparer of the elements.</param>
        void Sort(IComparer<T> comparer);

        /// <summary>
        /// Serializes the list manager to the specified binary file.
        /// </summary>
        /// <param name="fileName">The filepath of the file.</param>
        /// <returns>True if successfull, false otherwise.</returns>
        bool BinarySerialize(string fileName);

        /// <summary>
        /// DeSerializes the list manager from the specified binary file.
        /// </summary>
        /// <param name="fileName">The filepath of the file.</param>
        /// <returns>True if successfull, false otherwise.</returns>
        bool BinaryDeSerialize(string fileName);

        /// <summary>
        /// Serializes the list manager to the specified XML file.
        /// </summary>
        /// <param name="fileName">The filepath of the file.</param>
        /// <returns>True if successfull, false otherwise.</returns>
        bool XMLSerialize(string fileName);

        /// <summary>
        /// DeSerializes the list manager from the specified XML file.
        /// </summary>
        /// <param name="fileName">The filepath of the file.</param>
        /// <returns>True if successfull, false otherwise.</returns>
        bool XMLDeSerialize(string fileName);
    }
}
