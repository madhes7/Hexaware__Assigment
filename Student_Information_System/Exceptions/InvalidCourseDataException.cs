﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Exceptions
{
    internal class InvalidCourseDataException : ApplicationException
    {
        public InvalidCourseDataException()
        {
            
        }
        public InvalidCourseDataException(string message ) : base( message ) 
        {
            
        }
    }
}
