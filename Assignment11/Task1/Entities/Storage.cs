
using Task1.Interfaces;

namespace Task1
{
    internal class Storage
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
        // TODO Replace with IEnumerable implementation
        public List<Product> Products
        {
            private set => Fill(value);
            get => new(_items);
        }
        public double TotalWeight => _items.Sum(p => p.Weight);
        public double TotalPrice => _items.Sum(p => p.Price);
        #endregion Properties
        #region Indexers
        public Product this[int index]
        {
            get => new(_items[index]);
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
        public void DecreasePrice(double percent)
        {
            foreach (var item in _items)
            {
                item.DecreasePrice(percent);
            }
        }
        // how to hande exceptions and document which ones methods throws when working through interface?
        public void IncreasePrice(double percent)
        {
            foreach (var item in _items)
            {
                item.IncreasePrice(percent); // does it work?
                // catch - reset price back for those that changed
            }
        }
        #endregion Methods
        #region Operator Overloads
        public static Storage operator /(Storage left, Storage right)
        {
            return new Storage(left.Products.Except(right.Products));
        }
        public static Storage operator *(Storage left, Storage right)
        {
            return new Storage(left.Products.Intersect(right.Products));
        }
        public static Storage operator +(Storage left, Storage right)
        {
            return new Storage(left.Products.Union(right.Products));
        }
        #endregion
    }
}
