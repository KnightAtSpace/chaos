using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knight.Arduino.Ethernet.CommandClient.Enum
{
    public enum PIN_MODE : byte
    {
        INPUT = 0x0,
        OUTPUT = 0x1,
        INPUT_PULLED = 0x2
    }
}
