using CRM.Data;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected DbSet<T> DbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        public async Task CreateAsync(T entity)
        {
            await DbSet.AddAsync(entity);
        }

        public Task<List<T>> GetAllAsync()
        {
            return DbSet.ToListAsync();
        }

        public virtual Task<T> GetByIdAsync(int id)
        {
            return DbSet.FindAsync(id);
        }

        public void RemoveAsync(T entity)
        {
            DbSet.Remove(entity);
        }

        public void UpdateAsync(T entity)
        {
            DbSet.Update(entity);
        }
    }
}
