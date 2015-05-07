using NUnit.Framework;
using System;

namespace Knight.Common.Controls.Wpf.Messaging
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class MessengerTest
    {
        private static bool noParamsCalled;
        private static bool oneParamCalled;
        private static bool multipleParamsCalled;

        private const string param1 = "param1";
        private object[] param2 = new object[] { 1, 2, 3, 4 };

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        [Test]
        public void SendWithoutParameterTest()
        {
            Messenger.Default.Register("noparams", NoParams);

            Messenger.Default.Send("noparams");

            Assert.IsTrue(noParamsCalled);

            Messenger.Default.Cleanup();
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        [Test]
        public void SendWithOneParameterTest()
        {
            Messenger.Default.Register(this, "oneparam", new Action<string>(s => OneParam(s)));

            Messenger.Default.Send("oneparam", param1);

            Assert.IsTrue(oneParamCalled);

            Messenger.Default.Cleanup();
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        [Test]
        public void SendWithMultiParameterTest()
        {
            Messenger.Default.Register(this, "multiparam", new Action<object[]>(s => MultiParams(s)));

            Messenger.Default.Send("multiparam", param2);

            Assert.IsTrue(multipleParamsCalled);

            Messenger.Default.Cleanup();
        }

        private void NoParams()
        {
            noParamsCalled = true;
        }

        private void OneParam(string a)
        {
            Assert.AreEqual(param1, a);
            oneParamCalled = true;
        }

        private void MultiParams(object[] parameter)
        {
            Assert.AreEqual(param2, parameter);
            Assert.AreEqual(4, parameter.Length);
            multipleParamsCalled = true;
        }
    }
}
