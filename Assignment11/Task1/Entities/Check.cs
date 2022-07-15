
namespace Task1
{
    public class Check : IViewerBuy
    {
        public void ViewBuy(Buy buy)
        {
            Console.WriteLine("Total buy price = " + buy.TotalPrice + " | Total buy weight = " + buy.TotalWeight);
            Console.WriteLine("All products:");
            Console.WriteLine($"{"Title",-10}|{"Price",-10}|{"Weight (g)",-10}|{"DaysBeforeSpoil / Meat Cat",-30}|{"Meat type",-10}|");
            foreach (Product product in buy.Products)
            {
                Console.WriteLine(product);
            }
        }
    }
}
