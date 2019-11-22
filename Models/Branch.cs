using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CRM.Models
{
    public class Branch : BaseEntity
    {
        [Required(ErrorMessage = "Поле названия не может быть пустым")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле адреса не может быть пустым")]
        public string Address { get; set; }
    }
}