using System;
using System.Globalization;

namespace StockAnalyzer
{
    class Program
    {
        /// <summary>
        /// Get the day of the week...
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            DayOfWeek day = calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            return calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }



        static Calendar calendar = CultureInfo.InvariantCulture.Calendar;
        static void Main(string[] args)
        {
            DateOperating();
            TimeSpaning();
            WeekChecking();

            DateTimeOffset startOff = ISOWeekChecking();
            ContractChecker();

            Console.WriteLine(GetIso8601WeekOfYear(startOff.DateTime));
        }

        private static void ContractChecker()
        {
            var contractDate = new DateTimeOffset(2019, 7, 1, 0, 0, 0, TimeSpan.Zero);

            Console.WriteLine(contractDate);

            contractDate = ExtendContract(contractDate, 6);
            Console.WriteLine(contractDate);
            contractDate = contractDate.AddMonths(6).AddTicks(-1);

            Console.WriteLine(contractDate);
        }

        private static DateTimeOffset ISOWeekChecking()
        {
            var startOff = new DateTimeOffset(2007, 12, 31, 0, 0, 0, TimeSpan.Zero);

            var week = calendar.GetWeekOfYear(startOff.DateTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            Console.WriteLine(week);

            var isoWeek = ISOWeek.GetWeekOfYear(startOff.DateTime);

            Console.WriteLine(isoWeek);
            return startOff;
        }

        private static void WeekChecking()
        {
            var start = DateTimeOffset.UtcNow;

            var end = start.AddSeconds(180);

            TimeSpan difference = end - start;

            difference = difference.Multiply(2);

            Console.WriteLine(difference.TotalMinutes);

            
        }

        private static void TimeSpaning()
        {
            var timeSpan = new TimeSpan(60, 100, 200);

            Console.WriteLine(timeSpan.Days);
            Console.WriteLine(timeSpan.Hours);
            Console.WriteLine(timeSpan.Minutes);
            Console.WriteLine(timeSpan.Seconds);
        }

        private static void DateOperating()
        {
            string formattedDate = ParseDate();

            var date1 = "9/10/2019 10:00:00 PM +02:00";

            var parsedDate2 = DateTime.Parse(date1, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);

            Console.WriteLine(parsedDate2.Kind);

            Console.WriteLine(formattedDate);
        }

        private static string ParseDate()
        {
            var date = "9/10/2019 10:00:00 PM";

            var parsedDate =
                DateTimeOffset.ParseExact(date, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

            parsedDate = parsedDate.ToOffset(TimeSpan.FromHours(10));
            var formattedDate = parsedDate.ToString("o");

            Console.WriteLine($"\n{parsedDate}");
            return formattedDate;
        }

        public static DateTimeOffset ExtendContract(DateTimeOffset current, int months)
        {
            var newContractDate = current.AddMonths(months).AddTicks(-1);

            return new DateTimeOffset(newContractDate.Year, newContractDate.Month, DateTime.DaysInMonth(newContractDate.Year, newContractDate.Month),
                23, 59, 59, current.Offset);
        }
    }
}
