﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment9
{
    internal class Menu
    {
        private List<Dish> _dishes = new List<Dish>(); // use set to have unique dishes
        public Menu(List<Dish> dishes)
        {
            _dishes = new(dishes);
        }
        public Menu(Menu copyMenu)
        {
           _dishes = new(copyMenu._dishes); // create own copy?
        }

        // copying and returning the whole collection seems too heavy, how to avoid this?
        // suppose using Iterator pattern
        public List<Dish> Dishes => new (_dishes);
        // when return List<> and when return IEnumerable<>?
        // public IEnumerable<Dish> Dishes => new List<Dish>(_dishes);
    }
}