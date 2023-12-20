using System;
using Application;

namespace SIS.BLL.Interface
{
	public interface IDatabaseInteraction
	{
        public void EnrollStudentInCourse(Student student, Course course);
        public void AssignTeacherToCourse(Teacher teacher, Course course);
        public void RecordPayment(Student student, decimal amount, DateTime paymentDate);
        public List<Student> GenerateEnrollmentReport(Course course);
        public List<Payment> GeneratePaymentReport(Student student);
        public (int enrollmentCount, decimal totalPayments) CalculateCourseStatistics(Course course);

    }
}

