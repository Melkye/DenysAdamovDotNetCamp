namespace Assignment9
{
    internal class CurrencyExchanger
    {
        private Dictionary<Currency, double> _exchangeRates;
        public CurrencyExchanger(Dictionary<Currency, double> exchangeRates)
        {
            _exchangeRates = new(exchangeRates);
        }
        public CurrencyExchanger(CurrencyExchanger copyExchanger)
        {
            _exchangeRates = new(copyExchanger._exchangeRates);
        }
        public double ExchangeUAH(double amount, Currency targetCurrency)
        {
            return amount / _exchangeRates[targetCurrency];
        }
    }
}