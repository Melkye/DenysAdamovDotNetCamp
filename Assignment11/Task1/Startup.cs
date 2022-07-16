
using Task1.BusinessLogic;
using Task1.Entities;
using Task1.Enums;
using Task1.Interfaces;
using Task1.Presentation;

namespace Task1
{
    internal static class Startup
    {
        public static void Run()
        {
            try
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
                Storage storage1 = new(products);
                Logger logger = new("../../../Data/Logs.txt");
                string storageSource = "../../../Data/StorageSource.txt";
                string storageDestination = "../../../Data/StorageDestination.txt";
                StorageService storageService = new(storage1, logger, storageSource, storageDestination);

                View view = new(storageService);

                //view.FillFromFile();

                //view.SaveToFile();

                view.PrintDetails();

                Storage storage2 = new(products[..3]);

                IStorage whaaat1 = storage1 / storage2;
                IStorage whaaat2 = storage1.GetExcept(storage2);

            }
            catch (ArgumentException ex) // change exception type
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
