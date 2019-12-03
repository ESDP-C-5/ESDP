using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.UoW
{
    public class CreateGroupViewModel
    {
        [Required]
        [Display (Name =  "Выберите преподавателя")]
        public SelectList Users { get; set; }
        [Required]
        [Display(Name = "Выберите филиал")]
        public SelectList Branches { get; set; }
        [Required]
        [Display(Name = "Выберите расписание")]
        public SelectList TimeTables { get; set; }

        public int BranchId { get; set; }
        
        public string UserId { get; set; }

        public int TimeTableId { get; set; }
    }
}
