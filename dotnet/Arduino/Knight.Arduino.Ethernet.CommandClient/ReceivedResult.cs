using Knight.Arduino.Ethernet.CommandClient.Enum;

namespace Knight.Arduino.Ethernet.CommandClient
{
    /// <summary>
    /// Holds a result received from a custom Arduino ehternet command server.
    /// </summary>
    internal class ReceivedResult
    {
        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public RESULT_STATUS Status { get; private set; }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public COMMAND Command { get; private set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceivedResult"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="command">The command.</param>
        /// <param name="value">The value.</param>
        public ReceivedResult(RESULT_STATUS status, COMMAND command, int value)
        {
            this.Status = status;
            this.Command = command;
            this.Value = value;
        }
    }
}
