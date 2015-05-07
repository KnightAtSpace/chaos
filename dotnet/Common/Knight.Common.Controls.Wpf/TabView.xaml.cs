using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using Knight.Common.Controls.Wpf.Model;
using Knight.Common.Controls.Wpf.ViewModel;

namespace Knight.Common.Controls.Wpf
{
    /// <summary>
    /// Provides interaction logic for a custom tab view user control
    /// </summary>
    public partial class TabView : UserControl
    {
        private readonly ObservableCollection<ClosableTabItem> tabItemControls = new ObservableCollection<ClosableTabItem>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TabView"/> class.
        /// </summary>
        public TabView()
        {
            InitializeComponent();

            tabItemControls.CollectionChanged += this.OnTabItemControlsChanged;
        }

        /// <summary>
        /// Returns the collection of available tab items.
        /// </summary>
        public ObservableCollection<ClosableTabItem> TabItemControls
        {
            get
            {
                return tabItemControls;
            }
        }

        /// <summary>
        /// Schows a tab item.
        /// </summary>
        /// <typeparam name="V">The type of the given instance that will be used as tab item content.</typeparam>
        public ClosableTabItem SchowTabItem<V>(V userControl, string displayName)
            where V : UserControl, new()
        {
            if (userControl == null) throw new ArgumentNullException("userControl");

            ClosableTabItem tabItem = tabItemControls.FirstOrDefault(c => c.Content is V && c.DisplayName.Equals(displayName)) as ClosableTabItem;

            if (tabItem == null)
            {
                tabItem = new ClosableTabItem(displayName, userControl);

                tabItemControls.Add(tabItem);
            }

            this.SetActiveTabItem(tabItem);

            return tabItem;
        }

        /// <summary>
        /// Schows a tab item.
        /// </summary>
        /// <typeparam name="V">The type a new instance will be created of and used as tab item content.</typeparam>
        public ClosableTabItem SchowTabItem<V>(string displayName)
            where V : UserControl, new()
        {
            ClosableTabItem tabItem = tabItemControls.FirstOrDefault(c => c.Content is V && c.DisplayName.Equals(displayName)) as ClosableTabItem;

            if (tabItem == null)
            {
                tabItem = new ClosableTabItem(displayName, new V());

                tabItemControls.Add(tabItem);
            }

            this.SetActiveTabItem(tabItem);

            return tabItem;
        }

        /// <summary>
        /// Schows a tab item.
        /// </summary>
        /// <typeparam name="V">The type a new instance will be created of and used as tab item content.</typeparam>
        /// <typeparam name="VM">The type of the view model (MVVM) that is instanciated and assigned as data context of <typeparamref name="V"/>.</typeparam>
        public ClosableTabItem SchowTabItem<V, VM>(string displayName)
            where V : UserControl, new()
            where VM : ViewModelBase, new()
        {
            ClosableTabItem tabItem = tabItemControls.FirstOrDefault(c => c.Content is V && c.DisplayName.Equals(displayName)) as ClosableTabItem;

            if (tabItem == null)
            {
                V control = new V();
                control.DataContext = new VM();

                tabItem = new ClosableTabItem(displayName, control);

                tabItemControls.Add(tabItem);
            }

            this.SetActiveTabItem(tabItem);

            return tabItem;
        }

        private void OnTabItemControlsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (ClosableTabItem tabItem in e.NewItems)
                {

                    tabItem.RequestClose += this.OnTabItemControlRequestClose;
                }

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (ClosableTabItem tabItem in e.OldItems)
                {
                    tabItem.RequestClose -= this.OnTabItemControlRequestClose;
                }
        }

        private void OnTabItemControlRequestClose(object sender, EventArgs e)
        {
            ClosableTabItem tabItem = sender as ClosableTabItem;

            if (tabItem != null)
            {
                if (tabItem.Content != null)
                {
                    UserControl userControl = tabItem.Content as UserControl;
                    if (userControl != null)
                    {
                        ViewModelBase viewModel = userControl.DataContext as ViewModelBase;
                        if (viewModel != null) viewModel.Dispose();
                    }
                }

                tabItemControls.Remove(tabItem);
            }
        }

        private void SetActiveTabItem(ClosableTabItem tabItem)
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(tabItemControls);
            if (collectionView != null)
                collectionView.MoveCurrentTo(tabItem);
        }
    }
}
