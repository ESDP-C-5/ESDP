using System.Collections.Generic;
using System.Data.SqlTypes;
using CRM.Helpers;

namespace CRM.ViewModels
{
    public class StudentsPaymentListViewModel: StudentPaymentViewModel
    {
        public IEnumerable<StudentPaymentViewModel> Student { get; set; }
        public PaymentSortingEnum LastNameSortState { get; set; }
        public PaymentSortingEnum NameSortState { get; set; }
        public PaymentSortingEnum MustTotalSortState { get; set; }
        public PaymentSortingEnum AllMustTotalSortState { get; set; }
        public PaymentSortingEnum AllTotalSortState { get; set; }
        public PaymentSortingEnum BalanceSortState { get; set; }
        public PaymentSortingEnum PeriodCountSortState { get; set; }
        public PaymentSortingEnum LastPaymentSortState { get; set; }
    }
}
