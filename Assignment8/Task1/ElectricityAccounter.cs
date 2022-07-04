using System.Globalization;

namespace Task1
{
    public class ElectricityAccounter
    {
        private List<ElectricityRecord> _data;
        private double _pricePerKW;
        public ElectricityAccounter() : this(new List<ElectricityRecord>(), 10.0)
        {  }
        public ElectricityAccounter(List<ElectricityRecord> data, double pricePerKW)
        {
            Data = data is not null ? data : new();
            PricePerKW = pricePerKW;
        }
        public List<ElectricityRecord> Data 
        {
            get => new(_data);
            private set => _data = value;
        }
        public double PricePerKW
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
        public void ReadFromFile(string filename)
        {
            List<ElectricityRecord> newData = new();

            StreamReader sr = new(filename);
            int numberOfRecords = int.Parse(sr.ReadLine().Trim());
            for (int i = 0; i < numberOfRecords; i++)
            {
                string[] flatInfo = sr.ReadLine().Trim().Split();
                int flatNumber = int.Parse(flatInfo[0]);
                string name = flatInfo[1];
                int startNumber = int.Parse(flatInfo[2]);
                int endNumber = int.Parse(flatInfo[3]);
                DateOnly date = DateOnly.Parse(flatInfo[4], CultureInfo.GetCultureInfo("uk-UA").DateTimeFormat);
                newData.Add(new ElectricityRecord(flatNumber, name, startNumber, endNumber, date));
            }
            Data = newData;
            sr.Close();
        }

        public void WriteQuarterToFile(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            string header = $"{"Flat Number",-15}|{"Owner",-15}|{"Start number",-15}|{"End number",-15}|{"Month",-10}|{"Price",-10}";
            sw.WriteLine(header);
            foreach (ElectricityRecord record in Data)
            {
                double price = GetTotalPrice(record.StartNumber, record.EndNumber);
                string month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(record.Date.Month);
                string flatLine = record.ToString() + $"|{month,-10}|{price,-10}";
                sw.WriteLine(flatLine);
            }
            sw.Close();
        }
        public void WriteDebtsToFile(string filename)
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
        public void WriteOneFlatQuarterToFile(int flatNumber, string filename)
        {
            ElectricityRecord record = Data.First(record => record.FlatNumber == flatNumber);

            StreamWriter sw = new StreamWriter(filename);
            string header = $"{"Flat Number",-15}|{"Owner",-15}|{"Start number",-15}|{"End number",-15}|{"Month",-10}|{"Price",-10}";
            sw.WriteLine(header);

            double price = GetTotalPrice(record.StartNumber, record.EndNumber);
            string flatLine = record.ToString() + $"|{price:10}";
            sw.WriteLine(flatLine);

            sw.Close();
        }

        public double GetTotalPrice(int startNumber, int endNumber)
        {
            return PricePerKW * (endNumber - startNumber);
        }
        public Dictionary<string, (double, int)> GetAllDebts()
        {
            Dictionary<string, (double total, int daysSinceMeasurement)> debts = new Dictionary<string, (double, int)>();
            foreach (ElectricityRecord record in Data)
            {
                double price = GetTotalPrice(record.StartNumber, record.EndNumber);
                if (debts.ContainsKey(record.OwnerName))
                {
                    double total = debts[record.OwnerName].total + price;

                    int currentDaysSinceMeasurement = debts[record.OwnerName].daysSinceMeasurement;
                    int daysSinceThisMeasurement = DateOnly.FromDateTime(DateTime.Now).DayNumber - record.Date.DayNumber;
                    if (daysSinceThisMeasurement < currentDaysSinceMeasurement)
                    {
                        debts[record.OwnerName] = (total, daysSinceThisMeasurement);
                    }
                    else
                    {
                        debts[record.OwnerName] = (total, currentDaysSinceMeasurement);
                    }
                }
                else
                {
                    int daysSinceMeasurement = DateOnly.FromDateTime(DateTime.Now).DayNumber - record.Date.DayNumber;
                    debts[record.OwnerName] = (price, daysSinceMeasurement);
                }

            }
            return debts;
        }
        public string GetLargestDebtorName()
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

        public int GetZeroConsumptionFlatNumber()
        {
            Dictionary<int, int> flatQuarterlyConsumption = new Dictionary<int, int>();
            foreach (ElectricityRecord record in Data)
            {
                int flatMonthlyConsumption = record.EndNumber - record.StartNumber;
                if (flatQuarterlyConsumption.ContainsKey(record.FlatNumber))
                {
                    flatQuarterlyConsumption[record.FlatNumber] += flatMonthlyConsumption;
                }
                else
                {
                    flatQuarterlyConsumption[record.FlatNumber] = flatMonthlyConsumption;
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
        // considering here that every flat & owner pair appears only once in the _data
        public static ElectricityAccounter operator +(ElectricityAccounter left, ElectricityAccounter right)
        {
            HashSet<ElectricityRecord> dataSet = new(left.Data);
            List<ElectricityRecord> unitedData = new(dataSet.Union(right.Data, new ElectricityRecordEqualityComparer()));
            return new ElectricityAccounter(unitedData, left.PricePerKW);
        }
        public static ElectricityAccounter operator -(ElectricityAccounter left, ElectricityAccounter right)
        {
            HashSet<ElectricityRecord> dataSet = new(left.Data);
            List<ElectricityRecord> exceptData = new(dataSet.Except(right.Data, new ElectricityRecordEqualityComparer()));
            return new ElectricityAccounter(exceptData, left.PricePerKW);
        }
    }


}
