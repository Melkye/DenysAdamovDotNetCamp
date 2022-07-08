namespace Assignment9
{
    internal class Dish
    {
        private Dictionary<string, double> _ingredients; // title and mass in grams
        public Dish(string title, Dictionary<string, double> ingredients)
        {
            Title = title;
            _ingredients = new(ingredients);
        }
        public string Title { get; private set; }
        // copying and returning the whole collection seems too heavy, how to avoid this?
        // suppose using Iterator pattern
        public Dictionary<string, double> Ingredients => new(_ingredients);
    }
}
