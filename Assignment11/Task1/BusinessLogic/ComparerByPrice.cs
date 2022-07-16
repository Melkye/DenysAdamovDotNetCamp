using Task1.Entities;

namespace Task1.BusinessLogic
{
    public class ComparerByPrice : IComparer<Product>
    {
        public int Compare(Product? x, Product? y)
        {
            return x?.Price.CompareTo(y?.Price ?? -1) ?? -1;
        }
    }
}
