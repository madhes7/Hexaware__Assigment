using Student_Information_System.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System
{
    internal class Main
    {
       public void CheckStudent()
        {
            var studentRepo = new StudentRepository();

            // Enroll a student in a course
            studentRepo.EnrollInCourse(1, 1); // Example: StudentID = 1, CourseID = 101
            Console.WriteLine("Student enrolled successfully!\n");

            // Update student info
            studentRepo.UpdateStudentInfo(1, "John", "Doe", new DateTime(1995, 8, 15), "john.doe@example.com", "123-456-7890");
            Console.WriteLine("Student information updated successfully!\n\n");

            // Make a payment
            studentRepo.MakePayment(1, 500.00M, DateTime.Now);
            Console.WriteLine("Payment recorded successfully!\n\n");

            // Display student info
            studentRepo.DisplayStudentInfo(1);

            // Get enrolled courses
            var courses = studentRepo.GetEnrolledCourses(1);
            Console.WriteLine("Enrolled Courses:\n");
            foreach (var course in courses)
            {
                Console.WriteLine($"Course ID: {course.CourseId}, Name: {course.CourseName}");
            }

            // Get payment history
            var payments = studentRepo.GetPaymentHistory(1);
            Console.WriteLine("Payment History:\n");
            foreach (var payment in payments)
            {
                Console.WriteLine($"Payment ID: {payment.PaymentId}, Amount: {payment.Amount}, Date: {payment.PaymentDate}");
            }
        }
        public void CheckCourse (){
            var courseRepo = new CourseRepository();

            // Assign a teacher to a course
            courseRepo.AssignTeacher(8, 1); // Example: CourseID = 101, TeacherID = 1
            Console.WriteLine("Teacher assigned successfully!");

            // Update course information
            courseRepo.Update(11, "Introduction to Computer Science", 3, 2);
            Console.WriteLine("Course information updated successfully!");

            // Display course info
            courseRepo.DisplayCourse(11);

            // Get enrollments for a course
            var students = courseRepo.GetEnrollments(11);
            Console.WriteLine("Students enrolled in the course:");
            if (students != null)
            {
                foreach (var student in students)
                {
                    Console.WriteLine($"Student ID: {student.Student_Id}, Name: {student.First_name} {student.Last_name}");
                }
            }
            else Console.WriteLine(  "No records Found");

            // Get the teacher assigned to the course
            var teacher = courseRepo.GetTeacher(11);
            if (teacher != null)
            {
                Console.WriteLine($"Assigned Teacher: {teacher.FirstName} {teacher.LastName}");
            }
            else
            {
                Console.WriteLine("No teacher assigned to this course.");
            }
        }
    }
}
