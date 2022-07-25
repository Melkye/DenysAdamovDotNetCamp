using Task1.Interfaces;
using Task1.Settings;

namespace Task1.Entities
{
    public abstract class FoodProduct : IGood
    {
        protected string _title;
        protected double _price;
        protected double _mass;
        protected DateOnly _bestBefore;
        protected FoodProduct() : this(default, default, default, default)
        { }
        protected FoodProduct(FoodProduct copyProduct) : this(copyProduct.Title, copyProduct.Price, copyProduct.Mass, copyProduct.BestBefore)
        { }
        // TODO: child constructors throw exception because of this
        // and this throws because of properties
        // how this should be documented?
        // TODO: is it ok that constructor throws an exception?
        protected FoodProduct(string title, double price, double mass, DateOnly bestBefore)
        {
            Title = title;
            Price = price;
            Mass = mass;
            BestBefore = bestBefore;
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
        /// <summary>
        /// The product's mass
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public double Mass
        {
            get => _mass;
            protected set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Mass can't be equal or less than zero");
                }
                else
                {
                    _mass = value;
                }
            }
        }
        /// <summary>
        /// The product's 'best before' date
        /// </summary>
        public DateOnly BestBefore
        {
            get => _bestBefore;
            protected set
            {
                _bestBefore = value;
            }
        }
        /// <summary>
        /// Decreases price by specified percent
        /// </summary>
        /// <param name="percent">A part of current price which will be subtracted</param>
        /// <exception cref="ArgumentException"></exception>
        public virtual void DecreasePrice(double percent)
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
            return (obj as FoodProduct)?.Title.CompareTo(Title) ?? -1;
        }
        public override string ToString()
        {
            return $"{Title,-FormatSettings.TITLE_PRINT_WIDTH}|" +
                $"{Price,-FormatSettings.PRICE_PRINT_WIDTH:C2}|" +
                $"{Mass,-FormatSettings.MASS_PRINT_WIDTH}|" + 
                $"{BestBefore,-FormatSettings.DATE_PRINT_WIDTH}|";
        }
    }
}
