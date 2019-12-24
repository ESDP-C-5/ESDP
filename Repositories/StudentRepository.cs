using CRM.Data;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;

namespace CRM.Repositories
{
    public class StudentRepository : BaseRepository<Student>
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await DbSet.Include(s => s.Level)
                    .Include(s => s.Group)
                    .SingleOrDefaultAsync(g => g.Id == id)
            ;
        }
        public async Task<List<Student>> GetAllStudentsByGroupIdAsync(int idGroup)
        {
            return await DbSet.Where(s => s.GroupId == idGroup).ToListAsync();
        }

        internal async Task<List<Student>> SelectLeadStudentsAsync()
        {
            return await DbSet.Where(s => s.Status == StudentStatusEnum.interested).ToListAsync();
        }

        public async Task<List<Student>> SelectStudyingStudentsAsync()
        {
            return await DbSet.Where(s => s.Status == StudentStatusEnum.studying).ToListAsync();
        }
    }
}