
namespace Task3
{
    public class Product : IComparable
    {
        protected double _price;
        protected double _weight;
        public Product() : this(default, default, default)
        { }
        public Product(Product copyProduct) : this(copyProduct.Title, copyProduct.Price, copyProduct.Weight)
        { }
        public Product(string title, double price, double weight)
        {
            Title = title;
            Price = price;
            Weight = weight;
        }
        public string Title { get; protected set; }
        public double Price
        {
            get => _price;
            protected set
            {
                if (value > 0)
                {
                    _price = value;
                }
                else
                {
                    throw new ArgumentException("Price can't be equal or less than zero");
                }
            }
        }
        public double Weight
        {
            get => _weight;
            protected set
            {
                if (value > 0)
                {
                    _weight = value;
                }
                else
                {
                    throw new ArgumentException("Weight can't be equal or less than zero");
                }
            }
        }
        public virtual void ChangePrice(double changePercent)
        {
            if (changePercent > -100)
            {
                Price += Price * changePercent / 100;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(changePercent), "Sale can't be less than 100%");
            }

        }

        public int CompareTo(object? obj)
        {
            return (obj as Product)?.Title.CompareTo(Title) ?? -1;
        }

        public override string ToString()
        {
            return $"{Title,-10}|{Price,-10:C2}|{Weight,-10}|";
        }
    }
}
