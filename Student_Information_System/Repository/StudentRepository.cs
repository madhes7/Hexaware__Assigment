using Microsoft.VisualBasic;
using Student_Information_System.Model;
using Student_Information_System.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Student_Information_System.Repository
{
    internal class StudentRepository : IStudentRepository
    {

        private readonly string _connectionString;
        private readonly SqlCommand _cmd;

        public StudentRepository()
        {
            _connectionString = DBConnect.GetConnectionString();
            _cmd = new SqlCommand();
        }

        public bool UpdateStudent(int studentId, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            bool isUpdated = false;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"
                           UPDATE Students
                           SET 
                            first_name = @FirstName,
                            last_name = @LastName,
                            date_of_birth = @DateOfBirth,
                            email = @Email,
                            phone_number = @PhoneNumber
                        WHERE student_id = @StudentId";

                    _cmd.CommandText = query;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@StudentId", studentId);
                    _cmd.Parameters.AddWithValue("@FirstName", firstName);
                    _cmd.Parameters.AddWithValue("@LastName", lastName);
                    _cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                    _cmd.Parameters.AddWithValue("@Email", email);
                    _cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                    _cmd.Connection = conn;
                    conn.Open();

                    int rowsAffected = _cmd.ExecuteNonQuery();
                    isUpdated = rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating student: " + ex.Message);
                }
            }

            return isUpdated;
        }

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    _cmd.CommandText = "SELECT * FROM Students";
                    _cmd.Connection = conn;
                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        students.Add(ExtractStudent(reader));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error fetching students: " + ex.Message);
                }
            }

            return students;
        }

        public bool MakePayment(int studentId, decimal amount, DateTime paymentDate)
        {
            bool isPaymentRecorded = false;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"
                    INSERT INTO Payments (student_id, amount, payment_date)
                    VALUES (@StudentId, @Amount, @PaymentDate)";

                    _cmd.CommandText = query;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@StudentId", studentId);
                    _cmd.Parameters.AddWithValue("@Amount", amount);
                    _cmd.Parameters.AddWithValue("@PaymentDate", paymentDate);
                    _cmd.Connection = conn;

                    conn.Open();
                    int rowsAffected = _cmd.ExecuteNonQuery();
                    isPaymentRecorded = rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error recording payment: {ex.Message}");
                }
            }

            return isPaymentRecorded;
        }

        public List<Payment> GetPaymentHistory(int studentId)
        {
            List<Payment> paymentHistory = new List<Payment>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"
                    SELECT payment_id, student_id, amount, payment_date
                    FROM Payments
                    WHERE student_id = @StudentId";

                    _cmd.CommandText = query;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@StudentId", studentId);
                    _cmd.Connection = conn;

                    conn.Open();
                    using (SqlDataReader reader = _cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            paymentHistory.Add(new Payment(
                                (int)reader["payment_id"],
                                (int)reader["student_id"],
                                Convert.ToDecimal(reader["amount"]),
                                Convert.ToDateTime(reader["payment_date"])
                            ));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error retrieving payment history: {ex.Message}");
                }
            }

            return paymentHistory;
        }
        private Student ExtractStudent(SqlDataReader reader)
        {
            return new Student(
                Convert.ToInt32(reader["student_id"]),
                reader["first_name"].ToString(),
                reader["last_name"].ToString(),
                DateOnly.FromDateTime(Convert.ToDateTime(reader["date_of_birth"])),
                reader["email"].ToString(),
                reader["phone_number"].ToString()
            );
        }
    }
}
