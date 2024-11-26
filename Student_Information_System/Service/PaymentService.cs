using Student_Information_System.Exceptions;
using Student_Information_System.Model;
using Student_Information_System.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Service
{
    internal class PaymentService : IPaymentService
    {

        private readonly IPaymentRepository _paymentRepository;

        public PaymentService()
        {
            _paymentRepository = new PaymentRepository();
        }

        public void DisplayPaymentsForStudent(int studentId)
        {
            
                List<Payment> payments = _paymentRepository.GetPaymentsForStudent(studentId);

                if (payments.Count == 0)
                {
                    Console.WriteLine($"No payments found for Student ID {studentId}.");
                }
                else
                {
                    Console.WriteLine($"Payments for Student ID {studentId}:");
                    foreach (var payment in payments)
                    {
                        Console.WriteLine($"Payment ID: {payment.PaymentId}, Amount: {payment.Amount:C}, Date: {payment.PaymentDate:yyyy-MM-dd}");
                    }
                }
            
          
        }

        public void DisplayPaymentAmount(int paymentId)
        {
            try
            {
                double amount = _paymentRepository.GetPaymentAmount(paymentId);

                if (amount > 0)
                {
                    Console.WriteLine($"Payment Amount for Payment ID {paymentId}: {amount:C}");
                }
                else
                {
                    throw new PaymentValidationException("Payment amount must be greater than zero.");
                }
            }
            catch (PaymentValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void DisplayPaymentDate(int paymentId)
        {
            try
            {
                DateTime? paymentDate = _paymentRepository.GetPaymentDate(paymentId);

                if (paymentDate.HasValue)
                {
                    Console.WriteLine($"Payment Date for Payment ID {paymentId}: {paymentDate.Value:yyyy-MM-dd}");
                }
                else
                {
                    throw new PaymentValidationException("Payment date is missing or invalid.");
                }
                if (paymentDate.Value > DateTime.Now)
                {
                    throw new PaymentValidationException("Payment date cannot be in the future.");
                }
            }
            catch (PaymentValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
