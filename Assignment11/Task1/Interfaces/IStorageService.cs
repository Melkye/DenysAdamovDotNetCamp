using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Interfaces
{
    internal interface IStorageService<T> : IEnumerable<T> where T : class, IGood
    {
        double TotalMass { get; }
        double TotalPrice { get; }
        void Fill(IEnumerable<T> items);
        void DecreasePrice(double percent);
        void IncreasePrice(double percent);
        public void FillStorageFromFile();
        public void WriteStorageReportToFile();
        public IEnumerable<(DateTime, string)> GetLogEntries();
        public IEnumerable<(DateTime, string)> GetLogEntries(DateTime date);
        public void ReplaceLogEntries(IEnumerable<(DateTime, string)> newEntries);
        public void ReplaceLogEntries(IEnumerable<(DateTime, string)> newEntries, DateTime date);
        public void UpdateLogEntry(string message, DateTime dateTime);
    }
}
