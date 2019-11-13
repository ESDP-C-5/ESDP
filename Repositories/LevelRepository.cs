using CRM.Data;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public class LevelRepository : BaseRepository<Level>
    {
        public LevelRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
