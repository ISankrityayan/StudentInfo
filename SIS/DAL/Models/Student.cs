using System;
using System.Collections.Generic;


namespace Application
{
	public class Student
	{
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        private List<Course> enrolledCourses = new List<Course>();
        private List<Payment> paymentHistory = new List<Payment>();

        public void EnrollInCourse(Course course)
        {
            enrolledCourses.Add(course);
            course.EnrollStudent(this);
        }

        public void UpdateStudentInfo(string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNumber = phoneNumber;

        }

        public void MakePayment(decimal amount, DateTime paymentDate)
        {
            Payment payment = new Payment
            {
                StudentID = this.StudentID,
                Amount = amount,
                PaymentDate = paymentDate
            };
            paymentHistory.Add(payment);
        }

        public void DisplayStudentInfo()
        {
            Console.WriteLine($"Student ID: {StudentID}, Name: {FirstName} {LastName}, DOB: {DateOfBirth.ToShortDateString()}, Email: {Email}, Phone: {PhoneNumber}");
            Console.WriteLine("Enrolled Courses:");
            foreach (var course in enrolledCourses)
            {
                Console.WriteLine($"  - {course.CourseName}");
            }
        }

        public List<Course> GetEnrolledCourses()
        {
            return enrolledCourses;
        }

        public List<Payment> GetPaymentHistory()
        {
            return paymentHistory;
        }

    }
}

