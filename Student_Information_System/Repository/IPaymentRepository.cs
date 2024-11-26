using Student_Information_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Repository
{
    internal interface IPaymentRepository
    {
        List<Payment> GetPaymentsForStudent(int studentId);

        double GetPaymentAmount(int paymentId);

        double GetTotalPaymentsForStudent(int studentId);
        DateTime? GetPaymentDate(int paymentId);
    }
}
