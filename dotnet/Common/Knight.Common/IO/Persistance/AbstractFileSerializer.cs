using System;
using System.IO;
using System.Runtime.Serialization;

namespace Knight.Common.IO.Persistance
{
    /// <summary>
    /// Provides base implementations for file serializers.
    /// </summary>
    /// <typeparam name="T">A type derived from <see cref="ISerializable"/>.</typeparam>
    public abstract class AbstractFileSerializer<T> where T : ISerializable
    {
        /// <summary>
        /// The file path. Needs to be set by derived constructors.
        /// </summary>
        public string Path { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractFileSerializer"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        protected AbstractFileSerializer(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException("Cannot handle a empty file path", "path");

            this.Path = path;
        }
        /// <summary>
        /// Serializes the given file object to given Path.
        /// </summary>
        /// <param name="fileObject">The file object</param>
        public abstract void Serialize(T fileObject);

        /// <summary>
        /// Deserialized a file object from the given path.
        /// </summary>
        /// <returns>The file object.</returns>
        public abstract T Deserialize();

        /// <summary>
        /// Creates a new file based on the local path.
        /// </summary>
        /// <returns>The file stream.</returns>
        /// <exception cref="Exceptions.IO.PathException"/>
        protected FileStream CreateFile()
        {
            try
            {
                string directory = System.IO.Path.GetDirectoryName(this.Path);

                if (string.IsNullOrWhiteSpace(directory)) throw new Exceptions.IO.PathException("Failed to get directory from path.", this.Path);

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                return File.Create(this.Path);
            }
            catch (SystemException sEx)
            {
                throw new Exceptions.IO.PathException("Failed to create file.", this.Path, sEx);
            }
        }

        /// <summary>
        /// Opens a file based on the local path.
        /// </summary>
        /// <returns>The file stream.</returns>
        /// <exception cref="Exceptions.IO.PathException"/>
        protected FileStream OpenFile()
        {
            if (!File.Exists(this.Path)) throw new Exceptions.IO.PathException("File doesn't exist.", this.Path);

            try
            {
                return File.Open(this.Path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            }
            catch (SystemException sEx)
            {
                throw new Exceptions.IO.PathException("Failed to open file.", this.Path, sEx);
            }
        }
    }
}
