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
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "Отчество")]
        public string FatherName { get; set; }
        public int? LevelId { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirthday { get; set; }
        [Display(Name = "Дата пробного урока")]
        public DateTime TrialDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ChangeStatusDate { get; set; }
        [Display(Name = "Имя родителя")]
        public string ParentName { get; set; }
        [Display(Name = "Фамилия родителя")]
        public string ParentLastName { get; set; }
        [Display(Name = "Отчество родителя")]
        public string ParentFatherName { get; set; }
        [Required(ErrorMessage = "Укажите номер телефона")]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Коментарии")]
        public List<Comment> Comments { get; set; }
        public int? GroupId { get; set; }
        [Display(Name = "Статус")]
        public StudentStatusEnum Status { get; set; }
        [Display(Name = "Название уровня")]
        public Level Level { get; set; }
        [Display(Name = "Название группы")]
        public Group Group { get; set; }
        public List<Attendance> Attendances { get; set; }
        public List<StudentPaymentAndPeriod> StudentPaymentAndPeriods { get; set; }
        [NotMapped]
        public IStatusStudent IStatusStudent { get; set; }
        public List<Payment> Payments { get; set; }
        [Display(Name = "Дата начало обучения")]
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
