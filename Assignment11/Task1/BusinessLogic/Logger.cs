using System.Globalization;

namespace Task1
{
    public class Logger
    {
        private string _filename;
        public Logger(string filename)
        {
            _filename = filename;
        }
        public void Log(string message)
        {
            using (StreamWriter sw = new StreamWriter(_filename, append: true))
            {
                sw.WriteLine(message);
            }
        }
        public void LogTimed(string message)
        {
            using (StreamWriter sw = new StreamWriter(_filename, append: true))
            {
                sw.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + " " + message);
            }
        }
        public List<(DateTime, string)> GetEntries()
        {
            List <(DateTime dateTime, string message)> entries = new List<(DateTime, string)>();
            using (StreamReader sr = new StreamReader(_filename))
            {
                while (!sr.EndOfStream)
                {
                    string[] entryData = sr.ReadLine().Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    DateTime dateTime = DateTime.Parse(entryData[0] + " " + entryData[1], CultureInfo.GetCultureInfo("uk-UA").DateTimeFormat);
                    string message = "";
                    for (int i = 2; i < entryData.Length; i++)
                    {
                        message += " " + entryData[i];
                    }
                    entries.Add((dateTime, message));
                }
            }
            return entries;
        }
        /// <summary>
        /// Returns all entries for the specified date
        /// </summary>
        /// <param name="date"> Wanted logs issue date e.g. DateTime.Now.Date </param>
        public List<(DateTime, string)> GetEntries(DateTime date)
        {
            List<(DateTime dateTime, string message)> entries = new List<(DateTime, string)>();
            using (StreamReader sr = new StreamReader(_filename))
            {
                while (!sr.EndOfStream)
                {
                    string[] entryData = sr.ReadLine().Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    DateTime dateTime = DateTime.Parse(entryData[0] + " " + entryData[1], CultureInfo.GetCultureInfo("uk-UA").DateTimeFormat);
                    if (dateTime.Date == date)
                    {
                        string message = "";
                        for (int i = 2; i < entryData.Length; i++)
                        {
                            message += " " + entryData[i];
                        }
                        entries.Add((dateTime, message));
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
        public void ReplaceEntries(List<(DateTime, string)> newEntries)
        {
            if (newEntries is not null)
            {
                using (StreamWriter sw = new StreamWriter(_filename))
                {
                    foreach ((DateTime dateTime, string message) in newEntries)
                    {
                        sw.WriteLine(dateTime.ToString("dd.MM.yyyy HH:mm:ss") + " " + message);
                    }
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
        public void ReplaceEntries(List<(DateTime, string)> newEntries, DateTime date)
        {
            if (newEntries is not null)
            {
                List<(DateTime dateTime, string message)> entries = GetEntries();
                entries.RemoveAll(e => e.dateTime.Date == date);
                entries.AddRange(newEntries);
                entries.Sort();
                using (StreamWriter sw = new StreamWriter(_filename))
                {
                    foreach ((DateTime dateTime, string message) in entries)
                    {
                        sw.WriteLine(dateTime.ToString("dd.MM.yyyy HH:mm:ss") + " " + message);
                    }
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
        public void UpdateEntry(string updatedMessage, DateTime dateTime)
        {
            List<(DateTime dateTime, string message)> entries = GetEntries();
            var a = default((DateTime, string));
            (DateTime dateTime, string message) updateEntry = entries.Find(e => e.dateTime == dateTime);
            if (updateEntry != default((DateTime, string)))
            {
                updateEntry.message = updatedMessage;
                entries.Insert(entries.IndexOf(entries.Find(e => e.dateTime == dateTime)), updateEntry);
                ReplaceEntries(entries);
            }
            else
            {
                throw new ArgumentException("Entry with specified dateTime not found", nameof(dateTime));
            }
        }
    }
}
