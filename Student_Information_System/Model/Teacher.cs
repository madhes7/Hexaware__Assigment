using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Model
{ 
   
    internal class Teacher
    {
        public Teacher() { }
        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public Teacher (int TeacherId, string FirstName, string LastName, string Email)
        {
            this.TeacherId = TeacherId;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
               
        }
    }
}
