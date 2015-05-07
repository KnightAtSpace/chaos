using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Knight.Common.Exceptions;

namespace Knight.Common.IO.Persistance
{
    /// <summary>
    /// Provides object persistance.
    /// </summary>
    [Obsolete("Deserialization not working properly")]
    public class BinaryFileSerializer<T> : AbstractFileSerializer<T> where T : ISerializable
    {
        private BinaryFormatter serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlFileSerializer"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public BinaryFileSerializer(string path)
            : base(path)
        {
            if (!path.EndsWith(".bin", StringComparison.Ordinal)) throw new ArgumentException("Path must end with extension '.bin'", "path");

            serializer = new BinaryFormatter();
        }

        /// <summary>
        /// Serialize.
        /// </summary>
        /// <param name="fileObject">The file object.</param>
        /// <exception cref="Exceptions.IO.PersistanceException"></exception>
        public override void Serialize(T fileObject)
        {
            FileStream stream = null;
            try
            {
                stream = this.OpenFile();
            }
            catch (CommonException)
            {
                stream = this.CreateFile();
            }

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
