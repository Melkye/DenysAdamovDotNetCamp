using System.Collections;
using Task1.Interfaces;
using Task1.Settings;

namespace Task1.BusinessLogic
{
    internal class StorageService<T> : IStorageService<T> where T : class, IGood
    {
        private readonly ILogger _logger;
        private readonly IStorage<T> _storage;
        private readonly string _sourceFilePath;
        private readonly string _destinationFilePath;

        private Func<string, ILogger, IEnumerable<T>> _readStorageFromFile;
        public StorageService(IStorage<T> storage, ILogger logger, string sourceFilePath, string destinationFilePath)
        {
            _storage = storage;
            _logger = logger;
            _sourceFilePath = sourceFilePath;
            _destinationFilePath = destinationFilePath;
            FillStorageFromFile();
        }
        public double TotalMass => _storage.TotalMass;
        public double TotalPrice => _storage.TotalPrice;
        public void Fill(IEnumerable<T> items)
        {
            _storage.Fill(items);
        }
        /// <summary>
        /// Decreases price for whole storage by specified percent
        /// </summary>
        /// <param name="percent">A part of current price which will be subtracted</param>
        /// <exception cref="ArgumentException"></exception>
        public void DecreasePrice(double percent)
        {
            try
            {
                _storage.DecreasePrice(percent);
            }
            catch (ArgumentException ex)
            {
                _logger.Log(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Decreases price for whole storage by specified percent
        /// </summary>
        /// <param name="percent">A part of current price which will be added</param>
        /// <exception cref="ArgumentException"></exception>
        public void IncreasePrice(double percent)
        {
            try
            {
                _storage.IncreasePrice(percent);
            }
            catch (ArgumentException ex)
            {
                _logger.Log(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Does except set operation 
        /// </summary>
        /// <param name="other">a storage to except with</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IEnumerable<T> GetStorageExcept(IEnumerable<T> storage)
        {
            try
            {
                return _storage.Except(storage);
            }
            catch (ArgumentException ex)
            {
                _logger.Log(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Does intersect set operation 
        /// </summary>
        /// <param name="other">a storage to intersect with</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IEnumerable<T> GetStorageIntersect(IEnumerable<T> storage)
        {
            try
            {
                return _storage.Intersect(storage);
            }
            catch (ArgumentException ex)
            {
                _logger.Log(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Does union set operation 
        /// </summary>
        /// <param name="other">a storage to union with</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IEnumerable<T> GetStorageUnion(IEnumerable<T> storage)
        {
            try
            {
                return _storage.Union(storage);
            }
            catch (ArgumentException ex)
            {
                _logger.Log(ex.Message);
                throw;
            }
        }
        public void FillStorageFromFile()
        {
            if (_readStorageFromFile is not null)
            {
                IEnumerable<T> items = _readStorageFromFile(_sourceFilePath, _logger);
                _storage.Fill(items);
            }
        }
        public void WriteStorageReportToFile()
        {
            using StreamWriter sw = new(_destinationFilePath);
            sw.WriteLine($"{"Title",-FormatSettings.TITLE_PRINT_WIDTH}|" +
                $"{"Price",-FormatSettings.PRICE_PRINT_WIDTH:C2}|" +
                $"{"Mass",-FormatSettings.MASS_PRINT_WIDTH}|");
            foreach (var item in _storage)
            {
                sw.WriteLine(item);
            }
        }
        public void RegisterReadStorageFromFile(Func<string, ILogger, IEnumerable<T>> methodToCall)
        {
            _readStorageFromFile = methodToCall;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _storage.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_storage).GetEnumerator();
        }

        public IEnumerable<(DateTime, string)> GetLogEntries()
        {
            return _logger.GetEntries();
        }
        public IEnumerable<(DateTime, string)> GetLogEntries(DateTime date)
        {
            return _logger.GetEntries(date);
        }
        public void ReplaceLogEntries(IEnumerable<(DateTime, string)> newEntries)
        {
            _logger.ReplaceEntries(newEntries);
        }
        public void ReplaceLogEntries(IEnumerable<(DateTime, string)> newEntries, DateTime date)
        {
            _logger.ReplaceEntries(newEntries, date);
        }
        public void UpdateLogEntry(string message, DateTime dateTime)
        {
            _logger.UpdateEntry(message, dateTime);
        }
    }
}
