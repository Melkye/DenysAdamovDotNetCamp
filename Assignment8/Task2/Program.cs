using Task2;

try
{
    AttendanceAnalizer analizer = new("Attendances.txt");
    int num = analizer.GetNumberOfVisits("");
    DayOfWeek dayOfWeek = analizer.GetHighestVisitedDay("127.0.0.1");
    TimeOnly highestVisitedUserTime = analizer.GetHighestVisitedHourStart("127.0.0.1");
    TimeOnly highestVisitedTime = analizer.GetHighestVisitedHourStart();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}