using Student_Information_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Repository
{
    internal interface ICourseRepository
    {
        void AssignTeacher(int course_id,int teacher_id);
        void Update(int CourseId, string CourseName, int Credits, int teacherId);
        void DisplayCourse(int course_id);
        List<Student> GetEnrollments(int course_id);

        Teacher GetTeacher(int course_id);

    }
}
