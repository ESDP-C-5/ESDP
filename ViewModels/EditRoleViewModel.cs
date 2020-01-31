using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }
        public string Id { get; set; }
        [Display(Name = "Название роли")]
        [Required(ErrorMessage = "Название роли не может быть пустым")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
