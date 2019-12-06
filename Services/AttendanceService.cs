using CRM.Models;
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
    }
}
