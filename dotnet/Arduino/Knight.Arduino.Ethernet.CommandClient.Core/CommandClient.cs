using Knight.Arduino.Ethernet.CommandClient.Enum;
using Knight.Arduino.Ethernet.CommandClient.Exceptions;
using System;
using System.Net.Sockets;

namespace Knight.Arduino.Ethernet.CommandClient
{
    /// <summary>
    /// Provides client side function calls for a custom arduino ethernet commend server.
    /// </summary>
    public class CommandClient
    {
        /// <summary>
        /// Gets the arduino_ ethernet_ ip.
        /// </summary>
        /// <value>
        /// The arduino_ ethernet_ ip.
        /// </value>
        public string Arduino_Ethernet_Ip { get; private set; }

        /// <summary>
        /// Gets the arduino_ ethernet_ port.
        /// </summary>
        /// <value>
        /// The arduino_ ethernet_ port.
        /// </value>
        public int Arduino_Ethernet_Port { get; private set; }

        private readonly Client client;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandClient"/> class.
        /// </summary>
        /// <param name="arduino_Ip">The arduino_ ip.</param>
        /// <param name="arduino_Port">The arduino_ port.</param>
        /// <exception cref="CommandClientException">Failed to initialize command client.</exception>
        public CommandClient(string arduino_Ip, string arduino_Port)
        {
            try
            {
                this.Arduino_Ethernet_Ip = arduino_Ip;
                this.Arduino_Ethernet_Port = Convert.ToInt32(arduino_Port);

                TcpClient tcpClient = new TcpClient();
                tcpClient.ConnectAsync(this.Arduino_Ethernet_Ip, this.Arduino_Ethernet_Port).Wait();
                NetworkStream stream = tcpClient.GetStream();
                this.client = new Client(stream);
            }
            catch (Exception ex)
            {
                this.Dispose();
                throw new CommandClientException("Failed to initialize command client.", ex);
            }
        }

        public void Dispose()
        {
            if(this.client != null)
            {
                this.client.Stream.Flush();
                this.client.Stream.Dispose();
            }
        }

        /// <summary>
        /// Sets a digital pin mode.
        /// </summary>
        /// <param name="pin">The pin.</param>
        /// <param name="mode">The mode.</param>
        /// <returns></returns>
        public bool PinMode(Pin pin, PIN_MODE mode)
        {
            ReceivedResult result = client.SendCommand(COMMAND.PIN_MODE, pin, (byte)mode, 0);

            if (result.Status == RESULT_STATUS.SUCCESS) return true;

            return false;
        }

        /// <summary>
        /// DigitalWrite.
        /// </summary>
        /// <param name="pin">The pin.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool DigitalWrite(Pin pin, DIGITAL_VALUE value)
        {
            ReceivedResult result = client.SendCommand(COMMAND.DIGITAL_WRITE, pin, (byte)value, 0);

            if (result.Status == RESULT_STATUS.SUCCESS) return true;

            return false;
        }

        /// <summary>
        /// DigitalPwm.
        /// </summary>
        /// <param name="pin">The pin.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool DigitalPwm(Pin pin, int value)
        {
            ReceivedResult result = client.SendCommand(COMMAND.DIGITAL_PWM, pin, (byte)value, 0);

            if (result.Status == RESULT_STATUS.SUCCESS) return true;

            return false;
        }

        /// <summary>
        /// DigitalRead.
        /// </summary>
        /// <param name="pin">The pin.</param>
        /// <returns></returns>
        /// <exception cref="CommandClientException"></exception>
        public DIGITAL_VALUE DigitalRead(Pin pin)
        {
            ReceivedResult result = client.SendCommand(COMMAND.DIGITAL_READ, pin, 0, 0);

            if (result.Status == RESULT_STATUS.SUCCESS)
            {
                if ((int)result.Value == 0) return DIGITAL_VALUE.LOW;
                return DIGITAL_VALUE.HIGH;
            }

            throw new CommandClientException(string.Format("Failed to retreive digital value! Return code was '{0}'!", result.Status.ToString()));
        }

        /// <summary>
        /// AnalogRead.
        /// </summary>
        /// <param name="pin">The pin.</param>
        /// <returns></returns>
        /// <exception cref="CommandClientException"></exception>
        public int AnalogRead(Pin pin)
        {
            ReceivedResult result = client.SendCommand(COMMAND.ANALOG_READ, pin, 0, 0);

            if (result.Status == RESULT_STATUS.SUCCESS)
            {
                return (int)result.Value;
            }

            throw new CommandClientException(string.Format("Failed to retreive analog value! Return code was '{0}'!", result.Status.ToString()));
        }

        /// <summary>
        /// OneWireTmpRead.
        /// </summary>
        /// <param name="devNumber">The dev number.</param>
        /// <returns></returns>
        /// <exception cref="CommandClientException"></exception>
        public int OneWireTmpRead(int devNumber)
        {
            ReceivedResult result = client.SendCommand(COMMAND.READ_ONEWIRE_TMP, Pin.D2, (byte)devNumber, 0);

            if (result.Status == RESULT_STATUS.SUCCESS)
            {
                return (int)result.Value;
            }

            throw new CommandClientException(string.Format("Failed to retreive analog value! Return code was '{0}'!", result.Status.ToString()));
        }

        public float OneWireTmpReadFloat(int devNumber)
        {
            ReceivedResult result = client.SendCommand(COMMAND.READ_ONEWIRE_TMPF, Pin.D2, (byte)devNumber, 0);

            if (result.Status == RESULT_STATUS.SUCCESS)
            {
                return (float)result.Value;
            }

            throw new CommandClientException(string.Format("Failed to retreive analog value! Return code was '{0}'!", result.Status.ToString()));
        }

        /// <summary>
        /// DefaultLoop.
        /// </summary>
        /// <returns></returns>
        public bool DefaultLoop()
        {
            ReceivedResult result = client.SendCommand(COMMAND.DEFAULT_LOOP, Pin.A0, 0, 0);

            if (result.Status == RESULT_STATUS.SUCCESS)
            {
                return true;
            }

            return false;
        }
    }
}
