using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class BranchesWithStudentsViewModel
    {
        public List<Branch> branches { get; set; }
        public List<Student> students { get; set; }
        
    }
}
