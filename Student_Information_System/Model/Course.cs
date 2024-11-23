using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Model
{
    internal class Course
    {
        public Course() { }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public int TeacherId { get; set; }

        public Course(int courseId, string courseName, int credits, int teacherId)

        {
            CourseId = courseId;
            CourseName = courseName;
            Credits = credits;
            TeacherId = teacherId;
          
        }
    }
}
