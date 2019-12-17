using CRM.Data;
using CRM.Models;

namespace CRM.Repositories
{
    public class JournalRepository : BaseRepository<Journal>
    {
        public JournalRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}