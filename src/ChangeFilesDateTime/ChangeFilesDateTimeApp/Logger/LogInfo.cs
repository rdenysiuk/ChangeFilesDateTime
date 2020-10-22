using CFDT.Abstractions;
using System.Windows.Forms;

namespace ChangeFilesDateTimeApp.Logger
{
    public class LogInfo : ILog
    {
        readonly ToolStripItem _tsItem;
        public LogInfo(ToolStripItem tsItem)
        {
            _tsItem = tsItem;
        }
        public void Log(string log)
        {
            _tsItem.Text = log;
        }
    }
}
