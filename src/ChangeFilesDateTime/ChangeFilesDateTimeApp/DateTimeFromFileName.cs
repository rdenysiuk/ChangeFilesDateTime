using CFDT.Abstractions;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ChangeFilesDateTimeApp
{
    public class DateTimeFromFileName : IFileNameParse
    {
        public DateTime Parse(FileInfo file)
        {
            var pattern = @"^([a-zA-Z]+)_(?<date>(\d+)?)_(?<time>(\d+)?).(\w+)";
            var match = Regex.Match(file.Name, pattern, RegexOptions.IgnoreCase);

            if (match.Success)
                return parse(match.Groups["date"].Value, match.Groups["time"].Value);
            else
                return DateTime.MinValue;
        }

        private DateTime parse(string date, string time)
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
