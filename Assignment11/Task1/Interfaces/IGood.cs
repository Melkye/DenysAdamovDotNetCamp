using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Interfaces
{
    internal interface IGood : IComparable
    {
        string Title { get; }
        double Price { get; }
        double Mass { get; }
        void DecreasePrice(double percent);
        /// <summary>
        /// blab
        /// </summary>
        /// <param name="percent"></param>
        void IncreasePrice(double percent);
        //string ToString(); // ???
    }
}
