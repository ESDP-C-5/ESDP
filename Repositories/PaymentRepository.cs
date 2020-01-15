using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM.Repositories
{
    public class PaymentRepository: BaseRepository<Payment>
    {
        public PaymentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Payment>> GetAllPaymentByStudentIdAsync(int studentId)
        {
            return await DbSet.Where(p => p.StudentId == studentId).ToListAsync();
        }

        public async Task<DateTime?> GetLastPaymentByStudentIdAsync(int studentId)
        {
            var lastPayment = await DbSet.LastOrDefaultAsync(p => p.StudentId == studentId);
            return lastPayment?.DateTimePayment;
        }

        public async Task<string> GetLastCommitByStudentIdAsync(int studentId)
        {
            var lastPayment = await DbSet.LastOrDefaultAsync(p => p.StudentId == studentId);
            return lastPayment?.Comment;
        }
    }
}