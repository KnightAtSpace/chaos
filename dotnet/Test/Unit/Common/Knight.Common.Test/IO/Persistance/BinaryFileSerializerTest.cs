using Knight.Common.Exceptions.IO;
using NUnit.Framework;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace Knight.Common.IO.Persistance
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class BinaryFileSerializerTest
    {
        string tempFilePath = Path.Combine(Path.GetTempPath(), "tmpFileObject.bin");

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        [Test]
        public void CtorTest()
        {
            Assert.Throws<ArgumentException>(() => new BinaryFileSerializer<ISerializable>(null));
            Assert.Throws<ArgumentException>(() => new BinaryFileSerializer<ISerializable>(""));
            Assert.Throws<ArgumentException>(() => new BinaryFileSerializer<ISerializable>("   "));
            Assert.Throws<ArgumentException>(() => new BinaryFileSerializer<ISerializable>("somePath"));

            const string path = "somePath.bin";
            BinaryFileSerializer<DateTime> serializer = new BinaryFileSerializer<DateTime>(path);

            Assert.AreEqual(path, serializer.Path);
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        [Test]
        public void SerializeTest()
        {
            BinaryFileSerializer<DateTime> serializer = new BinaryFileSerializer<DateTime>(tempFilePath);

            serializer.Serialize(DateTime.Now);

            Assert.IsTrue(File.Exists(tempFilePath));
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        [Test]
        public void DeserializeTest()
        {
            BinaryFileSerializer<DateTime> serializer = new BinaryFileSerializer<DateTime>(tempFilePath);

            if (File.Exists(tempFilePath)) File.Delete(tempFilePath);

            Assert.Throws<PathException>(() => serializer.Deserialize());

            serializer.Serialize(DateTime.Now);

            Assert.IsNotNull(serializer.Deserialize());
        }
    }
}
