
using Task1.BusinessLogic;
using Task1.Entities;
using Task1.Enums;
using Task1.Interfaces;
using Task1.Presentation;
using Task1.Settings;

namespace Task1
{
    internal class Startup
    {
        private View _view;
        public void Initialize()
        {

            Logger logger = new("../../../Data/Logs.txt");
            string storageSource = "../../../Data/StorageSource.txt";
            string storageDestination = "../../../Data/StorageDestination.txt";
            string storageSpoils = "../../../Data/StorageSpoils.txt";

            {
                using StreamWriter sw = new(storageSpoils);
                sw.WriteLine(
                    $"{"Title",-FormatSettings.TITLE_PRINT_WIDTH}|" +
                    $"{"Price",-FormatSettings.PRICE_PRINT_WIDTH}|" +
                    $"{"Mass (g)",-FormatSettings.MASS_PRINT_WIDTH}|" +
                    $"{"Best before",-FormatSettings.DATE_PRINT_WIDTH}|");
            }

            Storage<FoodProduct> storage1 = new();
            StorageService<FoodProduct> storageService = new(storage1, logger, storageSource, storageDestination, storageSpoils, 
                (storageSpoilsFile, product) =>
                {
                    using StreamWriter sw = new(storageSpoilsFile, append: true);
                    sw.WriteLine(product);
                }
            );

            storageService.RegisterReadStorageFromFile((storageSourceFile, logger) =>
            {
                using StreamReader sr = new(storageSourceFile);
                List<FoodProduct> products = new();
                int lineNumber = 0;
                while (!sr.EndOfStream)
                {
                    string? line = sr.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] productInfo = line.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                        if (productInfo.Length != 5 && productInfo.Length != 6)
                        {
                            logger.Log($"Line {lineNumber + 1}: Invalid number of parameters of product");
                        }
                        else
                        {
                            bool isProductValid = true;
                            string title = char.ToUpper(productInfo[0][0]) + productInfo[0][1..];

                            bool successfulInput = double.TryParse(productInfo[1], out double price);
                            if (!successfulInput)
                            {
                                logger.Log($"Line {lineNumber + 1}: Unable to parse price to double: {productInfo[1]}");
                                isProductValid = false;
                            }

                            successfulInput = double.TryParse(productInfo[2], out double mass);
                            if (!successfulInput)
                            {
                                logger.Log($"Line {lineNumber + 1}: Unable to parse weight to double: {productInfo[2]}");
                                isProductValid = false;
                            }

                            successfulInput = DateOnly.TryParse(productInfo[3], out DateOnly bestBefore);
                            if (!successfulInput)
                            {
                                logger.Log($"Line {lineNumber + 1}: Unable to 'best before' date: {productInfo[3]}");
                                isProductValid = false;
                            }

                            if (productInfo.Length == 5)
                            {
                                successfulInput = int.TryParse(productInfo[4], out int daysBeforeSpoil);
                                if (!successfulInput)
                                {
                                    logger.Log($"Line {lineNumber + 1}: Unable to parse daysBeforeSpoil to int: {productInfo[4]}");
                                    isProductValid = false;
                                }
                                if (isProductValid)
                                {
                                    products.Add(new DairyProduct(title, price, mass, bestBefore, daysBeforeSpoil));
                                }
                            }
                            else
                            {
                                MeatCategory category = MeatCategory.Highest;
                                successfulInput = Enum.TryParse(typeof(MeatCategory), productInfo[4], out object? catObj);
                                if (!successfulInput)
                                {
                                    logger.Log($"Line {lineNumber + 1}: Unable to parse meat category to enum: {productInfo[3]}");
                                    isProductValid = false;
                                }
                                else
                                {
                                    category = (MeatCategory)catObj;
                                }

                                MeatType type = MeatType.Chicken;
                                successfulInput = Enum.TryParse(typeof(MeatType), productInfo[5], out object? typeObj);
                                if (!successfulInput)
                                {
                                    logger.Log($"Line {lineNumber + 1}: Unable to parse meat type to enum: {productInfo[4]}");
                                    isProductValid = false;
                                }
                                else
                                {
                                    type = (MeatType)typeObj;
                                }

                                if (isProductValid)
                                {
                                    products.Add(new Meat(title, price, mass, bestBefore, category, type));
                                }
                            }
                        }
                    }
                    lineNumber++;
                }
                return products;
            });

            _view = new(storageService);
        }
        public void Run()
        {
            try
            {

                _view.FillFromFile();
                _view.PrintDetails();

                _view.WriteReportToFile();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
