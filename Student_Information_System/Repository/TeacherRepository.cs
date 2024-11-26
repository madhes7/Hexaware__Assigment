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
    internal class TeacherRepository : ITeacherRepository
    {

        private readonly string _connectionString;
        private readonly SqlCommand _cmd;

        public TeacherRepository()
        {
            _connectionString = DBConnect.GetConnectionString();
            _cmd = new SqlCommand();
        }

        public List<Teacher> GetAllTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"
            SELECT teacher_id, first_name, last_name, email
            FROM Teacher";

                    _cmd.CommandText = query;
                    _cmd.Connection = conn;

                    conn.Open();

                    SqlDataReader reader = _cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        teachers.Add(new Teacher(
                            Convert.ToInt32(reader["teacher_id"]),
                            reader["first_name"].ToString(),
                            reader["last_name"].ToString(),
                            reader["email"].ToString()
                        ));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching teacher information: {ex.Message}");
                }
            }

            return teachers;
        }

        public List<Course> GetAssignedCoursesforTeacher(int teacherId)
        {
            List<Course> courses = new List<Course>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    _cmd.CommandText = @"
                    SELECT course_id, course_name, credits, teacher_id
                    FROM Courses
                    WHERE teacher_id = @TeacherId";
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@TeacherId", teacherId);
                    _cmd.Connection = conn;

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        courses.Add(new Course(
                            Convert.ToInt32(reader["course_id"]),
                            reader["course_name"].ToString(),
                            Convert.ToInt32(reader["credits"]),
                            Convert.ToInt32(reader["teacher_id"])
                        ));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return courses;
        }


        public bool UpdateTeacherInfo(Teacher teacher)
        {
            bool isUpdated = false;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"
                    UPDATE Teacher
                    SET 
                        first_name = ISNULL(@FirstName, first_name),
                        last_name = ISNULL(@LastName, last_name),
                        email = ISNULL(@Email, email)
                    WHERE teacher_id = @TeacherId";

                    _cmd.CommandText = query;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@TeacherId", teacher.TeacherId);
                    _cmd.Parameters.AddWithValue("@FirstName", (object)teacher.FirstName ?? DBNull.Value);
                    _cmd.Parameters.AddWithValue("@LastName", (object)teacher.LastName ?? DBNull.Value);
                    _cmd.Parameters.AddWithValue("@Email", (object)teacher.Email ?? DBNull.Value);

                    _cmd.Connection = conn;
                    conn.Open();

                    int rowsAffected = _cmd.ExecuteNonQuery();
                    isUpdated = rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating teacher information: {ex.Message}");
                }
            }

            return isUpdated;
        }
        public bool DoesTeacherExist(int teacherId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"
                        SELECT COUNT(1)
                        FROM Teacher
                        WHERE teacher_id = @TeacherId";

                    _cmd.CommandText = query;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@TeacherId", teacherId);
                    _cmd.Connection = conn;

                    conn.Open();

                    int count = (int)_cmd.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error checking teacher existence: {ex.Message}");
                    return false;
                }
            }

        }


    }
}







