namespace Assignment9
{
    internal class Dish
    {
        private Dictionary<string, double> _ingredients; // title and mass in kg
        //public Dish()
        //{
        //    _ingredients = new();
        //}
        public Dish(Dictionary<string, double> ingredients)
        {
            _ingredients = new(ingredients);
        }
        // copying and returning the whole collectuin seems too heavy, how to avoid this?
        // suppose using Iterator pattern
        public Dictionary<string, double> Ingredients => new(_ingredients);
    }
}
