using ChangeFilesDateTimeApp.Extensions;
using ChangeFilesDateTimeApp.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChangeFilesDateTimeApp
{
    public class DateTimeChange
    {
        readonly ILog _log;
        string _pathFolder { get; set; }
        public string Filter { get; set; }
        public List<FileData> ListFile
        {
            get { return GetFiles(); }
        }
        
        public DateTimeChange(ILog renamerLog)
        {
            _log = renamerLog;
        }

        public void SetPathFolder(string pathFolder)
        {
            _pathFolder = pathFolder;
            _log.Log(isFolderExists(_pathFolder)
                ? $"Success. Folder exist {_pathFolder}"
                : $"Fail. Folder {_pathFolder} doesn't exist");
        }

        #region Private methods

        void ChangeLastWriteTime()
        {
            for (int i = 0; i < ListFile.Count; i++)
            {
                ListFile[i].File.LastWriteTime = ListFile[i].NewDateTime;
            }

            _log.Log($"Success. Modified time changed {ListFile.Count} times");
        }

        void ChangeCreationTime()
        {
            for (int i = 0; i < ListFile.Count; i++)
            {
                ListFile[i].File.CreationTime = ListFile[i].NewDateTime;
            }

            _log.Log($"Success. Creation time changed {ListFile.Count} times");
        }

        List<FileData> GetFiles()
        {
            if (!isFolderExists(_pathFolder))
                return new List<FileData>();

            var folder = new DirectoryInfo(_pathFolder);
            var filters = Filter.Split(new char[] {',', ';'}, StringSplitOptions.None);
            var files = folder.GetFilesByExtensions(filters);

            _log.Log($"Found {files.Count()} files");

            return files.Select(f => new FileData()
                {
                    File = f
                }
            ).ToList();
        }

        bool isFolderExists(string pathFolder)
        {
            if (Directory.Exists(pathFolder))
                return true;

            return false;
        }

        #endregion
    }
}