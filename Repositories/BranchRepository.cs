using CRM.Data;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Branch> GetByIdAsync(int id)
        {
            return await DbSet.Include(b => b.Groups)
                    .SingleOrDefaultAsync(g => g.Id == id);
        }
    }
}
