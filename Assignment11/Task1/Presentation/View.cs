using Task1.BusinessLogic;
using Task1.Interfaces;
using Task1.Settings;

namespace Task1.Presentation
{
    internal class View 
    {
        private readonly ProductStorageService _storageService; // IStorageService?

        public View(ProductStorageService storageService)
        {
            _storageService = storageService;
        }
        public void PrintDetails()
        {
            Console.WriteLine("Total price = " + _storageService.TotalPrice + " | Total weight = " + _storageService.TotalWeight);
            Console.WriteLine("All products:");
            Console.WriteLine(
                $"{"Title",-FormatSettings.TITLE_PRINT_WIDTH}|" +
                $"{"Price",-FormatSettings.PRICE_PRINT_WIDTH}|" +
                $"{"Mass (g)",-FormatSettings.MASS_PRINT_WIDTH}|"); // {"DaysBeforeSpoil / Meat Cat",-30}|{"Meat type",-10}|");
            foreach (IGood good in _storageService)
            {
                Console.WriteLine(good);
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
        public void SaveToFile()
        {
            _storageService.SaveToFile();
        }
    }
}
