using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Parent : BaseEntity
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Phone1 { get; set; }
        public int Phone2 { get; set; }
    }
}
