using System.Collections;

namespace Task1.Entities
{
    public class Buy : IEnumerable<FoodProduct>
    {
        private List<FoodProduct> _items;
        public Buy() :this(default)
        { }
        public Buy(IEnumerable<FoodProduct> products)
        { 
            Fill(products);
        }
        public double TotalWeight => _items.Sum(p => p.Mass);
        public double TotalPrice => _items.Sum(p => p.Price);
        public void Fill(IEnumerable<FoodProduct> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items), "Can't fill with emptiness");
            }
            else
            {
                _items = new(items);
            }
        }

        public IEnumerator<FoodProduct> GetEnumerator()
        {
            return ((IEnumerable<FoodProduct>)_items).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_items).GetEnumerator();
        }
    }
}
