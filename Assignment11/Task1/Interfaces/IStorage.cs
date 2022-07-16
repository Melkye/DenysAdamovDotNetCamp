using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Interfaces
{
    // TODO: IStorage< out T>?
    internal interface IStorage<T> : IEnumerable<T> where T : class, IGood // IEnumerable where T : class, IGood
    {
        double TotalMass { get; }
        double TotalPrice { get; }
        T this[int index] { get; set; } // ??
        void Fill(IEnumerable<T> items);
        void DecreasePrice(double percent);
        void IncreasePrice(double percent);
        // TODO: how to let use method only when tis and other are the same type?
        //public IStorage<T> Except(IStorage<T> other);
        //public IStorage<T> Intersect(IStorage<T> other);
        //public IStorage<T> Union(IStorage<T> other);
        public IEnumerable<T> Except(IEnumerable<T> other);
        public IEnumerable<T> Intersect(IEnumerable<T> other);
        public IEnumerable<T> Union(IEnumerable<T> other);
    }
}
