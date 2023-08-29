using HrApi.Models;

namespace HrApi.Calculations
{
    public static class Calculate
    {
        public static bool IsPast(DateTime date)
        {
            DateTime currentDate = DateTime.Now.Date;
            return date < currentDate;
        }
        public static int CountWorkingDays(DateTime startDate, DateTime endDate, List<Holiday> listOfHolidays)
        {
            int workingDays = 0;
            TimeSpan totalDays = endDate - startDate;

            for (int i = 0; i <= totalDays.Days; i++)
            {
                DateTime currentDate = startDate.AddDays(i);
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    Console.WriteLine(currentDate.Date + "date");

                    bool isHoliday = listOfHolidays.Any(holiday => holiday.HolidayDate.Date == currentDate.Date);
                    if (!isHoliday)
                    {
                        Console.WriteLine("not a holiday");
                        workingDays++;
                    }
                    else
                    {
                        Console.WriteLine("holiday");
                    }

                }
            }

            return workingDays;
        }

        public static int NextHolidayIn(List<Holiday> listOfHolidays)
        {
            foreach (Holiday holiday in listOfHolidays)
            {
                DateTime currentDate = DateTime.Now.Date;
                DateTime holidayDate = holiday.HolidayDate.Date;
                if (currentDate < holidayDate)
                {
                    int NextHolidayIn = (holidayDate - currentDate).Days;
                    return NextHolidayIn;

                }
            }

            return -1;
        }

        public static List<DateTime> StartAndEndDayOfWeek()
        {
            DateTime today = DateTime.Today;
            DayOfWeek currentDayOfWeek = today.DayOfWeek;
            
            int daysUntilStartOfWeek = ((int)currentDayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            int daysUntilEndOfWeek = 6 - daysUntilStartOfWeek;

            DateTime startOfWeek = today.AddDays(-daysUntilStartOfWeek);
            DateTime endOfWeek = today.AddDays(daysUntilEndOfWeek);

            List<DateTime> startAndEndDayOfWeek = new List<DateTime>() {startOfWeek,endOfWeek};
            return startAndEndDayOfWeek;
        }
    }
}