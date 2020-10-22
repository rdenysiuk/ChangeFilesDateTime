using System;
using System.IO;

namespace CFDT.Abstractions
{
    public interface IFileNameParse
    {
        DateTime Parse(FileInfo file);
    }
}