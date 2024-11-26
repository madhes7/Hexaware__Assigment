using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Service
{
    internal interface IPaymentService
    {
        void DisplayPaymentsForStudent(int studentId);
        void DisplayPaymentAmount(int paymentId);

        void DisplayPaymentDate(int paymentId);
    }
}
