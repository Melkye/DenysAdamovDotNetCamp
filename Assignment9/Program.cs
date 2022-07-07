using Assignment9;

//Dictionary<string, double> ingredients1 = new();
//ingredients1.Add("1", 1.1);
//ingredients1.Add("2", 2.2);
//ingredients1.Add("3", 3.3);

//Dictionary<string, double> ingredients2 = new();
//ingredients2.Add("4", 1.1);
//ingredients2.Add("5", 2.2);
//ingredients2.Add("6", 3.3);

//Dictionary<string, double> ingredients3 = new();
//ingredients3.Add("7", 1.1);
//ingredients3.Add("8", 2.2);
//ingredients3.Add("9", 3.3);

//Dish dish1 = new(ingredients1);
//Dish dish2 = new(ingredients2);
//Dish dish3 = new(ingredients3);

//ingredients1.Clear();

//List<Dish> dishes1 = new List<Dish>() { dish1 };

//Menu menu = new(dishes1);

//dishes1.Add(dish2);

//Console.WriteLine("");

//List<Dish> dishes2 = menu.Dishes;

//dishes2.Add(dish3);

//string p = Path.GetFullPath("../../../" + "Poop.txt");


string pricesFile = Path.GetFullPath("../../../Prices.txt");
string menuFile = Path.GetFullPath("../../../Menu.txt");
string exchangeRatesFile = Path.GetFullPath("../../../ExchangeRates.txt");
string needsFile = Path.GetFullPath("../../../Result.txt");

MenuService ms = new(new DbSimulator(pricesFile, menuFile, exchangeRatesFile, needsFile));

//ms.TryGetTotalCost(out double totalMenuCost);

//ms.TryGetProductPrice("onion", out double priceUAH);
//ms.TryGetProductPrice("oni ono", Currency.USD, out double priceUSD);
//ms.TryGetProductPrice("onion", Currency.EUR, out double priceEUR);

//Dictionary<string, (double mass, double price)>? menuIngredientsInfo;
//Dictionary<string, (double mass, double price)>? menuIngredientsInfoUSD;
//Dictionary<string, (double mass, double price)>? menuIngredientsInfoEUR;
//var menuInfo = ms.TryGetMenuIngredientsMassAndCost(out menuIngredientsInfo);
//var menuInfoUSD = ms.TryGetMenuIngredientsMassAndCost(Currency.USD, out menuIngredientsInfoUSD);
//var menuInfoEUR = ms.TryGetMenuIngredientsMassAndCost(Currency.EUR, out menuIngredientsInfoEUR);
bool userTriedToResolve = false;
bool noProblemOccured = false;
while (!noProblemOccured && !userTriedToResolve)
{
    try
    {
        double priceUAH = ms.GetProductPrice("onion");
        double priceUSD = ms.GetProductPrice("onion", Currency.USD);

        Dictionary<string, (double mass, double price)>? menuIngredientsInfo;
        Dictionary<string, (double mass, double price)>? menuIngredientsInfoUSD;

        menuIngredientsInfo = ms.GetMenuIngredientsMassAndCost();
        menuIngredientsInfoUSD = ms.GetMenuIngredientsMassAndCost(Currency.USD);

        noProblemOccured = true;
        Console.WriteLine("");
    }
    //catch (InvalidOperationException ex) when (ex.InnerException is InvalidOperationException && ex.InnerException.InnerException is ArgumentException)
    //{
    //    Console.WriteLine(ex.InnerException.InnerException.Message);
    //    throw ex.InnerException;
    //}
    //catch (InvalidOperationException ex) when (ex.InnerException is ArgumentException)
    //{
    //    Console.WriteLine(ex.InnerException.Message);
    //    throw ex.InnerException;
    //}
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
        string productTitle = ex.Message.Split()[^1];
        if(!AddProductToPriceListWhenNotFound(ms, productTitle))
        {
            Console.WriteLine("Bad try");
        }
        userTriedToResolve = true;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

static bool AddProductToPriceListWhenNotFound(MenuService ms, string productTitle)
{
    Console.WriteLine($"Let's add information about {productTitle} to storage");
    Console.WriteLine("Enter its price per 1 kg:");
    bool isInputSuccsessful = double.TryParse(Console.ReadLine(), out double price);
    if (!isInputSuccsessful)
    {
        return false;
    }
    else
    {
        ms.AddPrice(new KeyValuePair<string, double>(productTitle, price));
        return true;
    }
}