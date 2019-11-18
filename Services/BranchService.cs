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
    }
}
