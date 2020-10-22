using System.Collections.Generic;

namespace CFDT.Abstractions
{
    public interface IChangeFileDateTime
    {
        void Change(IEnumerable<string> fileList);
    }
}
