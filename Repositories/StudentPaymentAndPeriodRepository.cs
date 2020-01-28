using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM.Repositories
{
    public class StudentPaymentAndPeriodRepository : BaseRepository<StudentPaymentAndPeriod>
    {
        public StudentPaymentAndPeriodRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<decimal> GetMustTotalByStudentIdAsync(int studentId)
        {
            var value = await DbSet.LastOrDefaultAsync(x => x.StudentId == studentId);
            return value.MustTotal;
        }

        public async Task<List<StudentPaymentAndPeriod>> GetAllPeriodAndPaymentAsync(int studentId)
        {
            return await DbSet.Where(s => s.StudentId == studentId).ToListAsync();
        }
    }
}