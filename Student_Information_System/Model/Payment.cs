using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Model
{
    internal class Payment
    {
        public Payment() { }
        public int PaymentId { get; set; }
        public int StudentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public Payment (int PaymentId, int StudentId, decimal Amount, DateTime PaymentDate)
        {
            this.PaymentId = PaymentId;
            this.StudentId = StudentId;
            this.Amount = Amount;
            this.PaymentDate = PaymentDate;

        }
    }
}
