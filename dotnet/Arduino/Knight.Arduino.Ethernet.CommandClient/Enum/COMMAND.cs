using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knight.Arduino.Ethernet.CommandClient.Enum
{
    /// <summary>
    /// Commands defined in the Arduiono.Ethernet.CommandServer.
    /// </summary>
    internal enum COMMAND : byte
    {
        PIN_MODE = 0xA,
        DIGITAL_WRITE = 0xB,
        DIGITAL_READ = 0xC,
        ANALOG_READ = 0xD,
        DIGITAL_PWM = 0xE,
        READ_ONEWIRE_TMP = 0x20,
        DEFAULT_LOOP = 0x23,
        UNKNOWN = 0xff
    }
}
