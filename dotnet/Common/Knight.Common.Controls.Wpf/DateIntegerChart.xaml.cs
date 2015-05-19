using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Knight.Common.Controls.Wpf.Model;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;

namespace Knight.Common.Controls.Wpf
{
    /// <summary>
    /// Interaction logic for ChartControl.xaml
    /// </summary>
    public partial class DateIntegerChart : UserControl
    {
        /// <summary>
        /// Dependency property for the Y axis title.
        /// </summary>
        public static readonly DependencyProperty YAxisTitleProperty = DependencyProperty.Register("YAxisTitle", typeof(string), typeof(DateIntegerChart), new FrameworkPropertyMetadata(string.Empty, OnYAxisTitleChanged));

        /// <summary>
        /// Dependency property for the chart lines.
        /// </summary>
        public static readonly DependencyProperty LinesProperty = DependencyProperty.Register("Lines", typeof(ObservableCollection<DateIntegerChartLine>), typeof(DateIntegerChart), new FrameworkPropertyMetadata(new ObservableCollection<DateIntegerChartLine>(), OnLinesChanged));

        /// <summary>
        /// Dependency property for automated fit into view.
        /// </summary>
        public static readonly DependencyProperty AutoFitIntoViewProperty = DependencyProperty.Register("AutoFitIntoView", typeof(bool), typeof(DateIntegerChart), new FrameworkPropertyMetadata(true, OnAutoFitIntoViewChanged));


        private bool autoFitIntoView;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogMessageGrid"/> class.
        /// </summary>
        public DateIntegerChart()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the Y axis title.
        /// </summary>
        /// <value>
        /// The Y axis title.
        /// </value>
        public string YAxisTitle
        {
            get { return (string)GetValue(YAxisTitleProperty); }
            set { SetValue(YAxisTitleProperty, value); }
        }

        /// <summary>
        /// Gets the lines.
        /// </summary>
        /// <value>
        /// The lines.
        /// </value>
        public ObservableCollection<DateIntegerChartLine> Lines
        {
            get { return (ObservableCollection<DateIntegerChartLine>)GetValue(LinesProperty); }
            set { SetValue(LinesProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [automatic fit into view].
        /// </summary>
        /// <value>
        /// <c>true</c> if [automatic fit into view]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoFitIntoView
        {
            get { return (bool)GetValue(AutoFitIntoViewProperty); }
            set { SetValue(AutoFitIntoViewProperty, value); }
        }

        private static void OnYAxisTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DateIntegerChart controlInstance = d as DateIntegerChart;

            if (controlInstance != null)
            {
                controlInstance.YAxisTitle = (string)e.NewValue;
                // explicitly set the data binding
                controlInstance.VerticalAxisTitle.Content = controlInstance.YAxisTitle;
            }
        }

        private static void OnLinesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DateIntegerChart controlInstance = d as DateIntegerChart;

            if (controlInstance != null)
            {
                controlInstance.OnLinesChanged(e);
            }
        }

        private static void OnAutoFitIntoViewChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DateIntegerChart controlInstance = d as DateIntegerChart;

            if (controlInstance != null)
            {
                controlInstance.OnAutoFitIntoViewChanged(e);
            }
        }

        private void OnLinesChanged(DependencyPropertyChangedEventArgs e)
        {
            if (this.Lines != null)
            {
                this.Lines.CollectionChanged -= LinesCollectionChanged;
            }

            this.Lines = (ObservableCollection<DateIntegerChartLine>)e.NewValue;
            this.Lines.CollectionChanged += LinesCollectionChanged;

            // ecplicit call initially changed event to update the chart
            this.LinesCollectionChanged(this, new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
        }

        private void OnAutoFitIntoViewChanged(DependencyPropertyChangedEventArgs e)
        {
            this.autoFitIntoView = (bool)e.NewValue;
        }

        private void LinesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action.Equals(System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
                || e.Action.Equals(System.Collections.Specialized.NotifyCollectionChangedAction.Reset))
            {
                this.Plotter.Children.RemoveAll(typeof(LineGraph));

                foreach (DateIntegerChartLine chartLine in this.Lines)
                {
                    chartLine.PointSource.SetXMapping(p => dateAxis.ConvertToDouble(p.Date));
                    chartLine.PointSource.SetYMapping(p => p.Number);

                    this.Plotter.AddLineGraph(chartLine.PointSource, chartLine.Color, chartLine.Thickness, chartLine.Description);

                    chartLine.PointSource.DataChanged += PointSource_DataChanged;
                }
            }

            if (e.Action.Equals(System.Collections.Specialized.NotifyCollectionChangedAction.Add))
            {
                DateIntegerChartLine[] newItems = new DateIntegerChartLine[e.NewItems.Count];
                e.NewItems.CopyTo(newItems, 0);

                foreach (DateIntegerChartLine chartLine in newItems)
                {
                    chartLine.PointSource.SetXMapping(p => dateAxis.ConvertToDouble(p.Date));
                    chartLine.PointSource.SetYMapping(p => p.Number);

                    this.Plotter.AddLineGraph(chartLine.PointSource, chartLine.Color, chartLine.Thickness, chartLine.Description);

                    chartLine.PointSource.DataChanged += PointSource_DataChanged;
                }
            }

            if (e.Action.Equals(System.Collections.Specialized.NotifyCollectionChangedAction.Remove))
            {

            }

            this.Plotter.Viewport.FitToView();
        }

        private void PointSource_DataChanged(object sender, EventArgs e)
        {
            if (this.autoFitIntoView)
            {
                this.Plotter.Viewport.FitToView();
            }
        }

        private void Init()
        {

            List<BugInfo> bugInfoList = LoadBugInfo();

            DateTime[] dates = new DateTime[bugInfoList.Count];
            int[] numberOpen = new int[bugInfoList.Count];
            int[] numberClosed = new int[bugInfoList.Count];

            DateIntegerPair[] pairs = new DateIntegerPair[bugInfoList.Count];

            for (int i = 0; i < bugInfoList.Count; ++i)
            {
                dates[i] = bugInfoList[i].date;
                numberOpen[i] = bugInfoList[i].numberOpen;
                numberClosed[i] = bugInfoList[i].numberClosed;

                pairs[i] = new DateIntegerPair(bugInfoList[i].date, bugInfoList[i].numberOpen);
            }

            //this.Lines = new ObservableCollection<DateIntegerChartLine>();

            //DateIntegerChartLine line = new DateIntegerChartLine(Colors.Aquamarine, 5, "New Line");
            //line.Points.AddMany(pairs);
            //this.Lines.Add(line);

            var datesDataSource = new EnumerableDataSource<DateTime>(dates);
            datesDataSource.SetXMapping(dateAxis.ConvertToDouble);

            var numberOpenDataSource = new EnumerableDataSource<int>(numberOpen);
            numberOpenDataSource.SetYMapping(y => y);

            var numberClosedDataSource = new EnumerableDataSource<int>(numberClosed);
            numberClosedDataSource.SetYMapping(y => y);

            CompositeDataSource compositeDataSource1 = new
              CompositeDataSource(datesDataSource, numberOpenDataSource);
            CompositeDataSource compositeDataSource2 = new
              CompositeDataSource(datesDataSource, numberClosedDataSource);

            // this.Plotter.AddLineGraph(compositeDataSource1, Colors.Blue, 2, "Open Bugs");
            //plotter.AddLineGraph(compositeDataSource1,
            //  new Pen(Brushes.Blue, 2),
            //  new CirclePointMarker { Size = 10.0, Fill = Brushes.Red },
            //  new PenDescription("Number bugs open"));

            this.Plotter.AddLineGraph(compositeDataSource2,
              new Pen(Brushes.Green, 2),
              new TrianglePointMarker
              {
                  Size = 10.0,
                  Pen = new Pen(Brushes.Black, 2.0),
                  Fill = Brushes.GreenYellow
              },
              new PenDescription("Number bugs closed"));

            this.Plotter.Viewport.FitToView();

        }

        private static List<BugInfo> LoadBugInfo()
        {
            var result = new List<BugInfo>();

            for (int i = 0; i < 20; i++)
            {
                DateTime d = DateTime.Now.AddHours(2 * i);
                int numopen = 10 * i;
                int numclosed = 5 * i;
                BugInfo bi = new BugInfo(d, numopen, numclosed);
                result.Add(bi);
            }

            return result;
        }

    }

    public class BugInfo
    {
        public DateTime date;
        public int numberOpen;
        public int numberClosed;

        public BugInfo(DateTime date, int numberOpen, int numberClosed)
        {
            this.date = date;
            this.numberOpen = numberOpen;
            this.numberClosed = numberClosed;
        }
    }
}
