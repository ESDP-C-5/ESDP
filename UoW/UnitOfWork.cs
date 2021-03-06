﻿using CRM.Data;
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
        public TimeTableRepository TimeTables;
        public AttendanceRepository Attendances;
        public PaymentRepository Payments;
        public StudentPaymentAndPeriodRepository StudentPaymentAndPeriods;
        public CommentRepository Comments;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Payments = new PaymentRepository(context);
            StudentPaymentAndPeriods = new StudentPaymentAndPeriodRepository(context);
            Groups = new GroupRepository(context);
            Student = new StudentRepository(context);
            Branchs = new BranchRepository(context);
            Levels = new LevelRepository(context);
            TimeTables = new TimeTableRepository(context);
            Attendances = new AttendanceRepository(context);
            Comments = new CommentRepository(context);
        }


        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
