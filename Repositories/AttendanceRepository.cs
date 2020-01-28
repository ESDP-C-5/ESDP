using CRM.Data;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public class AttendanceRepository : BaseRepository<Attendance>
    {
        public AttendanceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task CreateRangeAsync(List<Attendance> attendances)
        {
            await DbSet.AddRangeAsync(attendances);
        }


    }
}
