﻿using Knight.Arduino.Ethernet.CommandClient.Enum;
using System;
using System.Net.Sockets;

namespace Knight.Arduino.Ethernet.CommandClient
{
    internal class Client
    {
        public NetworkStream Stream { get; private set; }

        public Client(NetworkStream stream)
        {
            this.Stream = stream;
        }

        public ReceivedResult SendCommand(COMMAND command, Pin pin, byte value1, byte value2)
        {
            byte[] c = new byte[] { 0x23, (byte)command, pin.ByteValue, value1, value2 };

            if (command == COMMAND.READ_ONEWIRE_TMPF)
            {
                return SendCommandF(c);
            }

            return SendCommand(c);
        }

        private ReceivedResult SendCommand(byte[] command)
        {
            Stream.Write(command, 0, command.Length);
            Stream.Flush();

            // byte, byte, int (status, function, result)
            int byteStatus = Stream.ReadByte();
            if (byteStatus == -1)
            {
                return BuildResult(0xff, command[0], 0);
            }

            int byteFuncttion = Stream.ReadByte();
            if (byteFuncttion == -1)
            {
                return BuildResult(0xff, command[0], 0);
            }

            int byteValue0 = Stream.ReadByte();
            if (byteValue0 == -1)
            {
                return BuildResult(0xff, command[0], 0);
            }

            int byteValue1 = Stream.ReadByte();
            if (byteValue1 == -1)
            {
                return BuildResult(0xff, command[0], 0);
            }
            int byteValue2 = Stream.ReadByte();
            if (byteValue2 == -1)
            {
                return BuildResult(0xff, command[0], 0);
            }
            int byteValue3 = Stream.ReadByte();
            if (byteValue3 == -1)
            {
                return BuildResult(0xff, command[0], 0);
            }

            byte b0 = Byte.Parse(byteValue0.ToString());
            byte b1 = Byte.Parse(byteValue1.ToString());
            byte b2 = Byte.Parse(byteValue2.ToString());
            byte b3 = Byte.Parse(byteValue3.ToString());


            uint result = BitConverter.ToUInt32(new Byte[] { b0, b1, b2, b3 }, 0);

            if (Stream.DataAvailable)
            {
                byte[] trash = new byte[sizeof(int)];
                Stream.Read(trash, 0, sizeof(int));
            }

            return BuildResult((byte)byteStatus, command[0], (int)result);
        }

        private ReceivedResult SendCommandF(byte[] command)
        {
            Stream.Write(command, 0, command.Length);
            Stream.Flush();

            // byte, byte, int (status, function, result)
            int byteStatus = Stream.ReadByte();
            if (byteStatus == -1)
            {
                return BuildResult(0xff, command[0], 0);
            }

            int byteFuncttion = Stream.ReadByte();
            if (byteFuncttion == -1)
            {
                return BuildResult(0xff, command[0], 0);
            }

            int byteValue0 = Stream.ReadByte();
            if (byteValue0 == -1)
            {
                return BuildResult(0xff, command[0], 0);
            }

            int byteValue1 = Stream.ReadByte();
            if (byteValue1 == -1)
            {
                return BuildResult(0xff, command[0], 0);
            }
            int byteValue2 = Stream.ReadByte();
            if (byteValue2 == -1)
            {
                return BuildResult(0xff, command[0], 0);
            }
            int byteValue3 = Stream.ReadByte();
            if (byteValue3 == -1)
            {
                return BuildResult(0xff, command[0], 0);
            }

            byte b0 = Byte.Parse(byteValue0.ToString());
            byte b1 = Byte.Parse(byteValue1.ToString());
            byte b2 = Byte.Parse(byteValue2.ToString());
            byte b3 = Byte.Parse(byteValue3.ToString());


            
            float result = BitConverter.ToSingle(new Byte[] { b3, b2, b1, b0 }, 0);

            if (Stream.DataAvailable)
            {
                byte[] trash = new byte[sizeof(int)];
                Stream.Read(trash, 0, sizeof(int));
            }

            return BuildResult((byte)byteStatus, command[0], result);
        }

        private ReceivedResult BuildResult(byte status, byte function, object value)
        {
            RESULT_STATUS rs = RESULT_STATUS.UNKNOWN;
            COMMAND rc = COMMAND.UNKNOWN;
            foreach (RESULT_STATUS s in System.Enum.GetValues(typeof(RESULT_STATUS)))
            {
                if ((byte)s == status) { rs = s; }
            }

            foreach (COMMAND c in System.Enum.GetValues(typeof(COMMAND)))
            {
                if ((byte)c == function) { rc = c; }
            }

            return new ReceivedResult(rs, rc, value);
        }
    }
}
