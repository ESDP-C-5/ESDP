using System.Collections.Generic;
using System.Linq;
using CRM.Models;
using CRM.ViewModels;

namespace CRM.Helpers.SortHelper
{
    public class PaymentStudentsSort
    {
        public static StudentsPaymentListViewModel Sort(List<StudentPaymentViewModel> students, PaymentSortingEnum sortState)
        {
            var model = new StudentsPaymentListViewModel
            {
                LastNameSortState = sortState == PaymentSortingEnum.LastNameAsc
                    ? PaymentSortingEnum.LastNameDesc
                    : PaymentSortingEnum.LastNameAsc,
                NameSortState = sortState == PaymentSortingEnum.NameAsc
                    ? PaymentSortingEnum.NameDesc
                    : PaymentSortingEnum.NameAsc,
                MustTotalSortState = sortState == PaymentSortingEnum.MustTotalAsc
                    ? PaymentSortingEnum.MustTotalDesc
                    : PaymentSortingEnum.MustTotalAsc,
                AllMustTotalSortState = sortState == PaymentSortingEnum.AllMustTotalAsc
                    ? PaymentSortingEnum.AllMustTotalDesc
                    : PaymentSortingEnum.AllMustTotalAsc,
                AllTotalSortState = sortState == PaymentSortingEnum.AllTotalAsc
                    ? PaymentSortingEnum.AllTotalDesc
                    : PaymentSortingEnum.AllTotalAsc,
                BalanceSortState = sortState == PaymentSortingEnum.BalanceAsc
                    ? PaymentSortingEnum.BalanceDesc
                    : PaymentSortingEnum.BalanceAsc,
                PeriodCountSortState = sortState == PaymentSortingEnum.PeriodCountAsc
                    ? PaymentSortingEnum.PeriodCountDesc
                    : PaymentSortingEnum.PeriodCountAsc,
                LastPaymentSortState = sortState == PaymentSortingEnum.LastPaymentAsc
                    ? PaymentSortingEnum.LastPaymentDesc
                    : PaymentSortingEnum.LastPaymentAsc
            };



            switch (sortState)
            {
                case PaymentSortingEnum.LastNameDesc:
                    students = students.OrderByDescending(s => s.LastName ?? s.Name ?? s.FatherName).ToList();
                    break;
                case PaymentSortingEnum.NameAsc:
                    students = students.OrderBy(s => s.Name).ToList();
                    break;
                case PaymentSortingEnum.NameDesc:
                    students = students.OrderByDescending(s => s.Name).ToList();
                    break;
                case PaymentSortingEnum.MustTotalAsc:
                    students = students.OrderBy(s => s.MustTotal).ToList();
                    break;
                case PaymentSortingEnum.MustTotalDesc:
                    students = students.OrderByDescending(s => s.MustTotal).ToList();
                    break;
                case PaymentSortingEnum.AllMustTotalAsc:
                    students = students.OrderBy(s => s.AllMustTotal).ToList();
                    break;
                case PaymentSortingEnum.AllMustTotalDesc:
                    students = students.OrderByDescending(s => s.AllMustTotal).ToList();
                    break;
                case PaymentSortingEnum.AllTotalAsc:
                    students = students.OrderBy(s => s.AllTotal).ToList();
                    break;
                case PaymentSortingEnum.AllTotalDesc:
                    students = students.OrderByDescending(s => s.AllTotal).ToList();
                    break;
                case PaymentSortingEnum.BalanceAsc:
                    students = students.OrderBy(s => s.Balance).ToList();
                    break;
                case PaymentSortingEnum.BalanceDesc:
                    students = students.OrderByDescending(s => s.Balance).ToList();
                    break;
                case PaymentSortingEnum.PeriodCountAsc:
                    students = students.OrderBy(s => s.PeriodCount).ToList();
                    break;
                case PaymentSortingEnum.PeriodCountDesc:
                    students = students.OrderByDescending(s => s.PeriodCount).ToList();
                    break;
                case PaymentSortingEnum.LastPaymentAsc:
                    students = students.OrderBy(s => s.LastPayment).ToList();
                    break;
                case PaymentSortingEnum.LastPaymentDesc:
                    students = students.OrderByDescending(s => s.LastPayment).ToList();
                    break;
            }


            model.Student = students;


            return model;
        }
    }
}