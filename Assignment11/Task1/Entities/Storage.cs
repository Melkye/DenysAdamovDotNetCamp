using System.Collections;
using Task1.Interfaces;

namespace Task1.Entities
{
    internal class Storage<T> : IStorage<T> where T : class, IGood //, IEnumerable<Product>
    {
        // TODO IDisposable ? for Enumerator?
        #region Fields
        private List<T> _items; // where IGood
        #endregion Fields
        #region Constructors
        public Storage() : this(default)
        { }
        public Storage(IEnumerable<T> items)
        {
            Fill(items);

            //if (items is null)
            //{
            //    throw new ArgumentNullException(nameof(items), "Can't fill with emptiness");
            //}
            //else
            //{
            //    // further text may be wrong

            //    // this copies Product instances' references to _items
            //    // suppose need to create new instances
            //    // but their real type can be not base but inherited
            //    // so declaring a new instance of type Product is not correct
            //    _items = new(items);
            //}
        }
        #endregion Constructors
        #region Properties
        public double TotalMass => _items.Sum(p => p.Mass);
        public double TotalPrice => _items.Sum(p => p.Price);
        #endregion Properties
        #region Indexers
        // TODO: return T or IGood?
        public T this[int index]
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
                    _items[index] = value;
                }
            }
        }
        #endregion Indexers
        #region Methods
        // TODO: remove Fill and move validation logic to constructor
        public void Fill(IEnumerable<T> items)
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
                _items = new(items);
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
        public IEnumerable<T> Except(IEnumerable<T> other) // IEnumerable<T>?
        {
            return new Storage<T>(_items.Except((other as Storage<T>)._items)); // what if not Storage?
        }
        public IEnumerable<T> Intersect(IEnumerable<T> other)
        {
            return new Storage<T>(_items.Intersect((other as Storage<T>)._items));
        }
        public IEnumerable<T> Union(IEnumerable<T> other)
        {
            return new Storage<T>(_items.Union((other as Storage<T>)._items));
        }

        //IEnumerator IEnumerable<T>.GetEnumerator()
        //{
        //    return ((IEnumerable)_items).GetEnumerator();
        //}

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_items).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_items).GetEnumerator();
        }
        #endregion Methods
        #region Operator Overloads
        // TODO: Storage<T> or IEnumerable<T>?
        public static Storage<T> operator /(Storage<T> left, Storage<T> right)
        {
            return (Storage<T>)left.Except(right);
        }
        public static Storage<T> operator *(Storage<T> left, Storage<T> right)
        {
            return (Storage<T>)left.Intersect(right);
        }
        public static Storage<T> operator +(Storage<T> left, Storage<T> right)
        {
            return (Storage<T>)right.Union(left);
        }
        #endregion
    }
}
