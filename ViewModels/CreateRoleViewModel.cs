using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Название роли не может быть пустым")]
        [Display(Name = "Название роли")]
        public string RoleName { get; set; }
    }
}
