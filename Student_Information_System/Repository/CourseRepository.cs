using Student_Information_System.Model;
using Student_Information_System.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Student_Information_System.Repository
{
    internal class CourseRepository : ICourseRepository
    {
        private readonly string _connectionString;
      

        public CourseRepository()
        {
            _connectionString = DBConnect.GetConnectionString();
           
        }
        public bool AssignTeacherToCourse(int course_id, int teacher_id)
        {
            int i = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "Update Courses Set Teacher_Id =@TeacherId Where Course_Id=@CourseId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("TeacherId", teacher_id);
                    cmd.Parameters.AddWithValue("CourseId", course_id);
                    con.Open();
                    i=cmd.ExecuteNonQuery();
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssignTeacher: {ex.Message}");
            }
            return i > 0;
        }

        public bool UpdateCourseInfo(int CourseId, string CourseName, int Credits, int teacherId)
        {
            int i = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "Update Courses Set Course_Name=@CourseName,Credits=@Credits, Teacher_Id =@TeacherId " +
                                   "Where Course_Id=@CourseId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("TeacherId", teacherId);
                    cmd.Parameters.AddWithValue("CourseId", CourseId);
                    cmd.Parameters.AddWithValue("CourseName", CourseName);
                    cmd.Parameters.AddWithValue("Credits", Credits);
                    con.Open();
                    i=cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Update: {ex.Message}");
            }
            return i > 0;
        }

        public Course GetCourseById(int course_id)
        {
            try
            {
                Course course = null;
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "Select * From Courses  Where Course_Id=@CourseId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("CourseId", course_id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            course = new Course(
                                Convert.ToInt32(reader["Course_ID"]),
                                reader["Course_Name"].ToString(),
                                Convert.ToInt32(reader["Credits"]),
                                Convert.ToInt32(reader["Teacher_Id"])
                            );
                        }
                        else
                        {
                            Console.WriteLine("Course not found.");
                        }
                    }
                }
                return course;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetCourseById: {ex.Message}");
                return null;
            }
        }

        public List<Student> GetEnrollments(int course_id)
        {
            try
            {
                List<Student> stu = new List<Student>();
                using (SqlConnection con = new SqlConnection(_connectionString))
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
                return stu;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetEnrollments: {ex.Message}");
                return null;
            }
        }

        public Teacher GetTeacherForCourse(int course_id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "SELECT T.Teacher_ID, T.First_Name, T.Last_Name, T.Email FROM Teacher T " +
                                   "JOIN Courses C ON T.Teacher_ID = C.Teacher_ID WHERE C.Course_ID = @CourseId";
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetTeacher: {ex.Message}");
                return null;
            }
        }

        public (int enrollmentCount, double totalPayments) GetCourseStatistics(int courseId)
        {
            int enrollmentCount = 0;
            double totalPayments = 0;

            
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    
                    string query = @"
                            SELECT COUNT(*) AS EnrollmentCount, 
                                   SUM(p.amount) AS TotalPayments
                            FROM Enrollments e
                            LEFT JOIN Payments p ON e.student_id = p.student_id
                            WHERE e.course_id = @CourseId";
                     SqlCommand _cmd = new SqlCommand(query,conn);
                    
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@CourseId", courseId);
                    _cmd.Connection = conn;

                    conn.Open();

                    using (SqlDataReader reader = _cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            enrollmentCount = reader["EnrollmentCount"] != DBNull.Value ? Convert.ToInt32(reader["EnrollmentCount"]) : 0;
                            totalPayments = reader["TotalPayments"] != DBNull.Value ? Convert.ToDouble(reader["TotalPayments"]) : 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching course statistics: {ex.Message}");
                }
            }

            return (enrollmentCount, totalPayments);
        }
    }
}
