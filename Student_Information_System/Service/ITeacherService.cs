using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Service
{
    internal interface ITeacherService
    {
        void DisplayTeacherInfo();

        void GetAssignedCoursesforTeacher(int teacherId);

        void UpdateTeacherInfo(int teacherId, string firstName, string lastName, string email);
    }
}
