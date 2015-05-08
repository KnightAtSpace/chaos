using Knight.Arduino.Ethernet.CommandClient.Enum;

namespace Knight.Arduino.Ethernet.CommandClient
{
    /// <summary>
    /// Singelton for Arduino PIN definitions.
    /// </summary>
    public class Pin
    {
        public static Pin D2 = new Pin("2", (byte)PIN_DIGITAL.D2);
        public static Pin D3 = new Pin("3-PWM", (byte)PIN_DIGITAL.D3);
        public static Pin D5 = new Pin("5-PWM", (byte)PIN_DIGITAL.D5);
        public static Pin D6 = new Pin("6-PWM", (byte)PIN_DIGITAL.D6);
        public static Pin D7 = new Pin("7", (byte)PIN_DIGITAL.D7);
        public static Pin D8 = new Pin("8", (byte)PIN_DIGITAL.D8);
        public static Pin D9 = new Pin("9-PWM", (byte)PIN_DIGITAL.D9);

        public static Pin A0 = new Pin("14-A0", (byte)PIN_ANALOG.A0);
        public static Pin A1 = new Pin("15-A1", (byte)PIN_ANALOG.A1);
        public static Pin A2 = new Pin("16-A2", (byte)PIN_ANALOG.A2);
        public static Pin A3 = new Pin("17-A3", (byte)PIN_ANALOG.A3);
        public static Pin A4 = new Pin("18-A4", (byte)PIN_ANALOG.A4);
        public static Pin A5 = new Pin("19-A5", (byte)PIN_ANALOG.A5);

        public string Description { get; private set; }

        internal byte ByteValue { get; private set; }

        private Pin(string description, byte byteValue)
        {
            this.Description = description;
            this.ByteValue = byteValue;
        }
    }
}
