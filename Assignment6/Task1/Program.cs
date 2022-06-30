using Task1;

try
{
    ElectricityAccounting.ReadFromFile("ElectricityInput.txt");
    ElectricityAccounting.PricePerKW = 7.9;
    ElectricityAccounting.WriteQuarterToFile("ElectricityQuaterInfo.txt");
    ElectricityAccounting.WriteDebtsToFile("ElectricityDebtsInfo.txt");
    string largetDebtor = ElectricityAccounting.GetLargestDebtorName();
    int zeroConsumptionFlatNUmber = ElectricityAccounting.GetZeroConsumptionFlatNumber();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

