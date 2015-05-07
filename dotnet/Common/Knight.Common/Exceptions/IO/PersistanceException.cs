using System;

namespace Knight.Common.Exceptions.IO
{
    /// <summary>
    /// Persistance exception.
    /// </summary>
    public class PersistanceException : CommonException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersistanceException"/> class.
        /// </summary>
        public PersistanceException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistanceException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public PersistanceException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistanceException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public PersistanceException(string message, Exception inner) : base(message, inner) { }
    }
}
