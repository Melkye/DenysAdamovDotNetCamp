namespace Task1.Interfaces
{
    internal interface ILogger
    {
        void Log(string message);
        IEnumerable<(DateTime, string)> GetEntries();
        IEnumerable<(DateTime, string)> GetEntries(DateTime date);
        void ReplaceEntries(IEnumerable<(DateTime, string)> newEntries);
        void ReplaceEntries(IEnumerable<(DateTime, string)> newEntries, DateTime date);
        void UpdateEntry(string message, DateTime dateTime);
    }
}
