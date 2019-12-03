using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class TimeTable : BaseEntity
    {
        public DayOfWeek Day1 { get; set; }
        public DayOfWeek Day2 { get; set; }
        public string Time { get; set; }
    }
}
