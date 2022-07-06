using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment9
{
    internal class Menu
    {
        private List<Dish> _dishes = new List<Dish>(); // use set to have unique dishes
        //public Menu()
        //{
        //    _dishes = new();
        //}
        public Menu(List<Dish> dishes)
        {
            _dishes = new(dishes);
        }

        // copying and returning the whole collectuin seems too heavy, how to avoid this?
        // suppose using Iterator pattern
        public List<Dish> Dishes => new (_dishes);
        // when return List<> and whrn return IEnumerable<>?
        // public IEnumerable<Dish> Dishes => new List<Dish>(_dishes);
    }
}
