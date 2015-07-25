using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace CellularAutomation
{
    /// <summary>
    /// Writes and pulls objects to/from files
    /// </summary>
    public class Serialization
    {
        /// <summary>
        /// Serializes an object of type "T" to a file
        /// </summary>
        public static void SerializeObject<T>(string FileName, T ObjectToSerialize)
        {
            Stream stream = File.Open(FileName, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, ObjectToSerialize);
            stream.Close();
        }

        /// <summary>
        /// Deserializes an object of type "T" from a file
        /// </summary>
        public static T DeserializeObject<T>(string FileName)
        {
            T ObjectToDeserialize;
            Stream stream = File.Open(FileName, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            ObjectToDeserialize = (T)bFormatter.Deserialize(stream);
            stream.Close();
            return ObjectToDeserialize;
        }
    }
}
