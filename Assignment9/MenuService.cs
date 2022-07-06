using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment9
{
    internal static class MenuService
    {
        public static bool TryGetProductPrice(string productTitle, PriceList priceList, out double price)
        {
            return priceList.Products.TryGetValue(productTitle, out price);
        }
        public static bool TryGetDishCost(Dish dish, PriceList priceList, out double price)
        {
            price = 0.0;
            foreach (string ingredientTitle in dish.Ingredients.Keys)
            {
                if (!TryGetProductPrice(ingredientTitle, priceList, out double ingredientPrice))
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
        public static bool TryGetDishIngredientsMassAndCost(Dish dish, PriceList priceList, out Dictionary<string, (double mass, double price)>? ingredientsInfo)
        {
            ingredientsInfo = new();
            foreach (string ingredientTitle in dish.Ingredients.Keys)
            {
                if (!TryGetProductPrice(ingredientTitle, priceList, out double ingredientPrice))
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
        public static bool TryGetTotalCost(Menu menu, PriceList priceList, out double price)
        {
            price = 0.0;
            foreach(Dish dish in menu.Dishes)
            {
                if(!TryGetDishCost(dish, priceList, out double dishPrice))
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
        public static bool TryGetMenuIngredientsMassAndCost(Menu menu, PriceList priceList, out Dictionary<string, (double mass, double price)>? menuIngredientsInfo)
        {
            menuIngredientsInfo = new();
            foreach(Dish dish in menu.Dishes)
            {
                if(!TryGetDishIngredientsMassAndCost(dish, priceList, out Dictionary<string, (double mass, double price)>? dishIngredientsInfo))
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
    }
}
