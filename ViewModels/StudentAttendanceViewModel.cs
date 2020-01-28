using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class StudentAttendanceViewModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<Attendance> StudentAttendances { get; set; }
    }
}
