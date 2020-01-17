using System;
using System.Threading.Tasks;
using CRM.Models;
using CRM.UoW;

namespace CRM.Services
{
    public class PaymentPeriodService
    {
        private readonly UnitOfWork _unitOfWork;

        public PaymentPeriodService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(DateTime dateTimeStart , decimal mustTotal,int StudentId,DateTime dateEnd)
        {
            StudentPaymentAndPeriod period = new StudentPaymentAndPeriod()
            {
                StudentId = StudentId,
                MustTotal = mustTotal,
                PaymentPeriodStart = dateTimeStart,
                PaymentPeriodEnd = dateEnd
            };
            await _unitOfWork.StudentPaymentAndPeriods.CreateAsync(period);
            await _unitOfWork.CompleteAsync();
        }

        public async Task Update(int periodId, decimal mustTotal,DateTime dateStart,DateTime dateEnd)
        {
            var testPeriod = await _unitOfWork.StudentPaymentAndPeriods.GetByIdAsync(periodId);

            testPeriod.MustTotal = mustTotal;
            testPeriod.PaymentPeriodStart = dateStart;
            testPeriod.PaymentPeriodEnd = dateEnd;
            _unitOfWork.StudentPaymentAndPeriods.UpdateAsync(testPeriod);
            await _unitOfWork.CompleteAsync();
        }
    }
}