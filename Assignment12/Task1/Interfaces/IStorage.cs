namespace Task1.Interfaces
{
    internal interface IStorage<T> : IEnumerable<T> where T : class, IGood
    {
        event Action<T> SpoiledSupply; // ok?
        double TotalMass { get; }
        double TotalPrice { get; }
        T this[int index] { get; set; }
        void Fill(IEnumerable<T> items);
        void DecreasePrice(double percent);
        void IncreasePrice(double percent);
        public IEnumerable<T> Except(IEnumerable<T> other);
        public IEnumerable<T> Intersect(IEnumerable<T> other);
        public IEnumerable<T> Union(IEnumerable<T> other);
    }
}
