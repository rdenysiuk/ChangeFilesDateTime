using System.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace ChangeFilesDateTimeApp.Config
{
    /// <summary>
    /// Work with application config file
    /// </summary>
    public static class AppConfig
    {
        static readonly Configuration _config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

        public const string DirectoryKey = "Directory";
        public const string ExtensionKey = "Extension";
        /// <summary>
        /// Add key with value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Add(string key, string value)
        {
            _config.AppSettings.Settings.Remove(key);
            _config.AppSettings.Settings.Add(key, value);
            _config.Save(ConfigurationSaveMode.Minimal);
        }
        /// <summary>
        /// Read key value
        /// </summary>
        /// <param name="key"></param>
        public static string Read(string key)
        {
            if (_config.AppSettings.Settings.AllKeys.Contains(key))                
                    return _config.AppSettings.Settings[key].Value;
            return string.Empty;
        }
    }
}
