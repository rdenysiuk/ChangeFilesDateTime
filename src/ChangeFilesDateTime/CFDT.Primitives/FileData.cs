using CFDT.Abstractions;
using System;
using System.IO;

namespace CFDT.Primitives
{
    public class FileData
    {
        public string FileName { get; protected set; }
        public DateTime LastWriteTime { get; protected set; }
        public DateTime NewDateTime { get; protected set; }

        public FileData(FileInfo file, IFileNameParse fileNames)
        {
            FileName = file.Name;
            LastWriteTime = file.LastWriteTime;
            NewDateTime = fileNames.Parse(file);
        }
    }
}
