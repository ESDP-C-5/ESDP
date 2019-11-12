using CRM.Data;
using CRM.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.UoW
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public GroupRepository Groups;
        public StudentRepository Student;
        public BranchRepository Branchs;
        public LevelRepository Levels;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Groups = new GroupRepository(context);
            Student = new StudentRepository(context);
            Branchs = new BranchRepository(context);
            Levels = new LevelRepository(context);

        }


        public Task<int> CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
