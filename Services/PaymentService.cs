using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CRM.Models;
using CRM.UoW;
using CRM.ViewModels;

namespace CRM.Services
{
    public class PaymentService
    {
        private readonly UnitOfWork _unitOfWork;

        public PaymentService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<StudentPaymentViewModel>> GetAllStudentsByBranchIdAsync(int branchId)
        {
            var students = await _unitOfWork.Student.GetAllStudentsByBranchIdAsync(branchId);
            List<StudentPaymentViewModel> studentPaymentViewModels = new List<StudentPaymentViewModel>();
            foreach (var student in students)
            {
                var model = Mapper.Map<StudentPaymentViewModel>(student);
                model.MustTotal = await _unitOfWork.StudentPaymentAndPeriods.GetMustTotalByStudentIdAsync(student.Id);
                model.AllMustTotal = await GetAllMustTotalByStudentIdAsync(student.Id);
                model.AllTotal = await GetAllTotalByStudentId(student.Id);
                model.Balance = model.AllMustTotal - model.AllTotal;
                model.PeriodCount = await GetCountPeriodsByStudentIdAsync(student.Id);
                model.LastPayment = await GetLastPaymentByStudentIdAsync(student.Id);
                model.LastCommit = await GetLastCommitByStudentIdAsync(student.Id);
                studentPaymentViewModels.Add(model);
            }

            return studentPaymentViewModels;
        }

        private async Task<string> GetLastCommitByStudentIdAsync(int studentId)
        {
            return await _unitOfWork.Payments.GetLastCommitByStudentIdAsync(studentId);
        }

        private async Task<DateTime?> GetLastPaymentByStudentIdAsync(int studentId)
        {
            return await _unitOfWork.Payments.GetLastPaymentByStudentIdAsync(studentId);
        }

        private async Task<int> GetCountPeriodsByStudentIdAsync(int studentId)
        {
            List<StudentPaymentAndPeriod> PeriodAndPaymentsByStudentId =await _unitOfWork.StudentPaymentAndPeriods.GetAllPeriodAndPaymentAsync(studentId);
            return PeriodAndPaymentsByStudentId.Count;
        }

        private async Task<decimal> GetAllTotalByStudentId(int studentId)
        {
            List<Payment> payments = await _unitOfWork.Payments.GetAllPaymentByStudentIdAsync(studentId);
            decimal result = 0;
            foreach (var payment in payments)
            {
                result += payment.Total;
            }

            return result;
        }

        private async Task<decimal> GetAllMustTotalByStudentIdAsync(int studentId)
        {
            var PeriodAndPaymentsByStudentId =await _unitOfWork.StudentPaymentAndPeriods.GetAllPeriodAndPaymentAsync(studentId);
            decimal result = 0;
            foreach (var value in PeriodAndPaymentsByStudentId)
            {
                result += value.MustTotal;
            }

            return result;
        }

        public async Task<Student> GetStudentCardByIdStudentAsync(int id)
        {
            var student = await _unitOfWork.Student.GetByIdForCardStudent(id);
            return student;
        }

        public async Task AddPayment(int periodId, int studentId, decimal total,string text)
        {
            Payment payment = new Payment()
            {
                StudentId = studentId,
                StudentPaymentAndPeriodId = periodId,
                Total = total,
                Comment = text
            };
            await _unitOfWork.Payments.CreateAsync(payment);
            await _unitOfWork.CompleteAsync();
        }
    }
}