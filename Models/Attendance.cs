using CRM.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Attendance : BaseEntity
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int Day { get; set; }
        public AttendanceRecord IsAttended { get; set; }
        public Month Month { get; set; }

        [StringLength(300)]
        public string Comment { get; set; }
    }
}
