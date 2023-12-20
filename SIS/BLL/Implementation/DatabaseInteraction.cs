using System;
using System.Data.SqlClient;
using Application;
using SIS.BLL.Interface;
using SIS.Utilities;

namespace SIS.BLL.Implementation
{
    public class DatabaseInteraction : IDatabaseInteraction
    {
        SqlCommand command = null;
        public string connectionString;

        public DatabaseInteraction()
        {
            connectionString = CoonectionStringUtility.GetConnectionString("DefaultConnection");
            command = new SqlCommand();
        }

        public void EnrollStudentInCourse(Student student, Course course)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO Enrollments (StudentID, CourseID, EnrollmentDate) VALUES (@StudentID, @CourseID, @EnrollmentDate)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@StudentID", student.StudentID);
                    command.Parameters.AddWithValue("@CourseID", course.CourseID);
                    command.Parameters.AddWithValue("@EnrollmentDate", DateTime.Now);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AssignTeacherToCourse(Teacher teacher, Course course)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "UPDATE Courses SET InstructorName = @InstructorName WHERE CourseID = @CourseID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@InstructorName", teacher.FirstName + " " + teacher.LastName);
                    command.Parameters.AddWithValue("@CourseID", course.CourseID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RecordPayment(Student student, decimal amount, DateTime paymentDate)
        {
            using (SqlConnection connection = new SqlConnection(/* connection string */))
            {
                string sql = "INSERT INTO Payments (StudentID, Amount, PaymentDate) VALUES (@StudentID, @Amount, @PaymentDate)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@StudentID", student.StudentID);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@PaymentDate", paymentDate);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Student> GenerateEnrollmentReport(Course course)
        {
            List<Student> enrolledStudents = new List<Student>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT s.* FROM Students s INNER JOIN Enrollments e ON s.StudentID = e.StudentID WHERE e.CourseID = @CourseID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CourseID", course.CourseID);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Student student = new Student
                            {
                                StudentID = Convert.ToInt32(reader["StudentID"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"])
                            };
                            enrolledStudents.Add(student);
                        }
                    }
                }
            }

            return enrolledStudents;
        }



        public List<Payment> GeneratePaymentReport(Student student)
        {
            List<Payment> payments = new List<Payment>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Payments WHERE StudentID = @StudentID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@StudentID", student.StudentID);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Payment payment = new Payment
                            {
                                PaymentID = Convert.ToInt32(reader["PaymentID"]),
                                StudentID = Convert.ToInt32(reader["StudentID"]),
                                Amount = Convert.ToDecimal(reader["Amount"]),
                                PaymentDate = Convert.ToDateTime(reader["PaymentDate"])
                            };
                            payments.Add(payment);
                        }
                    }
                }
            }

            return payments;
        }



        public (int enrollmentCount, decimal totalPayments) CalculateCourseStatistics(Course course)
        {
            int enrollmentCount;
            decimal totalPayments;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Calculate Enrollment Count
                string sqlEnrollmentCount = "SELECT COUNT(*) FROM Enrollments WHERE CourseID = @CourseID";
                using (SqlCommand command = new SqlCommand(sqlEnrollmentCount, connection))
                {
                    command.Parameters.AddWithValue("@CourseID", course.CourseID);
                    connection.Open();
                    enrollmentCount = (int)command.ExecuteScalar();
                }

                // Calculate Total Payments
                string sqlTotalPayments = "SELECT SUM(p.Amount) FROM Payments p INNER JOIN Enrollments e ON p.StudentID = e.StudentID WHERE e.CourseID = @CourseID";
                using (SqlCommand command = new SqlCommand(sqlTotalPayments, connection))
                {
                    command.Parameters.AddWithValue("@CourseID", course.CourseID);
                    totalPayments = (decimal)(command.ExecuteScalar() ?? 0);
                }
            }

            return (enrollmentCount, totalPayments);
        }
    }
}
