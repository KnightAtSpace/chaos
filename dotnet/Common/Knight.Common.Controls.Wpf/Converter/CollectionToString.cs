using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace Knight.Common.Controls.Wpf.Converter
{
    /// <summary>
    /// Provides a value converter for ICollection<ValidationError> to string.
    /// </summary>
    [ValueConversion(typeof(ICollection<ValidationError>), typeof(string))]
    public class CollectionToString : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionToString" /> class.
        /// </summary>
        public CollectionToString()
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
            return new CollectionToString();
        }

        /// <summary>
        /// Converts a collection into a string list.
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
            ICollection<ValidationError> errors =
                value as ICollection<ValidationError>;

            if (errors == null)
            {
                return string.Empty;
            }

            return string.Join(Environment.NewLine, (from e in errors
                                                     select e.ErrorContent as string).ToArray());
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
            return DependencyProperty.UnsetValue;
        }
    }
}
