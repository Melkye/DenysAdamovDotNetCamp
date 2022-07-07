using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment9
{
    internal class MenuService
    {
        private DbSimulator _db;
        public MenuService(DbSimulator db)
        {
               _db = new(db); // or just _db = db?
        }
        public bool TryGetProductPrice(string productTitle, out double price)
        {
            return _db.ProductPrices.Products.TryGetValue(productTitle, out price); // add summmary that it returns UAH
        }
        public bool TryGetProductPrice(string productTitle, Currency currency, out double price) // get UAH without currency?
        {
            bool isFetchSuccessful = TryGetProductPrice(productTitle, out price);
            if (isFetchSuccessful)
            {
                price = _db.CurrencyExchanger.ExchangeUAH(price, currency);
            }
            return isFetchSuccessful;
        }
        public bool TryGetDishCost(Dish dish, out double price)
        {
            price = 0.0;
            foreach (string ingredientTitle in dish.Ingredients.Keys)
            {
                if (!TryGetProductPrice(ingredientTitle, out double ingredientPrice))
                {
                    price = default;
                    return false;
                }
                else
                {
                    price += ingredientPrice * dish.Ingredients[ingredientTitle] / 1000;
                }
            }
            return true;
        }
        public bool TryGetDishCost(Dish dish, Currency currency, out double price)
        {
            bool isFetchSuccessful = TryGetDishCost(dish, out price);
            if (isFetchSuccessful)
            {
                price = _db.CurrencyExchanger.ExchangeUAH(price, currency);
            }
            return isFetchSuccessful;
        }
        public bool TryGetDishIngredientsMassAndCost(Dish dish, out Dictionary<string, (double mass, double price)>? ingredientsInfo)
        {
            ingredientsInfo = new();
            foreach (string ingredientTitle in dish.Ingredients.Keys)
            {
                if (!TryGetProductPrice(ingredientTitle, out double ingredientPrice))
                {
                    ingredientsInfo = default;
                    return false;
                }
                else
                {
                    double ingredientMass = dish.Ingredients[ingredientTitle];
                    double ingredientPricePerMass = ingredientPrice * ingredientMass / 1000;
                    ingredientsInfo[ingredientTitle] = (ingredientMass, ingredientPricePerMass);
                }
            }
            return true;
        }
        public bool TryGetDishIngredientsMassAndCost(Dish dish, Currency currency, out Dictionary<string, (double mass, double price)>? ingredientsInfo)
        {
            bool isFetchSuccessful = TryGetDishIngredientsMassAndCost(dish, out ingredientsInfo); // maybe better not use other method here but set all prices in-place?
            if (isFetchSuccessful)
            {
                foreach (KeyValuePair<string, (double mass, double price)> ing in ingredientsInfo)
                {
                    double exchangedPrice = _db.CurrencyExchanger.ExchangeUAH(ing.Value.price, currency);
                    ingredientsInfo[ing.Key] = (ing.Value.mass, exchangedPrice);
                }
            }
            return isFetchSuccessful;
        }
        public bool TryGetTotalCost(out double price)
        {
            price = 0.0;
            foreach(Dish dish in _db.Menu.Dishes)
            {
                if(!TryGetDishCost(dish, out double dishPrice))
                {
                    price = default;
                    return false;
                }
                else
                {
                    price += dishPrice;
                }
            }
            return true;
        }
        public bool TryGetTotalCost(Currency currency, out double price)
        {
            bool isFetchSuccessful = TryGetTotalCost(out price);
            if (isFetchSuccessful)
            {
                price = _db.CurrencyExchanger.ExchangeUAH(price, currency);
            }
            return isFetchSuccessful;

        }
        public bool TryGetMenuIngredientsMassAndCost(out Dictionary<string, (double mass, double price)>? menuIngredientsInfo)
        {
            menuIngredientsInfo = new();
            foreach(Dish dish in _db.Menu.Dishes)
            {
                if(!TryGetDishIngredientsMassAndCost(dish, out Dictionary<string, (double mass, double price)>? dishIngredientsInfo))
                {
                    menuIngredientsInfo = default;
                    return false;
                }
                else
                {
                    foreach(KeyValuePair<string, (double mass, double price)> dishIngredientInfo in dishIngredientsInfo)
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
            }
            return true;
        }
        public bool TryGetMenuIngredientsMassAndCost(Currency currency, out Dictionary<string, (double mass, double price)>? menuIngredientsInfo)
        {
            bool isFetchSuccessful = TryGetMenuIngredientsMassAndCost(out menuIngredientsInfo); // use non-currency method or duplicate code but change 1 line there?
            if(isFetchSuccessful)
            {
                foreach (KeyValuePair<string, (double mass, double price)> ing in menuIngredientsInfo)
                {
                    double exchangedPrice = _db.CurrencyExchanger.ExchangeUAH(ing.Value.price, currency);
                    menuIngredientsInfo[ing.Key] = (ing.Value.mass, exchangedPrice);
                }
            }
            return isFetchSuccessful;
        }
        public void SaveMenuIngredientsMassAndCostToFile()
        {
            bool isFetchSuccsessful = TryGetMenuIngredientsMassAndCost(out Dictionary<string, (double mass, double price)>? menuIngredientsInfo);
            if (isFetchSuccsessful)
            {
                _db.SaveMenuIngredientsMassAndCostToFile(menuIngredientsInfo);
            }
        }
        public void SaveMenuIngredientsMassAndCostToFile(Currency currency)
        {
            bool isFetchSuccsessful = TryGetMenuIngredientsMassAndCost(currency, out Dictionary<string, (double mass, double price)>? menuIngredientsInfo);
            if (isFetchSuccsessful)
            {
                _db.SaveMenuIngredientsMassAndCostToFile(menuIngredientsInfo, currency); // passing currency only to print $ or ₴ sign
            }
        }
    }
}
