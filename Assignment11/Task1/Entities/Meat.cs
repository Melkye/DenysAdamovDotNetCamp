using Task1.Enums;

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
        //public override void ChangePrice(double changePercent = 0)
        //{
        //    if (changePercent != 0)
        //    {
        //        double changePercentPerType = 0;
        //        switch (_type)
        //        {
        //            case MeatType.Mutton:
        //                changePercentPerType = 20;
        //                break;
        //            case MeatType.Veal:
        //                changePercentPerType = 15;
        //                break;
        //            case MeatType.Pork:
        //                changePercentPerType = 10;
        //                break;
        //            case MeatType.Chicken:
        //                changePercentPerType = 5;
        //                break;
        //            default:
        //                changePercentPerType = 0;
        //                break;
        //        }
        //        if (changePercent < 0)
        //        {
        //            changePercentPerType = -changePercentPerType;
        //        }
        //        base.ChangePrice(changePercent + changePercentPerType);
        //    }
        //}
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
            return base.ToString() + $"{ Category, -30}|{Type, -10}|";
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
