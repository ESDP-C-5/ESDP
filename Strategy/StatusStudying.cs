using System;
using CRM.Models;
using CRM.UoW;

namespace CRM.Strategy
{
    public class StatusStudying: IStatusStudent
    {
        private readonly UnitOfWork _unitOfWork;

        public StatusStudying(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async void CreatePeriod(Student student)
        {
            student.DataStartStudying = DateTime.Today.AddDays(1);
            StudentPaymentAndPeriod period = new StudentPaymentAndPeriod
            {
                StudentId = student.Id,
                MustTotal =  0,
                PaymentPeriodStart = student.DataStartStudying,
                PaymentPeriodEnd = DateTime.Today.AddMonths(1)
            };
            student.ChangeStatusDate = DateTime.Now;
            await _unitOfWork.StudentPaymentAndPeriods.CreateAsync(period);

        }
        public async void CreateComment(Student student)
        {
            Comment comment = new Comment()
            {
                Create = DateTime.Now,
                StudentId = student.Id,
                Text = "Change status studying"
            };
            await _unitOfWork.Comments.CreateAsync(comment);

        }
    }
}