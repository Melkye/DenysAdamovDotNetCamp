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

FileWorker fileWorker = new FileWorker(pricesFile, menuFile, exchangeRatesFile); // a-la db connection

MenuService ms = new(fileWorker);

ms.TryGetTotalCost(out double totalMenuCost);

ms.TryGetProductPrice("onion", out double priceUAH);
ms.TryGetProductPrice("oni ono", Currency.USD, out double priceUSD);
ms.TryGetProductPrice("onion", Currency.EUR, out double priceEUR);

Dictionary<string, (double mass, double price)>? menuIngredientsInfo;
Dictionary<string, (double mass, double price)>? menuIngredientsInfoUSD;
Dictionary<string, (double mass, double price)>? menuIngredientsInfoEUR;
var menuInfo = ms.TryGetMenuIngredientsMassAndCost(out menuIngredientsInfo);
var menuInfoUSD = ms.TryGetMenuIngredientsMassAndCost(Currency.USD, out menuIngredientsInfoUSD);
var menuInfoEUR = ms.TryGetMenuIngredientsMassAndCost(Currency.EUR, out menuIngredientsInfoEUR);



ms.PriceList = null;

Console.WriteLine("");