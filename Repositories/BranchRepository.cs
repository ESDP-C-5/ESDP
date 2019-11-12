using CRM.Data;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public class BranchRepository : BaseRepository<Branch>
    {
        public BranchRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
