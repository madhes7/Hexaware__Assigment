using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Exceptions
{
    internal class TeacherNotFoundException : ApplicationException
    {

        public TeacherNotFoundException()
        {
            
        }
        public TeacherNotFoundException( String message) : base(message) 
        {
            
        }
    }
}
