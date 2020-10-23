using CFDT.Abstractions;
using CFDT.Primitives;
using ChangeFilesDateTimeApp.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChangeFilesDateTimeApp
{
    public class DateTimeChange
    {
        #region FIELDS AND .CTOR
        private readonly ILog _log;
        private readonly IFileNameParse _fileNames;        
        public DateTimeChange(ILog log, IFileNameParse fileNames)
        {
            _log = log;
            _fileNames = fileNames;            
        }
        #endregion
        private string _pathFolder;
        public string Filter { get; set; }
        public List<FileData> ListFile => GetFiles();
        public void SetPathFolder(string pathFolder)
        {
            _pathFolder = pathFolder;
            _log.Log(isFolderExists(_pathFolder)
                ? $"Success. Folder exist {_pathFolder}"
                : $"Fail. Folder {_pathFolder} doesn't exist");
        }

        public IEnumerable<string> ChangeDateTime(IEnumerable<FileData> fileList)
        {
            var fileNameList = new List<string>();
            foreach (FileData fileItem in fileList)
            {
                try
                {
                    fileItem.FileInfo.LastWriteTime = fileItem.NewDateTime;
                    fileNameList.Add(fileItem.FileInfo.Name);
                }
                catch (Exception)
                {
                }                
            }
            return fileNameList;
        }

        #region Private methods

        void ChangeLastWriteTime()
        {
            //foreach (var file in ListFile)            
            //    file.File.LastWriteTime = file.NewDateTime;            

            _log.Log($"Success. Modified time changed {ListFile.Count} times");
        }

        void ChangeCreationTime()
        {
            //foreach (var file in ListFile)            
            //    file.File.CreationTime = file.NewDateTime;            

            _log.Log($"Success. Creation time changed {ListFile.Count} times");
        }

        List<FileData> GetFiles()
        {
            if (!isFolderExists(_pathFolder))
                return new List<FileData>();

            var folder = new DirectoryInfo(_pathFolder);
            var filters = Filter.Split(new char[] { ',', ';' }, StringSplitOptions.None);
            var files = folder.GetFilesByExtensions(filters);

            var fileList = files.Select(f => new FileData(f, _fileNames)).Where(a => a.LastWriteTime != a.NewDateTime).ToList();
            _log.Log($"Found {files.Count()} files. {fileList.Count} of them has wrong datetime");

            return fileList;
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