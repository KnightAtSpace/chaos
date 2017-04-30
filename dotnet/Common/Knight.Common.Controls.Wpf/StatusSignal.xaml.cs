using System.Windows;
using System.Windows.Controls;

namespace Knight.Common.Controls.Wpf
{
    /// <summary>
    /// Interaction logic for StatusSignal.xaml
    /// </summary>
    public partial class StatusSignal : UserControl
    {
        /// <summary>
        /// The signal value.
        /// </summary>
        public static readonly DependencyProperty SignalValueProperty = DependencyProperty.Register("SignalValue", typeof(StatusSignalValue), typeof(StatusSignal), new FrameworkPropertyMetadata(StatusSignalValue.OFF, OnSignalValueChanged));

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusSignal"/> class.
        /// </summary>
        public StatusSignal()
        {
            InitializeComponent();
            this.SignalImage.Source = Knight.Common.Resources.BitmapImageProvider.Instance.GetByName("statusgrey");
        }

        /// <summary>
        /// Gets or sets the signal value.
        /// </summary>
        /// <value>
        /// The signal value.
        /// </value>
        public StatusSignalValue SignalValue
        {
            get { return (StatusSignalValue)GetValue(SignalValueProperty); }
            set { SetValue(SignalValueProperty, value); }
        }

        private static void OnSignalValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StatusSignal controlInstance = d as StatusSignal;

            if (controlInstance != null)
            {
                switch ((StatusSignalValue)e.NewValue)
                {
                    case StatusSignalValue.RED:
                        controlInstance.SignalImage.Source = Knight.Common.Resources.BitmapImageProvider.Instance.GetByName("statusred");
                        break;
                    case StatusSignalValue.YELLOW:
                        controlInstance.SignalImage.Source = Knight.Common.Resources.BitmapImageProvider.Instance.GetByName("statusyellow");
                        break;
                    case StatusSignalValue.GREEN:
                        controlInstance.SignalImage.Source = Knight.Common.Resources.BitmapImageProvider.Instance.GetByName("statusgreen");
                        break;
                    case StatusSignalValue.OFF:
                        controlInstance.SignalImage.Source = Knight.Common.Resources.BitmapImageProvider.Instance.GetByName("statusgrey");
                        break;
                    default:
                        controlInstance.SignalImage.Source = Knight.Common.Resources.BitmapImageProvider.Instance.GetByName("statusgrey");
                        break;
                }
            }
        }

        /// <summary>
        /// Status signal values.
        /// </summary>
        public enum StatusSignalValue
        {
            /// <summary>
            /// The red
            /// </summary>
            RED,
            /// <summary>
            /// The yellow
            /// </summary>
            YELLOW,
            /// <summary>
            /// The green
            /// </summary>
            GREEN,
            /// <summary>
            /// The off
            /// </summary>
            OFF
        }
    }
}
