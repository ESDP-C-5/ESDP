using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CRM.ViewModels;

namespace CRM.Models
{
    public class StudentPaymentAndPeriod : BaseEntity
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public decimal MustTotal { get; set; }
        [DataType(DataType.Date)]
        public DateTime PaymentPeriodStart { get; set; }
        [DataType(DataType.Date)]
        public DateTime PaymentPeriodEnd { get; set; }
        public List<Payment> Payments { get; set; }

        public StudentPaymentAndPeriod()
        {
            Payments = new List<Payment>();
        }
    }
}