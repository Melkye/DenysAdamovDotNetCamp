using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment9
{
    internal class MenuService
    {
        private PriceList _priceList;
        private Menu _menu;
        private CurrencyExchanger _currencyExchanger; // suppose this should not be here
        public MenuService(PriceList priceList, Menu menu, CurrencyExchanger currencyExchanger)
        {
            PriceList = priceList;
            Menu = menu;
            CurrencyExchanger = currencyExchanger;
        }
        public MenuService(FileWorker fileWorker) // do we even need this?
        {
            PriceList = new(fileWorker);
            Menu = new(fileWorker);
            CurrencyExchanger = new(fileWorker);
        }
        public PriceList PriceList
        {
            get => new(_priceList);
            set => _priceList = value is not null ? value : _priceList; // reassigning? if so, guess better use only if-conditional
        }
        public Menu Menu
        {
            get => new(_menu);
            set => _menu = value is not null ? value : _menu; // reassigning? if so, guess better use only if-conditional
        }
        public CurrencyExchanger CurrencyExchanger
        {
            get => new(_currencyExchanger);
            set => _currencyExchanger = value is not null ? value : _currencyExchanger; // reassigning? if so, guess better use only if-conditional
        }
        public bool TryGetProductPrice(string productTitle, out double price) // get UAH without currency?
        {
            return _priceList.Products.TryGetValue(productTitle, out price); // add summmary that it returns UAH
        }
        public bool TryGetProductPrice(string productTitle, Currency currency, out double price) // get UAH without currency?
        {
            bool isFetchSuccessful = TryGetProductPrice(productTitle, out price); // fetch price in specified currency + how will it be printed? all dollars?
            if (isFetchSuccessful)
            {
                price = _currencyExchanger.ExchangeUAH(price, currency);
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
                price = _currencyExchanger.ExchangeUAH(price, currency);
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
                    double exchangedPrice = _currencyExchanger.ExchangeUAH(ing.Value.price, currency);
                    ingredientsInfo[ing.Key] = (ing.Value.mass, exchangedPrice);
                }
            }
            return isFetchSuccessful;
        }
        public bool TryGetTotalCost(out double price)
        {
            price = 0.0;
            foreach(Dish dish in _menu.Dishes)
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
                price = _currencyExchanger.ExchangeUAH(price, currency);
            }
            return isFetchSuccessful;

        }
        public bool TryGetMenuIngredientsMassAndCost(out Dictionary<string, (double mass, double price)>? menuIngredientsInfo)
        {
            menuIngredientsInfo = new();
            foreach(Dish dish in _menu.Dishes)
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
                    double exchangedPrice = _currencyExchanger.ExchangeUAH(ing.Value.price, currency);
                    menuIngredientsInfo[ing.Key] = (ing.Value.mass, exchangedPrice);
                }
            }
            return isFetchSuccessful;
        }
    }
}
