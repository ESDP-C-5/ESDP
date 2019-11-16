using CRM.Data;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public class GroupRepository : BaseRepository<Group>
    {
        public GroupRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Group> GetByIdAsync(int id)
        {
            return await DbSet.Include(g => g.Branch)
                    .Include(g => g.Level)
                    .Include(g => g.User)
                    .SingleOrDefaultAsync(g => g.Id == id)
            ;
        }
    }
}