using CRM.Data;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public class TimeTableRepository : BaseRepository<TimeTable>
    {
        public TimeTableRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<TimeTable> GetIdTimeTable(TimeTable timeTable)
        {
            TimeTable result = DbSet.FirstOrDefault(
                t => t.Day1 == timeTable.Day1 && t.Day2 == timeTable.Day2 && t.Time == timeTable.Time);
            return result;
        }
    }
}
