using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;
using System.Security;

// Daniel Bäckström, 2014-11-05, Assignment 4
namespace Assignment4
{
    /// <summary>
    /// Utility with generic XML serializing/deserializing methods.
    /// </summary>
    public static class XMLSerializerUtility
    {
        /// <summary>
        /// Serializes the specified object to the specified filepath using XML.
        /// 
        /// If the file exists, it will be overwritten, else it will be created.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="filePath">The path to the file.</param>
        internal static void XMLFileSerialize<T>(T obj, string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Create, FileAccess.Write))
            {
                XmlSerializer xmlFormat = new XmlSerializer(obj.GetType());
                xmlFormat.Serialize(stream, obj);
            }
        }

        /// <summary>
        /// Deserializes the specified object from the specified filepath using XML and returns it.
        /// </summary>
        /// <typeparam name="T">The type of the serialized object in the file.</typeparam>
        /// <param name="filePath">The file to deserialize.</param>
        /// <returns>The deserialised object</returns>
        internal static T XMLFileDeSerialize<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(T));
                return (T)xmlFormat.Deserialize(stream);
            }
        }
    }
}
