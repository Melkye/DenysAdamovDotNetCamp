
namespace Task3
{
    public class Storage
    {
        #region Fields
        private List<Product> _products;
        #endregion Fields
        #region Constructors
        public Storage() : this(default)
        { }
        public Storage(IEnumerable<Product> products)
        {
            Fill(products);
        }
        #endregion Constructors
        #region Properties
        public List<Product> Products
        {
            private set => Fill(value);
            get => new(_products);
        }
        public double TotalWeight => _products.Sum(p => p.Weight);
        public double TotalPrice => _products.Sum(p => p.Price);
        #endregion Properties
        #region Indexers
        public Product this[int index]
        {
            get => new Product(_products[index]);
            set
            {
                if (value is not null and Product)
                {
                    _products[index] = value;
                }
                else
                {
                    throw new ArgumentException("Can't insert a non-Product");
                }
            }
        }
        #endregion Indexers
        #region Methods
        public void Fill(IEnumerable<Product> products)
        {
            if (products is not null)
            {
                _products = new List<Product>(products);
            }
            else
            {
                throw new ArgumentNullException(nameof(products), "Can't fill with emptiness");
            }
        }
        public List<Meat> GetMeatProducts()
        {
            List<Meat> meatProducts = new();
            foreach (Product product in _products)
            {
                if (product is Meat)
                {
                    meatProducts.Add(product as Meat);
                }
            }
            return meatProducts;
        }
        public void ChangePrice(double changePercent)
        {
            foreach (var product in Products)
            {
                product.ChangePrice(changePercent);
            }
        }
        #endregion Methods
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
    }
}
