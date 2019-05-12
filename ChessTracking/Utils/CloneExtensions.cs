using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.Utils
{
    static class CloneExtensions
    {
        /// <summary>
        /// Performs deep clone on Serializable objects
        /// </summary>
        /// <param name="obj">Object to copy</param>
        /// <returns>Deep copy of object</returns>
        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
    }
}
