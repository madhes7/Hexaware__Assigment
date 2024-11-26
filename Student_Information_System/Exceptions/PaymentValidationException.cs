using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Exceptions
{
    internal class PaymentValidationException : ApplicationException
    {
        public PaymentValidationException()
        {
            
        }
        public PaymentValidationException(String message) : base(message) 
        {
            
        }
    }
}
