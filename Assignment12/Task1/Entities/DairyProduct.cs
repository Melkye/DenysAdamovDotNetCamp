using Task1.Settings;

namespace Task1.Entities
{
    public class DairyProduct : FoodProduct
    {
        private readonly int _daysBeforeSpoil;

        public DairyProduct() : this(default, default, default, default, default)
        { }
        public DairyProduct(string name, double price, double weight, DateOnly bestBefore, int daysBeforeSpoil) : base(name, price, weight, bestBefore)
        {
            _daysBeforeSpoil = daysBeforeSpoil;
        }
        public int DaysBeforeSpoil => _daysBeforeSpoil;
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
            return base.ToString() + $"{ DaysBeforeSpoil,-FormatSettings.DAYS_BEFORE_SPOIL_PRINT_WIDTH}";
        }
        public override bool Equals(object? obj)
        {
            if (obj is DairyProduct comparedDp)
            {
                if (Title == comparedDp.Title
                    && Math.Abs(Price - comparedDp.Price) < 0.000001
                    && Math.Abs(Mass - comparedDp.Mass) < 0.000001
                    && _daysBeforeSpoil == comparedDp._daysBeforeSpoil)
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
