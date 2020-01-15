using System;
using System.ComponentModel.DataAnnotations;

namespace CRM.Models
{
    public class Payment : BaseEntity
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public StudentPaymentAndPeriod StudentPaymentAndPeriod { get; set; }
        public int StudentPaymentAndPeriodId { get; set; }
        public decimal Total { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateTimePayment { get; set; }
        public string Comment { get; set; }
        public Payment()
        {
            DateTimePayment = DateTime.Now;
        }
    }
}