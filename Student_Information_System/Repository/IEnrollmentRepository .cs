
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student_Information_System.Model;

namespace Student_Information_System.Repository
{
    internal interface IEnrollmentRepository
    {
        List<Course> GetEnrolledCourses(int studentId);
         bool  EnrollInCourse(int studentId, int courseId);

        List<Student> GetEnrollments(int courseId);

        Student GetStudent(int enrollmentId);

        Course GetCourse(int enrollmentId);
    }
}
