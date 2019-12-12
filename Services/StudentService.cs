﻿using CRM.Models;
using CRM.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services
{
    public class StudentService
    {

        private readonly UnitOfWork _unitOfWork;

        public StudentService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Student>> GetAllStudents()
        {
            var studentUow = _unitOfWork.Student;
            var student = await studentUow.GetAllAsync();

            return student;
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            var studentUow = _unitOfWork.Student;
            var students = await studentUow.GetByIdAsync(id);

            return students;
        }

        public async Task CreateAsync(Student student)
        {

            var studentUow = _unitOfWork.Student;
            await studentUow.CreateAsync(student);
            await _unitOfWork.CompleteAsync();
        }

        public async Task EditAsync(Student student)
        {
            var studentUow = _unitOfWork.Student;
            studentUow.UpdateAsync(student);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(Student student)
        {
            var studentUow = _unitOfWork.Student;
            studentUow.RemoveAsync(student);
            await _unitOfWork.CompleteAsync();
        }
        public async Task<List<Student>> GetAllStudentsByGroupIdAsync(int idGroup)
        {
            return await _unitOfWork.Student.GetAllStudentsByGroupIdAsync(idGroup);
        }

        public async Task<IEnumerable<Student>> SearchAsync(string value)
        {
            var students = await GetAllStudents();
            students = students.Where(x => x.Name.ToUpper().Contains(value.ToUpper())
                                           || x.PhoneNumber.ToUpper().Contains(value.ToUpper())
                                           || x.LastName.ToUpper().Contains(value.ToUpper())
                                           || x.ParentLastName.ToUpper().Contains(value.ToUpper())).ToList();
            return students;
        }

        public async Task<List<Student>> GetArchiveStudentsByBranchIdAsync(int BranchId, Helpers.StudentStatusEnum? status)
        {
            var groups = await _unitOfWork.Groups.GetIncludeStudentsByBranchIdAsync(BranchId);
            var students = new List<Student>();
            foreach (var g in groups)
            {
                students.AddRange(g.Students);
            }
            if (status != null)
            {
                students = students.Where(x => x.Status == status.Value).ToList();
            }
            return students;
        }
    }
}
