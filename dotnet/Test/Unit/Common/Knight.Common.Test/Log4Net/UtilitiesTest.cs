using log4net.Core;
using NUnit.Framework;

namespace Knight.Common.Log4Net
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class UtilitiesTest
    {
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        [Test]
        public void FindLogLevelTest()
        {
            // default if no level found
            Assert.AreEqual(Level.Debug, Utilities.FindLogLevel(string.Empty));
            Assert.AreEqual(Level.Debug, Utilities.FindLogLevel(null));
            Assert.AreEqual(Level.Debug, Utilities.FindLogLevel("some undefined"));

            // correct level
            Assert.AreEqual(Level.Debug, Utilities.FindLogLevel("Debug"));
            Assert.AreEqual(Level.Info, Utilities.FindLogLevel("Info"));
            Assert.AreEqual(Level.Warn, Utilities.FindLogLevel("Warn"));
            Assert.AreEqual(Level.Error, Utilities.FindLogLevel("Error"));
            Assert.AreEqual(Level.Error, Utilities.FindLogLevel("erROr"));
            Assert.AreEqual(Level.Fatal, Utilities.FindLogLevel("fatal"));
        }
    }
}
