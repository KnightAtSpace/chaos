using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Knight.Common.IO.Persistance
{
    /// <summary>
    /// Provides object persistance.
    /// </summary>
    public class XmlFileSerializer<T> : AbstractFileSerializer<T> where T : ISerializable
    {
        private XmlSerializer serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlFileSerializer" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public XmlFileSerializer(string path)
            : base(path)
        {
            if (path == null) throw new ArgumentNullException("path");
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException("Expected valid file path", "path");

            serializer = new XmlSerializer(typeof(T));
        }

        /// <summary>
        /// Serialize.
        /// </summary>
        /// <param name="fileObject">The file object.</param>
        /// <exception cref="Exceptions.IO.PersistanceException"></exception>
        public override void Serialize(T fileObject)
        {
            FileStream stream = this.CreateFile();

            try
            {
                serializer.Serialize(stream, fileObject);
            }
            catch (SystemException sEx)
            {
                throw new Exceptions.IO.PersistanceException(string.Format(CultureInfo.InvariantCulture, "Failed to serialize '{0}'", this.Path), sEx);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserialize.
        /// </summary>
        /// <returns>The deserialized object.</returns>
        /// <exception cref="Exceptions.IO.PersistanceException"></exception>
        public override T Deserialize()
        {
            FileStream stream = this.OpenFile();

            try
            {
                T result = (T)serializer.Deserialize(stream);

                return result;
            }
            catch (SystemException sEx)
            {
                throw new Exceptions.IO.PersistanceException(string.Format(CultureInfo.InvariantCulture, "Failed to deserialize '{0}'", this.Path), sEx);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }
    }
}
