using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ChangeFilesDateTimeApp
{
    public class FileData
    {
        public FileInfo File { get; set; }

        public DateTime NewDateTime
        {
            get
            {
                var pattern = @"^([a-zA-Z]+)_(?<date>(\d+)?)_(?<time>(\d+)?).(\w+)";                
                var match = Regex.Match(FileName, pattern, RegexOptions.IgnoreCase);

                if (match.Success) 
                    return Parse(match.Groups["date"].Value, match.Groups["time"].Value);
                else
                    return this.File.LastWriteTime;
            }
        }

        public string FileName
        {
            get
            {
                return this.File.Name;
            }
        }

        public string LastWriteTime
        {
            get
            {
                return this.File.LastWriteTime.ToString("dd.MM.yyyy HH:mm:ss");
            }
        }

        private DateTime Parse(string date, string time)
        {
            int year = Convert.ToInt32(date.Substring(0, 4));
            int month = Convert.ToInt32(date.Substring(4, 2));
            int day = Convert.ToInt32(date.Substring(6, 2));

            int hour = Convert.ToInt32(time.Substring(0, 2));
            int min = Convert.ToInt32(time.Substring(2, 2));
            int sec = Convert.ToInt32(time.Substring(4, 2));

            return new DateTime(year, month, day, hour, min, sec);
        }
    }
}
