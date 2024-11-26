
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
    internal class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService()
        {
            _courseRepository = new CourseRepository();
        }

        public void AssignTeacher(int courseId, int teacherId)
        {
            if (courseId <= 0 || teacherId <= 0)
            {
                Console.WriteLine("Invalid course or teacher ID.");
                return;
            }

            bool isAssigned = _courseRepository.AssignTeacherToCourse(courseId, teacherId);

            if (isAssigned)
            {
                Console.WriteLine($"Teacher with ID {teacherId} has been assigned to Course with ID {courseId}.");
            }
            else
            {
                Console.WriteLine("Error: Could not assign the teacher to the course.");
            }
        }

        public void UpdateCourseInfo(int courseId, string courseName, int credits, int teacherId)
        {
            try
            {
                if (courseId <= 0)
                    throw new InvalidCourseDataException("Invalid Course ID. It must be a positive integer.");

                if (string.IsNullOrWhiteSpace(courseName) || courseName.Length > 50)
                    throw new InvalidCourseDataException("Invalid Course Name. It cannot be empty or exceed 50 characters.");

                if (credits <= 0 || credits > 10)
                    throw new InvalidCourseDataException("Invalid Credits. It must be a positive number between 1 and 10.");

                bool isUpdated = _courseRepository.UpdateCourseInfo(courseId, courseName, credits, teacherId);

                if (isUpdated)
                {
                    Console.WriteLine($"Course {courseId} information has been successfully updated.");
                }
                else
                {
                    Console.WriteLine("Failed to update course information. Please check the course ID.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex.Message);
            }
        }

        public void DisplayCourseInfo(int courseId)
        {
            Course course = _courseRepository.GetCourseById(courseId);

            if (course == null)
            {
                Console.WriteLine($"No course found with ID: {courseId}");
            }
            else
            {
                Console.WriteLine("Course Information:");
                Console.WriteLine($"{course.CourseId}\t\t{course.CourseName}\t\t{course.Credits}\t\t{course.TeacherId}");  
            }
        }
        public void GetTeacherForCourse(int courseId)
        {
            Teacher teacher = _courseRepository.GetTeacherForCourse(courseId);

            if (teacher == null)
            {
                Console.WriteLine($"No teacher assigned to course with ID: {courseId}");
            }
            else
            {
                Console.WriteLine("Assigned Teacher Information:");
                Console.WriteLine($"{teacher.TeacherId}\t\t{teacher.FirstName}\t\t{teacher.LastName}\t\t{teacher.Email}"); 
            }
        }

        public void CalculateCourseStatistics(int courseId)
        {
            var (enrollmentCount, totalPayments) = _courseRepository.GetCourseStatistics(courseId);

            Console.WriteLine($"Statistics for Course ID: {courseId}:");
            Console.WriteLine($"Number of Enrollments: {enrollmentCount}");
            Console.WriteLine($"Total Payments: {totalPayments:C}"); 
        }



    }
}
