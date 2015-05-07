using System;
using System.IO;
using System.Security.AccessControl;
using Knight.Common.Exceptions.IO;

namespace Knight.Common.IO
{
    /// <summary>
    /// Provides file path functions for System.IO.
    /// </summary>
    public static class FilePath
    {
        /// <summary>
        /// Checks if the given file has set the attribute read only.
        /// </summary>
        /// <param name="path">The file name the attribute is checked for.</param>
        /// <returns>True if the read only attribute is set.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="PathException"></exception>
        public static bool IsFileReadOnly(string path)
        {
            if (path == null) throw new ArgumentNullException("path");
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException("Cannot check a empty path.", "path");
            if (!File.Exists(path)) throw new ArgumentException("File doesn't exist", "path");

            try
            {
                return File.GetAttributes(path).HasFlag(FileAttributes.ReadOnly);
            }
            catch (SystemException sEx)
            {
                throw new PathException("Getting file attributes failed.", path, sEx);
            }
        }

        /// <summary>
        /// Checks if the given file is enabled for write access by the current user.
        /// Permissions check also includes access rights on NTFS file systems.
        /// </summary>
        /// <param name="path">The file name to check for access.</param>
        /// <returns>True if write access is granted.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="PathException"></exception>
        public static bool HasCurrentUserWriteAccess(string path)
        {
            if (path == null) throw new ArgumentNullException("path");
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException("Cannot check a empty path.", "path");
            if (!File.Exists(path)) throw new ArgumentException("File doesn't exist", "path");

            try
            {
                DriveInfo i = new DriveInfo(Path.GetPathRoot(path));

                // for no NTFS file systems acces rule check is not needed
                if (!i.DriveFormat.Equals("NTFS"))
                {
                    return !IsFileReadOnly(path);
                }
            }
            catch (SystemException sEx)
            {
                throw new PathException("Getting drive information failed.", path, sEx);
            }

            string userName = Environment.UserDomainName + @"\" + Environment.UserName;

            try
            {
                // can open for write?
                foreach (FileSystemAccessRule rule in File.GetAccessControl(path).GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount)))
                {
                    FileSystemRights rights = rule.FileSystemRights;
                    if (userName.Equals(rule.IdentityReference.Value))
                    {
                        if (rights.HasFlag(FileSystemRights.Write))
                        {
                            return true;
                        }
                    }
                }
            }
            catch (SystemException sEx)
            {
                throw new PathException("Getting access control information failed.", path, sEx);
            }

            return false;
        }

        /// <summary>
        /// Builds the absolute path to the location of the assembly containing the given type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The absolute path.</returns>
        public static string BuildFullPath(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            return type.Assembly.Location.Remove(type.Assembly.Location.LastIndexOf(type.Assembly.GetName(false).Name));
        }
    }
}
