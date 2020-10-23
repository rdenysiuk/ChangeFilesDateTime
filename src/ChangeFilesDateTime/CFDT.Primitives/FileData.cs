using CFDT.Abstractions;
using System;
using System.IO;

namespace CFDT.Primitives
{
    public class FileData
    {
        public FileInfo FileInfo { get; protected set; }
        public DateTime LastWriteTime { get; protected set; }
        public DateTime NewDateTime { get; protected set; }

        public FileData(FileInfo file, IFileNameParse fileNames)
        {
            FileInfo = file;
            LastWriteTime = file.LastWriteTime;
            NewDateTime = fileNames.Parse(file);
        }
    }
}
