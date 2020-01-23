using System;
using System.Threading.Tasks;
using CRM.Helpers;
using CRM.Models;
using CRM.Services;
using CRM.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    // GET
    public class PaymentController : Controller
    {
        private readonly BranchService _branchService;
        private readonly PaymentService _paymentService;
        private readonly PaymentPeriodService _periodService ;

        public PaymentController(BranchService branchService, 
            PaymentService paymentService, PaymentPeriodService periodService)
        {
            _branchService = branchService;
            _paymentService = paymentService;
            _periodService = periodService;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            var branches = await _branchService.GetAllBranch();
            return View(branches);
        }

        public async Task<IActionResult> SortSelectedStudentsByBranchId(PaymentSortingEnum sort, int Id)
        {
            StudentsPaymentListViewModel students = null;
            if (Id == 0)
            {
                students = await _paymentService.GetAllStudentsByPayment(sort); 
            }
            else
            {
                students = await _paymentService.GetAllStudentsByBranchIdAsync(Id, sort);
                ViewBag.BranchId = Id;
            }
            return PartialView("_ListStudentByBranchIdPartialView", students);
        }
        public async Task<IActionResult> SelectStudentsByBranchId(int Id,PaymentSortingEnum sortState = PaymentSortingEnum.BalanceAsc)
        {
            var students = await _paymentService.GetAllStudentsByBranchIdAsync(Id,sortState);
            ViewBag.BranchId = Id;
            return PartialView("_ListStudentByBranchIdPartialView", students);
        }

        public async Task<IActionResult> GetAllStudentsByPayment(PaymentSortingEnum sortState = PaymentSortingEnum.BalanceAsc)
        {
            var students = await _paymentService.GetAllStudentsByPayment(sortState);
            if (sortState != PaymentSortingEnum.BalanceAsc)
            {
                ViewBag.value = true;
            }
            else
            {
                ViewBag.value = false;
            }
            return PartialView("_ListStudentByBranchIdPartialView", students);
        }

        public async Task<IActionResult> CardStudent(int Id)
        {
            var student = await _paymentService.GetStudentCardByIdStudentAsync(Id);
            return View(student);
        }

        public async Task<JsonResult> AddPayment(int periodID, DateTime dataPayment, int studentId, decimal payment,string text)
        {
            await _paymentService.AddPayment(periodID,dataPayment, studentId, payment,text);
            //var student = await _paymentService.GetStudentCardByIdStudentAsync(studentId);
            return Json(StatusCode(200));
        }

        public async Task<JsonResult> AddPeriod(DateTime dateStart, decimal mustTotal, int StudentId,DateTime dateEnd)
        {
            await _periodService.CreateAsync(dateStart, mustTotal,StudentId,dateEnd);
            return Json(StatusCode(200));
        }

        public JsonResult UpdatePeriod( int periodId ,decimal mustTotal,DateTime dateStart,DateTime dateEnd)
        {
            _periodService.Update(periodId, mustTotal, dateStart, dateEnd);
            return Json(StatusCode(200));
        }

    }
}