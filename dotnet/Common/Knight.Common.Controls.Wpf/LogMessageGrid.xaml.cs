using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Knight.Common.Controls.Wpf.Model;
using Xceed.Wpf.DataGrid;

namespace Knight.Common.Controls.Wpf
{
    /// <summary>
    /// Interaction logic for LogWindow.xaml
    /// </summary>
    public partial class LogMessageGrid : UserControl
    {
        /// <summary>
        /// Dependency property for log messages.
        /// </summary>
        public static readonly DependencyProperty LogMessagesProperty = DependencyProperty.Register("LogMessages", typeof(ObservableCollection<LogMessage>), typeof(LogMessageGrid), new FrameworkPropertyMetadata(new ObservableCollection<LogMessage>(), OnLogMessagesChanged));

        /// <summary>
        /// Initializes a new instance of the <see cref="LogMessageGrid"/> class.
        /// </summary>
        public LogMessageGrid()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the log messages.
        /// </summary>
        /// <value>
        /// The log messages.
        /// </value>
        public ObservableCollection<LogMessage> LogMessages
        {
            get { return (ObservableCollection<LogMessage>)GetValue(LogMessagesProperty); }
            set
            {
                if (this.LogMessages != null) this.LogMessages.CollectionChanged -= LogMessagesItemsChanged;
                SetValue(LogMessagesProperty, value);
                this.LogMessages.CollectionChanged += LogMessagesItemsChanged;
            }
        }

        private static void OnLogMessagesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LogMessageGrid controlInstance = d as LogMessageGrid;

            if (controlInstance != null)
            {
                controlInstance.LogMessages = (ObservableCollection<LogMessage>)e.NewValue;
                // explicitly set the data binding
                DataGridCollectionViewSource viewSource = controlInstance.Resources["MessageGridViewSource"] as DataGridCollectionViewSource;
                if (viewSource != null)
                    viewSource.Source = controlInstance.LogMessages;
            }
        }

        private void LogMessagesItemsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            DataGridCollectionViewSource viewSource = this.Resources["MessageGridViewSource"] as DataGridCollectionViewSource;
            viewSource.View.MoveCurrentToFirst();
        }

        private void OnClearClick(object sender, RoutedEventArgs e)
        {
            this.LogMessages.Clear();
        }

        private void OnExportClick(object sender, RoutedEventArgs e)
        {
        }
    }
}
