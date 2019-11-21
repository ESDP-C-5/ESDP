using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CRM.Models
{
    public class Group : BaseEntity
    {
        public ApplicationUser User { get; set; }
        public string Name { get; set; }
        //public string Adress { get; set; }
        public string UserId { get; set; }
        public int BranchId { get; set; }
        public int LevelId { get; set; }
        public int TimeTableId { get; set; }
        public Branch Branch { get; set; }
        public Level Level { get; set; }
        public TimeTable TimeTable { get; set; }
    }
}
