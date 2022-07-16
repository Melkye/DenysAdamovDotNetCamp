using Assignment7;

Logger logger = new Logger("Logs.txt");
try
{
    //List<Product> products = new List<Product>();
    //products.Add(new Meat("1 - meat", 10, 120, MeatCategory.Highest, MeatType.Veal));
    //Storage storage = new Storage(products);
    //Presentation.PrintStorageDetails(storage);
    //int numberOfTries = 3;
    //bool isFileFound = false;
    //string filename = ""; // "Storage.txt";
    //for (int i = 0; i < numberOfTries && !isFileFound; i++)
    //{
    //    try
    //    {
    //        Console.WriteLine("Enter filename: (Storage.txt? (y))");
    //        filename = Console.ReadLine();
    //        if (filename.ToLower() == "y")
    //        {
    //            filename = "Storage.txt";
    //        }
    //        Presentation.FillStorageFromFile(storage, filename);
    //        isFileFound = true;
    //    }
    //    catch (FileNotFoundException ex)
    //    {
    //        logger.LogTimed($"File {filename} not found. Tring again");
    //        Console.WriteLine($"File {filename} not found. Try again: ");
    //    }
    //    catch (IOException ex)
    //    {
    //        logger.LogTimed(ex.Message);
    //        Console.WriteLine(ex.Message);
    //    }
    //}

    //Console.WriteLine("--------------------------");
    //Presentation.PrintStorageDetails(storage);
    //Presentation.SaveStorageToFile(storage, "SavedStorage.txt");

    //try
    //{
    //    List<(DateTime, string)> loggerAllEntries = logger.GetEntries();
    //    List<(DateTime, string)> loggerDateEntries = logger.GetEntries(DateTime.Parse("6/29/2022"));

    //    logger.UpdateEntry("The new message", DateTime.Parse("6/29/2022 19:54:27"));
    //}
    //catch (ArgumentException ex)
    //{
    //    logger.LogTimed(ex.Message);
    //    Console.WriteLine(ex.Message);
    //}
    //catch (Exception)
    //{

    //    throw;
    // }
    List<Product> products = new List<Product>();
    products.Add(new Meat("1 - meat", 10, 120, MeatCategory.Highest, MeatType.Veal));
    Buy buy = new Buy(products);
    IViewerBuy viewer = new Check();
    viewer.ViewBuy(buy);

    Product product1 = new Product("product1", 11.1, 11.1);
    Product product2 = new Product("product2", 2.2, 2.2);
    Console.WriteLine(product1.CompareTo(3));

    products.Add(product1);
    products.Add(product2);
    foreach (Product product in products)
    {
        Console.WriteLine(product);
    }

    Console.WriteLine("--------");

    products.Sort(new ComparerByPrice());
    foreach (Product product in products)
    {
        Console.WriteLine(product);
    }

}
catch (Exception ex)
{
    logger.LogTimed(ex.Message);
    Console.WriteLine(ex.Message);
}
