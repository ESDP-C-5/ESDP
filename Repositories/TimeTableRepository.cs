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
    }
}
