using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment9
{
    internal class Menu
    {
        private SortedSet<Dish> _dishes;
        public Menu(SortedSet<Dish> dishes)
        {
            _dishes = new(dishes);
        }
        public Menu(Menu copyMenu)
        {
           _dishes = new(copyMenu._dishes);
        }
        // copying and returning the whole collection seems too heavy, how to avoid this?
        // suppose using Iterator pattern
        public SortedSet<Dish> Dishes => new (_dishes);
        // when return List<> and when return IEnumerable<>?
        // public IEnumerable<Dish> Dishes => new SortedSet<Dish>(_dishes);
    }
}
