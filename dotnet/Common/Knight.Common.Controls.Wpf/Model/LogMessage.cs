using System;
using System.Runtime.Serialization;
using System.Windows.Media.Imaging;
using Knight.Common.Resources;
using log4net.Core;

namespace Knight.Common.Controls.Wpf.Model
{
    /// <summary>
    /// Provides a serilizable model for log messages.
    /// </summary>
    public class LogMessage : ISerializable
    {
        [NonSerialized]
        private BitmapImage icon;
        /// <summary>
        /// Initializes a new instance of the <see cref="LogMessage" /> class.
        /// </summary>
        /// <param name="timeStamp">The time stamp.</param>
        /// <param name="level">The level.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <exception cref="System.ArgumentNullException">level
        /// or
        /// timeStamp
        /// or
        /// message</exception>
        public LogMessage(DateTime timeStamp, Level level, string message, string exception = null)
        {
            if (level == null) throw new ArgumentNullException("level");
            if (timeStamp == null) throw new ArgumentNullException("timeStamp");
            if (message == null) throw new ArgumentNullException("message");

            this.icon = BitmapImageProvider.Instance.GetForLogLevel(level);
            this.Timestamp = timeStamp;
            this.Level = level.DisplayName;
            this.Message = message;
            this.Exception = exception;
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public BitmapImage Icon { get { return this.icon; } }

        /// <summary>
        /// Gets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public string Level { get; set; }

        /// <summary>
        /// Gets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public string User { get; set; }

        /// <summary>
        /// Gets the machine.
        /// </summary>
        /// <value>
        /// The machine.
        /// </value>
        public string Machine { get; set; }

        /// <summary>
        /// Gets the application.
        /// </summary>
        /// <value>
        /// The application.
        /// </value>
        public string Application { get; set; }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public string Category { get; set; }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public string Exception { get; set; }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // not used
        }
    }
}
