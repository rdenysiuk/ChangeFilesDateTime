using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChangeFilesDateTimeApp.Extensions
{
    public static class DirectoryInfoExtension
    {
        public static IEnumerable<FileInfo> GetFilesByExtensions(this DirectoryInfo dir, params string[] extensions)
        {
            if (extensions == null)
                throw new ArgumentNullException(nameof(extensions));

            IEnumerable<FileInfo> files = Enumerable.Empty<FileInfo>();
            foreach (string extension in extensions)
            {
                files = files.Concat(dir.GetFiles(extension));
            }
            return files;
        }
    }
}
