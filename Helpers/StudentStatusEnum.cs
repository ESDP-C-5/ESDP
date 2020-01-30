using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Helpers
{
    public enum StudentStatusEnum
    {
            [Display(Name = "Учится")]
            studying = 1,
            [Display(Name = "Архив")]
            archive,
            [Display(Name = "Интереуется")]
            interested,
            [Display(Name = "Пробный")]
            trial 
    }
}
