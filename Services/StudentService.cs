using CRM.Models;
using CRM.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRM.Helpers;
using CRM.Strategy;
using CRM.ViewModels;
using CRM.Helpers.SortHelper;

namespace CRM.Services
{
    public class StudentService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly CommentService _commentService;

        public StudentService(UnitOfWork unitOfWork, CommentService commentService)
        {
            _unitOfWork = unitOfWork;
            _commentService = commentService;
        }
        public async Task<StudentDetailsViewModel> GetByIdAsync(int id)
        {
            var student = await _unitOfWork.Student.GetByIdAsync(id);
            var model = Mapper.Map<StudentDetailsViewModel>(student);
            model.GroupName = $"{student.Group.Branch.Name} {student.Group.TimeTable.Day1}-" +
                                            $"{student.Group.TimeTable.Day2} {student.Group.TimeTable.Time}";
            return model;
        }
        public async Task<int> CreateAsyncReturnId(Student student)
        {
            var studentUow = _unitOfWork.Student;
            student.Status = StudentStatusEnum.interested;
            await studentUow.CreateAsync(student);
            await _unitOfWork.CompleteAsync();
            return student.Id;
        }

        internal async Task<StudentViewModel> SelectLeadStudentsAsync(SortingEnum sortState = SortingEnum.LastNameAsc)
        {
            var students = await _unitOfWork.Student.SelectLeadStudentsAsync();

            return SortStudents.Sort(students, sortState);
        }
        internal async Task<StudentViewModel> SelectTrialStudentsAsync(SortingEnum sortState = SortingEnum.LastNameAsc)
        {
            var students = await _unitOfWork.Student.SelectTrialStudentsAsync();

            return SortStudents.Sort(students, sortState);
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

        public async Task<List<Student>> GetArchiveStudentsByBranchIdAsync(int branchId)
        {
            var groups = await _unitOfWork.Groups.GetIncludeStudentsByBranchIdAsync(branchId);
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

        public async Task<StudentViewModel> SelectStudyingStudentsAsync(SortingEnum sortState = SortingEnum.LastNameAsc)
        {
            var students = await _unitOfWork.Student.SelectStudyingStudentsAsync();
            return SortStudents.Sort(students, sortState);
        }

        public async Task<List<Student>> GetStudyingAndTrialStudentsWithoutAttendanceByGroupId(int id)
        {
            return await _unitOfWork.Student.GetStudyingAndTrialStudentsWithoutAttendanceByGroupId(id);
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
        public async Task<List<Student>> GetTrialStudentsByBranchIdAsync(int BranchId)
        {
            var groups = await _unitOfWork.Groups.GetIncludeStudentsByBranchIdAsync(BranchId);
            var students = new List<Student>();
            foreach (var g in groups)
            {
                students.AddRange(g.Students);
            }
            students = students.Where(x => x.Status == StudentStatusEnum.trial).ToList();
            return students;
        }

        public async Task<List<Student>> GetAllStudentsByArchive()
        {
            return await _unitOfWork.Student.GetAllStudentsByArchiveAsync();
        }
        public async Task<List<StudentAttendanceViewModel>> GetStudyingAndTrialStudentsAttendanceByGroupId(int id)
        {
            return await _unitOfWork.Student.GetAllStudentsWithAttendancesByGroupIdAsync(id);
        }
        public async Task EditAsync(EditStudentViewModel student)
        {
            if (student.Comment != null)
            {
                Comment comment = new Comment
                {
                    StudentId = student.Id,
                    Text = student.Comment,
                    Create = DateTime.Now
                };
                await _unitOfWork.Comments.CreateAsync(comment);
            }
            if (student.Status != student.StudentStatusEnum)
            {
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
                        statusStudent = new StatusArchive(_unitOfWork);
                        break;
                }
                statusStudent?.CreatePeriod(student);
                statusStudent?.CreateComment(student);
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
        }

        private async Task<IStatusStudent> GetStudentInterface(StudentStatusEnum statusEnum)
        {
            switch (statusEnum)
            {
                case StudentStatusEnum.interested:
                    return new StatusInterested(_unitOfWork);
                case StudentStatusEnum.trial:
                    return new StatusTrial(_unitOfWork);
                case StudentStatusEnum.studying:
                    return new StatusStudying(_unitOfWork);
                case StudentStatusEnum.archive:
                    return new StatusArchive(_unitOfWork);
                default: throw new Exception();
            }
        }
        public async Task AddStudent(string name, string lastName, string fatherName, DateTime dateOfBirth, DateTime trialDate, string parentName, string parentLastName, string parentFatherName, string phoneNumber, int status, string text, int groupId)
        {
            try
            {
                Student student = new Student()
                {
                    Name = name,
                    LastName = lastName,
                    FatherName = fatherName,
                    DateOfBirthday = dateOfBirth,
                    TrialDate = trialDate,
                    ParentName = parentName,
                    ParentLastName = parentLastName,
                    ParentFatherName = parentFatherName,
                    PhoneNumber = phoneNumber,
                    Status = GetStatusEnum(status),
                    GroupId = groupId
                };
                await CreateAsyncReturnStudent(student);
                if (text != null)
                {
                    Comment comment = new Comment
                    {
                        StudentId = student.Id,
                        Text = text,
                        Create = DateTime.Now
                    };
                    await _commentService.CreateAsync(comment);
                }
                await CreatePeriod(student);
                await _unitOfWork.CompleteAsync();
            }
            catch
            {
                throw new Exception();
            }
        }

        private async Task CreatePeriod(Student student)
        {
            if (student.Status == StudentStatusEnum.studying)
            {
                StudentPaymentAndPeriod period = new StudentPaymentAndPeriod
                {
                    StudentId = student.Id,
                    MustTotal = 0,
                    PaymentPeriodStart = student.DataStartStudying,
                    PaymentPeriodEnd = DateTime.Today.AddDays(30)
                };
                student.ChangeStatusDate = DateTime.Now;
                await _unitOfWork.StudentPaymentAndPeriods.CreateAsync(period);
            }
        }

        private StudentStatusEnum GetStatusEnum(int value)
        {
            StudentStatusEnum status;
            switch (value)
            {
                case 1:
                    return StudentStatusEnum.studying;
                case 2:
                    return StudentStatusEnum.trial;
                default:
                    return StudentStatusEnum.interested;
            }
        }

        private async Task CreateAsyncReturnStudent(Student student)
        {
            var studentUow = _unitOfWork.Student;
            await studentUow.CreateAsync(student);
            await _unitOfWork.CompleteAsync();
        }
    }
}