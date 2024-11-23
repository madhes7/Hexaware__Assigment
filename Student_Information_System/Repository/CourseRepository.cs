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
    internal class CourseRepository : ICourseRepository
    {
        public void AssignTeacher(int course_id, int teacher_id)
        {
            using (var con = DBConnect.GetSqlConnection())
            {
                string query = "Update Courses Set Teacher_Id =@TeacherId Where Course_Id=@CourseId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("TeacherId", teacher_id);
                cmd.Parameters.AddWithValue("CourseId", course_id);
                con.Open();
                cmd.ExecuteNonQuery();

            }
        }
        public void Update(int CourseId, string CourseName, int Credits, int teacherId)
        {
            using (var con = DBConnect.GetSqlConnection())
            {
                string query = "Update Courses Set Course_Name=@CourseName,Credits=@Credits, Teacher_Id =@TeacherId " +
                    "Where Course_Id=@CourseId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("TeacherId", teacherId);
                cmd.Parameters.AddWithValue("CourseId", CourseId);
                cmd.Parameters.AddWithValue("CourseName", CourseName);
                cmd.Parameters.AddWithValue("Credits", Credits);
                con.Open();
                cmd.ExecuteNonQuery();

            }
        }
        public void DisplayCourse(int course_id)
        {
            using (var con = DBConnect.GetSqlConnection())
            {
                string query = "Select * From Courses  Where Course_Id=@CourseId";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("CourseId", course_id);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine($"Course ID: {reader["Course_ID"]}");
                        Console.WriteLine($"Course Name: {reader["Course_Name"]}");
                        Console.WriteLine($"Credits: {reader["Credits"]}");
                        Console.WriteLine($"Techer Id: {reader["Teacher_Id"]}");
                    }
                    else
                    {
                        Console.WriteLine("Course not found.");
                    }
                }

            }

        }
        public List<Student> GetEnrollments(int course_id)
        {
            List<Student> stu = new List<Student>();
            using (var con = DBConnect.GetSqlConnection())
            {
                string query = "SELECT S.Student_ID, S.First_Name, S.Last_Name FROM Students S " +
                               "JOIN Enrollments E ON S.Student_ID = E.Student_ID WHERE E.Course_ID = @Course_Id";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("Course_Id", course_id);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stu.Add(new Student
                        {
                            Student_Id = Convert.ToInt32(reader["Student_Id"]),
                            First_name = reader["First_Name"].ToString(),
                            Last_name = reader["Last_Name"].ToString()
                        });
                    }

                }
            }
           return null;
        }

        public Teacher GetTeacher(int course_id)
        {
            using (var con = DBConnect.GetSqlConnection())
            {
                string query = "SELECT T.Teacher_ID, T.First_Name, T.Last_Name, T.Email FROM Teacher T " +
                              "JOIN Courses C ON T.Teacher_ID = C.Teacher_ID WHERE C.Course_ID = @CourseId";
                var command = new SqlCommand(query, con);
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("CourseId", course_id);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Teacher
                        {
                            TeacherId = Convert.ToInt32(reader["Teacher_ID"]),
                            FirstName = reader["First_Name"].ToString(),
                            LastName = reader["Last_Name"].ToString(),
                            Email = reader["Email"].ToString()
                        };
                    }
                }
            }
            return null;
        }
    }
}
