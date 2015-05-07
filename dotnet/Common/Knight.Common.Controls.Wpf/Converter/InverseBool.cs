using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Knight.Common.Controls.Wpf.Converter
{
    /// <summary>
    /// Provides a value converter for bool to inverse bool.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBool : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InverseBool" /> class.
        /// </summary>
        public InverseBool()
        {
            // empty default constuctor for xaml bindings
        }

        /// <summary>
        /// When implemented in a derived class, returns an object that is set as the value of the target property for this markup extension.
        /// </summary>
        /// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
        /// <returns>
        /// The object value to set on the property where the extension is applied.
        /// </returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new InverseBool();
        }

        /// <summary>
        /// Converts a bool into a inverse bool.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            bool source = (bool)value;

            return !source;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            bool source = (bool)value;

            return !source;
        }
    }
}
