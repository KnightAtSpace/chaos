using System;
using System.Windows.Controls;

namespace Knight.Common.Controls.Wpf.Model
{
    /// <summary>
    /// Provides a closable tab item for <see cref="TabView"/>.
    /// </summary>
    public class ClosableTabItem : ContentControl
    {
        private string displayName;
        private RelayCommand closeCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClosableTabItem" /> class.
        /// </summary>
        /// <param name="displayName">The name for the tab header.</param>
        /// <param name="content">The content.</param>
        public ClosableTabItem(string displayName, UserControl content)
        {
            if (content == null) throw new ArgumentNullException("content");

            this.DisplayName = string.IsNullOrWhiteSpace(displayName) ? content.GetType().Name : displayName;

            this.Content = content;
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName
        {
            get
            {
                return this.displayName;
            }
            set
            {
                if (this.displayName != value && !string.IsNullOrWhiteSpace(value))
                {
                    this.displayName = value;
                }
            }
        }

        /// <summary>
        /// Returns the command that, when invoked, attempts
        /// to remove this tab item from the tab view.
        /// </summary>
        public RelayCommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                    closeCommand = new RelayCommand(param => this.OnRequestClose());

                return closeCommand;
            }
        }

        /// <summary>
        /// Raised when this tab item should be removed from the tab view.
        /// </summary>
        public event EventHandler RequestClose;

        private void OnRequestClose()
        {
            EventHandler handler = this.RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}
