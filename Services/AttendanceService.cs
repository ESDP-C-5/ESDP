using CRM.Models;
using CRM.Helpers;
using CRM.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services
{
    public class AttendanceService
    {
        private readonly UnitOfWork _unitOfWork;

        public AttendanceService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Attendance>> GetAllAttendance()
        {
            return await _unitOfWork.Attendances.GetAllAsync();
        }

        public async Task<Attendance> GetById(int id)
        {
            var posts = _unitOfWork.Attendances;
            return await posts.GetByIdAsync(id);
        }

        public async Task CreateAttendance(Attendance attendance)
        {
            var attendances = _unitOfWork.Attendances;
            await attendances.CreateAsync(attendance);
            await _unitOfWork.CompleteAsync();
        }

        public async Task EditAttendance(Attendance attendance)
        {
            var attendances = _unitOfWork.Attendances;
            attendances.UpdateAsync(attendance);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAttendance(Attendance attendance)
        {
            var attendances = _unitOfWork.Attendances;
            attendances.RemoveAsync(attendance);
            await _unitOfWork.CompleteAsync();
        }

        public List<int> SetDays(DateTime dateTime, DayOfWeek first, DayOfWeek second)
        {
            var month = dateTime.Month;
            var year = dateTime.Year;

            int daysCount = DateTime.DaysInMonth(year, month);
            List<int> daysOfWeek = new List<int>();

            for (int i = 1; i <= daysCount; i++)
            {
                DateTime dateValue = new DateTime(year, month, i);
                if (dateValue.DayOfWeek == first)
                {
                    daysOfWeek.Add(i);
                }
                else if (dateValue.DayOfWeek == second)
                {
                    daysOfWeek.Add(i);
                }
            }

            daysOfWeek.Sort();

            return daysOfWeek;
        }

        public async Task SetAttendances(DateTime dateTime, Group group, List<Student> students)
        {
            if (students.Count != 0)
            {
                int month = dateTime.Month;

                List<int> days = SetDays(dateTime, group.TimeTable.Day1, group.TimeTable.Day2);

                List<Attendance> dayAttendances = new List<Attendance>();
                for (int j = 0; j < students.Count; j++)
                {
                    for (int i = 0; i < days.Count; i++)
                    {
                        dayAttendances.Add(new Attendance()
                        {
                            StudentId = students[j].Id,
                            Day = days[i],
                            IsAttended = 0,
                            Month = (Month)month

                        });
                    }
                }

                await _unitOfWork.Attendances.CreateRangeAsync(dayAttendances);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task UpdateAttendance(int studentId, int attendanceId, int attendanceDay, string attendanceMonth, int isAttend, string comment)
        {
            Attendance attendance = new Attendance()
            {
                Id = attendanceId,
                StudentId = studentId,
                Day = attendanceDay,
                Month = GetMonth(attendanceMonth),
                IsAttended = GetAttendanceRecord(isAttend),
                Comment = comment
            };
            _unitOfWork.Attendances.UpdateAsync(attendance);
            _unitOfWork.CompleteAsync();
        }

        private Month GetMonth(string value)
        {
            switch (value)
            {
                case "January": return Month.January;
                case "February": return Month.February;
                case "March": return Month.March;
                case "April": return Month.April;
                case "May": return Month.May;
                case "June": return Month.June;
                case "July": return Month.July;
                case "August": return Month.August;
                case "September": return Month.September;
                case "October": return Month.October;
                case "November": return Month.November;
                case "December": return Month.December;
                default: throw new Exception();
            }
        }

        private AttendanceRecord GetAttendanceRecord(int value)
        {
            switch (value)
            {
                case 1: return AttendanceRecord.Attendended;
                case 2: return AttendanceRecord.Absent;
                default: return AttendanceRecord.NotDefined;
            }
        }
    }
}
