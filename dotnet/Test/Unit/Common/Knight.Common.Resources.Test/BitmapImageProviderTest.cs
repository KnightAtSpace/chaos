using log4net.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Knight.Common.Resources
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class BitmapImageProviderTest
    {
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        [Test]
        public void GetByNameTest()
        {
            Assert.IsNotNull(BitmapImageProvider.Instance);

            Assert.IsNull(BitmapImageProvider.Instance.GetByName("1234abc"));

            const string name = "Info";
            const string nameLower = "info";
            const string nameUpper = "INFO";

            Assert.IsNotNull(BitmapImageProvider.Instance.GetByName(name), "Expected image for '{0}'.", name);
            Assert.IsNotNull(BitmapImageProvider.Instance.GetByName(nameLower), "Expected image for '{0}'.", nameLower);
            Assert.IsNotNull(BitmapImageProvider.Instance.GetByName(nameUpper), "Expected image for '{0}'.", nameUpper);
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        [Test]
        public void GetForLogLevelTest()
        {
            Assert.IsNotNull(BitmapImageProvider.Instance);

            Assert.Throws<ArgumentNullException>(() => BitmapImageProvider.Instance.GetForLogLevel(null));

            foreach (Level level in LogLevelProvider())
            {
                Assert.IsNotNull(BitmapImageProvider.Instance.GetForLogLevel(level), "Expected image for '{0}'.", level.DisplayName);
            }
        }

        private IEnumerable<Level> LogLevelProvider()
        {
            yield return Level.Debug;
            yield return Level.Info;
            yield return Level.Warn;
            yield return Level.Alert;
            yield return Level.Error;
            yield return Level.Fatal;
            yield return Level.Verbose;
        }
    }
}
