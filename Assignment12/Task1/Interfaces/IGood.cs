namespace Task1.Interfaces
{
    internal interface IGood : IComparable
    {
        string Title { get; }
        double Price { get; }
        double Mass { get; }
        DateOnly BestBefore { get; }
        void DecreasePrice(double percent);
        void IncreasePrice(double percent);
    }
}
