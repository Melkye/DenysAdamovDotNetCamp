using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment9
{
    internal class PriceList
    {
        public Dictionary<string, double> _products; // title and price per 1 kg
        //public PriceList()
        //{
        //    _products = new();
        //}
        public PriceList(Dictionary<string, double> ingredients)
        {
            _products = new(ingredients);
        }
        // copying and returning the whole collection seems too heavy, how to avoid this?
        // suppose using Iterator pattern
        public Dictionary<string, double> Products => new(_products);
    }
}
