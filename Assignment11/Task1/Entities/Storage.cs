using System.Collections;

namespace Task1.Entities
{
    internal class Storage : IEnumerable<Product>
    {
        #region Fields
        private List<Product> _items;
        #endregion Fields
        #region Constructors
        public Storage() : this(default)
        { }
        public Storage(IEnumerable<Product> items)
        {
            Fill(items);
        }
        #endregion Constructors
        #region Properties
        public double TotalWeight => _items.Sum(p => p.Weight);
        public double TotalPrice => _items.Sum(p => p.Price);
        #endregion Properties
        #region Indexers
        public Product this[int index]
        {
            // returning an internal object and using base reference
            // due to disability to determine its
            // real type and create a deep copy
            get => _items[index];
            set
            {
                if (value is null)
                {
                    throw new ArgumentException("Can't insert a null");
                }
                else
                {
                    _items[index] = value;
                }
            }
        }
        #endregion Indexers
        #region Methods
        public void Fill(IEnumerable<Product> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items), "Can't fill with emptiness");
            }
            else
            {
                // this copies Product instances' references to _items
                // suppose need to create new instances
                // but their real type can be not base but inherited
                // so declaring a new instance of type Product is not correct
                _items = new(items);
            }
        }
        //public List<Meat> GetMeatProducts()
        //{
        //    List<Meat> meatProducts = new();
        //    foreach (Product product in _products)
        //    {
        //        if (product is Meat)
        //        {
        //            meatProducts.Add(product as Meat);
        //        }
        //    }
        //    return meatProducts;
        //}
        //public void ChangePrice(double changePercent)
        //{
        //    foreach (var product in Products)
        //    {
        //        product.ChangePrice(changePercent);
        //    }
        //}
        // how to hande exceptions and document which ones methods throws when working through interface?
        /// <summary>
        /// Decreases prices for each item
        /// </summary>
        /// <param name="percent"></param>
        /// <exception cref="ArgumentException">Thrown internally when <paramref name="percent"/> less than 0</exception>
        public void DecreasePrice(double percent)
        {
            foreach (var item in _items)
            {
                item.DecreasePrice(percent);
            }
        }
        // how to hande exceptions and document which ones methods throws when working through interface?
        /// <summary>
        /// Increases prices for each item
        /// </summary>
        /// <param name="percent"></param>
        /// <exception cref="ArgumentException">Thrown internally when <paramref name="percent"/> less than 0</exception>
        public void IncreasePrice(double percent)
        {
            foreach (var item in _items)
            {
                item.IncreasePrice(percent);
            }
        }

        public IEnumerator<Product> GetEnumerator()
        {
            return ((IEnumerable<Product>)_items).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_items).GetEnumerator();
        }
        #endregion Methods
        #region Operator Overloads
        public static Storage operator /(Storage left, Storage right)
        {
            return new Storage(left._items.Except(right._items));
        }
        public static Storage operator *(Storage left, Storage right)
        {
            return new Storage(left._items.Intersect(right._items));
        }
        public static Storage operator +(Storage left, Storage right)
        {
            return new Storage(left._items.Union(right._items));
        }
        #endregion
    }
}
