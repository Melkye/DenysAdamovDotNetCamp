using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment9
{
    /// <summary>
    /// Works as a simulation of db connection
    /// </summary>
    internal class FileWorker
    {
        private string _pricesFile;
        private string _menuFile;
        public FileWorker(string pricesFile, string menuFile)
        {
            _pricesFile = pricesFile;
            _menuFile = menuFile;
        }
        public Dictionary<string, double> ReadPricesFromFile()
        {
            using (StreamReader file = new(_pricesFile)) // handle exceptions
            {
                Dictionary<string, double> products = new Dictionary<string, double>();
                while (!file.EndOfStream)
                {
                    string? line = file.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] productInfo = line.Trim().Split();
                        string title = productInfo[0];
                        double price = double.Parse(productInfo[1]);
                        products[title] = price;
                    }
                }
                return products; // maybe better return after the using block?
            }
        }
        public List<Dish> ReadMenuFromFile()
        {
            using (StreamReader file = new(_menuFile))
            {
                List <Dish> dishes = new();
                while (!file.EndOfStream)
                {
                    dishes.Add(ReadDishFromFile(file));
                }
                return dishes;
            }
        }
        private Dish ReadDishFromFile(StreamReader file)
        {
            string dishTitle = file.ReadLine();

            Dictionary<string, double> ingredients = new();
            string? ingredientLine;
            while (!file.EndOfStream)
            {
                ingredientLine = file.ReadLine();
                if (string.IsNullOrEmpty(ingredientLine))
                {
                    break;
                }
                string[] ingredientInfo = ingredientLine.Trim().Split();
                string title = ingredientInfo[0];
                double mass = double.Parse(ingredientInfo[1]);

                ingredients[title] = mass;                
            }
            return new Dish(dishTitle, ingredients);
        }
    }
}
