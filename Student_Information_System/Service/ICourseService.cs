using Student_Information_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Service
{
    internal interface ICourseService
    {
        void AssignTeacher(int courseId, int teacherId);
        void UpdateCourseInfo(int courseId, string courseName, int credits, int teacherId);
        void DisplayCourseInfo(int courseId);
        void GetTeacherForCourse(int courseId);
        void CalculateCourseStatistics(int courseId);
    }
}
