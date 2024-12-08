using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTrackerLibrary.Models
{
    public class SumByDayAndYear
    {
        public DayOfWeek SDay { get; set; }
        public int SYear { get; set; }
        public double Sum { get; set; }

        public SumByDayAndYear(DayOfWeek dayOfWeek, int dateTime, double sum)
        {
            SDay = dayOfWeek;
            SYear = dateTime;
            Sum = sum;
        }
    }
}
