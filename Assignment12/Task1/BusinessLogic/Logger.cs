using System.Globalization;
using Task1.Interfaces;

namespace Task1.BusinessLogic
{
    public class Logger : ILogger
    {
        private readonly string _filename;
        public Logger(string filename)
        {
            _filename = filename;
        }
        public void Log(string message)
        {
            using StreamWriter sw = new(_filename, append: true);
            sw.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + " " + message);
        }
        public IEnumerable<(DateTime, string)> GetEntries()
        {
            List <(DateTime dateTime, string message)> entries = new();
            using (StreamReader sr = new(_filename))
            {
                while (!sr.EndOfStream)
                {
                    string? line = sr.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        int splitIndex = line.IndexOf(' ', line.IndexOf(' ') + 1); // index of the second space: 29.06.2022 19:54:27 Message
                        string dateTimeString = line[..splitIndex];
                        DateTime dateTime = DateTime.Parse(dateTimeString, CultureInfo.GetCultureInfo("uk-UA").DateTimeFormat);
                        string message = line[(splitIndex + 1)..];
                        entries.Add((dateTime, message));
                    }
                }
            }
            return entries;
        }
        /// <summary>
        /// Returns all entries for the specified date
        /// </summary>
        /// <param name="date"> Wanted logs issue date e.g. DateTime.Now.Date </param>
        public IEnumerable<(DateTime, string)> GetEntries(DateTime date)
        {
            List<(DateTime dateTime, string message)> entries = new();
            using (StreamReader sr = new(_filename))
            {
                while (!sr.EndOfStream)
                {
                    string? line = sr.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        int splitIndex = line.IndexOf(' ', line.IndexOf(' ')); // index of the second space: 29.06.2022 19:54:27 Message
                        string dateTimeString = line[..splitIndex];
                        DateTime dateTime = DateTime.Parse(dateTimeString, CultureInfo.GetCultureInfo("uk-UA").DateTimeFormat);
                        if (dateTime.Date == date)
                        {
                            string message = line[(splitIndex + 1)..];
                            entries.Add((dateTime, message));
                        }
                    }
                }
            }
            return entries;
        }
        /// <summary>
        /// Replaces all entries with the new ones
        /// </summary>
        /// <param name="newEntries">List of entries to replace the old ones</param>
        /// <exception cref="ArgumentNullException">Thrown when newEntries is null</exception>
        public void ReplaceEntries(IEnumerable<(DateTime, string)> newEntries)
        {
            if (newEntries is not null)
            {
                using StreamWriter sw = new(_filename);
                foreach ((DateTime dateTime, string message) in newEntries)
                {
                    sw.WriteLine(dateTime.ToString("dd.MM.yyyy HH:mm:ss") + " " + message);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(newEntries), "New entries can not be empty");
            }
        }
        /// <summary>
        /// Replaces all entries issued on the specified date with the new ones
        /// </summary>
        /// <param name="newEntries">List of entries to replace the old ones</param>
        /// <param name="date">Date to check to replace an entry</param>
        /// <exception cref="ArgumentNullException">Thrown when newEntries is null</exception>
        public void ReplaceEntries(IEnumerable<(DateTime, string)> newEntries, DateTime date)
        {
            if (newEntries is not null)
            {
                List<(DateTime dateTime, string message)> entries = GetEntries().ToList();
                entries.RemoveAll(e => e.dateTime.Date == date);
                entries.AddRange(newEntries);
                entries.Sort();
                using StreamWriter sw = new(_filename);
                foreach ((DateTime dateTime, string message) in entries)
                {
                    sw.WriteLine(dateTime.ToString("dd.MM.yyyy HH:mm:ss") + " " + message);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(newEntries), "New entries can not be empty");
            }
        }
        /// <summary>
        /// Searches for an entry with specified timestamp and replaces its message, thows exception otherwise
        /// </summary>
        /// <param name="updatedMessage">The new message for the entry</param>
        /// <param name="dateTime">The timestamp whick identifies the entry</param>
        /// <exception cref="ArgumentException">Thrown when no entry is found with the specified timestamp</exception>
        public void UpdateEntry(string message, DateTime dateTime)
        {
            List<(DateTime dateTime, string message)> entries = GetEntries().ToList();
            (DateTime dateTime, string message) updateEntry = entries.Find(e => e.dateTime == dateTime);
            if (updateEntry == default((DateTime, string)))
            {
                throw new ArgumentException("Entry with specified dateTime not found", nameof(dateTime));
            }
            else
            {
                updateEntry.message = message;
                entries.Insert(entries.IndexOf(entries.Find(e => e.dateTime == dateTime)), updateEntry);
                ReplaceEntries(entries);
            }
        }
    }
}
