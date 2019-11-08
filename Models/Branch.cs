using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CRM.Models
{
    public class Branch : BaseEntity
    {
        public string Name { get; set; }

        public string Address { get; set; }
    }
}