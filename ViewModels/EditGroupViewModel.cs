using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.ViewModels
{
    public class EditGroupViewModel
    {
        public int Id { get; set; }
        public SelectList Users { get; set; }
        public SelectList Branches { get; set; }
        public SelectList Levels { get; set; }
        public int BranchId { get; set; }
        public int LevelId { get; set; }
        public string UserId { get; set; }

        public string Name { get; set; }
        public string Adress { get; set; }
    }
}
