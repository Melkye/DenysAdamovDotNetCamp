namespace Assignment9
{
    internal class CurrencyExchanger
    {
        private FileWorker _fileWorker;
        private Dictionary<Currency, double> _exchangeRates;
        public CurrencyExchanger(FileWorker fileWorker)
        {
            _fileWorker = fileWorker;
            ReadFromFile();
        }
        public CurrencyExchanger(CurrencyExchanger copyExchanger)
        {
            _fileWorker = new(copyExchanger._fileWorker);
            _exchangeRates = new(copyExchanger._exchangeRates);
        }
        public void ReadFromFile()
        {
            _exchangeRates = _fileWorker.ReadExchangeRatesFromFile();
        }
        public double ExchangeUAH(double amount, Currency targetCurrency)
        {
            return amount / _exchangeRates[targetCurrency];
        }
    }
}