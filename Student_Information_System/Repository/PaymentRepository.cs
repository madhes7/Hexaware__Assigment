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
    internal class PaymentRepository : IPaymentRepository
    {
        private readonly string _connectionString;
        private readonly SqlCommand _cmd;

        public PaymentRepository()
        {
            
            _connectionString  =DBConnect.GetConnectionString();
            _cmd = new SqlCommand();
        }

        public List<Payment> GetPaymentsForStudent(int studentId)
        {
            List<Payment> payments = new List<Payment>();

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

                    SqlDataReader reader = _cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        payments.Add(new Payment(
                            (int)reader["payment_id"],
                            (int)reader["student_id"],
                            Convert.ToDecimal(reader["amount"]),
                            (DateTime)reader["payment_date"]
                        ));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error retrieving payments for student {studentId}: {ex.Message}");
                }
            }

            return payments;
        }



        public double GetPaymentAmount(int paymentId)
        {
            double amount = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"
                        SELECT amount
                        FROM Payments
                        WHERE payment_id = @PaymentId";

                    _cmd.CommandText = query;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@PaymentId", paymentId);

                    _cmd.Connection = conn;
                    conn.Open();

                    object result = _cmd.ExecuteScalar();

                    if (result != null)
                    {
                        amount = Convert.ToDouble(result);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error retrieving payment amount for Payment ID {paymentId}: {ex.Message}");
                }
            }

            return amount;
        }


        public DateTime? GetPaymentDate(int paymentId)
        {
            DateTime? paymentDate = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"
                        SELECT payment_date
                        FROM Payments
                        WHERE payment_id = @PaymentId";

                    _cmd.CommandText = query;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@PaymentId", paymentId);

                    _cmd.Connection = conn;
                    conn.Open();

                    object result = _cmd.ExecuteScalar();

                    if (result != null)
                    {
                        paymentDate = Convert.ToDateTime(result);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error retrieving payment date for Payment ID {paymentId}: {ex.Message}");
                }
            }

            return paymentDate;
        }
        public double GetTotalPaymentsForStudent(int studentId)
        {
            double totalPayments = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = "SELECT SUM(amount) FROM Payments WHERE student_id = @StudentId";
                    _cmd.CommandText = query;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@StudentId", studentId);
                    _cmd.Connection = conn;

                    conn.Open();
                    object result = _cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        totalPayments = Convert.ToDouble(result);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching payment information: {ex.Message}");
                }
            }

            return totalPayments;
        }

    }
}

