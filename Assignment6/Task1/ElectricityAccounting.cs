using System.Globalization;

namespace Task1
{
    public static class ElectricityAccounting
    {
        private static List<(int flatNumber, string name, int startNumber, int endNumber, DateOnly date)> _data
                = new List<(int, string, int, int, DateOnly)>();
        private static double _pricePerKW = 0;
        public static List<(int flatNumber, string name, int startNumber, int endNumber, DateOnly date)> Data 
        {
            get => new List<(int flatNumber, string name, int startNumber, int endNumber, DateOnly date)>(_data);
            private set => _data = value;
        }
        public static double PricePerKW
        {
            get => _pricePerKW;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                else
                {
                    _pricePerKW = value;
                }
            }
        }
        public static void ReadFromFile(string filename) // requires exception handling 
        {
            List<(int flatNumber, string name, int startNumber, int endNumber, DateOnly date)> newData
                = new List<(int, string, int, int, DateOnly)>();

            StreamReader sr = new StreamReader(filename);
            int numberOfRecords = int.Parse(sr.ReadLine().Trim());
            for (int i = 0; i < numberOfRecords; i++)
            {
                string[] flatInfo = sr.ReadLine().Trim().Split();
                int flatNumber = int.Parse(flatInfo[0]);
                string name = flatInfo[1];
                int startNumber = int.Parse(flatInfo[2]);
                int endNumber = int.Parse(flatInfo[3]);
                DateOnly date = DateOnly.Parse(flatInfo[4], CultureInfo.GetCultureInfo("uk-UA").DateTimeFormat);
                newData.Add((flatNumber, name, startNumber, endNumber, date));
            }
            Data = newData;
            sr.Close();
        }

        public static void WriteQuarterToFile(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            string header = $"{"Flat Number",-15}|{"Owner",-15}|{"Start number",-15}|{"End number",-15}|{"Month",-10}|{"Price",-10}";
            sw.WriteLine(header);
            foreach ((int flatNumber, string name, int startNumber, int endNumber, DateOnly date) in Data)
            {
                double price = GetTotalPrice(startNumber, endNumber);
                string month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);
                string flatLine = $"{flatNumber,-15}|{name,-15}|{startNumber,-15}" +
                    $"|{endNumber,-15}|{month,-10}|{price,-10}";
                sw.WriteLine(flatLine);
            }
            sw.Close();
        }
        public static void WriteDebtsToFile(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            string header = $"{"Owner",-15}|{"Total debt",-15}|{"Days since measurenent", -15}";
            sw.WriteLine(header);
            Dictionary<string, (double total, int daysSinceMeasurement)> debts = GetAllDebts();
            foreach (KeyValuePair<string, (double total, int daysSinceMeasurement)> debt in debts)
            {
                string debtLine = $"{debt.Key,-15}|{debt.Value.total,-15}|{debt.Value.daysSinceMeasurement,-15}";
                sw.WriteLine(debtLine);
            }
            sw.Close();
        }
        public static void WriteOneFlatQuarterToFile(int flatNumber, string filename)
        {
            (int flatNumber, string name, int startNumber, int endNumber, DateOnly date) flatInfo = Data.First(record => record.flatNumber == flatNumber);

            StreamWriter sw = new StreamWriter(filename);
            string header = $"{"Flat Number",-15}|{"Owner",-15}|{"Start number",-15}|{"End number",-15}|{"Month",-10}|{"Price",-10}";
            sw.WriteLine(header);

            double price = GetTotalPrice(flatInfo.startNumber, flatInfo.endNumber);
            string flatLine = $"{flatNumber:6}|{flatInfo.name:10}|{flatInfo.startNumber:15}" +
                $"|{flatInfo.endNumber:15}|{flatInfo.date: 10}|{price:10}";
            sw.WriteLine(flatLine);

            sw.Close();
        }

        public static double GetTotalPrice(int startNumber, int endNumber)
        {
            return PricePerKW * (endNumber - startNumber);
        }
        public static Dictionary<string, (double, int)> GetAllDebts()
        {
            Dictionary<string, (double total, int daysSinceMeasurement)> debts = new Dictionary<string, (double, int)>();
            foreach ((int flatNumber, string name, int startNumber, int endNumber, DateOnly date) flatInfo in Data)
            {
                double price = GetTotalPrice(flatInfo.startNumber, flatInfo.endNumber);
                if (debts.ContainsKey(flatInfo.name))
                {
                    double total = debts[flatInfo.name].total + price;

                    int currentDaysSinceMeasurement = debts[flatInfo.name].daysSinceMeasurement;
                    int daysSinceThisMeasurement = DateOnly.FromDateTime(DateTime.Now).DayNumber - flatInfo.date.DayNumber;
                    if (daysSinceThisMeasurement < currentDaysSinceMeasurement)
                    {
                        debts[flatInfo.name] = (total, daysSinceThisMeasurement);
                    }
                    else
                    {
                        debts[flatInfo.name] = (total, currentDaysSinceMeasurement);
                    }
                }
                else
                {
                    int daysSinceMeasurement = DateOnly.FromDateTime(DateTime.Now).DayNumber - flatInfo.date.DayNumber;
                    debts[flatInfo.name] = (price, daysSinceMeasurement);
                }

            }
            return debts;
        }
        public static string GetLargestDebtorName()
        {
            Dictionary<string, (double total, int daysSinceMeasurement)> debts = GetAllDebts();
            string largestDebtorName = "";
            double largestDebtorDebt = 0;
            foreach (KeyValuePair<string, (double total, int daysSinceMeasurement)> debtorInfo in debts)
            { 
                if (debtorInfo.Value.total > largestDebtorDebt)
                {
                    largestDebtorDebt = debtorInfo.Value.total;
                    largestDebtorName = debtorInfo.Key;
                }
            }
            return largestDebtorName;
        }

        public static int GetZeroConsumptionFlatNumber()
        {
            Dictionary<int, int> flatQuarterlyConsumption = new Dictionary<int, int>();
            foreach ((int flatNumber, string name, int startNumber, int endNumber, DateOnly date) in Data)
            {
                int flatMonthlyConsumption = endNumber - startNumber;
                if (flatQuarterlyConsumption.ContainsKey(flatNumber))
                {
                    flatQuarterlyConsumption[flatNumber] += flatMonthlyConsumption;
                }
                else
                {
                    flatQuarterlyConsumption[flatNumber] = flatMonthlyConsumption;
                }
            }
            int zeroConsumptionFlatNumber = -1;
            KeyValuePair<int, int> zeroConsumptionFlat = flatQuarterlyConsumption.FirstOrDefault(record => record.Value == 0);
            if (!zeroConsumptionFlat.Equals(default(KeyValuePair<int, int>)))
            {
                zeroConsumptionFlatNumber = zeroConsumptionFlat.Key;
            }
            return zeroConsumptionFlatNumber;
        }
    }


}
