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

        internal class StudentService : IStudentService
        {

        private readonly IStudentRepository _studentRepository;

        public StudentService()
        {
            _studentRepository = new StudentRepository();
        }

        public void UpdateStudentInfo(int studentId, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(email) || !email.Contains('@') || email.Length > 100)
                    throw new InvalidStudentDataException("Invalid Email. Ensure it contains '@' and does not exceed 100 characters.");

                if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length != 10 || !phoneNumber.All(char.IsDigit))
                    throw new InvalidStudentDataException("Invalid Phone Number. It must be a 10-digit numeric value.");

                bool isUpdated = _studentRepository.UpdateStudent(studentId, firstName, lastName, dateOfBirth, email, phoneNumber);

                if (isUpdated)
                {
                    Console.WriteLine("Student information updated successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to update student information. Please check the details provided.");
                }
            }
            catch (InvalidStudentDataException ex)
            {
                Console.WriteLine( ex.Message);
            }
        }


        public void DisplayStudentInfo()
        {
            List<Student> students = _studentRepository.GetAllStudents();

            if (students.Count == 0)
            {
                Console.WriteLine("No students found in the database.");
            }
            else
            {
                Console.WriteLine("Student List:");
                foreach (var student in students)
                {
                    Console.WriteLine($"{student.Student_Id}\t\t{student.First_name}" +
                        $"\t\t{student.Last_name}\t\t{student.Date_Of_Birth}\t\t{student.Email}\t\t{student.PhoneNumber}");
                }
            }
        }

        public void MakePayment(int studentId, decimal amount, DateTime paymentDate)
        {
            if (studentId <= 0 || amount <= 0)
            {
                Console.WriteLine("Invalid student ID or payment amount.");
                return;
            }

            bool isPaymentRecorded = _studentRepository.MakePayment(studentId, amount, paymentDate);

            if (isPaymentRecorded)
            {
                Console.WriteLine("Payment recorded successfully.");
            }
            else
            {
                Console.WriteLine("Failed to record payment. Please check the student ID and try again.");
            }
        }

        public void GetPaymentHistory(int studentId)
        {
            if (studentId <= 0)
            {
                Console.WriteLine("Invalid student ID.");
                return;
            }

            List<Payment> paymentHistory = _studentRepository.GetPaymentHistory(studentId);

            if (paymentHistory.Count == 0)
            {
                Console.WriteLine("No payment records found for this student.");
            }
            else
            {
                Console.WriteLine($"Payment History for Student ID {studentId}:");
                foreach (var payment in paymentHistory)
                {
                    Console.WriteLine($"Payment ID: {payment.PaymentId}");
                    Console.WriteLine($"Amount: {payment.Amount}");
                    Console.WriteLine($"Date: {payment.PaymentDate.ToShortDateString()}");
                    Console.WriteLine("-----------------------------");
                }
            }
        }


    }
}


