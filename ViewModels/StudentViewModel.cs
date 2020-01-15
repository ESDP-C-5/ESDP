using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Models;

namespace CRM.ViewModels
{
    public class StudentViewModel : Student
    {
        public string Comment { get; set; }
    }
}
