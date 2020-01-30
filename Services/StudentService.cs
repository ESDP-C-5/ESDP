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
using Microsoft.CodeAnalysis.CSharp.Syntax;
using CRM.ViewModels;
using AutoMapper;
using CRM.Helpers.SortHelper;

namespace CRM.Services
{
    public class StudentService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly PaymentPeriodService _paymentPeriodService;
        private readonly CommentService _commentService;

        public StudentService(UnitOfWork unitOfWork, PaymentPeriodService paymentPeriodService, CommentService commentService)
        {
            _unitOfWork = unitOfWork;
            _paymentPeriodService = paymentPeriodService;
            _commentService = commentService;
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

        public async Task<StudentViewModel> SelectStudyingStudentsAsync(SortingEnum sortState = SortingEnum.LastNameAsc)
        {
            var students = await _unitOfWork.Student.SelectStudyingStudentsAsync();
            return SortStudents.Sort(students, sortState);
        }

        public async Task<List<Student>> GetStudyingAndTrialStudentsWithoutAttendanceByGroupId(int id)
        {
            var students = await _unitOfWork.Student.GetStudyingAndTrialStudentsWithoutAttendanceByGroupId(id);
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
            var students = await _unitOfWork.Student.GetAllStudentsByArchiveAsync();
            return students;
        }
        public async Task<List<StudentAttendanceViewModel>> GetStudyingAndTrialStudentsAttendanceByGroupId(int id)
        {
            var students = await _unitOfWork.Student.GetAllStudentsWithAttendancesByGroupIdAsync(id);

            return students;
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
                    default:
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
                    break;
                case StudentStatusEnum.trial:
                    return new StatusTrial(_unitOfWork);
                    break;
                case StudentStatusEnum.studying:
                    return new StatusStudying(_unitOfWork);
                    break;
                case StudentStatusEnum.archive:
                    return new StatusArchive(_unitOfWork);
                    break;
                default: throw new Exception();
            }
        }
        private async void UpdateAndCompleteAsync(Student student)
        {
            _unitOfWork.Student.UpdateAsync(student);
            await _unitOfWork.CompleteAsync();
        }

        public async Task AddStudent(string name, string lastName, string fatherName, DateTime dateOfBirth, DateTime trialDate, DateTime startDate, string parentName, string parentLastName, string parentFatherName, string phoneNumber, int status,int levelId, string text, int groupId)
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
                    DataStartStudying = startDate,
                    ParentName = parentName,
                    ParentLastName = parentLastName,
                    ParentFatherName = parentFatherName,
                    PhoneNumber = phoneNumber,
                    Status = GetStatusEnum(status),
                    LevelId = levelId,
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

        public async Task CreatePeriod(Student student)
        {
            if (student.Status == StudentStatusEnum.studying || student.Status == StudentStatusEnum.trial)
            {
                student.DataStartStudying = DateTime.Today.AddDays(1);
                StudentPaymentAndPeriod period = new StudentPaymentAndPeriod
                {
                    StudentId = student.Id,
                    MustTotal = 0,
                    PaymentPeriodStart = student.DataStartStudying,
                    PaymentPeriodEnd = DateTime.Today.AddMonths(1)
                };

                student.ChangeStatusDate = DateTime.Now;
                await _unitOfWork.StudentPaymentAndPeriods.CreateAsync(period);
            }
        }

        public StudentStatusEnum GetStatusEnum(int value)
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

        public async Task CreateAsyncReturnStudent(Student student)
        {
            var studentUow = _unitOfWork.Student;
            await studentUow.CreateAsync(student);
            await _unitOfWork.CompleteAsync();
        }
    }
}