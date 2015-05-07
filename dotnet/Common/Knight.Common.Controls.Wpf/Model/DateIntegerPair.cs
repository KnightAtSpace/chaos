using System;

namespace Knight.Common.Controls.Wpf.Model
{
    /// <summary>
    /// Provides a pair of date and integer.
    /// </summary>
    public class DateIntegerPair
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateIntegerPair"/> class.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="number">The integer.</param>
        public DateIntegerPair(DateTime date, int number)
        {
            if (date == null) throw new ArgumentNullException("date");

            this.Date = date;
            this.Number = number;
        }

        /// <summary>
        /// Gets the date.
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Gets the number.
        /// </summary>
        public int Number { get; private set; }
    }
}
