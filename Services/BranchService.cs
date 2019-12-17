using CRM.Models;
using CRM.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services
{
    public class BranchService
    {
        private readonly UnitOfWork _unitOfWork;

        public BranchService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Branch>> GetAllBranch()
        {
            return await _unitOfWork.Branchs.GetAllAsync();
        }

        public async Task<Branch> GetById(int id)
        {
            var posts = _unitOfWork.Branchs;
            return await posts.GetByIdAsync(id);
        }

        public async Task CreateBranch(Branch branch)
        {
            var branchs = _unitOfWork.Branchs;
            await branchs.CreateAsync(branch);
            await _unitOfWork.CompleteAsync();
        }

        public async Task EditBranch(Branch branch)
        {
            var branchs = _unitOfWork.Branchs;
            branchs.UpdateAsync(branch);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteBranch(Branch branch)
        {
            var branchs = _unitOfWork.Branchs;
            branchs.RemoveAsync(branch);
            await _unitOfWork.CompleteAsync();
        }

        public async Task CreateGroupAndTimeTable(CreateGroupViewModel groupViewModel)
        {
            TimeTable timeTable =await CreateTimeTableAsync(groupViewModel);
            CreateGroupAsync(groupViewModel, timeTable).Wait();

        }

        private async Task CreateGroupAsync(CreateGroupViewModel groupViewModel, TimeTable timeTable)
        {
            Group group = new Group
            {
                UserId = groupViewModel.UserId,
                BranchId = groupViewModel.BranchId,
                TimeTableId = timeTable.Id
            };
            await _unitOfWork.Groups.CreateAsync(group);
            await _unitOfWork.CompleteAsync();
        }

        private async Task<TimeTable> CreateTimeTableAsync(CreateGroupViewModel groupViewModel)
        {
            TimeTable timeTable = new TimeTable();
            timeTable.Day1 = groupViewModel.Day1;
            timeTable.Day2 = groupViewModel.Day2;
            timeTable.Time = groupViewModel.Time;
            await _unitOfWork.TimeTables.CreateAsync(timeTable);
            await _unitOfWork.CompleteAsync();
            return await _unitOfWork.TimeTables.GetIdTimeTable(timeTable);
        }
    }
}
