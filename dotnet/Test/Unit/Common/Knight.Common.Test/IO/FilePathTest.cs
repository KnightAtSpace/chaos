using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;

namespace Knight.Common.IO
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class FilePathTest
    {
        private readonly string tmpFile = Environment.CurrentDirectory + @"\tmpTest.txt";
        private readonly string tmpFile2 = Environment.CurrentDirectory + @"\tmpTest2.txt";

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        [Test]
        public void IsFileReadOnlyTest()
        {
            Assert.Throws<ArgumentNullException>(() => FilePath.IsFileReadOnly(null));
            Assert.Throws<ArgumentException>(() => FilePath.IsFileReadOnly(" "));
            Assert.Throws<ArgumentException>(() => FilePath.IsFileReadOnly("dummy"));

            File.CreateText(tmpFile).Close();

            Assert.False(FilePath.IsFileReadOnly(tmpFile), "Expected false for temporary file");

            File.SetAttributes(tmpFile, FileAttributes.ReadOnly);
            Assert.True(FilePath.IsFileReadOnly(tmpFile), "Expected false for temporary file");

            File.SetAttributes(tmpFile, File.GetAttributes(tmpFile) & ~FileAttributes.ReadOnly);
            Assert.False(FilePath.IsFileReadOnly(tmpFile), "Expected false for temporary file");

            File.Delete(tmpFile);
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        [Test]
        public void HasCurrentUserWriteAccessTest()
        {
            Assert.Throws<ArgumentNullException>(() => FilePath.HasCurrentUserWriteAccess(null));
            Assert.Throws<ArgumentException>(() => FilePath.HasCurrentUserWriteAccess(" "));
            Assert.Throws<ArgumentException>(() => FilePath.HasCurrentUserWriteAccess("dummy"));

            DriveInfo i = new DriveInfo(Path.GetPathRoot(tmpFile));

            string account = Environment.UserDomainName + @"\" + Environment.UserName;

            File.CreateText(tmpFile).Close();
            File.SetAttributes(tmpFile, FileAttributes.Normal);
            FileSecurity fSecurity = File.GetAccessControl(tmpFile);

            if (i.DriveType != DriveType.Network)
            {
                if (i.DriveFormat.Equals("NTFS"))
                {
                    fSecurity.AddAccessRule(new FileSystemAccessRule(account, FileSystemRights.Write, AccessControlType.Allow));
                    File.SetAccessControl(tmpFile, fSecurity);
                }

                Assert.True(FilePath.HasCurrentUserWriteAccess(tmpFile), "Expected true for temporary file");
            }

            File.Delete(tmpFile);

            // no write permissions on NTFS
            if (i.DriveFormat.Equals("NTFS"))
            {
                fSecurity = new FileSecurity();
                fSecurity.AddAccessRule(new FileSystemAccessRule(account, FileSystemRights.ReadAndExecute, AccessControlType.Allow));
                File.Create(tmpFile2, 8, FileOptions.None, fSecurity).Close();

                Assert.False(FilePath.HasCurrentUserWriteAccess(tmpFile2), "Expected false for temporary file without write access");

                if (i.DriveType != DriveType.Network)
                {
                    // allow access for delete the temporary file
                    fSecurity.AddAccessRule(new FileSystemAccessRule(account, FileSystemRights.FullControl, AccessControlType.Allow));
                    File.SetAccessControl(tmpFile2, fSecurity);
                }
                File.Delete(tmpFile2);
            }
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        [Test]
        public void GetFullPathTest()
        {
            Assert.Throws<ArgumentNullException>(() => FilePath.BuildFullPath(null));

            string pathResult = FilePath.BuildFullPath(typeof(FilePathTest));

            Assert.IsNotNullOrEmpty(pathResult);
            Assert.IsTrue(Directory.Exists(pathResult));
        }
    }
}
