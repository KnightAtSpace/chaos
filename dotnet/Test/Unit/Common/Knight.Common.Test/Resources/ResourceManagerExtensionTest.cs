using Knight.Common.Exceptions.Resources;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Resources;

namespace Knight.Common.Resources
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class ResourceManagerExtensionTest
    {
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        [Test]
        public void GetFormattedStringTest()
        {
            const string resourceName = "resource name";
            const string resourceValue = "resource value";
            const string resourceFormatValue = "resource value {0}";

            object formatObject = 1;
            string passFormattedResult = string.Format(resourceFormatValue, formatObject);

            ResourceManager resourceManagerMock = MockRepository.GenerateMock<ResourceManager>();

            Assert.Throws<ArgumentNullException>(() => ResourceManagerExtension.GetFormattedString(null, null));
            Assert.Throws<ArgumentNullException>(() => ResourceManagerExtension.GetFormattedString(null, resourceName));
            Assert.Throws<ArgumentNullException>(() => ResourceManagerExtension.GetFormattedString(resourceManagerMock, null));
            Assert.Throws<ArgumentException>(() => ResourceManagerExtension.GetFormattedString(resourceManagerMock, resourceName));
            Assert.Throws<FormatResourceStringException>(() => ResourceManagerExtension.GetFormattedString(resourceManagerMock, resourceName, formatObject));
            resourceManagerMock.AssertWasCalled(r => r.GetString(resourceName));

            resourceManagerMock.Stub(r => r.GetString(resourceName)).Return(resourceValue).Repeat.Once();
            Assert.AreEqual(resourceValue, resourceManagerMock.GetFormattedString(resourceName, formatObject));
            resourceManagerMock.AssertWasCalled(r => r.GetString(resourceName));

            resourceManagerMock.Stub(r => r.GetString(resourceName)).Return(resourceFormatValue);
            Assert.AreEqual(passFormattedResult, resourceManagerMock.GetFormattedString(resourceName, formatObject));
            resourceManagerMock.AssertWasCalled(r => r.GetString(resourceName));
        }
    }
}
