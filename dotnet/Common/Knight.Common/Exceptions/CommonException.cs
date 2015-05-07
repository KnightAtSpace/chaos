using System;

namespace Knight.Common.Exceptions
{
    /// <summary>
    /// Common exception.
    /// </summary>
    public class CommonException : Exception
    {
        /// <inheritDoc/>
        public CommonException() : base() { }

        /// <inheritDoc/>
        public CommonException(string message) : base(message) { }

        /// <inheritDoc/>
        public CommonException(string message, Exception inner) : base(message, inner) { }
    }
}
