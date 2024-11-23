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
        void EnrollInCourse(int studentId, int courseId);
        void UpdateStudentInfo(int studentId, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber);
        void MakePayment(int studentId, decimal amount, DateTime paymentDate);
        Student GetStudentById(int studentId);
        List<Course> GetEnrolledCourses(int studentId);
        List<Payment> GetPaymentHistory(int studentId);
        void DisplayStudentInfo(int studentId);
    }
}
