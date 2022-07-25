using Task1.Entities;

namespace Task1.BusinessLogic
{
    public class ComparerByPrice : IComparer<FoodProduct>
    {
        public int Compare(FoodProduct? x, FoodProduct? y)
        {
            return x?.Price.CompareTo(y?.Price ?? -1) ?? -1;
        }
    }
}
