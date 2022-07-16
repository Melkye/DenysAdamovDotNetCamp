using System.Collections;
using Task1.Interfaces;

namespace Task1.Entities
{
    internal class Storage : IStorage //, IEnumerable<Product>
    {
        #region Fields
        private List<Product> _items; // where IGood
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
        public double TotalMass => _items.Sum(p => p.Mass);
        public double TotalPrice => _items.Sum(p => p.Price);
        #endregion Properties
        #region Indexers
        public IGood this[int index]
        {
            // returning an internal object and using base reference
            // due to unability to determine its
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
                    _items[index] = (Product)value;
                }
            }
        }
        #endregion Indexers
        #region Methods
        public void Fill(IEnumerable<IGood> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items), "Can't fill with emptiness");
            }
            else
            {
                // further text may be wrong

                // this copies Product instances' references to _items
                // suppose need to create new instances
                // but their real type can be not base but inherited
                // so declaring a new instance of type Product is not correct
                _items = new(items.Cast<Product>());
            }
        }
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
        public IStorage GetExcept(IStorage other)
        {
            return new Storage(_items.Except((other as Storage)._items)); // what if not Storage?
        }
        public IStorage GetIntersect(IStorage other)
        {
            return new Storage(_items.Intersect((other as Storage)._items));
        }
        public IStorage GetUnion(IStorage other)
        {
            return new Storage(_items.Union((other as Storage)._items));
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)_items).GetEnumerator();
        }

        //public IEnumerator<Product> GetEnumerator()
        //{
        //    return ((IEnumerable<Product>)_items).GetEnumerator();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return ((IEnumerable)_items).GetEnumerator();
        //}
        #endregion Methods
        #region Operator Overloads
        public static Storage operator /(Storage left, Storage right)
        {
            return (Storage)left.GetExcept(right); // remove cast
        }
        public static Storage operator *(Storage left, Storage right)
        {
            return (Storage)left.GetIntersect(right);
        }
        public static Storage operator +(Storage left, Storage right)
        {
            return (Storage)right.GetUnion(left);
        }
        #endregion
    }
}
