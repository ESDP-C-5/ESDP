using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CRM.Models
{
    public class Groupe : BaseEntity
    {
        private ApplicationUser User { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string UserId { get; set; }
        //4thyyyyyyh
        public  int BranchId { get; set; }
        public int LevelId { get; set; }
    }
}
