namespace Assignment9
{
    internal class Presentation
    {
        private MenuService _menuService;
        public Presentation(MenuService menuService)
        {
            _menuService = menuService;
        }
        public void Run()
        {
            bool userTriedToResolve = false;
            bool noProblemOccured = false;
            while (!noProblemOccured && !userTriedToResolve)
            {
                try
                {
                    double priceUAH = _menuService.GetProductPrice("onion");
                    double priceUSD = _menuService.GetProductPrice("onion", Currency.USD);

                    Dictionary<string, (double mass, double price)>? menuIngredientsInfo;
                    Dictionary<string, (double mass, double price)>? menuIngredientsInfoUSD;

                    menuIngredientsInfo = _menuService.GetMenuIngredientsMassAndPrice();
                    menuIngredientsInfoUSD = _menuService.GetMenuIngredientsMassAndPrice(Currency.USD);

                    _menuService.SaveMenuIngredientsMassAndPriceToFile(Currency.EUR);
                    noProblemOccured = true;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    string productTitle = ex.Message.Split()[^1];
                    if (!AddProductToPriceListWhenNotFound(productTitle))
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
        }
        private bool AddProductToPriceListWhenNotFound(string productTitle)
        {
            Console.WriteLine($"Let's add information about {productTitle} to storage");
            Console.WriteLine("Enter its price in UAH per 1 kg:");
            bool isInputSuccsessful = double.TryParse(Console.ReadLine(), out double price);
            if (!isInputSuccsessful)
            {
                return false;
            }
            else
            {
                _menuService.AddPrice(new(productTitle, price));
                return true;
            }
        }
    }
}
