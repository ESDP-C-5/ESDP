using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public interface IRepository<T> where T : Models.BaseEntity
    {
        Task CreateAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        void UpdateAsync(T entity);
        void RemoveAsync(T entity);
    }
}
