using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Level : BaseEntity
    {
        [Required(ErrorMessage = "Названия уровня не может быть пустым")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина не должна быть от 2 до 50 символов")]
        public string Name { get; set; }
    }
}
