using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knight.Arduino.Ethernet.CommandClient.Exceptions
{
    public class CommandClientException : Exception
    {
        public CommandClientException(string message) : base(message) { }

        public CommandClientException(string message, Exception inner) : base(message, inner) { }
    }
}
