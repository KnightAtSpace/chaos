using Knight.Common.Exceptions.IO;
using NUnit.Framework;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace Knight.Common.IO.Persistance
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class XmlFileSerializerTest
    {
        string tempFilePath = Path.Combine(Path.GetTempPath(), "tmpFileObject.xml");

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        [Test]
        public void CtorTest()
        {
            Assert.Throws<ArgumentException>(() => new XmlFileSerializer<ISerializable>(null));
            Assert.Throws<ArgumentException>(() => new XmlFileSerializer<ISerializable>(""));
            Assert.Throws<ArgumentException>(() => new XmlFileSerializer<ISerializable>("   "));

            const string path = "somePath.xml";
            XmlFileSerializer<DateTime> serializer = new XmlFileSerializer<DateTime>(path);

            Assert.AreEqual(path, serializer.Path);
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        [Test]
        public void SerializeTest()
        {
            XmlFileSerializer<DateTime> serializer = new XmlFileSerializer<DateTime>(tempFilePath);

            serializer.Serialize(DateTime.Now);

            Assert.IsTrue(File.Exists(tempFilePath));
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        [Test]
        public void DeserializeTest()
        {
            XmlFileSerializer<DateTime> serializer = new XmlFileSerializer<DateTime>(tempFilePath);

            if (File.Exists(tempFilePath)) File.Delete(tempFilePath);

            Assert.Throws<PathException>(() => serializer.Deserialize());

            serializer.Serialize(DateTime.Now);

            Assert.IsNotNull(serializer.Deserialize());
        }
    }
}
