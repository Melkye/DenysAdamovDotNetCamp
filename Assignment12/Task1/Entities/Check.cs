using Task1.Interfaces;
using Task1.Settings;

namespace Task1.Entities
{
    public class Check : IViewerBuy
    {
        public void ViewBuy(Buy buy)
        {
            Console.WriteLine("Total buy price = " + buy.TotalPrice + " | Total buy weight = " + buy.TotalWeight);
            Console.WriteLine("All products:");
            Console.WriteLine(
                $"{"Title",-FormatSettings.TITLE_PRINT_WIDTH}|" +
                $"{"Price",-FormatSettings.PRICE_PRINT_WIDTH}|" +
                $"{"Mass (g)",-FormatSettings.MASS_PRINT_WIDTH}|");// {"DaysBeforeSpoil / Meat Cat",-30}|{"Meat type",-10}|");
            foreach (FoodProduct product in buy)
            {
                Console.WriteLine(product);
            }
        }
    }
}
