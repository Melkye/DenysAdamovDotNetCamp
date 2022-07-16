using System.Collections;
using Task1.Interfaces;
using Task1.Entities;
using Task1.Enums;

namespace Task1.BusinessLogic
{
    internal class StorageService : IEnumerable//IEnumerable<Product>
    {
        private readonly ILogger _logger;
        private readonly IStorage _storage; // IStorage?
        private readonly string _sourceFilePath;
        private readonly string _destinationFilePath;
        public StorageService(IStorage storage, ILogger logger, string sourceFilePath, string destinationFilePath)
        {
            _storage = storage;
            _logger = logger;
            _sourceFilePath = sourceFilePath;
            _destinationFilePath = destinationFilePath;
        }
        public double TotalWeight => _storage.TotalMass;
        public double TotalPrice => _storage.TotalPrice;
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
        public IStorage GetExcept(IStorage storage)
        {
            return _storage.GetExcept(storage);
        }
        public IStorage GetIntersect(IStorage storage)
        {
            return _storage.GetIntersect(storage);
        }
        public IStorage GetUnion(IStorage storage)
        {
            return _storage.GetUnion(storage);
        }
        public void FillStorageFromFile()
        {
            using StreamReader sr = new(_sourceFilePath);
            List<Product> products = new();
            int lineNumber = 0;
            while (!sr.EndOfStream)
            {
                string? line = sr.ReadLine();
                if (!string.IsNullOrEmpty(line))
                {
                    string[] productInfo = line.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    if (productInfo.Length != 4 && productInfo.Length != 5)
                    {
                        _logger.Log($"Line {lineNumber + 1}: Invalid number of parameters of product");
                    }
                    else
                    {
                        bool isProductValid = true;
                        string title = char.ToUpper(productInfo[0][0]) + productInfo[0][1..];

                        bool successfulInput = double.TryParse(productInfo[1], out double price);
                        if (!successfulInput)
                        {
                            _logger.Log($"Line {lineNumber + 1}: Unable to parse price to double: {productInfo[1]}");
                            isProductValid = false;
                        }

                        successfulInput = double.TryParse(productInfo[2], out double weight);
                        if (!successfulInput)
                        {
                            _logger.Log($"Line {lineNumber + 1}: Unable to parse weight to double: {productInfo[2]}");
                            isProductValid = false;
                        }

                        if (productInfo.Length == 4)
                        {
                            successfulInput = int.TryParse(productInfo[3], out int daysBeforeSpoil);
                            if (!successfulInput)
                            {
                                _logger.Log($"Line {lineNumber + 1}: Unable to parse daysBeforeSpoil to int: {productInfo[3]}");
                                isProductValid = false;
                            }
                            if (isProductValid)
                            {
                                products.Add(new DairyProduct(title, price, weight, daysBeforeSpoil));
                            }
                        }
                        else
                        {
                            MeatCategory category = MeatCategory.Highest;
                            successfulInput = Enum.TryParse(typeof(MeatCategory), productInfo[3], out object? catObj);
                            if (!successfulInput)
                            {
                                _logger.Log($"Line {lineNumber + 1}: Unable to parse meat category to enum: {productInfo[3]}");
                                isProductValid = false;
                            }
                            else
                            {
                                category = (MeatCategory)catObj;
                            }

                            MeatType type = MeatType.Chicken;
                            successfulInput = Enum.TryParse(typeof(MeatType), productInfo[4], out object? typeObj);
                            if (!successfulInput)
                            {
                                _logger.Log($"Line {lineNumber + 1}: Unable to parse meat type to enum: {productInfo[4]}");
                                isProductValid = false;
                            }
                            else
                            {
                                type = (MeatType)typeObj;
                            }

                            if (isProductValid)
                            {
                                products.Add(new Meat(title, price, weight, category, type));
                            }
                        }
                    }
                }
                lineNumber++;
            }
            _storage.Fill(products);
        }
        public void SaveToFile()
        {
            using StreamWriter sw = new(_destinationFilePath);
            sw.WriteLine($"{"Title",-10}|{"Price",-10}|{"Weight (g)",-10}|");
            foreach (var product in _storage)
            {
                sw.WriteLine(product);
            }
        }
        //public IEnumerator<Product> GetEnumerator()
        //{
        //    return ((IEnumerable<Product>)_storage).GetEnumerator();
        //}
        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return ((IEnumerable)_storage).GetEnumerator();
        //}

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

        public IEnumerator GetEnumerator()
        {
            return _storage.GetEnumerator();
        }
    }
}
