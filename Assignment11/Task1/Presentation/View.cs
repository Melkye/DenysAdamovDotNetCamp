using Task1.BusinessLogic;
using Task1.Interfaces;
using Task1.Settings;

namespace Task1.Presentation
{
    internal class View 
    {
        private readonly StorageService _storageService; // IStorageService?

        public View(StorageService storageService)
        {
            _storageService = storageService;
        }
        public void PrintDetails()
        {
            Console.WriteLine("Total price = " + _storageService.TotalPrice + " | Total weight = " + _storageService.TotalWeight);
            Console.WriteLine("All products:");
            Console.WriteLine($"{"Title",-FormatSettings.TITLE_PRINT_WIDTH}|{"Price",-FormatSettings.PRICE_PRINT_WIDTH}|" +
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
        //public void FillInStorageByHand(Storage storage)
        //{
        //    Console.WriteLine("Set the number of products to add:");
        //    bool successfulInput = int.TryParse(Console.ReadLine(), out int numberOfProducts);
        //    while (!successfulInput)
        //    {
        //        Console.WriteLine("Wrong input. Try again:");
        //        successfulInput = int.TryParse(Console.ReadLine(), out numberOfProducts);
        //    };
        //    List<Product> products = new List<Product>(numberOfProducts);

        //    for (int i = 0; i < numberOfProducts; i++)
        //    {
        //        Console.WriteLine($"Enter data for the product number {i + 1}:");

        //        Console.WriteLine("Set title:");
        //        string title = Console.ReadLine();

        //        Console.WriteLine("Set price:");
        //        successfulInput = double.TryParse(Console.ReadLine(), out double price);
        //        while (!successfulInput)
        //        {
        //            Console.WriteLine("Wrong input. Try again:");
        //            successfulInput = double.TryParse(Console.ReadLine(), out price);
        //        };

        //        Console.WriteLine("Set weight:");
        //        successfulInput = double.TryParse(Console.ReadLine(), out double weight);
        //        while (!successfulInput)
        //        {
        //            Console.WriteLine("Wrong input. Try again:");
        //            successfulInput = double.TryParse(Console.ReadLine(), out weight);
        //        };

        //        Console.WriteLine("Enter 'm' for meat, 'dp' for dairy product, otherwise it will be a simple product");
        //        string productType = Console.ReadLine();
        //        if (productType == "m")
        //        {
        //            Console.WriteLine("Set category:");
        //            string catString = Console.ReadLine();

        //            successfulInput = Enum.TryParse(typeof(MeatCategory), catString, out object catObj);
        //            while (!successfulInput)
        //            {
        //                Console.WriteLine("Wrong input. Try again:");
        //                catString = Console.ReadLine();
        //                successfulInput = Enum.TryParse(typeof(MeatCategory), catString, out catObj);
        //            }

        //            MeatCategory category = (MeatCategory)catObj;

        //            Console.WriteLine("Set type:");
        //            string tpString = Console.ReadLine();

        //            successfulInput = Enum.TryParse(typeof(MeatType), tpString, out object typeObj);
        //            while (!successfulInput)
        //            {
        //                Console.WriteLine("Wrong input. Try again:");
        //                tpString = Console.ReadLine();
        //                successfulInput = Enum.TryParse(typeof(MeatType), tpString, out typeObj);
        //            }

        //            MeatType type = (MeatType)typeObj;

        //            products.Add(new Meat(title, price, weight, category, type));
        //        }
        //        else if (productType == "dp")
        //        {
        //            Console.WriteLine("Set number of days before spoil:");
        //            successfulInput = int.TryParse(Console.ReadLine(), out int daysBeforeSpoil);
        //            while (!successfulInput || daysBeforeSpoil <= 0)
        //            {
        //                Console.WriteLine("Wrong input. Try again:");
        //                successfulInput = int.TryParse(Console.ReadLine(), out daysBeforeSpoil);
        //            }

        //            products.Add(new DairyProduct(title, price, weight, daysBeforeSpoil));
        //        }
        //        else
        //        {
        //            products.Add(new Product(title, price, weight));
        //        }
        //    }
        //    storage.Fill(products);
        //}
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
