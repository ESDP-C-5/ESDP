using CRM.Models;
using CRM.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;
using CRM.ViewModels;
using AutoMapper;
using CRM.Helpers.SortHelper;

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

        public async Task<StudentViewModel> SearchAsync(
                string value,
                SortingEnum sortState = SortingEnum.LastNameAsc)
        {
            var students = await _unitOfWork.Student.GetAllAsync();
            students = students.Where(x => (x.Name?.ToUpper() ?? string.Empty).Contains(value.ToUpper())
                                           || (x.PhoneNumber?.ToUpper() ?? string.Empty).Contains(value.ToUpper())
                                           || (x.LastName?.ToUpper() ?? string.Empty).Contains(value.ToUpper())
                                           || (x.ParentLastName?.ToUpper() ?? string.Empty).Contains(value.ToUpper())).ToList();



            return SortStudents.Sort(students, sortState);
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

        public async Task<IEnumerable<StudentViewModel>> SortStudentsAsync(SortingEnum sortState = SortingEnum.LastNameAsc)
        {
            var students = await _unitOfWork.Student.GetAllStudentsByArchiveAsync();
            #region Sort
            var model = new StudentViewModel
            {
                LastNameSortState = sortState == SortingEnum.ParentLastNameAsc
                    ? SortingEnum.LastNameDesc
                    : SortingEnum.LastNameAsc,
                CreatedDateSortState = sortState == SortingEnum.CreatedDateAsc
                    ? SortingEnum.CreatedDateDesc
                    : SortingEnum.CreatedDateAsc,
                DateOfBirthdaySortState = sortState == SortingEnum.DateOfBirthdayAsc
                    ? SortingEnum.DateOfBirthdayDesc
                    : SortingEnum.DateOfBirthdayAsc,
                ParentLastNameSortState = sortState == SortingEnum.ParentLastNameAsc
                    ? SortingEnum.ParentLastNameDesc
                    : SortingEnum.ParentLastNameAsc,
                PhoneNumberSortState = sortState == SortingEnum.PhoneNumberAsc
                    ? SortingEnum.PhoneNumberDesc
                    : SortingEnum.PhoneNumberAsc,
                StatusSortState = sortState == SortingEnum.StatusAsc
                    ? SortingEnum.StatusDesc
                    : SortingEnum.StatusAsc
            };

            

            switch (sortState)
            {
                case SortingEnum.LastNameDesc:
                    students = students.OrderByDescending(s => s.LastName).ToList();
                    break;
                case SortingEnum.CreatedDateAsc:
                    students = students.OrderBy(s => s.CreatedDate).ToList();
                    break;
                case SortingEnum.CreatedDateDesc:
                    students = students.OrderByDescending(s => s.CreatedDate).ToList();
                    break;

            }

            #endregion

            List<StudentViewModel> studentList = new List<StudentViewModel>();
            foreach (var student in students)
            {
                StudentViewModel studentViewModel = new StudentViewModel
                {
                    LastName = student.LastName,
                    Name = student.Name,
                    FatherName = student.FatherName,
                    ParentName = student.ParentName,
                    ParentLastName = student.ParentLastName,
                    ParentFatherName = student.ParentFatherName,
                    Status = student.Status,
                    PhoneNumber = student.PhoneNumber
                };
                studentList.Add(studentViewModel);
            }
            
            return studentList;
        }
    }
}