namespace Assignment7
{
    public class Buy
    {
        private List<Product> _products;
        public Buy() :this(default)
        { }
        public Buy(List<Product> products)
        { 
            Fill(products);
        }
        public List<Product> Products
        {
            private set => Fill(value);
            get => new List<Product>(_products);
        }
        public void Fill(List<Product> products)
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
        public double TotalWeight => _products.Sum(p => p.Weight);
        public double TotalPrice => _products.Sum(p => p.Price);

    }
}
