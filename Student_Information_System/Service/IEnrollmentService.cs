using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Service
{
    internal interface IEnrollmentService
    {
        void GetEnrolledCourses(int studentId);
        void EnrollInCourse(int studentId, int courseId);
        void GetEnrollments(int courseId);
        public void GetStudentByEnrollmentId(int enrollmentId);
        public void GetCourseByEnrollmentId(int enrollmentId);
    }
}
