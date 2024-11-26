
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
    internal class EnrollmentRepository : IEnrollmentRepository
    {

        private readonly string _connectionString;
        private readonly SqlCommand _cmd;

        public EnrollmentRepository()
        {
            _connectionString = DBConnect.GetConnectionString();
            _cmd = new SqlCommand();
        }

        public List<Student_Information_System.Model.Course> GetEnrolledCourses(int studentId)
        {
            List<Course> courses = new List<Course>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    _cmd.CommandText = @"
                SELECT c.course_id, c.course_name, c.credits, c.teacher_id
                FROM Courses c
                INNER JOIN Enrollments e ON c.course_id = e.course_id
                WHERE e.student_id = @StudentId";

                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@StudentId", studentId);

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
                    Console.WriteLine($"Error fetching enrolled courses: {ex.Message}");
                }
            }

            return courses;
        }

        public bool EnrollInCourse(int studentId, int courseId)
        {
            bool isEnrolled = false;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"
                    INSERT INTO Enrollments (student_id, course_id, enrollment_date)
                    VALUES (@StudentId, @CourseId, @EnrollmentDate)";

                    _cmd.CommandText = query;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@StudentId", studentId);
                    _cmd.Parameters.AddWithValue("@CourseId", courseId);
                    _cmd.Parameters.AddWithValue("@EnrollmentDate", DateTime.Now); 

                    _cmd.Connection = conn;
                    conn.Open();

                    int rowsAffected = _cmd.ExecuteNonQuery();
                    isEnrolled = rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error enrolling student in course: {ex.Message}");
                }
            }

            return isEnrolled;
        }

        public List<Student> GetEnrollments(int courseId)
        {
            List<Student> students = new List<Student>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"
                    SELECT s.student_id, s.first_name, s.last_name, s.email
                    FROM Students s
                    INNER JOIN Enrollments e ON s.student_id = e.student_id
                    WHERE e.course_id = @CourseId";

                    _cmd.CommandText = query;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@CourseId", courseId);

                    _cmd.Connection = conn;
                    conn.Open();

                    SqlDataReader reader = _cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        students.Add(new Student(
                            Convert.ToInt32(reader["student_id"]),
                            reader["first_name"].ToString(),
                            reader["last_name"].ToString(),
                            DateOnly.FromDateTime(DateTime.MinValue), 
                            reader["email"].ToString(),
                            "" 
                        ));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching enrollments: {ex.Message}");
                }
            }

            return students;
        }

        public Student GetStudent(int enrollmentId)
        {
            Student student = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"
                SELECT s.student_id, s.first_name, s.last_name, s.email
                FROM Students s
                INNER JOIN Enrollments e ON s.student_id = e.student_id
                WHERE e.enrollment_id = @EnrollmentId";

                    _cmd.CommandText = query;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@EnrollmentId", enrollmentId);

                    _cmd.Connection = conn;
                    conn.Open();

                    SqlDataReader reader = _cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        student = new Student(
                            Convert.ToInt32(reader["student_id"]),
                            reader["first_name"].ToString(),
                            reader["last_name"].ToString(),
                            DateOnly.FromDateTime(DateTime.MinValue), 
                            reader["email"].ToString(),
                            "" 
                        );
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching student: {ex.Message}");
                }
            }

            return student;
        }



        public Course GetCourse(int enrollmentId)
        {
            Course course = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"
                SELECT c.course_id, c.course_name, c.credits, c.teacher_id
                FROM Courses c
                INNER JOIN Enrollments e ON c.course_id = e.course_id
                WHERE e.enrollment_id = @EnrollmentId";

                    _cmd.CommandText = query;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@EnrollmentId", enrollmentId);

                    _cmd.Connection = conn;
                    conn.Open();

                    SqlDataReader reader = _cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        course = new Course(
                            Convert.ToInt32(reader["course_id"]),
                            reader["course_name"].ToString(),
                            Convert.ToInt32(reader["credits"]),
                            Convert.ToInt32(reader["teacher_id"])
                        );
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching course: {ex.Message}");
                }
            }

            return course;
        }



    }
}
