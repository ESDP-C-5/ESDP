using System;
using CRM.Models;
using CRM.UoW;

namespace CRM.Strategy
{
    public class StatusArchive : IStatusStudent
    {
        private readonly UnitOfWork _unitOfWork;

        public StatusArchive(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void CreatePeriod(Student student)
        {
            student.DataEndStudying = DateTime.Now;
            student.ChangeStatusDate = DateTime.Now;
        }

        public async void CreateComment(Student student)
        {
            Comment comment = new Comment()
            {
                Create = DateTime.Now,
                StudentId = student.Id,
                Text = DateTime.Now +" Статус студента изменен на 'Архив'"
            };
            await _unitOfWork.Comments.CreateAsync(comment);
        }
    }
}