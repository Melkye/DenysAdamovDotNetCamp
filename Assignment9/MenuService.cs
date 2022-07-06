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
                    price += ingredientPrice * dish.Ingredients[ingredientTitle];
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
    }
}
