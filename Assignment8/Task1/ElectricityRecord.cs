namespace Task1
{
    public class ElectricityRecord
    {
        public ElectricityRecord(int flatNumber, string ownerName, int startNumber, int endNumber, DateOnly date)
        {
            FlatNumber = flatNumber;
            OwnerName = ownerName;
            StartNumber = startNumber;
            EndNumber = endNumber;
            Date = date;
        }
        public ElectricityRecord(int flatNumber, string ownerName, int startNumber, int endNumber, DateTime date)
            : this(flatNumber, ownerName, startNumber, endNumber, DateOnly.FromDateTime(date))
        {  }
        public int FlatNumber { get; }
        public string OwnerName { get; }
        public int StartNumber { get; }
        public int EndNumber { get; }
        public DateOnly Date { get; }
        public override bool Equals(object? obj)
        {
            if (obj is ElectricityRecord compared)
            {
                return compared.FlatNumber.Equals(FlatNumber) && compared.OwnerName.Equals(OwnerName);
            }
            else
            {
                return false;
            }
        }
        public override string ToString()
        {
            return $"{FlatNumber,-15}|{OwnerName,-15}|{StartNumber,-15}|{EndNumber,-15}|{Date, -15}";
        }
    }
}
