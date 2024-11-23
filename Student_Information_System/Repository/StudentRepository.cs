using Student_Information_System.Model;
using Student_Information_System.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Student_Information_System.Repository
{
    internal class StudentRepository : IStudentRepository

    {
        public void EnrollInCourse(int studentId, int courseId)
        {
            using (var con = DBConnect.GetSqlConnection())
            {
                string query = "Insert into Enrollments (student_id,course_id) values (@student_id,@course_id)";
                var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("student_id", studentId);
                cmd.Parameters.AddWithValue("course_id", courseId);
                con.Open();
                cmd.ExecuteNonQuery();
                
            }
        }
        public void UpdateStudentInfo(int studentId, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            using (var con = DBConnect.GetSqlConnection())
            {
                string query = "UPDATE Students SET First_Name = @FirstName, Last_Name = @LastName, " +
                               "Date_Of_Birth = @DateOfBirth, Email = @Email, Phone_Number = @PhoneNumber " +
                               "WHERE Student_ID = @StudentId";
                var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("StudentId", studentId);
                cmd.Parameters.AddWithValue("FirstName", firstName);
                cmd.Parameters.AddWithValue("LastName", lastName);
                cmd.Parameters.AddWithValue("DateOfBirth", dateOfBirth);
                cmd.Parameters.AddWithValue("Email", email);
                cmd.Parameters.AddWithValue("PhoneNumber", phoneNumber);

                con.Open();
                cmd.ExecuteNonQuery();
                
            }
        }


        public void MakePayment(int studentId, decimal amount, DateTime paymentDate)
        {
            using (var con = DBConnect.GetSqlConnection())
            {
                string query = "Insert into Payments (student_id,amount,payment_date) values (@student_id,@amt,@date)";
                var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("student_id", studentId);
                cmd.Parameters.AddWithValue("amt", amount);
                cmd.Parameters.AddWithValue("date", paymentDate);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Student GetStudentById(int studentId)
        {
            using (var con = DBConnect.GetSqlConnection())
            {
                string query = "Select * from  Students WHERE Student_ID = @StudentId";
                var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("StudentId", studentId);
                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Student
                        {
                            Student_Id = Convert.ToInt32(reader["Student_ID"]),
                            First_name = reader["First_Name"].ToString(),
                            Last_name = reader["Last_Name"].ToString(),
                            Date_Of_Birth = DateOnly.FromDateTime(Convert.ToDateTime(reader["Date_Of_Birth"])),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["Phone_Number"].ToString()
                        };
                    }
                }
               
            }
            return null;
        }
        public List<Course> GetEnrolledCourses(int studentId)
        {
            List<Course> course = new List<Course>();
            using (var con = DBConnect.GetSqlConnection())
            {
                string query = "SELECT C.Course_ID, C.Course_Name, C.Credits FROM Courses C " +
                               "JOIN Enrollments E ON C.Course_ID = E.Course_ID WHERE E.Student_ID = @StudentId";
                var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("StudentId", studentId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader .Read())
                    {
                        course.Add(
                            new Course
                            {
                                CourseId = Convert.ToInt32(reader["Course_ID"]),
                                CourseName = reader["Course_Name"].ToString(),
                                Credits = Convert.ToInt32(reader["Credits"]),
                                
                            }
                            );

                    }
                }

            }
            
            return course;
        }
        public List<Payment> GetPaymentHistory(int studentId)
        {
            List<Payment> payment = new List<Payment>();
            using (var con = DBConnect.GetSqlConnection())
            {
                string query = "SELECT * FROM Payments WHERE Student_ID = @StudentId";
                var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("StudentId", studentId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        payment.Add(
                            new Payment
                            {
                                PaymentId = Convert.ToInt32(reader["Payment_ID"]),
                                StudentId = Convert.ToInt32(reader["Student_ID"]),
                                Amount = Convert.ToDecimal(reader["Amount"]),
                                PaymentDate = Convert.ToDateTime(reader["Payment_Date"])

                            }
                            );

                    }
                }

            }

            return payment;
        }
        public void DisplayStudentInfo(int studentId)
        {
            var student = GetStudentById(studentId);
            if (student != null)
            {
                Console.WriteLine($"Student ID: {student.Student_Id}");
                Console.WriteLine($"Name: {student.First_name} {student.Last_name}");
                Console.WriteLine($"Date of Birth: {student.Date_Of_Birth.ToShortDateString()}");
                Console.WriteLine($"Email: {student.Email}");
                Console.WriteLine($"Phone: {student.PhoneNumber}");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }

        }

    }
}
