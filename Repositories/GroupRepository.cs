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

        public override async Task<Group> GetByIdAsync(int id)
        {
            return await DbSet.Include(g => g.Branch)
                    .Include(g => g.User)
                    .Include(g =>g.TimeTable)
                    .SingleOrDefaultAsync(g => g.Id == id)
            ;
        }

        public override async Task<List<Group>> GetAllAsync()
        {
            return DbSet
                .Include(g => g.Branch)
                .Include(g => g.TimeTable)
                .Include(g => g.User)
                .ToList();
        }

    }
}