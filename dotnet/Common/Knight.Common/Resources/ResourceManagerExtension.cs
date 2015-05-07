using Knight.Common.Exceptions.Resources;
using System;
using System.Globalization;
using System.Resources;

namespace Knight.Common.Resources
{
    /// <summary>
    /// Provides extension methods for <see cref="ResourceManager"/>
    /// </summary>
    public static class ResourceManagerExtension
    {
        /// <summary>
        /// Gets a formatted string using <see cref="string.Format"/>
        /// </summary>
        /// <param name="resourceManager">The resource manager.</param>
        /// <param name="resourceName">The resource name.</param>
        /// <param name="formatParameter">the format parameter</param>
        /// <returns>The formatted string.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FormatResourceStringException"></exception>
        public static string GetFormattedString(this ResourceManager resourceManager, string resourceName, params object[] formatParameter)
        {
            if (resourceManager == null) throw new ArgumentNullException("resourceManager");
            if (resourceName == null) throw new ArgumentNullException("resourceName");
            if (string.IsNullOrWhiteSpace(resourceName)) throw new ArgumentException("Cannot get a resource by empty name.", "resourceName");
            if (formatParameter.Length == 0) throw new ArgumentException("Cannot format a string with empty parameter.", "formatParameter");

            string resourceValue = resourceManager.GetString(resourceName);

            if (string.IsNullOrWhiteSpace(resourceValue))
            {
                throw new FormatResourceStringException("Resource string not found or empty.", resourceManager.BaseName, resourceName);
            }

            try
            {
                return string.Format(CultureInfo.InvariantCulture, resourceValue, formatParameter);
            }
            catch (SystemException sEx)
            {
                throw new FormatResourceStringException("Failed to format resource string.", resourceManager.BaseName, resourceName, sEx);
            }
        }
    }
}
