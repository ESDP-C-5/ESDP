using CRM.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Student : BaseEntity
    {
        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина имени должна быть от 2 до 50 символов")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указана фамилия")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина фамилии должна быть от 2 до 50 символов")]
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public int? LevelId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirthday { get; set; }
        public DateTime TrialDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ChangeStatusDate { get; set; }
        [Required(ErrorMessage = "Не указано имя")]
        public string ParentName { get; set; }
        [Required(ErrorMessage = "Не указана фамилия")]
        public string ParentLastName { get; set; }
        public string ParentFatherName { get; set; }
        [Required(ErrorMessage = "Укажите номер телефона")]
        public string PhoneNumber { get; set; }
        public string Comment { get; set; }
        public int? GroupId { get; set; }
        public StudentStatusEnum Status { get; set; }
        public Level Level { get; set; }
        public Group Group { get; set; }

        public Student()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
