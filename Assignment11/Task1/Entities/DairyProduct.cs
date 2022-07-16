namespace Task1.Entities
{
    public class DairyProduct : Product
    {
        private readonly int _daysBeforeSpoil;

        public DairyProduct() : this(default, default, default, default)
        { }
        public DairyProduct(string name, double price, double weight, int daysBeforeSpoil) : base(name, price, weight)
        {
            _daysBeforeSpoil = daysBeforeSpoil;
        }
        public int DaysBeforeSpoil => _daysBeforeSpoil;
        //public override void ChangePrice(double changePercent = 0)
        //{
        //    if (changePercent >= 0)
        //    {
        //        double changePercentPerDayBeforeSpoil = 0;
        //        if (_daysBeforeSpoil == 1)
        //        {
        //            changePercentPerDayBeforeSpoil = 50;
        //        }
        //        else if (_daysBeforeSpoil == 2)
        //        {
        //            changePercentPerDayBeforeSpoil = 40;
        //        }
        //        else if (_daysBeforeSpoil == 3)
        //        {
        //            changePercentPerDayBeforeSpoil = 20;
        //        }
        //        else if (_daysBeforeSpoil > 3)
        //        {
        //            changePercentPerDayBeforeSpoil = 10;
        //        }
        //        else if (_daysBeforeSpoil > 6)
        //        {
        //            changePercentPerDayBeforeSpoil = 5;
        //        }
        //        else if (_daysBeforeSpoil > 9)
        //        {
        //            changePercentPerDayBeforeSpoil = 0;
        //        }
        //        base.ChangePrice(changePercent + changePercentPerDayBeforeSpoil);
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
            return base.ToString() + $"{ DaysBeforeSpoil, -30}";
        }
        public override bool Equals(object? obj)
        {
            if (obj is DairyProduct comparedDp)
            {
                if (Title == comparedDp.Title
                    && Math.Abs(Price - comparedDp.Price) < 0.000001
                    && Math.Abs(Weight - comparedDp.Weight) < 0.000001
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
