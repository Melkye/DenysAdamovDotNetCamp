using Task1;

try
{
    ElectricityAccounter accounter = new();
    accounter.ReadFromFile("ElectricityInput.txt");
    accounter.PricePerKW = 10.0;

    ElectricityAccounter accounter2 = new(
        new List<ElectricityRecord>() 
            { 
            new ElectricityRecord(10, "Les", 1887, 1937, DateOnly.Parse("02/25/1887")), 
            new ElectricityRecord(11, "Mykola", 1892, 1937, DateOnly.Parse("12/18/1892")) 
            },
        10.0
        );

    ElectricityAccounter accounterUnited = accounter + accounter2;
    accounterUnited.WriteDebtsToFile("AccounterUnitedDebts.txt");

    ElectricityAccounter accounter3 = new(
        new List<ElectricityRecord>()
            {
            new ElectricityRecord(1, "Taras", 1814, 1861, DateOnly.Parse("09/30/2021")),
            new ElectricityRecord(2, "Ivan", 1856, 1916, DateOnly.Parse("09/29/2021"))
            },
        10.0
        );

    ElectricityAccounter accounterExcept = accounter - accounter3;
    accounterExcept.WriteDebtsToFile("AccounterExceptDebts.txt");

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

