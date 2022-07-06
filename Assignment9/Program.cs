using Assignment9;

Dictionary<string, double> ingredients1 = new();
ingredients1.Add("1", 1.1);
ingredients1.Add("2", 2.2);
ingredients1.Add("3", 3.3);

Dictionary<string, double> ingredients2 = new();
ingredients2.Add("4", 1.1);
ingredients2.Add("5", 2.2);
ingredients2.Add("6", 3.3);

Dictionary<string, double> ingredients3 = new();
ingredients3.Add("7", 1.1);
ingredients3.Add("8", 2.2);
ingredients3.Add("9", 3.3);

Dish dish1 = new(ingredients1);
Dish dish2 = new(ingredients2);
Dish dish3 = new(ingredients3);

ingredients1.Clear();

List<Dish> dishes1 = new List<Dish>() { dish1 };

Menu menu = new(dishes1);

dishes1.Add(dish2);

Console.WriteLine("");

List<Dish> dishes2 = menu.Dishes;

dishes2.Add(dish3);

Console.WriteLine("");