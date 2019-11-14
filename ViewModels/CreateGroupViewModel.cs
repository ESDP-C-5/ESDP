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
        public SelectList Users { get; set; }
        [Required]
        public SelectList Branches { get; set; }
        [Required]
        public SelectList Levels { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Adress { get; set; }
        public int BranchId { get; set; }
        public int LevelId { get; set; }
        public string UserId { get; set; }
    }
}
