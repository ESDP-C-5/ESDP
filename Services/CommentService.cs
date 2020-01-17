using CRM.Models;
using CRM.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services
{
    public class CommentService
    {
        private readonly UnitOfWork _unitOfWork;
        public CommentService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(Comment comment)
        {
            await _unitOfWork.Comments.CreateAsync(comment);
            await _unitOfWork.CompleteAsync();
        }
    }
}
