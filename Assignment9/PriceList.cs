using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment9
{
    internal class PriceList
    {

        private FileWorker _fileWorker;
        private Dictionary<string, double> _products; // title and price per 1 kg
        //public PriceList()
        //{
        //    _products = new();
        //}
        //public PriceList(Dictionary<string, double> products)
        //{
        //    _products = new(products);
        //}
        public PriceList(FileWorker fileWorker)
        {
            _fileWorker = fileWorker; // use the passed fileworker or create own copy?
            ReadFromFile();
        }
        public PriceList(PriceList copyList)
        {
            _fileWorker = new(copyList._fileWorker); // create own copy?
            _products = new(copyList._products); // create own copy?
        }
        // copying and returning the whole collection seems too heavy, how to avoid this?
        // suppose using Iterator pattern
        public Dictionary<string, double> Products => new(_products);
        public void ReadFromFile()
        {
            _products = _fileWorker.ReadPricesFromFile();
        }
    }
}
