using Task1.Enums;
using Task1.Settings;

namespace Task1.Entities
{
    public class Meat : Product
    {
        private readonly MeatCategory _category;
        private readonly MeatType _type;
        public Meat() : this(default, default, default, default, default)
        { }
        public Meat(string title, double price, double weight, MeatCategory category, MeatType type) : base(title, price, weight)
        {
            _category = category;
            _type = type;
        }
        public MeatCategory Category => _category;
        public MeatType Type => _type;
        public override void DecreasePrice(double percent)
        {
            base.DecreasePrice(percent);
        }
        public override void IncreasePrice(double percent)
        {
            base.IncreasePrice(percent);
        }
        public override string ToString()
        {
            return base.ToString() + 
                $"{ Category, -FormatSettings.MEAT_CATEGORY_PRINT_WIDTH}|" +
                $"{Type, -FormatSettings.MEAT_TYPE_PRINT_WIDTH}|";
        }
        public override bool Equals(object? obj)
        {
            if (obj is Meat comparedMeat)
            {
                if (Title == comparedMeat.Title
                    && Math.Abs(Price - comparedMeat.Price) < 0.000001
                    && Math.Abs(Mass - comparedMeat.Mass) < 0.000001
                    && _category == comparedMeat._category
                    && _type == comparedMeat._type)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
