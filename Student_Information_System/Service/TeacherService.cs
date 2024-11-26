
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
    internal class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherService()
        {
            _teacherRepository = new TeacherRepository();
        }

        public void DisplayTeacherInfo()
        {
         
                List<Teacher> teachers = _teacherRepository.GetAllTeachers();

                if (teachers.Count == 0)
                {
                    Console.WriteLine("No teacher records found.");
                    return;
                }
                foreach (var teacher in teachers)
                {
                    Console.WriteLine($" {teacher.TeacherId} " +
                        $"{teacher.FirstName} {teacher.LastName} " +
                        $" {teacher.Email}");
                    
                }
            
           
        }

        public void GetAssignedCoursesforTeacher(int teacherId)

        {
            try
            {
                if (!_teacherRepository.DoesTeacherExist(teacherId))
                {
                    throw new TeacherNotFoundException($"Teacher with ID {teacherId} does not exist.");
                }
                List<Course> courses = _teacherRepository.GetAssignedCoursesforTeacher(teacherId);

                if (courses.Count == 0)
                {
                    Console.WriteLine("No courses are assigned to this teacher.");
                }
                else
                {
                    Console.WriteLine($"Courses assigned to Teacher ID {teacherId}:");
                    foreach (var course in courses)
                    {
                        Console.WriteLine(course);
                    }
                }
            }
            catch (TeacherNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
        public void UpdateTeacherInfo(int teacherId, string firstName, string lastName, string email)
        {
            try
            {
                if (!_teacherRepository.DoesTeacherExist(teacherId))
                {
                    throw new TeacherNotFoundException($"Teacher with ID {teacherId} does not exist.");
                }

                Teacher teacher = new Teacher(teacherId, firstName, lastName, email);

                bool isUpdated = _teacherRepository.UpdateTeacherInfo(teacher);

                if (isUpdated)
                {
                    Console.WriteLine("Teacher information updated successfully.");
                }
                else
                {
                    throw new InvalidTeacherDataException("Failed to update teacher information. Please check the Teacher ID and try again.");
                }
            }
            catch (TeacherNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidTeacherDataException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
