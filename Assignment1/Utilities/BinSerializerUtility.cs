using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Security;

// Daniel Bäckström, 2014-11-05, Assignment 4
namespace Assignment4
{
    /// <summary>
    /// Utility with generic binary serializing/deserializing methods.
    /// </summary>
    public static class BinSerializerUtility
    {
        /// <summary>
        /// Binary serializes the specified object to the specified filepath using.
        /// 
        /// If the file exists, it will be overwritten, else it will be created.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="filePath">The path to the file.</param>
        internal static void BinaryFileSerialize<T>(T obj, string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
            }
        }

        /// <summary>
        /// Binary deserializes the specified object from the specified filepath and returns it.
        /// </summary>
        /// <typeparam name="T">The type of the serialized object in the file.</typeparam>
        /// <param name="filePath">The file to deserialize.</param>
        /// <returns>The deserialised object</returns>
        internal static T BinaryFileDeSerialize<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
