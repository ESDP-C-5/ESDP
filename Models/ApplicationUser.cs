using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } 

        public string Surname {get;set;}

        public int Phone { get; set;}

        public int ParentId { get; set;}

        public DateTime DateOfBirth { get; set;}
    }
}
