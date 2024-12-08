using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTrackerLibrary.Models
{
    public class SumByCategoryName
    {
        public string Category { get; set; }
        public double Sum { get; set; }

        public SumByCategoryName(string category, double sum)
        {
            Category = category;
            Sum = sum;
        }
    }
}
