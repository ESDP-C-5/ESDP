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
        public DateTime Day { get; set; }
        public bool IsAttended { get; set; }

        [StringLength(300)]
        public string Comment { get; set; }
    }
}
