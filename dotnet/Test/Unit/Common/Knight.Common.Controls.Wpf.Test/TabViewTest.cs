using Knight.Common.Controls.Wpf.ViewModel;
using NUnit.Framework;
using System.Windows.Controls;

namespace Knight.Common.Controls.Wpf
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class TabViewTest
    {
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        [Test, RequiresSTA]
        public void ConstructorTest()
        {
            TabView tv = new TabView();
            Assert.NotNull(tv.TabItemControls);
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        [Test, RequiresSTA]
        public void ShowTabItemVTest()
        {
            TabView tv = new TabView();

            const string item1 = "item 1";
            const string item2 = "item 2";

            tv.SchowTabItem<UserControl>(item1);

            Assert.AreEqual(1, tv.TabItemControls.Count);
            Assert.AreEqual(item1, tv.TabItemControls[0].DisplayName);
            Assert.NotNull(tv.TabItemControls[0].Content);

            tv.SchowTabItem<UserControl>(item2);

            Assert.AreEqual(2, tv.TabItemControls.Count);
            Assert.AreEqual(item2, tv.TabItemControls[1].DisplayName);
            Assert.NotNull(tv.TabItemControls[1].Content);

            tv.TabItemControls[0].CloseCommand.Execute(null);

            Assert.AreEqual(1, tv.TabItemControls.Count);
            Assert.AreEqual(item2, tv.TabItemControls[0].DisplayName);
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        [Test, RequiresSTA]
        public void ShowTabItemVMTest()
        {
            TabView tv = new TabView();

            const string item1 = "item 1";
            const string item2 = "item 2";

            tv.SchowTabItem<UserControl, DummyViewModel>(item1);

            Assert.AreEqual(1, tv.TabItemControls.Count);
            Assert.AreEqual(item1, tv.TabItemControls[0].DisplayName);
            Assert.NotNull(tv.TabItemControls[0].Content);
            Assert.NotNull((tv.TabItemControls[0].Content as UserControl).DataContext);
            Assert.NotNull((tv.TabItemControls[0].Content as UserControl).DataContext is DummyViewModel);

            tv.SchowTabItem<UserControl, DummyViewModel>(item2);

            Assert.AreEqual(2, tv.TabItemControls.Count);
            Assert.AreEqual(item2, tv.TabItemControls[1].DisplayName);
            Assert.NotNull(tv.TabItemControls[1].Content);
            Assert.NotNull((tv.TabItemControls[1].Content as UserControl).DataContext);
            Assert.NotNull((tv.TabItemControls[1].Content as UserControl).DataContext is DummyViewModel);

            tv.TabItemControls[0].CloseCommand.Execute(null);

            Assert.AreEqual(1, tv.TabItemControls.Count);
            Assert.AreEqual(item2, tv.TabItemControls[0].DisplayName);
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        [Test, RequiresSTA]
        public void ShowTabItemVcontrolTest()
        {
            TabView tv = new TabView();

            const string item1 = "item 1";
            const string item2 = "item 2";

            tv.SchowTabItem<CustomControl>(new CustomControl(), item1);

            Assert.AreEqual(1, tv.TabItemControls.Count);
            Assert.AreEqual(item1, tv.TabItemControls[0].DisplayName);
            Assert.NotNull(tv.TabItemControls[0].Content);
            Assert.NotNull((tv.TabItemControls[0].Content as UserControl).DataContext is CustomControl);

            tv.SchowTabItem<CustomControl>(new CustomControl(), item2);

            Assert.AreEqual(2, tv.TabItemControls.Count);
            Assert.AreEqual(item2, tv.TabItemControls[1].DisplayName);
            Assert.NotNull(tv.TabItemControls[1].Content);
            Assert.NotNull((tv.TabItemControls[1].Content as UserControl).DataContext is CustomControl);

            tv.TabItemControls[1].CloseCommand.Execute(null);

            Assert.AreEqual(1, tv.TabItemControls.Count);
            Assert.AreEqual(item1, tv.TabItemControls[0].DisplayName);
        }

        internal class CustomControl : UserControl
        {
            public CustomControl(){}
        }

        internal class DummyViewModel : ViewModelBase
        {
            public DummyViewModel() { }
        }
    }
}
