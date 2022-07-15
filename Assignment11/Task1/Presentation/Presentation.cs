using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Presentation 
    {
        //private static Logger logger = new Logger("Logs.txt");
        private StorageService _storageService; // IStorageService?
        //public void PrintStorageDetails()
        //{
        //    // TODO make storage items IProduct etc
        //    Console.WriteLine("Total price = " + _storageService.TotalPrice + " | Total weight = " + _storageService.TotalWeight);
        //    Console.WriteLine("All products:");
        //    // TODO: change constants
        //    Console.WriteLine($"{"Title", -10}|{"Price", -10}|{"Weight (g)", -10}|{"DaysBeforeSpoil / Meat Cat", -30}|{"Meat type", -10}|");
        //    foreach (Product product in _storageService)
        //    {
        //        Console.WriteLine(product);
        //    }
        //}
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
        //public void FillStorageFromFile(Storage storage, string filename)
        //{
        //    StreamReader sr = new StreamReader(filename);
        //    bool successfulInput = int.TryParse(sr.ReadLine(), out int numberOfProducts);
        //    if (!successfulInput)
        //    {
        //        throw new IOException("Error reading number of products");
        //    }
        //    else
        //    {
        //        List <Product> products = new List<Product>();
        //        for (int i = 0; i < numberOfProducts; i++)
        //        {
        //            string[] productInfo = sr.ReadLine().Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
        //            if (productInfo.Length < 3 || productInfo.Length > 5)
        //            {
        //                logger.LogTimed($"Line {i + 1}: Invalid number of parameters of product");
        //            }
        //            else
        //            {
        //                bool isProductValid = true;
        //                string title = char.ToUpper(productInfo[0][0]) + productInfo[0][1..];

        //                successfulInput = double.TryParse(productInfo[1], out double price);
        //                if (!successfulInput)
        //                {
        //                    logger.LogTimed($"Line {i + 1}: Unable to parse price to double: {productInfo[1]}");
        //                    isProductValid = false;
        //                }

        //                successfulInput = double.TryParse(productInfo[2], out double weight);
        //                if (!successfulInput)
        //                {
        //                    logger.LogTimed($"Line {i + 1}: Unable to parse weight to double: {productInfo[2]}");
        //                    isProductValid = false;
        //                }

        //                if (productInfo.Length == 3)
        //                {
        //                    if (isProductValid)
        //                    {
        //                        products.Add(new Product(title, price, weight));
        //                    }
        //                }
        //                else if (productInfo.Length == 4)
        //                {
        //                    successfulInput = int.TryParse(productInfo[3], out int daysBeforeSpoil);
        //                    if (!successfulInput)
        //                    {
        //                        logger.LogTimed($"Line {i + 1}: Unable to parse daysBeforeSpoil to int: {productInfo[3]}");
        //                        isProductValid = false;
        //                    }
        //                    if (isProductValid)
        //                    {
        //                        products.Add(new DairyProduct(title, price, weight, daysBeforeSpoil));
        //                    }
        //                }
        //                else
        //                {
        //                    MeatCategory category = MeatCategory.Highest;
        //                    successfulInput = Enum.TryParse(typeof(MeatCategory), productInfo[3], out object catObj);
        //                    if (!successfulInput)
        //                    {
        //                        logger.LogTimed($"Line {i + 1}: Unable to parse meat category to enum: {productInfo[3]}");
        //                        isProductValid = false;
        //                    }
        //                    else
        //                    {
        //                        category = (MeatCategory)catObj;
        //                    }

        //                    MeatType type = MeatType.Chicken;
        //                    successfulInput = Enum.TryParse(typeof(MeatType), productInfo[4], out object typeObj);
        //                    if (!successfulInput)
        //                    {
        //                        logger.LogTimed($"Line {i + 1}: Unable to parse meat type to enum: {productInfo[4]}");
        //                        isProductValid = false;
        //                    }
        //                    else
        //                    {
        //                        type = (MeatType)typeObj;
        //                    }

        //                    if (isProductValid)
        //                    {
        //                        products.Add(new Meat(title, price, weight, category, type));
        //                    }
        //                }
        //            }
        //        }
        //        storage.Fill(products);
        //    }

        //}
        //public void SaveStorageToFile(Storage storage, string filename)
        //{
        //    using (StreamWriter sw = new StreamWriter(filename))
        //    {
        //        sw.WriteLine($"{"Title",-10}|{"Price",-10}|{"Weight (g)",-10}|{"DaysBeforeSpoil / Meat Cat",-30}|{"Meat type",-10}|");
        //        foreach (var product in storage.Products)
        //        {
        //            sw.WriteLine(product);
        //        }
        //    }
        //}
    }
}
