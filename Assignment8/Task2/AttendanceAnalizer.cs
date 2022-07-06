namespace Task2
{
    public class AttendanceAnalizer
    {
        private Dictionary<string, Dictionary<DayOfWeek, List<TimeOnly>>> _attendances;

        /// <exception cref="FileNotFoundException"></exception>
        public AttendanceAnalizer(string filename)
        {
            FillFromFile(filename);
        }
        /// <summary>
        /// Fills the data from the specified file
        /// </summary>
        /// <exception cref="FileNotFoundException"></exception>
        public void FillFromFile(string filename) // assumming that whole file is written correctly
        {
            Dictionary<string, Dictionary<DayOfWeek, List<TimeOnly>>> attendances = new();
            using (StreamReader sr = new(filename)) // implicitly thows exception - is it ok or better when it is explicitly?
            {
                while (!sr.EndOfStream)
                {
                    string[] visitInfo = sr.ReadLine().Trim().Split();
                    string ipAddress = visitInfo[0];
                    TimeOnly time = TimeOnly.Parse(visitInfo[1]);
                    Enum.TryParse(visitInfo[2], true, out DayOfWeek day);

                    if (attendances.ContainsKey(ipAddress))
                    {
                        if (!attendances[ipAddress].ContainsKey(day))
                        {
                            attendances[ipAddress][day] = new List<TimeOnly>();
                        }
                        attendances[ipAddress][day].Add(time);
                    }
                    else
                    {
                        Dictionary<DayOfWeek, List<TimeOnly>> ipVisits = new();
                        ipVisits[day] = new List<TimeOnly>() { time };
                        attendances[ipAddress] = ipVisits;
                    }
                }
            }
            _attendances = attendances;

        }
        public int GetNumberOfVisits(string ipAddress)
        {
            int count = 0;
            if (_attendances.ContainsKey(ipAddress))
            {
                count = _attendances[ipAddress].Count;
            }
            return count;
        }
        public DayOfWeek GetHighestVisitedDay(string ipAddress)
        {
            if (_attendances.ContainsKey(ipAddress))
            {
                DayOfWeek highestVisitedDay = (DayOfWeek)(-1);
                Dictionary<DayOfWeek, List<TimeOnly>> userVisits = _attendances[ipAddress];
                int visitsCount = 0;
                foreach (KeyValuePair<DayOfWeek, List<TimeOnly>> userDayVisits in userVisits)
                {
                    if (userDayVisits.Value.Count > visitsCount)
                    {
                        visitsCount = userDayVisits.Value.Count;
                        highestVisitedDay = userDayVisits.Key;
                    }
                }
                return highestVisitedDay;
            }
            else
            {
                throw new NullReferenceException("Ip not found"); // better write own specifit exception
            }
        }
        /// <summary>
        /// Searches the hour with highest number of visits of the specified user
        /// </summary>
        /// <returns>The start of the hour</returns>
        /// <exception cref="NullReferenceException"></exception>
        public TimeOnly GetHighestVisitedHourStart(string ipAddress)
        {
            if (_attendances.ContainsKey(ipAddress))
            {
                Dictionary<DayOfWeek, List<TimeOnly>> ipVisits = _attendances[ipAddress];
                Dictionary<TimeOnly, int> ipVisitsPerHour = new();
                foreach(KeyValuePair<DayOfWeek, List<TimeOnly>> ipDayVisits in ipVisits)
                {
                    foreach(TimeOnly visitTime in ipDayVisits.Value)
                    {
                        TimeOnly hourStart = new TimeOnly(visitTime.Hour, 0);
                        if (ipVisitsPerHour.ContainsKey(hourStart))
                        {
                            ipVisitsPerHour[hourStart]++;
                        }
                        else
                        {
                            ipVisitsPerHour[hourStart] = 1;
                        }
                    }
                }
                return ipVisitsPerHour.MaxBy(v => v.Value).Key;
            }
            else
            {
                throw new NullReferenceException("Ip not found");
            }
        }
        /// <summary>
        /// Searches the hour with highest number of visits of all users
        /// </summary>
        /// <returns>The start of the hour</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public TimeOnly GetHighestVisitedHourStart()
        {
            if (_attendances.Any())
            {
                Dictionary<TimeOnly, int> visitsPerHour = new();
                foreach (Dictionary<DayOfWeek, List<TimeOnly>> ipVisits in _attendances.Values)
                {
                    foreach (KeyValuePair<DayOfWeek, List<TimeOnly>> ipDayVisits in ipVisits)
                    {
                        foreach (TimeOnly visitTime in ipDayVisits.Value)
                        {
                            TimeOnly hourStart = new TimeOnly(visitTime.Hour, 0);
                            if (visitsPerHour.ContainsKey(hourStart))
                            {
                                visitsPerHour[hourStart]++;
                            }
                            else
                            {
                                visitsPerHour[hourStart] = 1;
                            }
                        }
                    }
                }
                return visitsPerHour.MaxBy(v => v.Value).Key;
            }

            else
            {
                throw new InvalidOperationException("Unable to find interval when number of visits is 0");
            }
        }
    }
}