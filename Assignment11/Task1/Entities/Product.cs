using Task1.Interfaces;

namespace Task1.Entities
{
    public abstract class Product : IGood // keep abstract?
    {
        protected string _title;
        protected double _price;
        protected double _weight;
        protected Product() : this(default, default, default)
        { }
        protected Product(Product copyProduct) : this(copyProduct.Title, copyProduct.Price, copyProduct.Weight)
        { }
        protected Product(string title, double price, double weight)
        {
            Title = title;
            Price = price;
            Weight = weight;
        }
        /// <summary>
        /// The product's title
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public string Title
        {
            get => _title;
            protected set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Title can't be null");
                }
                else
                {
                    _title = value;
                }
            }
        }
        /// <summary>
        /// The product's price
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public double Price
        {
            get => _price;
            protected set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Price can't be equal or less than zero");
                }
                else
                {
                    _price = value;
                }
            }
        }
        public double Weight
        {
            get => _weight;
            protected set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Weight can't be equal or less than zero");
                }
                else
                {
                    _weight = value;
                }
            }
        }
        /// <summary>
        /// Decreases price by specified percent
        /// </summary>
        /// <param name="percent">A part of current price which will be subtracted</param>
        /// <exception cref="ArgumentException"></exception>
        public virtual void DecreasePrice(double percent) // what if price will be 0 or less? exception should pe thrown in Price
        {
            if (percent < 0)
            {
                throw new ArgumentException("Decrease percent can't be negative");
            }
            else if (percent >= 100)
            {
                throw new ArgumentException("Decrease percent can't be 100 or higher");
            }
            else
            {
                Price *= (100 - percent) / 100;
            }
        }
        /// <summary>
        /// Increases price by specified percent
        /// </summary>
        /// <param name="percent">A part of current price which will be added</param>
        /// <exception cref="ArgumentException"></exception>
        public virtual void IncreasePrice(double percent)
        {
            if (percent < 0)
            {
                throw new ArgumentException("Increase percent can't be negative");
            }
            else
            {
                Price *= (100 + percent) / 100;
            }
        }
        public int CompareTo(object? obj)
        {
            return (obj as Product)?.Title.CompareTo(Title) ?? -1;
        }
        public override string ToString() // change constants
        {
            return $"{Title,-10}|{Price,-10:C2}|{Weight,-10}|";
        }
    }
}
