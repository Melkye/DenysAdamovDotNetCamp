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
        /// 
        /// </summary>
        /// <param name="productTitle"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public double GetProductPrice(string productTitle)
        {

            if (_db.ProductPrices.Products.TryGetValue(productTitle, out double price)) // add summmary that it returns UAH
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
        /// 
        /// </summary>
        /// <param name="productTitle"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="dish"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="dish"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public double GetDishPrice(Dish dish, Currency currency)
        {
            try
            {
                double price = GetDishPrice(dish);
                price = _db.CurrencyExchanger.ExchangeUAH(price, currency);
                return price;

            }
            catch (ArgumentException e)
            {

                throw;
            }
        }
        //public bool TryGetDishIngredientsMassAndCost(Dish dish, out Dictionary<string, (double mass, double price)>? ingredientsInfo)
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
        /// 
        /// </summary>
        /// <param name="dish"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Dictionary<string, (double mass, double price)>? GetDishIngredientsMassAndCost(Dish dish)
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
                catch (ArgumentException e)
                {
                    throw;
                }
            }
            return ingredientsInfo;
        }
        //public bool TryGetDishIngredientsMassAndCost(Dish dish, Currency currency, out Dictionary<string, (double mass, double price)>? ingredientsInfo)
        //{
        //    bool isFetchSuccessful = TryGetDishIngredientsMassAndCost(dish, out ingredientsInfo); // maybe better not use other method here but set all prices in-place?
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
        /// 
        /// </summary>
        /// <param name="dish"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Dictionary<string, (double mass, double price)>? GetDishIngredientsMassAndCost(Dish dish, Currency currency)
        {
            try
            {
                Dictionary<string, (double mass, double price)>? ingredientsInfo = GetDishIngredientsMassAndCost(dish);
                foreach (KeyValuePair<string, (double mass, double price)> ing in ingredientsInfo)
                {
                    double exchangedPrice = _db.CurrencyExchanger.ExchangeUAH(ing.Value.price, currency);
                    ingredientsInfo[ing.Key] = (ing.Value.mass, exchangedPrice);
                }
                return ingredientsInfo;
            }
            catch (ArgumentException e)
            {
                throw;
            }
        }
        //public bool TryGetTotalCost(out double price)
        //{
        //    price = 0.0;
        //    foreach(Dish dish in _db.Menu.Dishes)
        //    {
        //        if(!TryGetDishCost(dish, out double dishPrice))
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
        //public bool TryGetTotalCost(Currency currency, out double price)
        //{
        //    bool isFetchSuccessful = TryGetTotalCost(out price);
        //    if (isFetchSuccessful)
        //    {
        //        price = _db.CurrencyExchanger.ExchangeUAH(price, currency);
        //    }
        //    return isFetchSuccessful;

        //}
        //public bool TryGetMenuIngredientsMassAndCost(out Dictionary<string, (double mass, double price)>? menuIngredientsInfo)
        //{
        //    menuIngredientsInfo = new();
        //    foreach(Dish dish in _db.Menu.Dishes)
        //    {
        //        if(!TryGetDishIngredientsMassAndCost(dish, out Dictionary<string, (double mass, double price)>? dishIngredientsInfo))
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
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Dictionary<string, (double mass, double price)>? GetMenuIngredientsMassAndCost()
        {
            Dictionary<string, (double mass, double price)>? menuIngredientsInfo = new();
            foreach (Dish dish in _db.Menu.Dishes)
            {
                try
                {
                    Dictionary<string, (double mass, double price)>? dishIngredientsInfo = GetDishIngredientsMassAndCost(dish);
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
                catch (ArgumentException e)
                {
                    throw;
                }
            }
            return menuIngredientsInfo;
        }
        //public bool TryGetMenuIngredientsMassAndCost(Currency currency, out Dictionary<string, (double mass, double price)>? menuIngredientsInfo)
        //{
        //    bool isFetchSuccessful = TryGetMenuIngredientsMassAndCost(out menuIngredientsInfo); // use non-currency method or duplicate code but change 1 line there?
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
        /// 
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Dictionary<string, (double mass, double price)>? GetMenuIngredientsMassAndCost(Currency currency)
        {
            try
            {
                Dictionary<string, (double mass, double price)>? menuIngredientsInfo = GetMenuIngredientsMassAndCost(); // use non-currency method or duplicate code but change 1 line there?
                foreach (KeyValuePair<string, (double mass, double price)> ing in menuIngredientsInfo)
                {
                    double exchangedPrice = _db.CurrencyExchanger.ExchangeUAH(ing.Value.price, currency);
                    menuIngredientsInfo[ing.Key] = (ing.Value.mass, exchangedPrice);
                }
                return menuIngredientsInfo;
            }
            catch (ArgumentException e)
            {
                throw;
            }
        }
        //public void SaveMenuIngredientsMassAndCostToFile()
        //{
        //    bool isFetchSuccsessful = TryGetMenuIngredientsMassAndCost(out Dictionary<string, (double mass, double price)>? menuIngredientsInfo);
        //    if (isFetchSuccsessful)
        //    {
        //        _db.SaveMenuIngredientsMassAndCostToFile(menuIngredientsInfo);
        //    }
        //}
        //public void SaveMenuIngredientsMassAndCostToFile()
        //{
        //    try
        //    {
        //        Dictionary<string, (double mass, double price)>? menuIngredientsInfo = GetMenuIngredientsMassAndCost();
        //        _db.SaveMenuIngredientsMassAndCostToFile(menuIngredientsInfo);
        //    }
        //    catch ()
        //}
        //public void SaveMenuIngredientsMassAndCostToFile(Currency currency)
        //{
        //    bool isFetchSuccsessful = TryGetMenuIngredientsMassAndCost(currency, out Dictionary<string, (double mass, double price)>? menuIngredientsInfo);
        //    if (isFetchSuccsessful)
        //    {
        //        _db.SaveMenuIngredientsMassAndCostToFile(menuIngredientsInfo, currency); // passing currency only to print $ or ₴ sign
        //    }
        //}
        public void AddPrice(KeyValuePair<string, double> product)
        {
            _db.AddPriceToFile(product);
        }
    }
}
