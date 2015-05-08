using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knight.Arduino.Ethernet.CommandClient.Enum
{
    internal enum RESULT_STATUS : byte
    {
        SUCCESS = 0x64,
        ERROR_COMMAND = 0x66,
        ERROR_PIN = 0x67,
        ERROR_VALUE = 0x68,
        DEFAULT_LOOP = 0x69,
        UNKNOWN = 0xff
    }
}
