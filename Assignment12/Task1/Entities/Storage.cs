using System.Collections;
using Task1.Interfaces;

namespace Task1.Entities
{
    internal class Storage<T> : IStorage<T> where T : class, IGood
    {
        #region Fields
        private List<T> _items;
        public event Action<T> SpoiledSupply;
        #endregion Fields
        #region Constructors
        public Storage()
        {
            _items = new List<T>();
        }
        public Storage(IEnumerable<T> items) : this()
        {
            Fill(items);
        }
        public Storage(Action<T> spoiledSupplyHandler, IEnumerable<T> items) : this()
        {
            SpoiledSupply += spoiledSupplyHandler;
            Fill(items);
        }
        #endregion Constructors
        #region Properties
        public double TotalMass => _items.Sum(p => p.Mass);
        public double TotalPrice => _items.Sum(p => p.Price);
        #endregion Properties
        #region Indexers
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
        public void Fill(IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items), "Can't fill with emptiness");
            }
            else
            {
                _items.Clear();
                // need to create new instances
                // but their real type can be not base but inherited
                // so declaring a new instance of type T is not correct
                foreach (var item in items)
                {
                    if (item.BestBefore <= DateOnly.FromDateTime(DateTime.Now))
                    {
                        SpoiledSupply?.Invoke(item);
                    }
                    else
                    {
                        _items.Add(item); // need to make a copy
                    }
                }
            }
        }
        // TODO: how to hande exceptions and document which ones methods throws when working through interface?
        /// <summary>
        /// Decreases prices for each item
        /// </summary>
        /// <param name="percent"></param>
        /// <exception cref="ArgumentException">Thrown internally when <paramref name="percent"/> is less than 0</exception>
        public void DecreasePrice(double percent)
        {
            foreach (var item in _items)
            {
                item.DecreasePrice(percent);
            }
        }
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
        /// <summary>
        /// Does except set operation 
        /// </summary>
        /// <param name="other">a storage to except with</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IEnumerable<T> Except(IEnumerable<T> other)
        {
            if (other is Storage<T> otherStorage)
            {
                return new Storage<T>(_items.Except(otherStorage._items));
            }
            else
            {
                throw new ArgumentException("Unable to except with non Storage");
            }
        }
        /// <summary>
        /// Does intersect set operation 
        /// </summary>
        /// <param name="other">a storage to intersect with</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IEnumerable<T> Intersect(IEnumerable<T> other)
        {
            if (other is Storage<T> otherStorage)
            {
                return new Storage<T>(_items.Intersect(otherStorage._items));
            }
            else
            {
                throw new ArgumentException("Unable to intersect with non Storage");
            }
        }
        /// <summary>
        /// Does union set operation 
        /// </summary>
        /// <param name="other">a storage to union with</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IEnumerable<T> Union(IEnumerable<T> other)
        {
            if (other is Storage<T> otherStorage)
            {
                return new Storage<T>(_items.Union(otherStorage._items));
            }
            else
            {
                throw new ArgumentException("Unable to union with non Storage");
            }
        }
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
