﻿using CRM.Models;
using CRM.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRM.Helpers;
using CRM.Strategy;
using CRM.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using CRM.ViewModels;
using AutoMapper;

namespace CRM.Services
{
    public class StudentService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly PaymentPeriodService _paymentPeriodService;

        public StudentService(UnitOfWork unitOfWork, PaymentPeriodService paymentPeriodService)
        {
            _unitOfWork = unitOfWork;
            _paymentPeriodService = paymentPeriodService;
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
            student.Status = StudentStatusEnum.interested;
            await studentUow.CreateAsync(student);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<int> CreateAsyncReturnId(Student student)
        {
            var studentUow = _unitOfWork.Student;
            student.Status = StudentStatusEnum.interested;
            await studentUow.CreateAsync(student);
            await _unitOfWork.CompleteAsync();
            return student.Id;
        }

        internal async Task<List<Student>> SelectLeadStudentsAsync()
        {
            var students = await _unitOfWork.Student.SelectLeadStudentsAsync();

            return students;
        }
        internal async Task<List<Student>> SelectTrialStudentsAsync()
        {
            var students = await _unitOfWork.Student.SelectTrialStudentsAsync();

            return students;
        }
        public async Task EditAsync(StudentViewModel student)
        {
            var studentUow = _unitOfWork.Student;
            var studentMapping = Mapper.Map<Student>(student);
            studentUow.UpdateAsync(studentMapping);
            await _unitOfWork.CompleteAsync();
            if (student.Comment != null)
            {
                await CreateComment(student);
            }
        }

        private async Task CreateComment(StudentViewModel student)
        {
            Comment comment = new Comment
            {
                Text = student.Comment,
                StudentId = student.Id,
                Create = DateTime.Now
            };
            await _unitOfWork.Comments.CreateAsync(comment);
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
            students = students.Where(x => (x.Name?.ToUpper() ?? String.Empty).Contains(value.ToUpper())
                                           || (x.PhoneNumber?.ToUpper() ?? String.Empty).Contains(value.ToUpper())
                                           || (x.LastName?.ToUpper() ?? String.Empty).Contains(value.ToUpper())
                                           || (x.ParentLastName?.ToUpper() ?? String.Empty).Contains(value.ToUpper())).ToList();
            return students;
        }

        public async Task<List<Student>> GetArchiveStudentsByBranchIdAsync(int BranchId)
        {
            var groups = await _unitOfWork.Groups.GetIncludeStudentsByBranchIdAsync(BranchId);
            var students = new List<Student>();
            foreach (var g in groups)
            {
                students.AddRange(g.Students);
            }

            students = students.Where(x => x.Status == StudentStatusEnum.archive).ToList();

            return students;
        }

        public IEnumerable<Level> GetAllLevel()
        {
            var levelUoF = _unitOfWork.Levels;
            return levelUoF.GetAll();
        }

        public IEnumerable<Group> GetAllGroup()
        {
            var groupsUoF = _unitOfWork.Groups;
            var groups = groupsUoF.GetAllGroupsAllInclude();
            return groups;
        }

        public async Task<List<Student>> SelectStudyingStudentsAsync()
        {
            var students = await _unitOfWork.Student.SelectStudyingStudentsAsync();
            return students;
        }

        public async Task<List<Student>> GetStudingStudentsByBranchIdAsync(int BranchId)
        {
            var groups = await _unitOfWork.Groups.GetIncludeStudentsByBranchIdAsync(BranchId);
            var students = new List<Student>();
            foreach (var g in groups)
            {
                students.AddRange(g.Students);
            }

            students = students.Where(x => x.Status == StudentStatusEnum.studying).ToList();

            return students;
        }

        public async Task<List<Student>> GetAllStudentsByArchive()
        {
            var students = await _unitOfWork.Student.GetAllStudentsByArchiveAsync();
            return students;
        }
        public async Task EditAsync(EditStudentViewModel student)
        {
            //StudentStatusEnum? studentStatusEnum = _unitOfWork.Student.GetStudentStatusByStudentId(student.Id);
            if (student.Status != student.StudentStatusEnum)
            {
               // studentStatusEnum = null;
                var something = await GetStudentInterface(student.StudentStatusEnum);

                student.IStatusStudent = something;
                IStatusStudent statusStudent = null;
                switch (student.Status)
                {
                    case StudentStatusEnum.interested:
                        statusStudent = new StatusInterested(_unitOfWork);
                        break;
                    case StudentStatusEnum.trial:
                       statusStudent = new StatusTrial(_unitOfWork);
                        break;
                    case StudentStatusEnum.studying:
                        statusStudent = new StatusStudying(_unitOfWork);
                        break;
                    case StudentStatusEnum.archive:
                        statusStudent = new StatusArchive();
                        break;
                    default:
                        break;
                }
              //  _unitOfWork.Dispose();
                statusStudent?.CreatePeriod(student);
                var model = Mapper.Map<Student>(student);
                _unitOfWork.Student.UpdateAsync(model);
                await _unitOfWork.CompleteAsync();
            }
            else
            {
                var model = Mapper.Map<Student>(student);
                _unitOfWork.Student.UpdateAsync(model);
                await _unitOfWork.CompleteAsync();
            }

/*            _unitOfWork.Student.UpdateAsync(student);
            await _unitOfWork.CompleteAsync();*/
        }

        private async Task<IStatusStudent> GetStudentInterface(StudentStatusEnum statusEnum)
        {
            switch (statusEnum)
            {
                case StudentStatusEnum.interested:
                    return new StatusInterested(_unitOfWork);
                    break;
                case StudentStatusEnum.trial:
                    return new StatusTrial(_unitOfWork);
                    break;
                case StudentStatusEnum.studying:
                    return new StatusStudying(_unitOfWork);
                    break;
                case StudentStatusEnum.archive:
                    return new StatusArchive();
                    break;
                default: throw new Exception();
            }
        }
        private async void UpdateAndCompleteAsync(Student student)
        {
            _unitOfWork.Student.UpdateAsync(student);
            await _unitOfWork.CompleteAsync();
        }
    }
}