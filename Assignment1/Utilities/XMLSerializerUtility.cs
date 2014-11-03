using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;
using System.Security;

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
        /// <exception cref="SerializationException">Thrown if an error occurs during serialization.</exception>
        internal static void XMLFileSerialize<T>(T obj, string filePath)
        {
            try
            {
                using (Stream stream = File.Open(filePath, FileMode.Create, FileAccess.Write))
                {
                    XmlSerializer xmlFormat = new XmlSerializer(obj.GetType());
                    xmlFormat.Serialize(stream, obj);
                }
            }
            catch (Exception e)
            {
                if (e is SecurityException || e is UnauthorizedAccessException)
                {
                    throw new SerializationException("You do not have permissions to save to " + filePath + ".", e);
                }
                throw new SerializationException("The file " + filePath + " could not be saved.", e);
            }
        }

        /// <summary>
        /// Deserializes the specified object from the specified filepath using XML and returns it.
        /// </summary>
        /// <typeparam name="T">The type of the serialized object in the file.</typeparam>
        /// <param name="filePath">The file to deserialize.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException">Thrown if an the file specified can not be found.</exception>
        /// <exception cref="SerializationException">Thrown if an error occurs during deserialization.</exception>
        internal static T XMLFileDeSerialize<T>(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("The file could not be found.", filePath);
            T obj;
            try
            {
                using (Stream stream = File.Open(filePath, FileMode.Open))
                {
                    XmlSerializer xmlFormat = new XmlSerializer(typeof(T));
                    obj = (T)xmlFormat.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                if (e is SecurityException || e is UnauthorizedAccessException)
                {
                    throw new SerializationException("You do not have permissions to open " + filePath + ".", e);
                }
                throw new SerializationException("Error reading from " + filePath + ".\n\nPlease make sure the xml file is formated properly.", e);
            }
            return obj;
        }
    }
}
