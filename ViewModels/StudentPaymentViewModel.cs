using System;
using CRM.Models;

namespace CRM.ViewModels
{
    public class StudentPaymentViewModel: Student
    {
        public decimal MustTotal { get; set; }
        public decimal AllMustTotal { get; set; }
        public decimal AllTotal { get; set; }
        public decimal Balance { get; set; }
        public int PeriodCount { get; set; }
        public DateTime? LastPayment { get; set; }
        public string LastCommit { get; set; }
        
        
    }
}