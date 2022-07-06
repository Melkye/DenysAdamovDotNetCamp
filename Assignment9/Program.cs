using Assignment9;

string pricesFile = Path.GetFullPath("../../../Prices.txt");
string menuFile = Path.GetFullPath("../../../Menu.txt");

FileWorker fileWorker = new FileWorker(pricesFile, menuFile);
PriceList prices = new(fileWorker);
Menu menu = new(fileWorker);

MenuService.TryGetTotalCost(menu, prices, out double totalMenuCost);

Dictionary<string, (double mass, double price)>? menuIngredientsInfo;
var menuInfo = MenuService.TryGetMenuIngredientsMassAndCost(menu, prices, out menuIngredientsInfo);

Console.WriteLine("");