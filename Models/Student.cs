using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Student : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public int LevelId { get; set; }
        public DateTime DateOfBirthday { get; set; }
        public DateTime TrialDate { get; set; }
        public string ParentName { get; set; }
        public string ParentLastName { get; set; }
        public string ParentFatherName { get; set; }
        public string PhoneNumber { get; set; }
        public string Comment { get; set; }
        public int GroupId { get; set; }
        public Level Level { get; set; }
        public Group Group { get; set; }
    }
}
