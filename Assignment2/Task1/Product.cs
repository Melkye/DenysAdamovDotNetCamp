﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    internal class Product
    {
        protected double _price;
        protected double _weight;
        public Product() : this(default, default, default)
        { }
        public Product(string title, double price, double weight)
        {
            Title = title;
            Price = price;
            Weight = weight;
        }
        public string Title { get; protected set; }
        public double Price
        {
            get => _price;
            protected set
            {
                if (value > 0)
                {
                    _price = value;
                }
                else
                {
                    throw new ArgumentException("Price can't be equal or less than zero");
                }
            }
        }
        public double Weight
        {
            get => _weight;
            protected set
            {
                if (value > 0)
                {
                    _weight = value;
                }
                else
                {
                    throw new ArgumentException("Weight can't be equal or less than zero");
                }
            }
        }
        public virtual void ChangePrice(double changePercent)
        {
            if (changePercent > -100)
            {
                Price += Price * changePercent / 100;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Sale can't be less than 100%");
            }

        }
        public override string ToString()
        {
            return $"{Title, 10} | {Price, 10} | {Weight, 10}";
        }
    }
}
