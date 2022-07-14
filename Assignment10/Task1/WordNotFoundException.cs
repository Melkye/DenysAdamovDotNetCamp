namespace Task1
{
    internal class WordNotFoundException: Exception
    {
        public WordNotFoundException() : base()
        { }

        public WordNotFoundException(string message) : base(message)
        { }
    }
}
