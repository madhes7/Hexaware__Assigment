using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Model
{
    internal class Student
    {
        public Student() { }
        
        public  int  Student_Id { get; set; }
        public  string First_name { get; set; }
        public  string  Last_name { get; set; }
        public  DateOnly Date_Of_Birth { get; set; }
        public  string Email { get; set; }
        public string PhoneNumber { get; set; }

       public Student(int Student_Id, string First_name, string Last_name, DateOnly Date_Of_Birth, string Email, string PhoneNumber)
        {
            this.Student_Id = Student_Id;
            this.First_name = First_name;
            this.Last_name = Last_name;
            this.Date_Of_Birth = Date_Of_Birth;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
        }
    }
}
