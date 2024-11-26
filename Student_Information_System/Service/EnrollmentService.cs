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
    internal class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IPaymentRepository _paymentRepository;

        public EnrollmentService()
        {
            _enrollmentRepository = new EnrollmentRepository();
            _paymentRepository = new PaymentRepository();
        }
       

        public void GetEnrolledCourses(int studentId)
        {
            try
            {
    
                List<Course> courses = _enrollmentRepository.GetEnrolledCourses(studentId);

               
                if (courses.Count == 0)
                {
                    Console.WriteLine($"No courses found for Student ID {studentId}.");
                }
                else
                {
                
                    Console.WriteLine($"Courses for Student ID {studentId}:");
                    foreach (var course in courses)
                    {
                        Console.WriteLine($"Course ID: {course.CourseId}, Name: {course.CourseName}, Credits: {course.Credits}, Teacher ID: {course.TeacherId}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying enrolled courses: {ex.Message}");
            }
        }


        public void EnrollInCourse(int studentId, int courseId)
        {
            try
            {
                if (studentId <= 0)
                    throw new InvalidEnrollmentDataException("Invalid Student ID. It must be a positive integer.");

                if (courseId <= 0)
                    throw new InvalidEnrollmentDataException("Invalid Course ID. It must be a positive integer.");

              
                double totalPayments = _paymentRepository.GetTotalPaymentsForStudent(studentId);

            
                double courseFee = 500.00; 

                if (totalPayments < courseFee)
                {
                    throw new InsufficientFundsException($"Student ID {studentId} does not have enough funds to enroll in Course ID {courseId}. " +
                                                          $"Required: {courseFee:C}, Available: {totalPayments:C}");
                }

                bool isEnrolled = _enrollmentRepository.EnrollInCourse(studentId, courseId);

                if (isEnrolled)
                {
                    Console.WriteLine($"Student with ID {studentId} has been successfully enrolled in course ID {courseId}.");
                }
                else
                {
                    Console.WriteLine($"Enrollment failed for Student ID {studentId} in Course ID {courseId}. Please check the details.");
                }
            }
            catch (InvalidEnrollmentDataException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }



        public void GetEnrollments(int courseId)
        {
            try
            {
                List<Student> students = _enrollmentRepository.GetEnrollments(courseId);

                if (students.Count == 0)
                {
                    Console.WriteLine($"No students found for Course ID {courseId}.");
                }
                else
                {
                    Console.WriteLine($"Students enrolled in Course ID {courseId}:");
                    foreach (var student in students)
                    {
                        Console.WriteLine($"Name: {student.First_name} {student.Last_name}, Email: {student.Email}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying enrollments: {ex.Message}");
            }
        }



        public void GetStudentByEnrollmentId(int enrollmentId)
        {
            try
            {
                Student student = _enrollmentRepository.GetStudent(enrollmentId);

                if (student == null)
                {
                    Console.WriteLine($"No student found for Enrollment ID {enrollmentId}.");
                }
                else
                {
                    Console.WriteLine($"Student for Enrollment ID {enrollmentId}:");
                    Console.WriteLine($"ID: {student.Student_Id}, Name: {student.First_name} {student.Last_name}, Email: {student.Email}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching student: {ex.Message}");
            }
        }


        public void GetCourseByEnrollmentId(int enrollmentId)
        {
            try
            {
                Course course = _enrollmentRepository.GetCourse(enrollmentId);

                if (course == null)
                {
                    Console.WriteLine($"No course found for Enrollment ID {enrollmentId}.");
                }
                else
                {
                    Console.WriteLine($"Course for Enrollment ID {enrollmentId}:");
                    Console.WriteLine($"ID: {course.CourseId}, Name: {course.CourseName}, Credits: {course.Credits}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching course: {ex.Message}");
            }
        }


    }
}
