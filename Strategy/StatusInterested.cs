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
            
        }
    }
}