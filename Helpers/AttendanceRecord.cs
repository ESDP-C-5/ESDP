using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Helpers
{
    public enum AttendanceRecord
    {
        [Display(Name = "Не указано")]
        NotDefined,
        [Display(Name = "Был")]
        Attendended,
        [Display(Name = "Не был")]
        Absent
    }
}
