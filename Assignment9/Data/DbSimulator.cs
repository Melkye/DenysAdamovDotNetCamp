using System.Globalization;

namespace Assignment9
{
    /// <summary>
    /// Works as a simulation of db connection
    /// </summary>
    internal class DbSimulator
    {
        private string _pricesFile;
        private string _menuFile;
        private string _exchangeRatesFile;

        private string _needsFile;

        public Menu Menu { get; set; }
        public PriceList ProductPrices { get; set; }
        public CurrencyExchanger CurrencyExchanger { get; set; }
        public DbSimulator(string pricesFile, string menuFile, string exchangeRatesFile, string needsFile)
        {
            _pricesFile = pricesFile;
            _menuFile = menuFile;
            _exchangeRatesFile = exchangeRatesFile;
            _needsFile = needsFile;

            ProductPrices = new(ReadPricesFromFile());
            Menu = new(ReadMenuFromFile());
            CurrencyExchanger = new(ReadExchangeRatesFromFile());
        }
        public DbSimulator(DbSimulator copyDbSimulator)
        {
            _pricesFile = copyDbSimulator._pricesFile;
            _menuFile = copyDbSimulator._menuFile;
            _exchangeRatesFile = copyDbSimulator._exchangeRatesFile;
            _needsFile = copyDbSimulator._needsFile;

            Menu = new(copyDbSimulator.Menu);
            ProductPrices = new(copyDbSimulator.ProductPrices);
            CurrencyExchanger = new(copyDbSimulator.CurrencyExchanger);
        }
        public Dictionary<string, double> ReadPricesFromFile()
        {
            using (StreamReader file = new(_pricesFile))
            {
                Dictionary<string, double> products = new();
                while (!file.EndOfStream)
                {
                    string? line = file.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] productInfo = line.Trim().Split();
                        string title = productInfo[0];
                        double price = double.Parse(productInfo[1]);
                        products[title] = price;
                    }
                }
                return products;
            }
        }
        public SortedSet<Dish> ReadMenuFromFile()
        {
            using (StreamReader file = new(_menuFile))
            {
                SortedSet<Dish> dishes = new();
                while (!file.EndOfStream)
                {
                    dishes.Add(ReadDishFromFile(file));
                }
                return dishes;
            }
        }
        private Dish ReadDishFromFile(StreamReader file)
        {
            string dishTitle = file.ReadLine();

            Dictionary<string, double> ingredients = new();
            string? ingredientLine;
            while (!file.EndOfStream)
            {
                ingredientLine = file.ReadLine();
                if (string.IsNullOrEmpty(ingredientLine))
                {
                    break;
                }
                string[] ingredientInfo = ingredientLine.Trim().Split();
                string title = ingredientInfo[0];
                double mass = double.Parse(ingredientInfo[1]);

                ingredients[title] = mass;
            }
            return new Dish(dishTitle, ingredients);
        }
        public Dictionary<Currency, double> ReadExchangeRatesFromFile()
        {
            Dictionary<Currency, double> exchangeRates = new();
            using (StreamReader file = new(_exchangeRatesFile))
            {
                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();
                    string[] currencyInfo = line.Trim().Split();
                    Currency currency = (Currency)Enum.Parse(typeof(Currency), currencyInfo[0]);
                    exchangeRates[currency] = double.Parse(currencyInfo[1]);
                }
            }
            return exchangeRates;
        }
        public void AddPriceToFile(KeyValuePair<string, double> product)
        {
            using (StreamWriter file = new(_pricesFile, append: true))
            {
                file.Write("\r\n" + product.Key + " " + product.Value.ToString());
            }
        }
        public void SaveMenuIngredientsMassAndPriceToFile(Dictionary<string, (double mass, double price)> menuIngredientsInfo, Currency currency = Currency.UAH)
        {
            switch (currency)
            {
                case Currency.EUR:
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
                    break;
                case Currency.UAH:
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("uk-UA");
                    break;
                case Currency.USD:
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                    break;
                default:
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("uk-UA");
                    break;
            }
            using (StreamWriter file = new(_needsFile))
            {
                string header = $"{"Product",-FormatSettings.TITLE_PRINT_WIDTH}|" +
                        $"{"Mass (g)",-FormatSettings.MASS_PRINT_WIDTH}|" +
                        $"{$"Price ({currency})",-FormatSettings.PRICE_PRINT_WIDTH}";
                file.WriteLine(header);
                int titlePrintWidth = menuIngredientsInfo.Max(i => i.Key.Length);
                int massPrintWidth = menuIngredientsInfo.Max(i => i.Value.mass.ToString().Length);
                int pricePrintWidth = menuIngredientsInfo.Max(i => i.Value.price.ToString().Length);

                foreach (KeyValuePair<string, (double mass, double price)> ingredientInfo in menuIngredientsInfo)
                {
                    string print = $"{ingredientInfo.Key,-FormatSettings.TITLE_PRINT_WIDTH}|" +
                        $"{ingredientInfo.Value.mass,-FormatSettings.MASS_PRINT_WIDTH}|" +
                        $"{ingredientInfo.Value.price,-FormatSettings.PRICE_PRINT_WIDTH:C2}";
                    file.WriteLine(print);
                }
            }
            Thread.CurrentThread.CurrentCulture = new CultureInfo("uk-UA");
        }
    }
}
