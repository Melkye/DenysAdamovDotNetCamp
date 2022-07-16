using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Interfaces
{
    internal interface IStorage : IEnumerable
    {
        double TotalMass { get; }
        double TotalPrice { get; }
        IGood this[int index] { get; set; } // ??
        void Fill(IEnumerable<IGood> items);
        void DecreasePrice(double percent);
        void IncreasePrice(double percent);
        public IStorage GetExcept(IStorage other); // T GetExcept(T other);
        public IStorage GetIntersect(IStorage other);
        public IStorage GetUnion(IStorage other);
    }
}
