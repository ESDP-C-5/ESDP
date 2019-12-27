using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Comment : BaseEntity
    {
        public int StudentId { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime Create { get; set; }
    }
}
