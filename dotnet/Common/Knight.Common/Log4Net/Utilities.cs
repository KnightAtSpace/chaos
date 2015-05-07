using System;
using log4net;
using log4net.Core;

namespace Knight.Common.Log4Net
{
    /// <summary>
    /// Provides utility functions for log4net.
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Searches for a log4net log level by the given level name.
        /// </summary>
        /// <param name="logLevel">The level name to search for.</param>
        /// <returns>The log level that corresponds to the given name or <see cref="log4net.Core.Level.Debug"/> if no level was found.</returns>
        public static Level FindLogLevel(string logLevel)
        {
            if (!string.IsNullOrWhiteSpace(logLevel) && null != LogManager.GetRepository().LevelMap[logLevel])
            {
                try
                {
                    return LogManager.GetRepository().LevelMap[logLevel];
                }
                catch (ArgumentNullException)
                {
                    return Level.Debug;
                }
            }

            return Level.Debug;
        }
    }
}
