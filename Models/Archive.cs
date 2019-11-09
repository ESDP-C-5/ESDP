using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Archive : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime DataStart { get; set; }
        public int GroupId { get; set; }
        public string Status { get; set; }
    }
}
