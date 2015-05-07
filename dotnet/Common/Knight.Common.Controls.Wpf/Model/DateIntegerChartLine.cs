using System.Collections.ObjectModel;
using System.Windows.Media;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace Knight.Common.Controls.Wpf.Model
{
    /// <summary>
    /// Provides a date inter line that can be plotted in a date integer chart.
    /// </summary>
    public class DateIntegerChartLine
    {
        internal ObservableDataSource<DateIntegerPair> PointSource { get; private set; }

        /// <summary>
        /// Gets the color.
        /// </summary>
        public Color Color { get; private set; }

        /// <summary>
        /// Gets the line thickness.
        /// </summary>
        public double Thickness { get; private set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the points collection.
        /// </summary>
        public ObservableCollection<DateIntegerPair> Points { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateIntegerChartLine"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The line thickness.</param>
        /// <param name="description">The description.</param>
        public DateIntegerChartLine(Color color, double thickness, string description)
        {
            if (color == null) { this.Color = Colors.Black; }
            else { this.Color = color; }

            if (thickness <= 0) { this.Thickness = 1; }
            else { this.Thickness = thickness; }

            if (string.IsNullOrWhiteSpace(description)) { this.Description = string.Empty; }
            else { this.Description = description; }

            this.Points = new ObservableCollection<DateIntegerPair>();

            this.Points.CollectionChanged += PointsCollectionChanged;

            this.PointSource = new ObservableDataSource<DateIntegerPair>();
        }

        private void PointsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // wrapped point source actualization
            if (e.Action.Equals(System.Collections.Specialized.NotifyCollectionChangedAction.Add))
            {
                DateIntegerPair[] newItems = new DateIntegerPair[e.NewItems.Count];
                e.NewItems.CopyTo(newItems, 0);
                this.PointSource.AppendMany(newItems);
            }
        }
    }
}
