using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Models;

namespace CRM.Repositories
{
    public class CommentRepository : BaseRepository<Comment>
    {
        public CommentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
