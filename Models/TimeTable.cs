using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class TimeTable : BaseEntity
    {
        [Required]
        public DayOfWeek Day1 { get; set; }
        [Required]
        public DayOfWeek Day2 { get; set; }
        [RegularExpression(@"^(0[1-9]|1[0-9]|2[0-4]):[0-5][0-9]$",
                   ErrorMessage = "Invalid Time.")]
        public string Time { get; set; }
    }
}
