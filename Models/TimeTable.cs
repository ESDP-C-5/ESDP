using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class TimeTable : BaseEntity
    {
        public DateTime DateStarT { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
