using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class ComparerByPrice : IComparer<Product>
    {
        public int Compare(Product? x, Product? y)
        {
            return x.Price.CompareTo(y.Price);
        }
    }
}
