using System;

namespace Knight.Common.Exceptions.Resources
{
    /// <summary>
    /// Format resource string exception.
    /// </summary>
    public class FormatResourceStringException : CommonException
    {
        /// <summary>
        /// The resource manager base name.
        /// </summary>
        public string ResourceManagerBaseName { get; private set; }

        /// <summary>
        /// The resource name.
        /// </summary>
        public string ResourceName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatResourceStringException"/> class.
        /// </summary>
        public FormatResourceStringException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatResourceStringException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public FormatResourceStringException(string message) : base(message) { }


        /// <summary>
        /// Initializes a new instance of the <see cref="FormatResourceStringException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public FormatResourceStringException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatResourceStringException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="resourceManagerBaseName">The resource manager base name.</param>
        /// <param name="resourceName">The resource name.</param>
        public FormatResourceStringException(string message, string resourceManagerBaseName, string resourceName)
            : base(message)
        {
            this.ResourceManagerBaseName = resourceManagerBaseName;
            this.ResourceName = resourceName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatResourceStringException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="resourceManagerBaseName">The resource manager base name.</param>
        /// <param name="resourceName">The resource name.</param>
        /// <param name="inner">The inner.</param>
        public FormatResourceStringException(string message, string resourceManagerBaseName, string resourceName, Exception inner)
            : base(message, inner)
        {
            this.ResourceManagerBaseName = resourceManagerBaseName;
            this.ResourceName = resourceName;
        }
    }
}
