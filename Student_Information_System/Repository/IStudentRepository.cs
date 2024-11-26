using Student_Information_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Repository
{
    internal interface IStudentRepository
    {

        bool UpdateStudent(int studentId, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber);
        List<Student> GetAllStudents();
        bool MakePayment(int studentId, decimal amount, DateTime paymentDate);

        List<Payment> GetPaymentHistory(int studentId);
    }
}
