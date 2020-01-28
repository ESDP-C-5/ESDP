using CRM.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CRM.Strategy;

namespace CRM.Models
{
    public class Student : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public int? LevelId { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirthday { get; set; }
        public DateTime TrialDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ChangeStatusDate { get; set; }
        public string ParentName { get; set; }
        public string ParentLastName { get; set; }
        public string ParentFatherName { get; set; }
        [Required(ErrorMessage = "Укажите номер телефона")]
        public string PhoneNumber { get; set; }
        public List<Comment> Comments { get; set; }
        public int? GroupId { get; set; }
        public StudentStatusEnum Status { get; set; }
        public Level Level { get; set; }
        public Group Group { get; set; }
        public List<Attendance> Attendances { get; set; }
        public List<StudentPaymentAndPeriod> StudentPaymentAndPeriods { get; set; }
        [NotMapped]
        public IStatusStudent IStatusStudent { get; set; }
        public List<Payment> Payments { get; set; }
        public DateTime DataStartStudying { get; set; }
        public DateTime DataEndStudying { get; set; }
        public Student()
        {
            CreatedDate = DateTime.Now;
            StudentPaymentAndPeriods = new List<StudentPaymentAndPeriod>();
            Comments = new List<Comment>();
        }
    }
}
