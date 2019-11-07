using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CRM.Models
{
    public class Student : BaseEntity
    {
        public string Name { get; set; }

        public string Surname { get; set; } 

        public int ParentId { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
} 