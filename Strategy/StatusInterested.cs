using System;
using CRM.Models;
using CRM.UoW;

namespace CRM.Strategy
{
    public class StatusInterested : IStatusStudent
    {
        private readonly UnitOfWork _unitOfWork;

        public StatusInterested(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async void CreatePeriod(Student student)
        {
            student.ChangeStatusDate = DateTime.Now;
        }

        public async void CreateComment(Student student)
        {
            Comment comment = new Comment()
            {
                Create = DateTime.Now,
                StudentId = student.Id,
                Text = "Change status interested"
            };
            await _unitOfWork.Comments.CreateAsync(comment);

        }
    }
}