﻿using CRM.Data;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;

namespace CRM.Repositories
{
    public class StudentRepository : BaseRepository<Student>
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await DbSet.Include(s => s.Level)
                    .Include(s => s.Group)
                    .Include(s=>s.Comments)
                    .SingleOrDefaultAsync(g => g.Id == id)
            ;
        }
        public async Task<List<Student>> GetAllStudentsByGroupIdAsync(int idGroup)
        {
            return await DbSet.Where(s => s.GroupId == idGroup && 
                                          (s.Status == StudentStatusEnum.studying || 
                                          s.Status == StudentStatusEnum.trial)).ToListAsync();
        }

        internal async Task<List<Student>> SelectLeadStudentsAsync()
        {
            return await DbSet.Where(s => s.Status == StudentStatusEnum.interested).ToListAsync();
        }

        public async Task<List<Student>> SelectStudyingStudentsAsync()
        {
            return await DbSet.Where(s => s.Status == StudentStatusEnum.studying).ToListAsync();
        }

        public async Task<List<Student>> GetAllStudentsByArchiveAsync()
        {
            return await DbSet.Where(s => s.Status == StudentStatusEnum.archive).ToListAsync();
        }

        public async Task<List<Student>> GetStudentsByGroupeIdByStudentStatusAsync(int groupeId)
        {
            return await DbSet.Where(s =>
                    s.GroupId == groupeId && (s.Status == StudentStatusEnum.studying ||
                    s.Status == StudentStatusEnum.trial))
                .ToListAsync();
        }

        public async Task<List<Student>> GetAllStudentsByBranchIdAsync(int branchId)
        {
            return await DbSet
                .Include(s => s.Group)
                .Where(s => s.Group.BranchId == branchId && s.Status != StudentStatusEnum.interested).ToListAsync();
        }
        public StudentStatusEnum GetStudentStatusByStudentId(int studentId)
        {
            var student = DbSet.FirstOrDefault(s => s.Id == studentId);
            return student.Status;
        }

        public async Task<Student> GetByIdForCardStudent(int id)
        {
            return await DbSet
                .Include(s => s.Payments)
                .Include(s => s.StudentPaymentAndPeriods)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}