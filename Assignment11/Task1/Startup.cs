
using Task1.BusinessLogic;
using Task1.Entities;
using Task1.Enums;
using Task1.Interfaces;
using Task1.Presentation;

namespace Task1
{
    internal class Startup
    {
        private View _view;
        public void Initialize()
        {
            Product[] products = new Product[6]
            {
                    new Meat("product 1 - meat", 1.1, 1.1, MeatCategory.Second, MeatType.Pork),
                    new DairyProduct("product 2 - dairy product", 2.2, 2.2, 2),
                    new Meat("product 3 - meat", 3.3, 3.3, MeatCategory.First, MeatType.Chicken),
                    new Meat("product 4 - meat", 4.4, 4.4, MeatCategory.Highest, MeatType.Veal),
                    new DairyProduct("product 5 - dairy product", 5.5, 5.5, 5),
                    new DairyProduct("product 6 - dairy product", 6.6, 6.6, 6)
            };
            Storage<Product> storage1 = new(products);
            Logger logger = new("../../../Data/Logs.txt");
            string storageSource = "../../../Data/StorageSource.txt";
            string storageDestination = "../../../Data/StorageDestination.txt";
            StorageService<Product> storageService = new(storage1, logger, storageSource, storageDestination);

            storageService.RegisterReadStorageFromFile((storageSourceFile, logger) =>
            {
                using StreamReader sr = new(storageSourceFile);
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

                            successfulInput = double.TryParse(productInfo[2], out double weight);
                            if (!successfulInput)
                            {
                                logger.Log($"Line {lineNumber + 1}: Unable to parse weight to double: {productInfo[2]}");
                                isProductValid = false;
                            }

                            if (productInfo.Length == 4)
                            {
                                successfulInput = int.TryParse(productInfo[3], out int daysBeforeSpoil);
                                if (!successfulInput)
                                {
                                    logger.Log($"Line {lineNumber + 1}: Unable to parse daysBeforeSpoil to int: {productInfo[3]}");
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
                                    logger.Log($"Line {lineNumber + 1}: Unable to parse meat category to enum: {productInfo[3]}");
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
                                    logger.Log($"Line {lineNumber + 1}: Unable to parse meat type to enum: {productInfo[4]}");
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
                return products;
            });

            _view = new(storageService);
        }
        public void Run()
        {
            try
            {
                _view.PrintDetails();

                _view.FillFromFile();
                _view.PrintDetails();

                _view.WriteReportToFile();

                _view.IncreasePrice(10);
                _view.PrintDetails();
                _view.DecreasePrice(10);
                _view.PrintDetails();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
