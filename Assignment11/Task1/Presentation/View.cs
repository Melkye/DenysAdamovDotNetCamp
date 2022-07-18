using Task1.BusinessLogic;
using Task1.Entities;
using Task1.Interfaces;
using Task1.Settings;

namespace Task1.Presentation
{
    internal class View 
    {
        private readonly IStorageService<Product> _storageService;

        public View(IStorageService<Product> storageService)
        {
            _storageService = storageService;
        }
        public void DecreasePrice(double percent)
        {
            try
            {
                // TODO: how to get method info in IDE if it's interface?
                _storageService.DecreasePrice(percent);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Price decrease not possible: " + ex.Message);
            }
        }
        public void IncreasePrice(double percent)
        {
            try
            {
                _storageService.IncreasePrice(percent);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Price increase not possible: " + ex.Message);
            }
        }
        public void PrintDetails()
        {
            Console.WriteLine("Total price = " + _storageService.TotalPrice + " | Total weight = " + _storageService.TotalMass);
            Console.WriteLine("All products:");
            Console.WriteLine(
                $"{"Title",-FormatSettings.TITLE_PRINT_WIDTH}|" +
                $"{"Price",-FormatSettings.PRICE_PRINT_WIDTH}|" +
                $"{"Mass (g)",-FormatSettings.MASS_PRINT_WIDTH}|");
            foreach (IGood item in _storageService)
            {
                Console.WriteLine(item);
            }
        }
        public void PrintLogs()
        {
            var logs = _storageService.GetLogEntries();
            foreach (var entry in logs)
            {
                Console.WriteLine(entry);
            }
        }
        public void FillFromFile()
        {
            _storageService.FillStorageFromFile();
        }
        public void WriteReportToFile()
        {
            _storageService.WriteStorageReportToFile();
        }
    }
}
