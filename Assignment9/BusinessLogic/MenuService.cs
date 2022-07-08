using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment9
{
    internal class MenuService
    {
        private readonly DbSimulator _db;
        public MenuService(DbSimulator db)
        {
               _db = new(db); // or just _db = db?
        }
        //public bool TryGetProductPrice(string productTitle, out double price)
        //{
        //    return _db.ProductPrices.Products.TryGetValue(productTitle, out price); // add summmary that it returns UAH
        //}
        /// <summary>
        /// Returns product price in UAH
        /// </summary>
        /// <param name="productTitle">The product to get price of</param>
        /// <returns>The price of the product</returns>
        /// <exception cref="ArgumentException"></exception>
        public double GetProductPrice(string productTitle)
        {

            if (_db.ProductPrices.Products.TryGetValue(productTitle, out double price))
            {
                return price;
            }
            else
            {
                throw new ArgumentException("Product with specified title not found: " + productTitle);
            }

        }
        //public bool TryGetProductPrice(string productTitle, Currency currency, out double price)
        //{
        //    bool isFetchSuccessful = TryGetProductPrice(productTitle, out price);
        //    if (isFetchSuccessful)
        //    {
        //        price = _db.CurrencyExchanger.ExchangeUAH(price, currency);
        //    }
        //    return isFetchSuccessful;
        //}
        /// <summary>
        /// Returns product price in <paramref name="currency"/>
        /// </summary>
        /// <param name="productTitle">The product to get price of</param>
        /// <param name="currency">The currency to get the price in</param>
        /// <returns>The price of the product</returns>
        public double GetProductPrice(string productTitle, Currency currency)
        {
            try
            {
                double price = GetProductPrice(productTitle);
                price = _db.CurrencyExchanger.ExchangeUAH(price, currency);
                return price;
            }
            catch (ArgumentException ex)
            {
                throw;
            }
        }
        //public bool TryGetDishPrice(Dish dish, out double price)
        //{
        //    price = 0.0;
        //    foreach (string ingredientTitle in dish.Ingredients.Keys)
        //    {
        //        if (!TryGetProductPrice(ingredientTitle, out double ingredientPrice))
        //        {
        //            price = default;
        //            return false;
        //        }
        //        else
        //        {
        //            price += ingredientPrice * dish.Ingredients[ingredientTitle] / 1000;
        //        }
        //    }
        //    return true;
        //}
        /// <summary>
        /// Calculates the price of the dish by multiplying mass and price per 1 kg
        /// </summary>
        /// <param name="dish">The dish to get price of</param>
        /// <returns>Total price of the dish in UAH</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public double GetDishPrice(Dish dish)
        {
            try
            {
                double price = 0.0;
                foreach (string ingredientTitle in dish.Ingredients.Keys)
                {

                    price += GetProductPrice(ingredientTitle) * dish.Ingredients[ingredientTitle] / 1000;
                }
                return price;
            }
            catch (ArgumentException e)
            {
                throw new InvalidOperationException("Unable to get price for the dish", e); // ok???
            }
        }
        //public bool TryGetDishPrice(Dish dish, Currency currency, out double price)
        //{
        //    bool isFetchSuccessful = TryGetDishPrice(dish, out price);
        //    if (isFetchSuccessful)
        //    {
        //        price = _db.CurrencyExchanger.ExchangeUAH(price, currency);
        //    }
        //    return isFetchSuccessful;
        //}
        /// <summary>
        /// Calculates the price of the dish by multiplying mass and price per 1 kg
        /// </summary>
        /// <param name="dish">The dish to get price of</param>
        /// <param name="currency">The currency to get price in</param>
        /// <returns>Total price of the dish in <paramref name="currency"/></returns>
        public double GetDishPrice(Dish dish, Currency currency)
        {
            try
            {
                double price = GetDishPrice(dish);
                price = _db.CurrencyExchanger.ExchangeUAH(price, currency);
                return price;

            }
            catch (ArgumentException ex)
            {
                throw;
            }
        }
        //public bool TryGetDishIngredientsMassAndPrice(Dish dish, out Dictionary<string, (double mass, double price)>? ingredientsInfo)
        //{
        //    ingredientsInfo = new();
        //    foreach (string ingredientTitle in dish.Ingredients.Keys)
        //    {
        //        if (!TryGetProductPrice(ingredientTitle, out double ingredientPrice))
        //        {
        //            ingredientsInfo = default;
        //            return false;
        //        }
        //        else
        //        {
        //            double ingredientMass = dish.Ingredients[ingredientTitle];
        //            double ingredientPricePerMass = ingredientPrice * ingredientMass / 1000;
        //            ingredientsInfo[ingredientTitle] = (ingredientMass, ingredientPricePerMass);
        //        }
        //    }
        //    return true;
        //}
        /// <summary>
        /// Retrieves the mass in grams and calculates the price of each ingredient
        /// </summary>
        /// <param name="dish">The dish to get info of</param>
        /// <returns>Mass and price in UAH about each ingredient of the dish</returns>
        /// <exception cref="ArgumentException"></exception>
        public Dictionary<string, (double mass, double price)>? GetDishIngredientsMassAndPrice(Dish dish)
        {
            Dictionary<string, (double mass, double price)>? ingredientsInfo = new();
            foreach (string ingredientTitle in dish.Ingredients.Keys)
            {
                try
                {
                    double ingredientMass = dish.Ingredients[ingredientTitle];
                    double ingredientPrice = GetProductPrice(ingredientTitle) * ingredientMass / 1000;
                    ingredientsInfo[ingredientTitle] = (ingredientMass, ingredientPrice);
                }
                catch (ArgumentException ex)
                {
                    throw;
                }
            }
            return ingredientsInfo;
        }
        //public bool TryGetDishIngredientsMassAndPrice(Dish dish, Currency currency, out Dictionary<string, (double mass, double price)>? ingredientsInfo)
        //{
        //    bool isFetchSuccessful = TryGetDishIngredientsMassAndPrice(dish, out ingredientsInfo); // maybe better not use other method here but set all prices in-place?
        //    if (isFetchSuccessful)
        //    {
        //        foreach (KeyValuePair<string, (double mass, double price)> ing in ingredientsInfo)
        //        {
        //            double exchangedPrice = _db.CurrencyExchanger.ExchangeUAH(ing.Value.price, currency);
        //            ingredientsInfo[ing.Key] = (ing.Value.mass, exchangedPrice);
        //        }
        //    }
        //    return isFetchSuccessful;
        //}
        /// <summary>
        /// Retrieves the mass in grams and calculates the price of each ingredient
        /// </summary>
        /// <param name="dish">The dish to get info of</param>
        /// <param name="currency">The currency to get price in</param>
        /// <returns>Mass and price in <paramref name="currency"/> about each ingredient of the dish</returns>
        /// <exception cref="ArgumentException"></exception>
        public Dictionary<string, (double mass, double price)>? GetDishIngredientsMassAndPrice(Dish dish, Currency currency)
        {
            try
            {
                Dictionary<string, (double mass, double price)>? ingredientsInfo = GetDishIngredientsMassAndPrice(dish);
                foreach (KeyValuePair<string, (double mass, double price)> ing in ingredientsInfo)
                {
                    double exchangedPrice = _db.CurrencyExchanger.ExchangeUAH(ing.Value.price, currency);
                    ingredientsInfo[ing.Key] = (ing.Value.mass, exchangedPrice);
                }
                return ingredientsInfo;
            }
            catch (ArgumentException ex)
            {
                throw;
            }
        }
        //public bool TryGetTotalPrice(out double price)
        //{
        //    price = 0.0;
        //    foreach(Dish dish in _db.Menu.Dishes)
        //    {
        //        if(!TryGetDishPrice(dish, out double dishPrice))
        //        {
        //            price = default;
        //            return false;
        //        }
        //        else
        //        {
        //            price += dishPrice;
        //        }
        //    }
        //    return true;
        //}
        //public bool TryGetTotalPrice(Currency currency, out double price)
        //{
        //    bool isFetchSuccessful = TryGetTotalPrice(out price);
        //    if (isFetchSuccessful)
        //    {
        //        price = _db.CurrencyExchanger.ExchangeUAH(price, currency);
        //    }
        //    return isFetchSuccessful;

        //}
        //public bool TryGetMenuIngredientsMassAndPrice(out Dictionary<string, (double mass, double price)>? menuIngredientsInfo)
        //{
        //    menuIngredientsInfo = new();
        //    foreach(Dish dish in _db.Menu.Dishes)
        //    {
        //        if(!TryGetDishIngredientsMassAndPrice(dish, out Dictionary<string, (double mass, double price)>? dishIngredientsInfo))
        //        {
        //            menuIngredientsInfo = default;
        //            return false;
        //        }
        //        else
        //        {
        //            foreach(KeyValuePair<string, (double mass, double price)> dishIngredientInfo in dishIngredientsInfo)
        //            {
        //                (double dishIngredientMass, double dishIngredientPrice) = dishIngredientInfo.Value;
        //                if (menuIngredientsInfo.ContainsKey(dishIngredientInfo.Key))
        //                {
        //                    (double massSoFar, double priceSoFar) = menuIngredientsInfo[dishIngredientInfo.Key];
        //                    menuIngredientsInfo[dishIngredientInfo.Key] = (massSoFar + dishIngredientMass, priceSoFar + dishIngredientPrice);
        //                }
        //                else
        //                {
        //                    menuIngredientsInfo[dishIngredientInfo.Key] = (dishIngredientMass, dishIngredientPrice);
        //                }
        //            }
        //        }
        //    }
        //    return true;
        //}
        /// <summary>
        /// Calculates the mass and the price of each ingredient of each dish
        /// </summary>
        /// <returns>Mass and price in UAH about each ingredient of the dish</returns>
        /// <exception cref="ArgumentException"></exception>
        public Dictionary<string, (double mass, double price)>? GetMenuIngredientsMassAndPrice()
        {
            Dictionary<string, (double mass, double price)>? menuIngredientsInfo = new();
            foreach (Dish dish in _db.Menu.Dishes)
            {
                try
                {
                    Dictionary<string, (double mass, double price)>? dishIngredientsInfo = GetDishIngredientsMassAndPrice(dish);
                    foreach (KeyValuePair<string, (double mass, double price)> dishIngredientInfo in dishIngredientsInfo)
                    {
                        (double dishIngredientMass, double dishIngredientPrice) = dishIngredientInfo.Value;
                        if (menuIngredientsInfo.ContainsKey(dishIngredientInfo.Key))
                        {
                            (double massSoFar, double priceSoFar) = menuIngredientsInfo[dishIngredientInfo.Key];
                            menuIngredientsInfo[dishIngredientInfo.Key] = (massSoFar + dishIngredientMass, priceSoFar + dishIngredientPrice);
                        }
                        else
                        {
                            menuIngredientsInfo[dishIngredientInfo.Key] = (dishIngredientMass, dishIngredientPrice);
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    throw;
                }
            }
            return menuIngredientsInfo;
        }
        //public bool TryGetMenuIngredientsMassAndPrice(Currency currency, out Dictionary<string, (double mass, double price)>? menuIngredientsInfo)
        //{
        //    bool isFetchSuccessful = TryGetMenuIngredientsMassAndPrice(out menuIngredientsInfo); // use non-currency method or duplicate code but change 1 line there?
        //    if(isFetchSuccessful)
        //    {
        //        foreach (KeyValuePair<string, (double mass, double price)> ing in menuIngredientsInfo)
        //        {
        //            double exchangedPrice = _db.CurrencyExchanger.ExchangeUAH(ing.Value.price, currency);
        //            menuIngredientsInfo[ing.Key] = (ing.Value.mass, exchangedPrice);
        //        }
        //    }
        //    return isFetchSuccessful;
        //}
        /// <summary>
        /// Calculates the mass and the price of each ingredient of each dish
        /// </summary>
        /// <param name="currency"></param>
        /// <returns>Mass and price in <paramref name="currency"/> about each ingredient of the dish</returns>
        /// <exception cref="ArgumentException"></exception>
        public Dictionary<string, (double mass, double price)>? GetMenuIngredientsMassAndPrice(Currency currency)
        {
            try
            {
                Dictionary<string, (double mass, double price)>? menuIngredientsInfo = GetMenuIngredientsMassAndPrice();
                foreach (KeyValuePair<string, (double mass, double price)> ing in menuIngredientsInfo)
                {
                    double exchangedPrice = _db.CurrencyExchanger.ExchangeUAH(ing.Value.price, currency);
                    menuIngredientsInfo[ing.Key] = (ing.Value.mass, exchangedPrice);
                }
                return menuIngredientsInfo;
            }
            catch (ArgumentException ex)
            {
                throw;
            }
        }
        //public void SaveMenuIngredientsMassAndPriceToFile()
        //{
        //    bool isFetchSuccsessful = TryGetMenuIngredientsMassAndPrice(out Dictionary<string, (double mass, double price)>? menuIngredientsInfo);
        //    if (isFetchSuccsessful)
        //    {
        //        _db.SaveMenuIngredientsMassAndPriceToFile(menuIngredientsInfo);
        //    }
        //}
        /// <summary>
        /// Writes the mass and the price of each ingredient of each dish to file
        /// </summary>
        /// <param name="currency">The currency to write in</param>
        public void SaveMenuIngredientsMassAndPriceToFile(Currency currency = Currency.UAH)
        {
            try
            {
                Dictionary<string, (double mass, double price)>? menuIngredientsInfo = GetMenuIngredientsMassAndPrice(currency);
                _db.SaveMenuIngredientsMassAndPriceToFile(menuIngredientsInfo, currency); // passing currency only to print $ or ₴ sign
            }
            catch (ArgumentException ex)
            {
                throw;
            }
        }
        public void AddPrice(KeyValuePair<string, double> product)
        {
            _db.AddPriceToFile(product);
        }
    }
}
