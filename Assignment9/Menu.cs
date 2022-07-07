using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment9
{
    internal class Menu
    {

        private FileWorker _fileWorker;
        private List<Dish> _dishes = new List<Dish>(); // use set to have unique dishes
        //public Menu()
        //{
        //    _dishes = new();
        //}
        //public Menu(List<Dish> dishes)
        //{
        //    _dishes = new(dishes);
        //}
        public Menu(FileWorker fileWorker)
        {
            _fileWorker = fileWorker; // use the passed fileworker or create own copy?
            ReadFromFile();
        }
        public Menu(Menu copyMenu)
        {
            _fileWorker = new(copyMenu._fileWorker); // create own copy?
            _dishes = new(copyMenu._dishes); // create own copy?
        }

        // copying and returning the whole collection seems too heavy, how to avoid this?
        // suppose using Iterator pattern
        public List<Dish> Dishes => new (_dishes);
        // when return List<> and when return IEnumerable<>?
        // public IEnumerable<Dish> Dishes => new List<Dish>(_dishes);

        public void ReadFromFile()
        {
            _dishes = _fileWorker.ReadMenuFromFile();
        }
    }
}
