using System;

namespace Knight.Common.Exceptions.IO
{
    /// <summary>
    /// Path exception.
    /// </summary>
    public class PathException : CommonException
    {
        /// <summary>
        /// The path.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PathException"/> class.
        /// </summary>
        public PathException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PathException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public PathException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PathException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public PathException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PathException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="path">The path.</param>
        public PathException(string message, string path) : base(message) { this.Path = path; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PathException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="path">The path.</param>
        /// <param name="inner">The inner.</param>
        public PathException(string message, string path, Exception inner) : base(message, inner) { this.Path = path; }
    }
}
